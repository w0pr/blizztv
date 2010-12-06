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
using System.Timers;
using BlizzTV.CommonLib.Logger;
using BlizzTV.CommonLib.Settings;
using BlizzTV.ModuleLib;
using BlizzTV.ModuleLib.Subscriptions;
using BlizzTV.ModuleLib.Subscriptions.Providers;
using BlizzTV.CommonLib.Workload;

namespace BlizzTV.Modules.Videos
{
    [ModuleAttributes("Videos", "Video aggregator plugin.","video_16")]
    public class VideosPlugin:Module
    {
        #region members

        internal Dictionary<string,Channel> _channels = new Dictionary<string,Channel>(); // the channels list.
        private Timer _update_timer;
        private bool disposed = false;

        public static VideosPlugin Instance;

        #endregion

        #region ctor

        public VideosPlugin() : base()
        {
            VideosPlugin.Instance = this;
            this.RootListItem = new ListItem("Videos");

            // register context menu's.
            this.RootListItem.ContextMenus.Add("manualupdate", new System.Windows.Forms.ToolStripMenuItem("Update Channels", null, new EventHandler(RunManualUpdate))); // mark as unread menu.
            this.RootListItem.ContextMenus.Add("markallaswatched", new System.Windows.Forms.ToolStripMenuItem("Mark All As Watched", null, new EventHandler(MenuMarkAllAsWatchedClicked))); // mark as read menu.
            this.RootListItem.ContextMenus.Add("markallasunwatched", new System.Windows.Forms.ToolStripMenuItem("Mark All As Unwatched", null, new EventHandler(MenuMarkAllAsUnWatchedClicked))); // mark as unread menu.
        }

        #endregion

        #region API handlers

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
                if ((pair.Value as VideoProvider).LinkValid(link))
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

        #endregion

        #region internal logic

        internal void UpdateChannels()
        {
            if (!this.Updating)
            {
                this.Updating = true;
                this.NotifyUpdateStarted();

                if (this._channels.Count > 0)  // clear previous entries before doing an update.
                {
                    this._channels.Clear();
                    this.RootListItem.Childs.Clear();
                }

                this.RootListItem.Style = ItemStyle.REGULAR;
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
                    foreach (Video v in pair.Value.Videos) { pair.Value.Childs.Add(v.GUID, v); } // register the video items.
                    Workload.Instance.Step(this);                    
                }

                this.RootListItem.SetTitle("Videos");
                NotifyUpdateComplete(new PluginUpdateCompleteEventArgs(true));
                this.Updating = false;
            }
        }

        void OnChildStyleChange(ItemStyle Style)
        {
            if (this.RootListItem.Style == Style) return;

            int unread = 0;
            foreach (KeyValuePair<string, Channel> pair in this._channels) { if (pair.Value.Style == ItemStyle.BOLD) unread++; }
            if (unread > 0) this.RootListItem.Style = ItemStyle.BOLD;
            else this.RootListItem.Style = ItemStyle.REGULAR;
        }
        
        public void OnSaveSettings()
        {
            this.SetupUpdateTimer();
        }

        private void SetupUpdateTimer()
        {
            if (this._update_timer != null)
            {
                this._update_timer.Enabled = false;
                this._update_timer.Elapsed -= OnTimerHit;                
                this._update_timer = null;
            }

            _update_timer = new Timer(Settings.Instance.UpdateEveryXMinutes * 60000);
            _update_timer.Elapsed += new ElapsedEventHandler(OnTimerHit);
            _update_timer.Enabled = true;
        }

        private void OnTimerHit(object source, ElapsedEventArgs e)
        {
            if (!GlobalSettings.Instance.InSleepMode) UpdateChannels();
        }

        private void RunManualUpdate(object sender, EventArgs e)
        {
            System.Threading.Thread t = new System.Threading.Thread(delegate() { UpdateChannels(); }) 
            { IsBackground = true, Name = string.Format("plugin-{0}-{1}", this.Attributes.Name, DateTime.Now.TimeOfDay.ToString()) };
            t.Start();
        }

        private void MenuMarkAllAsWatchedClicked(object sender, EventArgs e)
        {
            foreach (KeyValuePair<string, Channel> pair in this._channels)
            {
                foreach (Video v in pair.Value.Videos) { v.Status = Video.Statutes.WATCHED; }
            }
        }

        private void MenuMarkAllAsUnWatchedClicked(object sender, EventArgs e)
        {
            foreach (KeyValuePair<string, Channel> pair in this._channels)
            {
                foreach (Video v in pair.Value.Videos) { v.Status = Video.Statutes.UNWATCHED; }
            }
        }

        #endregion

        #region de-ctor

        protected override void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing) // managed resources
                {
                    if (this._update_timer != null)
                    {
                        this._update_timer.Enabled = false;
                        this._update_timer.Elapsed -= OnTimerHit;
                        this._update_timer.Dispose();
                        this._update_timer = null;
                    }
                    foreach (KeyValuePair<string,Channel> pair in this._channels) { pair.Value.Dispose(); }
                    this._channels.Clear();
                    this._channels = null;
                }
                base.Dispose(disposing);
            }
        }

        #endregion
    }
}
