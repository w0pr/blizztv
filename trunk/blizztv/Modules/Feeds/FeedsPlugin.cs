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
using BlizzTV.CommonLib.UI;
using BlizzTV.ModuleLib;
using BlizzTV.CommonLib.Workload;

namespace BlizzTV.Modules.Feeds
{
    [ModuleAttributes("Feeds","Feed aggregator plugin.","feed_16")]
    public class FeedsPlugin:Module
    {
        #region members

        internal Dictionary<string,Feed> _feeds = new Dictionary<string,Feed>(); // the feeds list 
        private Timer _update_timer;
        private bool disposed = false;

        public static FeedsPlugin Instance;

        #endregion

        #region ctor

        public FeedsPlugin(): base()
        {
            FeedsPlugin.Instance = this;
            this.RootListItem = new ListItem("Feeds");

            // register context menu's.
            this.RootListItem.ContextMenus.Add("manualupdate", new System.Windows.Forms.ToolStripMenuItem("Update Feeds", null, new EventHandler(RunManualUpdate))); // mark as unread menu.
            this.RootListItem.ContextMenus.Add("markallasread", new System.Windows.Forms.ToolStripMenuItem("Mark All As Read", null, new EventHandler(MenuMarkAllAsReadClicked))); // mark as read menu.
            this.RootListItem.ContextMenus.Add("markallasunread", new System.Windows.Forms.ToolStripMenuItem("Mark All As Unread", null, new EventHandler(MenuMarkAllAsUnReadClicked))); // mark as unread menu.            
        }

        #endregion

        #region API handlers

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
            feedSubscription.URL = link;

            using (Feed feed = new Feed(feedSubscription))
            {
                if (feed.IsValid())
                {
                    FeedSubscription subscription = new FeedSubscription();
                    string feedName = "";
                    if (InputBox.Show("Add New Feed", "Please enter name for the new feed", ref feedName) == System.Windows.Forms.DialogResult.OK)
                    {
                        subscription.Name = feedName;
                        subscription.URL = link;
                        if (Subscriptions.Instance.Add(subscription)) this.RunManualUpdate(this, new EventArgs()); else return false;
                        return true;
                    }
                }
            }

            return false;
        }

        #endregion

        #region internal logic

        internal void UpdateFeeds()
        {
            if (!this.Updating)
            {
                this.Updating = true;
                this.NotifyUpdateStarted();

                if (this._feeds.Count > 0) // clear previous entries before doing an update.
                {
                    this._feeds.Clear();
                    this.RootListItem.Childs.Clear();
                }
                
                this.RootListItem.Style = ItemStyle.REGULAR;
                this.RootListItem.SetTitle("Updating feeds..");

                foreach (KeyValuePair<string, FeedSubscription> pair in Subscriptions.Instance.Dictionary)
                {
                    Feed feed = new Feed(pair.Value);
                    this._feeds.Add(pair.Value.URL, feed);
                    feed.OnStyleChange += ChildStyleChange;
                }

                Workload.Instance.Add(this, this._feeds.Count);

                foreach (KeyValuePair<string, Feed> pair in this._feeds) // loop through feeds.
                {
                    try
                    {
                        pair.Value.Update(); // update the feed.
                        this.RootListItem.Childs.Add(pair.Key, pair.Value);
                        foreach (Story story in pair.Value.Stories) { pair.Value.Childs.Add(story.GUID, story); } // register the story items.
                    }
                    catch (Exception e) { Log.Instance.Write(LogMessageTypes.Error, string.Format("Feed Plugin - UpdateFeeds Exception: {0}", e.ToString())); }
                    Workload.Instance.Step(this);
                }

                this.RootListItem.SetTitle("Feeds");
                this.NotifyUpdateComplete(new PluginUpdateCompleteEventArgs(true));
                this.Updating = false;
            }
        }

        void ChildStyleChange(ItemStyle Style)
        {
            if (this.RootListItem.Style == Style) return;

            int unread = 0;
            foreach (KeyValuePair<string, Feed> pair in this._feeds) { if (pair.Value.Style == ItemStyle.BOLD) unread++; }
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
            if (!GlobalSettings.Instance.InSleepMode) this.UpdateFeeds();
        }

        private void MenuMarkAllAsReadClicked(object sender, EventArgs e)
        {
            foreach (KeyValuePair<string, Feed> pair in this._feeds)
            {
                foreach (Story s in pair.Value.Stories) { s.Status = Story.Statutes.READ; }
            }
        }

        private void MenuMarkAllAsUnReadClicked(object sender, EventArgs e)
        {
            foreach (KeyValuePair<string, Feed> pair in this._feeds)
            {
                foreach (Story s in pair.Value.Stories) { s.Status = Story.Statutes.UNREAD; }
            }
        }

        private void RunManualUpdate(object sender, EventArgs e)
        {
            System.Threading.Thread t = new System.Threading.Thread(delegate() { this.UpdateFeeds(); }) 
            { IsBackground = true, Name = string.Format("plugin-{0}-{1}", this.Attributes.Name, DateTime.Now.TimeOfDay.ToString()) };
            t.Start();                 
        }

        #endregion

        #region de-ctor

        protected override void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing) // managed resources
                {
                    this._update_timer.Enabled = false;
                    this._update_timer.Elapsed -= OnTimerHit;
                    this._update_timer.Dispose();
                    this._update_timer = null;
                    foreach (KeyValuePair<string,Feed> pair in this._feeds) { pair.Value.Dispose(); }
                    this._feeds.Clear();
                    this._feeds = null;
                }
                base.Dispose(disposing);
            }            
        }

        #endregion
    }
}
