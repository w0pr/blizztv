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
using System.Windows.Forms;
using BlizzTV.Configuration;
using BlizzTV.Log;
using BlizzTV.Modules;
using BlizzTV.Modules.Settings;
using BlizzTV.Utility.Imaging;
using BlizzTV.Utility.UI;
using BlizzTV.Assets.i18n;

namespace BlizzTV.Feeds
{
    [ModuleAttributes("Feeds","Feed aggregator plugin.","feed")]
    public class ModuleFeeds:Module
    {
        private Dictionary<string,Feed> _feeds = new Dictionary<string,Feed>(); // list of feeds.
        private System.Timers.Timer _updateTimer;
        private bool _disposed = false;

        public static ModuleFeeds Instance;

        public ModuleFeeds()
        {
            ModuleFeeds.Instance = this;
            this.RootListItem = new ListItem("Feeds")
                                    {
                                        Icon = new NamedImage("feed", Assets.Images.Icons.Png._16.feed)
                                    };

            this.RootListItem.ContextMenus.Add("refresh", new ToolStripMenuItem(i18n.Refresh, Assets.Images.Icons.Png._16.update, new EventHandler(RunManualUpdate)));
            this.RootListItem.ContextMenus.Add("markallasread", new ToolStripMenuItem(i18n.MarkAllAsRead, Assets.Images.Icons.Png._16.read, new EventHandler(MenuMarkAllAsReadClicked)));
            this.RootListItem.ContextMenus.Add("markallasunread", new ToolStripMenuItem(i18n.MarkAllAsUnread, Assets.Images.Icons.Png._16.unread, new EventHandler(MenuMarkAllAsUnReadClicked))); 
            this.RootListItem.ContextMenus.Add("settings", new ToolStripMenuItem(i18n.Settings, Assets.Images.Icons.Png._16.settings, new EventHandler(MenuSettingsClicked)));
        }

        public override void Run()
        {
            this.UpdateFeeds();
            this.SetupUpdateTimer();
        }

        public override bool TryDragDrop(string link) // Tries parsing a drag & dropped link to see if it's a feed and parsable.
        {
            if (Subscriptions.Instance.Dictionary.ContainsKey(link))
            {
                MessageBox.Show(string.Format(i18n.FeedSubscriptionAlreadyExists, Subscriptions.Instance.Dictionary[link].Name), i18n.SubscriptionExists, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            FeedSubscription feedSubscription = new FeedSubscription {Name = "test-feed", Url = link};

            using (Feed feed = new Feed(feedSubscription))
            {
                if (feed.IsValid())
                {
                    FeedSubscription subscription = new FeedSubscription();
                    string feedName = "";

                    if (InputBox.Show(i18n.AddNewFeedTitle, i18n.AddNewFeedMessage, ref feedName) == DialogResult.OK)
                    {
                        subscription.Name = feedName;
                        subscription.Url = link;
                        if (Subscriptions.Instance.Add(subscription))
                        {
                            this.RunManualUpdate(this, new EventArgs());
                            return true;
                        }
                        return false;
                    }
                }
            }

            return false;
        }

        private void UpdateFeeds()
        {
            if (this.Updating) return;

            this.Updating = true;
            this.NotifyUpdateStarted();

            if (this._feeds.Count > 0) // clear previous entries before doing an update.
            {
                this._feeds.Clear();
                this.RootListItem.Childs.Clear();
            }
                
            this.RootListItem.SetTitle("Updating feeds..");

            foreach (KeyValuePair<string, FeedSubscription> pair in Subscriptions.Instance.Dictionary)
            {
                Feed feed = new Feed(pair.Value);
                feed.OnStateChange += OnChildStateChange;
                this._feeds.Add(pair.Value.Url, feed);                
            }

            Workload.WorkloadManager.Instance.Add(this._feeds.Count);

            foreach (KeyValuePair<string, Feed> pair in this._feeds) // loop through feeds.
            {
                try
                {
                    pair.Value.Update(); // update the feed.
                    this.RootListItem.Childs.Add(pair.Key, pair.Value);
                    foreach (Story story in pair.Value.Stories) { pair.Value.Childs.Add(story.Guid, story); } // register the story items.
                }
                catch (Exception e) { LogManager.Instance.Write(LogMessageTypes.Error, string.Format("Module feeds caught an exception while updating feeds: {0}", e)); }
                Workload.WorkloadManager.Instance.Step();
            }

            this.RootListItem.SetTitle("Feeds");
            this.NotifyUpdateComplete(new PluginUpdateCompleteEventArgs(true));
            this.Updating = false;
        }

        private void OnChildStateChange(object sender, EventArgs e)
        {
            if (this.RootListItem.State == ((Feed) sender).State) return;

            int unread = this._feeds.Count(pair => pair.Value.State == State.Unread);
            this.RootListItem.State = unread > 0 ? State.Unread : State.Read;
        }

        public override Form GetPreferencesForm()
        {
            return new SettingsForm();
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

            _updateTimer = new System.Timers.Timer(Settings.Instance.UpdatePeriod * 60000);
            _updateTimer.Elapsed += OnTimerHit;
            _updateTimer.Enabled = true;
        }

        private void OnTimerHit(object source, ElapsedEventArgs e)
        {
            if (!RuntimeConfiguration.Instance.InSleepMode) this.UpdateFeeds();
        }

        private void MenuMarkAllAsReadClicked(object sender, EventArgs e)
        {
            foreach (Story s in this._feeds.SelectMany(pair => pair.Value.Stories))
            {
                s.State = State.Read;
            }
        }

        private void MenuMarkAllAsUnReadClicked(object sender, EventArgs e)
        {
            foreach (Story s in this._feeds.SelectMany(pair => pair.Value.Stories))
            {
                s.State = State.Unread;
            }
        }

        private void MenuSettingsClicked(object sender, EventArgs e)
        {
            ModuleSettingsHostForm f = new ModuleSettingsHostForm(this.Attributes, this.GetPreferencesForm());
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
