/*    
 * Copyright (C) 2010-2011, BlizzTV Project - http://code.google.com/p/blizztv/
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
using System.Diagnostics;
using System.Linq;
using System.Timers;
using System.Windows.Forms;
using BlizzTV.BlizzBlues.Parsers;
using BlizzTV.Configuration;
using BlizzTV.Log;
using BlizzTV.Modules;
using BlizzTV.Modules.Settings;
using BlizzTV.Utility.Imaging;
using BlizzTV.Assets.i18n;

namespace BlizzTV.BlizzBlues
{
    [ModuleAttributes("BlizzBlues", "Blizzard GM-Blue's aggregator.", "blizzblues")]
    class ModuleBlizzBlues : Module
    {
        private readonly List<BlueParser> _parsers = new List<BlueParser>(); // list of blue-post parsers
        private System.Timers.Timer _updateTimer;

        public static ModuleBlizzBlues Instance; // the module instance.

        public ModuleBlizzBlues()
        {
            Instance = this;

            this.RootListItem=new ListItem("BlizzBlues")
                                  {
                                      Icon = new NamedImage("blizzblues", Assets.Images.Icons.Png._16.blizzblues)
                                  };

            this.RootListItem.ContextMenus.Add("refresh", new ToolStripMenuItem(i18n.Refresh, Assets.Images.Icons.Png._16.update, new EventHandler(MenuUpdate))); 
            this.RootListItem.ContextMenus.Add("markallasread", new ToolStripMenuItem(i18n.MarkAsRead, Assets.Images.Icons.Png._16.read, new EventHandler(MenuMarkAllAsReadClicked))); 
            this.RootListItem.ContextMenus.Add("markallasunread", new ToolStripMenuItem(i18n.MarkAsUnread, Assets.Images.Icons.Png._16.unread, new EventHandler(MenuMarkAllAsUnReadClicked))); 
            this.RootListItem.ContextMenus.Add("settings", new ToolStripMenuItem(i18n.Settings, Assets.Images.Icons.Png._16.settings, new EventHandler(MenuSettingsClicked))); 
        }

        public override void Refresh()
        {
            this.UpdateBlues();
            if (this._updateTimer == null) this.SetupUpdateTimer();
        }

        private void UpdateBlues()
        {
            if (this.RefreshingData) return; // if module is already updating, ignore this request.

            this.RefreshingData = true;
            this.OnDataRefreshStarting(EventArgs.Empty);

            if (this._parsers.Count > 0) // reset the current data.
            {
                this._parsers.Clear();
                this.RootListItem.Childs.Clear();
            }

            this.RootListItem.SetTitle("Updating BlizzBlues..");

            if (Settings.Instance.TrackWorldofWarcraft)
            {
                WorldofWarcraft wow = new WorldofWarcraft();
                this._parsers.Add(wow);
                wow.OnStateChange += OnChildStateChange;
            }

            if (Settings.Instance.TrackStarcraft)
            {
                Starcraft sc = new Starcraft();
                this._parsers.Add(sc);
                sc.OnStateChange += OnChildStateChange;
            }

            if (this._parsers.Count > 0)
            {
                Workload.WorkloadManager.Instance.Add(this._parsers.Count);

                var stopwatch = new Stopwatch();
                stopwatch.Start();

                foreach (BlueParser parser in this._parsers)
                {
                    parser.Update();
                    this.RootListItem.Childs.Add(parser.Title, parser);
                    foreach (KeyValuePair<string, BlueStory> storyPair in parser.Stories)
                    {
                        parser.Childs.Add(storyPair.Key, storyPair.Value);
                        if (storyPair.Value.Successors.Count > 0) // if story have posts more than one..
                        {
                            foreach (KeyValuePair<string, BlueStory> postPair in storyPair.Value.Successors)
                            {
                                storyPair.Value.Childs.Add(string.Format("{0}-{1}", postPair.Value.TopicId, postPair.Value.PostId), postPair.Value);
                            }
                        }
                    }
                    Workload.WorkloadManager.Instance.Step();
                }

                stopwatch.Stop();
                TimeSpan ts = stopwatch.Elapsed;
                LogManager.Instance.Write(LogMessageTypes.Trace, string.Format("Updated {0} blizzblue-parsers in {1}.", this._parsers.Count, String.Format("{0:00}:{1:00}:{2:00}.{3:00}", ts.Hours, ts.Minutes, ts.Seconds, ts.Milliseconds / 10)));
            }

            this.RootListItem.SetTitle("BlizzBlues");
            this.OnDataRefreshCompleted(new DataRefreshCompletedEventArgs(true));
            this.RefreshingData = false;
        }

        public override Form GetPreferencesForm()
        {
            return new SettingsForm();
        }

        private void OnChildStateChange(object sender, EventArgs e)
        {
            if (this.RootListItem.State == ((BlueParser) sender).State) return;

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
                    foreach (KeyValuePair<string, BlueStory> post in pair.Value.Successors) { post.Value.State = State.Read; }
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
                    foreach (KeyValuePair<string, BlueStory> post in pair.Value.Successors) { post.Value.State = State.Unread; }
                }
            }
        }

        private void MenuUpdate(object sender, EventArgs e)
        {
            System.Threading.Thread thread = new System.Threading.Thread(this.UpdateBlues) { IsBackground = true };
            thread.Start();
        }

        private void MenuSettingsClicked(object sender, EventArgs e)
        {
            ModuleSettingsHostForm f = new ModuleSettingsHostForm(this.Attributes, this.GetPreferencesForm());
            f.ShowDialog();
        }

        public void OnSaveSettings()
        {
            this.SetupUpdateTimer();
        }

        private void SetupUpdateTimer()
        {
            if (this._updateTimer != null) // clean the old update timer if exists.
            {
                this._updateTimer.Enabled = false;
                this._updateTimer.Elapsed -= OnTimerHit;
                this._updateTimer = null;
            }

            _updateTimer = new System.Timers.Timer(Settings.Instance.UpdatePeriod * 60000);
            _updateTimer.Elapsed += OnTimerHit;
            _updateTimer.Enabled = true;
        }

        private void OnTimerHit(object source, ElapsedEventArgs e)
        {
            if (!RuntimeConfiguration.Instance.InSleepMode) this.UpdateBlues();
        }
    }
}
