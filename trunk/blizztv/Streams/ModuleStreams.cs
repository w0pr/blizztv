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
using System.Timers;
using BlizzTV.Assets.i18n;
using BlizzTV.Configuration;
using BlizzTV.Log;
using BlizzTV.Modules;
using BlizzTV.Modules.Settings;
using BlizzTV.Modules.Subscriptions.Providers;
using BlizzTV.Utility.Imaging;
using BlizzTV.Utility.UI;

namespace BlizzTV.Streams
{
    [ModuleAttributes("Streams", "Live stream aggregator plugin.","stream")]
    public class ModuleStreams:Module
    {
        private Dictionary<string,Stream> _streams = new Dictionary<string,Stream>();
        private Timer _updateTimer;
        private bool _disposed = false;

        public static ModuleStreams Instance;

        public ModuleStreams()
        {
            ModuleStreams.Instance = this;
            this.RootListItem = new ListItem("Streams")
                                    {
                                        Icon = new NamedImage("stream", Assets.Images.Icons.Png._16.stream)
                                    };

            this.RootListItem.ContextMenus.Add("refresh", new System.Windows.Forms.ToolStripMenuItem(i18n.Refresh, Assets.Images.Icons.Png._16.update, new EventHandler(RunManualUpdate))); 
            this.RootListItem.ContextMenus.Add("settings", new System.Windows.Forms.ToolStripMenuItem(i18n.Settings, Assets.Images.Icons.Png._16.settings, new EventHandler(MenuSettingsClicked)));
        }

        public override void Run()
        {            
            this.UpdateStreams();
            this.SetupUpdateTimer();
        }

        public override System.Windows.Forms.Form GetPreferencesForm()
        {
            return new SettingsForm();
        }

        public override bool TryDragDrop(string link)
        {
            foreach (KeyValuePair<string, Provider> pair in Providers.Instance.Dictionary)
            {
                if (((StreamProvider) pair.Value).LinkValid(link))
                {
                    StreamSubscription s = new StreamSubscription();
                    s.Slug = (pair.Value as StreamProvider).GetSlug(link);                    
                    s.Provider = pair.Value.Name;
                    s.Name = (pair.Value as StreamProvider).GetSlug(link);
                    
                    if(s.Provider.ToLower()=="own3dtv")
                    {
                        string name = "";
                        if (InputBox.Show("Add New Stream", "Please enter name for the new stream", ref name) == System.Windows.Forms.DialogResult.OK) s.Name = name;
                        else return false;
                    }

                    if(Subscriptions.Instance.Add(s)) this.RunManualUpdate(this, new EventArgs());
                    else System.Windows.Forms.MessageBox.Show(string.Format("The stream already exists in your subscriptions named as '{0}'.", Subscriptions.Instance.Dictionary[s.Slug].Name), "Subscription Exists", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                    return true;
                }
            }
            return false;
        }

        private void UpdateStreams()
        {
            if (this.Updating) return;

            this.Updating = true;
            this.NotifyUpdateStarted();

            if (this._streams.Count > 0)// clear previous entries before doing an update.
            {
                this._streams.Clear();
                this.RootListItem.Childs.Clear();
            }

            this.RootListItem.SetTitle("Updating streams..");

            foreach (KeyValuePair<string, StreamSubscription> pair in Subscriptions.Instance.Dictionary)
            {
                this._streams.Add(string.Format("{0}@{1}",pair.Value.Slug,pair.Value.Provider), StreamFactory.CreateStream(pair.Value));
            }

            int availableCount = 0; // available live streams count
            Workload.WorkloadManager.Instance.Add(this._streams.Count);

            foreach (KeyValuePair<string, Stream> pair in this._streams) // loop through all streams
            {
                try
                {
                    pair.Value.Update(); // update the stream
                    if (pair.Value.IsLive) // if it's live
                    {
                        pair.Value.SetTitle(string.Format("{0} ({1})", pair.Value.Title, pair.Value.ViewerCount)); // put stream viewers count on title.
                        availableCount++; // increment available live streams count.
                        this.RootListItem.Childs.Add(pair.Key, pair.Value);
                    }
                    Workload.WorkloadManager.Instance.Step();
                }
                catch (Exception e) { LogManager.Instance.Write(LogMessageTypes.Error, string.Format("StreamsPlugin ParseStreams() Error: \n {0}", e)); } // catch errors for inner stream-handlers.
            }

            if (availableCount > 0) this.RootListItem.SetTitle(string.Format("Streams ({0})", availableCount));  // put available streams count on root object's title.
            else
            {
                this.RootListItem.SetTitle("Streams");
                this.RootListItem.State = State.Read;
            }

            NotifyUpdateComplete(new PluginUpdateCompleteEventArgs(true));
            this.Updating = false;
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
            if (!RuntimeConfiguration.Instance.InSleepMode) UpdateStreams();
        }

        private void RunManualUpdate(object sender, EventArgs e)
        {
            System.Threading.Thread thread = new System.Threading.Thread(UpdateStreams) {IsBackground = true};
            thread.Start();
        }

        private void MenuSettingsClicked(object sender, EventArgs e)
        {
            ModuleSettingsHostForm f = new ModuleSettingsHostForm(this.Attributes, this.GetPreferencesForm());
            f.ShowDialog();
        }

        #region de-ctor

        protected override void Dispose(bool disposing)
        {
            if (this._disposed) return;
            if (disposing) // managed resources
            {
                this._updateTimer.Enabled = false;
                this._updateTimer.Elapsed -= OnTimerHit;
                this._updateTimer.Dispose();
                this._updateTimer = null;
                foreach (KeyValuePair<string,Stream> pair in this._streams) { pair.Value.Dispose(); }
                this._streams.Clear();
                this._streams = null;
            }
            base.Dispose(disposing);
        }

        #endregion
    }
}
