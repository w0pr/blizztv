/*    
 * Copyright (C) 2010, BlizzTV Project - http://code.google.com/p/blizztv/
 *  
 * This program is free software: you can redistribute it and/or modify it under the terms of the GNU General 
 * Public License as published by the Free Software Foundation, either version 3 of the License, or (at your 
 * option) any later version.
 * 
 * This program is distributed in the hope that it will be useful, but WITHOUT ANY WARRANTY; without even the 
 * implied warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the GNU General Public License 
 * for more details.
 * 
 * You should have received a copy of the GNU General Public License along with this program.  If not, see 
 * <http://www.gnu.org/licenses/>. 
 * 
 * $Id$
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Timers;
using BlizzTV.CommonLib.Utils;
using BlizzTV.CommonLib.Settings;
using BlizzTV.CommonLib.Workload;
using BlizzTV.CommonLib.Config;
using BlizzTV.ModuleLib;
using BlizzTV.ModuleLib.Settings;
using BlizzTV.Modules.BlizzBlues.Game;

namespace BlizzTV.Modules.BlizzBlues
{
    [ModuleAttributes("BlizzBlues", "Blizzard GM Blue post aggregator.", "blizzblues")]
    class BlizzBluesModule:Module
    {
        internal List<BlueParser> _parsers = new List<BlueParser>();
        private Timer _updateTimer;

        public static BlizzBluesModule Instance;

        public BlizzBluesModule()
        {
            BlizzBluesModule.Instance = this;
            this.RootListItem=new ListItem("BlizzBlues");
            this.RootListItem.Icon = new NamedImage("blizzblues", Assets.Images.Icons.Png._16.blizzblues);

            this.RootListItem.ContextMenus.Add("manualupdate", new System.Windows.Forms.ToolStripMenuItem("Update Blues", Assets.Images.Icons.Png._16.update, new EventHandler(RunManualUpdate))); // mark as unread menu.
            this.RootListItem.ContextMenus.Add("markallasread", new System.Windows.Forms.ToolStripMenuItem("Mark All As Read", Assets.Images.Icons.Png._16.read, new EventHandler(MenuMarkAllAsReadClicked))); // mark as read menu.
            this.RootListItem.ContextMenus.Add("markallasunread", new System.Windows.Forms.ToolStripMenuItem("Mark All As Unread", Assets.Images.Icons.Png._16.unread, new EventHandler(MenuMarkAllAsUnReadClicked))); // mark as unread menu.            
            this.RootListItem.ContextMenus.Add("settings", new System.Windows.Forms.ToolStripMenuItem("Settings", Assets.Images.Icons.Png._16.settings, new EventHandler(MenuSettingsClicked)));
        }

        public override System.Windows.Forms.Form GetPreferencesForm()
        {
            return new frmSettings();            
        }

        public override void Run()
        {
            this.UpdateBlues();
            this.SetupUpdateTimer();
        }

        private void UpdateBlues()
        {
            if (this.Updating) return;


            this.Updating = true;
            this.NotifyUpdateStarted();

            if (this._parsers.Count > 0)
            {
                this._parsers.Clear();
                this.RootListItem.Childs.Clear();
            }

            this.RootListItem.SetTitle("Updating BlizzBlues..");

            if (Settings.Instance.TrackWorldofWarcraft)
            {
                WOWBlues wow = new WOWBlues();
                this._parsers.Add(wow);
                wow.OnStateChange += OnChildStateChange;
            }

            if (Settings.Instance.TrackStarcraft)
            {
                SCBlues sc = new SCBlues();
                this._parsers.Add(sc);
                sc.OnStateChange += OnChildStateChange;
            }

            if (this._parsers.Count > 0)
            {
                Workload.Instance.Add(this, this._parsers.Count);

                foreach (BlueParser parser in this._parsers)
                {
                    parser.Update();
                    this.RootListItem.Childs.Add(parser.Title, parser);
                    foreach (KeyValuePair<string, BlueStory> storyPair in parser.Stories)
                    {
                        parser.Childs.Add(storyPair.Key, storyPair.Value);
                        if (storyPair.Value.More.Count > 0)
                        {
                            foreach (KeyValuePair<string, BlueStory> postPair in storyPair.Value.More)
                            {
                                storyPair.Value.Childs.Add(string.Format("{0}-{1}", postPair.Value.TopicId, postPair.Value.PostId), postPair.Value);
                            }
                        }
                    }
                    Workload.Instance.Step(this);
                }
            }

            this.RootListItem.SetTitle("BlizzBlues");
            this.NotifyUpdateComplete(new PluginUpdateCompleteEventArgs(true));
            this.Updating = false;
        }

        private void OnChildStateChange(object sender, EventArgs e)
        {
            if (this.RootListItem.State == (sender as BlueParser).State) return;
            int unread = this._parsers.Count(parser => parser.State == State.Unread);
            this.RootListItem.State = unread > 0 ? State.Unread : State.Read;
        }

        private void MenuMarkAllAsReadClicked(object sender, EventArgs e)
        {
            foreach (BlueParser parser in this._parsers)
            {
                foreach (KeyValuePair<string, BlueStory> pair in parser.Stories)
                {
                    pair.Value.State = State.Read;
                    foreach (KeyValuePair<string, BlueStory> post in pair.Value.More) { post.Value.State = State.Read; }
                }
            }
        }

        private void MenuMarkAllAsUnReadClicked(object sender, EventArgs e)
        {
            foreach (BlueParser parser in this._parsers)
            {
                foreach (KeyValuePair<string, BlueStory> pair in parser.Stories)
                {
                    pair.Value.State = State.Unread;
                    foreach (KeyValuePair<string, BlueStory> post in pair.Value.More) { post.Value.State = State.Unread; }
                }
            }
        }

        private void RunManualUpdate(object sender, EventArgs e)
        {
            System.Threading.Thread thread = new System.Threading.Thread(this.UpdateBlues) { IsBackground = true };
            thread.Start();
        }

        private void MenuSettingsClicked(object sender, EventArgs e)
        {
            frmModuleSettingsHost f = new frmModuleSettingsHost(this.Attributes, this.GetPreferencesForm());
            f.ShowDialog();
        }

        public void OnSaveSettings()
        {
            this.SetupUpdateTimer();
        }

        private void SetupUpdateTimer()
        {
            if (this._updateTimer != null)
            {
                this._updateTimer.Enabled = false;
                this._updateTimer.Elapsed -= OnTimerHit;
                this._updateTimer = null;
            }

            _updateTimer = new Timer(Settings.Instance.UpdatePeriod * 60000);
            _updateTimer.Elapsed += OnTimerHit;
            _updateTimer.Enabled = true;
        }

        private void OnTimerHit(object source, ElapsedEventArgs e)
        {
            if (!RuntimeConfiguration.Instance.InSleepMode) this.UpdateBlues();
        }
    }
}
