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
using System.Timers;
using BlizzTV.CommonLib.Utils;
using BlizzTV.CommonLib.Settings;
using BlizzTV.ModuleLib;
using BlizzTV.ModuleLib.Settings;
using BlizzTV.ModuleLib.Subscriptions.Providers;
using BlizzTV.CommonLib.Workload;

namespace BlizzTV.Modules.Videos
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
            this.RootListItem.Icon = new NamedImage("video_16", Assets.Images.Icons.Png._16.video);

            // register context menu's.
            this.RootListItem.ContextMenus.Add("manualupdate", new System.Windows.Forms.ToolStripMenuItem("Update Channels", Assets.Images.Icons.Png._16.update, new EventHandler(RunManualUpdate))); // mark as unread menu.
            this.RootListItem.ContextMenus.Add("markallaswatched", new System.Windows.Forms.ToolStripMenuItem("Mark All As Watched", Assets.Images.Icons.Png._16.read, new EventHandler(MenuMarkAllAsWatchedClicked))); // mark as read menu.
            this.RootListItem.ContextMenus.Add("markallasunwatched", new System.Windows.Forms.ToolStripMenuItem("Mark All As Unwatched", Assets.Images.Icons.Png._16.unread, new EventHandler(MenuMarkAllAsUnWatchedClicked))); // mark as unread menu.
            this.RootListItem.ContextMenus.Add("settings", new System.Windows.Forms.ToolStripMenuItem("Settings", Assets.Images.Icons.Png._16.settings, new EventHandler(MenuSettingsClicked)));
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
            foreach (KeyValuePair<string, IProvider> pair in Providers.Instance.Dictionary)
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

            this.RootListItem.Style = ItemStyle.Regular;
            this.RootListItem.SetTitle("Updating videos..");

            foreach (KeyValuePair<string, VideoSubscription> pair in Subscriptions.Instance.Dictionary)
            {
                Channel c = ChannelFactory.CreateChannel(pair.Value);
                c.OnStyleChange += OnChildStyleChange;
                this._channels.Add(pair.Value.Slug, c);
            }

            Workload.Instance.Add(this,this._channels.Count);

            foreach (KeyValuePair<string, Channel> pair in this._channels) // loop through videos.
            {
                pair.Value.Update(); // update the channel.
                this.RootListItem.Childs.Add(pair.Key, pair.Value);
                foreach (Video v in pair.Value.Videos) { pair.Value.Childs.Add(v.Guid, v); } // register the video items.
                Workload.Instance.Step(this);                    
            }

            this.RootListItem.SetTitle("Videos");
            NotifyUpdateComplete(new PluginUpdateCompleteEventArgs(true));
            this.Updating = false;
        }

        void OnChildStyleChange(ItemStyle style)
        {
            if (this.RootListItem.Style == style) return;

            int unread = this._channels.Count(pair => pair.Value.Style == ItemStyle.Bold);
            this.RootListItem.Style = unread > 0 ? ItemStyle.Bold : ItemStyle.Regular;
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
            if (!GlobalSettings.Instance.InSleepMode) UpdateChannels();
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
                foreach (Video v in pair.Value.Videos) { v.Status = Video.Statutes.Watched; }
            }
        }

        private void MenuMarkAllAsUnWatchedClicked(object sender, EventArgs e)
        {
            foreach (KeyValuePair<string, Channel> pair in this._channels)
            {
                foreach (Video v in pair.Value.Videos) { v.Status = Video.Statutes.Unwatched; }
            }
        }

        private void MenuSettingsClicked(object sender, EventArgs e)
        {
            frmModuleSettingsHost f = new frmModuleSettingsHost(this.Attributes, this.GetPreferencesForm());
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
