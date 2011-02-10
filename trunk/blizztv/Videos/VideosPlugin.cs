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
using System.Linq;
using System.Timers;
using BlizzTV.Assets.i18n;
using BlizzTV.Configuration;
using BlizzTV.Modules;
using BlizzTV.Modules.Settings;
using BlizzTV.Modules.Subscriptions.Providers;
using BlizzTV.Utility.Imaging;

namespace BlizzTV.Videos
{
    [ModuleAttributes("Videos", "Video aggregator plugin.","video")]
    public class VideosPlugin:Module
    {
        internal Dictionary<string,Channel> _channels = new Dictionary<string,Channel>(); // the channels list.
        private Timer _updateTimer;
        private bool _disposed = false;

        public static VideosPlugin Instance;

        public VideosPlugin() : base()
        {
            VideosPlugin.Instance = this;
            this.RootListItem = new ListItem("Videos");
            this.RootListItem.Icon = new NamedImage("video", Assets.Images.Icons.Png._16.video);

            this.RootListItem.ContextMenus.Add("refresh", new System.Windows.Forms.ToolStripMenuItem(i18n.Refresh, Assets.Images.Icons.Png._16.update, new EventHandler(RunManualUpdate))); 
            this.RootListItem.ContextMenus.Add("markallaswatched", new System.Windows.Forms.ToolStripMenuItem(i18n.MarkAllAsWatched, Assets.Images.Icons.Png._16.read, new EventHandler(MenuMarkAllAsWatchedClicked)));
            this.RootListItem.ContextMenus.Add("markallasunwatched", new System.Windows.Forms.ToolStripMenuItem(i18n.MarkAllAsUnwatched, Assets.Images.Icons.Png._16.unread, new EventHandler(MenuMarkAllAsUnWatchedClicked)));
            this.RootListItem.ContextMenus.Add("settings", new System.Windows.Forms.ToolStripMenuItem(i18n.Settings, Assets.Images.Icons.Png._16.settings, new EventHandler(MenuSettingsClicked)));
        }

        public override void Run()
        {
            this.UpdateChannels();
            this.SetupUpdateTimer();
        }

        public override System.Windows.Forms.Form GetPreferencesForm()
        {
            return new frmSettings();
        }

        public override bool TryDragDrop(string link)
        {
            foreach (KeyValuePair<string, Provider> pair in Providers.Instance.Dictionary)
            {
                if (((VideoProvider) pair.Value).LinkValid(link))
                {
                    VideoSubscription v = new VideoSubscription();
                    v.Name = v.Slug = (pair.Value as VideoProvider).GetSlug(link);
                    v.Provider = pair.Value.Name;

                    using (Channel channel = ChannelFactory.CreateChannel(v))
                    {
                        if (channel.IsValid())
                        {
                            if (Subscriptions.Instance.Add(v)) this.RunManualUpdate(this, new EventArgs());
                            else System.Windows.Forms.MessageBox.Show(string.Format("The channel already exists in your subscriptions named as '{0}'.", Subscriptions.Instance.Dictionary[v.Slug].Name), "Subscription Exists", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                            return true;
                        }
                    }
                }
            }

            return false;
        }

        internal void UpdateChannels()
        {
            if (this.Updating) return;
            this.Updating = true;
            this.NotifyUpdateStarted();

            if (this._channels.Count > 0)  // clear previous entries before doing an update.
            {
                this._channels.Clear();
                this.RootListItem.Childs.Clear();
            }

            this.RootListItem.SetTitle("Updating videos..");

            foreach (KeyValuePair<string, VideoSubscription> pair in Subscriptions.Instance.Dictionary)
            {
                Channel c = ChannelFactory.CreateChannel(pair.Value);
                c.OnStateChange += OnChildStateChange;
                this._channels.Add(string.Format("{0}@{1}",pair.Value.Slug,pair.Value.Provider), c);
            }

            Workload.WorkloadManager.Instance.Add(this._channels.Count);

            foreach (KeyValuePair<string, Channel> pair in this._channels) // loop through videos.
            {
                pair.Value.Update(); // update the channel.
                this.RootListItem.Childs.Add(pair.Key, pair.Value);
                foreach (Video v in pair.Value.Videos) { pair.Value.Childs.Add(v.Guid, v); } // register the video items.
                Workload.WorkloadManager.Instance.Step();                    
            }

            this.RootListItem.SetTitle("Videos");
            NotifyUpdateComplete(new PluginUpdateCompleteEventArgs(true));
            this.Updating = false;
        }
        
        private void OnChildStateChange(object sender, EventArgs e)
        {
            if (this.RootListItem.State == (sender as Channel).State) return;
            int unread = this._channels.Count(pair => pair.Value.State == State.Unread);
            this.RootListItem.State = unread > 0 ? State.Unread : State.Read;
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

            _updateTimer = new Timer(BlizzTV.Videos.Settings.Instance.UpdatePeriod * 60000);
            _updateTimer.Elapsed += OnTimerHit;
            _updateTimer.Enabled = true;
        }

        private void OnTimerHit(object source, ElapsedEventArgs e)
        {
            if (!RuntimeConfiguration.Instance.InSleepMode) UpdateChannels();
        }

        private void RunManualUpdate(object sender, EventArgs e)
        {
            System.Threading.Thread thread = new System.Threading.Thread(UpdateChannels) {IsBackground = true};
            thread.Start();
        }

        private void MenuMarkAllAsWatchedClicked(object sender, EventArgs e)
        {
            foreach (KeyValuePair<string, Channel> pair in this._channels)
            {
                foreach (Video v in pair.Value.Videos) { v.State = State.Read; }
            }
        }

        private void MenuMarkAllAsUnWatchedClicked(object sender, EventArgs e)
        {
            foreach (KeyValuePair<string, Channel> pair in this._channels)
            {
                foreach (Video v in pair.Value.Videos) { v.State = State.Unread; }
            }
        }

        private void MenuSettingsClicked(object sender, EventArgs e)
        {
            ModuleSettingsHost f = new ModuleSettingsHost(this.Attributes, this.GetPreferencesForm());
            f.ShowDialog();
        }

        #region de-ctor

        protected override void Dispose(bool disposing)
        {
            if (this._disposed) return;
            if (disposing) // managed resources
            {
                if (this._updateTimer != null)
                {
                    this._updateTimer.Enabled = false;
                    this._updateTimer.Elapsed -= OnTimerHit;
                    this._updateTimer.Dispose();
                    this._updateTimer = null;
                }
                foreach (KeyValuePair<string,Channel> pair in this._channels) { pair.Value.Dispose(); }
                this._channels.Clear();
                this._channels = null;
            }
            base.Dispose(disposing);
        }

        #endregion
    }
}
