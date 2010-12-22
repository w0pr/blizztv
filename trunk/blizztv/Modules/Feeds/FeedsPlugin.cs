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
using BlizzTV.CommonLib.Logger;
using BlizzTV.CommonLib.Settings;
using BlizzTV.CommonLib.UI;
using BlizzTV.ModuleLib;
using BlizzTV.ModuleLib.Settings;
using BlizzTV.CommonLib.Workload;

namespace BlizzTV.Modules.Feeds
{
    [ModuleAttributes("Feeds","Feed aggregator plugin.","feed_16")]
    public class FeedsPlugin:Module
    {
        internal Dictionary<string,Feed> _feeds = new Dictionary<string,Feed>(); // the feeds list 
        private Timer _updateTimer;
        private bool _disposed = false;

        public static FeedsPlugin Instance;

        public FeedsPlugin()
        {
            FeedsPlugin.Instance = this;
            this.RootListItem = new ListItem("Feeds");
            this.RootListItem.Icon = new NamedImage("feed_16", Assets.Images.Icons.Png._16.feed);

            // register context menu's.
            this.RootListItem.ContextMenus.Add("manualupdate", new System.Windows.Forms.ToolStripMenuItem("Update Feeds", Assets.Images.Icons.Png._16.update, new EventHandler(RunManualUpdate))); // mark as unread menu.
            this.RootListItem.ContextMenus.Add("markallasread", new System.Windows.Forms.ToolStripMenuItem("Mark All As Read", Assets.Images.Icons.Png._16.read, new EventHandler(MenuMarkAllAsReadClicked))); // mark as read menu.
            this.RootListItem.ContextMenus.Add("markallasunread", new System.Windows.Forms.ToolStripMenuItem("Mark All As Unread", Assets.Images.Icons.Png._16.unread, new EventHandler(MenuMarkAllAsUnReadClicked))); // mark as unread menu.            
            this.RootListItem.ContextMenus.Add("settings", new System.Windows.Forms.ToolStripMenuItem("Settings", Assets.Images.Icons.Png._16.settings, new EventHandler(MenuSettingsClicked)));
        }

        public override void Run()
        {
            this.UpdateFeeds();
            this.SetupUpdateTimer();
        }

        public override System.Windows.Forms.Form GetPreferencesForm()
        {
            return new frmSettings();
        }

        public override bool TryDragDrop(string link)
        {
            if (Subscriptions.Instance.Dictionary.ContainsKey(link))
            {
                System.Windows.Forms.MessageBox.Show(string.Format("The feed already exists in your subscriptions named as '{0}'.", Subscriptions.Instance.Dictionary[link].Name), "Subscription Exists", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                return false;
            }

            FeedSubscription feedSubscription = new FeedSubscription();
            feedSubscription.Name = "test-feed";
            feedSubscription.Url = link;

            using (Feed feed = new Feed(feedSubscription))
            {
                if (feed.IsValid())
                {
                    FeedSubscription subscription = new FeedSubscription();
                    string feedName = "";
                    if (InputBox.Show("Add New Feed", "Please enter name for the new feed", ref feedName) == System.Windows.Forms.DialogResult.OK)
                    {
                        subscription.Name = feedName;
                        subscription.Url = link;
                        if (Subscriptions.Instance.Add(subscription)) this.RunManualUpdate(this, new EventArgs()); else return false;
                        return true;
                    }
                }
            }

            return false;
        }

        internal void UpdateFeeds()
        {
            if (this.Updating) return;

            this.Updating = true;
            this.NotifyUpdateStarted();

            if (this._feeds.Count > 0) // clear previous entries before doing an update.
            {
                this._feeds.Clear();
                this.RootListItem.Childs.Clear();
            }
                
            this.RootListItem.Style = ItemStyle.Regular;
            this.RootListItem.SetTitle("Updating feeds..");

            foreach (KeyValuePair<string, FeedSubscription> pair in Subscriptions.Instance.Dictionary)
            {
                Feed feed = new Feed(pair.Value);
                this._feeds.Add(pair.Value.Url, feed);
                feed.OnStyleChange += ChildStyleChange;
            }

            Workload.Instance.Add(this, this._feeds.Count);

            foreach (KeyValuePair<string, Feed> pair in this._feeds) // loop through feeds.
            {
                try
                {
                    pair.Value.Update(); // update the feed.
                    this.RootListItem.Childs.Add(pair.Key, pair.Value);
                    foreach (Story story in pair.Value.Stories) { pair.Value.Childs.Add(story.Guid, story); } // register the story items.
                }
                catch (Exception e) { Log.Instance.Write(LogMessageTypes.Error, string.Format("Feed Plugin - UpdateFeeds Exception: {0}", e)); }
                Workload.Instance.Step(this);
            }

            this.RootListItem.SetTitle("Feeds");
            this.NotifyUpdateComplete(new PluginUpdateCompleteEventArgs(true));
            this.Updating = false;
        }

        void ChildStyleChange(ItemStyle style)
        {
            if (this.RootListItem.Style == style) return;

            int unread = this._feeds.Count(pair => pair.Value.Style == ItemStyle.Bold);
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

            _updateTimer = new Timer(Settings.Instance.UpdateEveryXMinutes * 60000);
            _updateTimer.Elapsed += OnTimerHit;
            _updateTimer.Enabled = true;
        }

        private void OnTimerHit(object source, ElapsedEventArgs e)
        {
            if (!GlobalSettings.Instance.InSleepMode) this.UpdateFeeds();
        }

        private void MenuMarkAllAsReadClicked(object sender, EventArgs e)
        {
            foreach (Story s in this._feeds.SelectMany(pair => pair.Value.Stories))
            {
                s.Status = Story.Statutes.Read;
            }
        }

        private void MenuMarkAllAsUnReadClicked(object sender, EventArgs e)
        {
            foreach (Story s in this._feeds.SelectMany(pair => pair.Value.Stories))
            {
                s.Status = Story.Statutes.Unread;
            }
        }

        private void MenuSettingsClicked(object sender, EventArgs e)
        {
            frmModuleSettingsHost f = new frmModuleSettingsHost(this.Attributes, this.GetPreferencesForm());
            f.ShowDialog();
        }

        private void RunManualUpdate(object sender, EventArgs e)
        {
            System.Threading.Thread thread = new System.Threading.Thread(this.UpdateFeeds) {IsBackground = true};
            thread.Start();                 
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
                foreach (KeyValuePair<string,Feed> pair in this._feeds) { pair.Value.Dispose(); }
                this._feeds.Clear();
                this._feeds = null;
            }
            base.Dispose(disposing);
        }

        #endregion
    }
}
