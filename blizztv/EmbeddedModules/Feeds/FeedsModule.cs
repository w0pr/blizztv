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
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Timers;
using System.Windows.Forms;
using System.Diagnostics;
using BlizzTV.Configuration;
using BlizzTV.EmbeddedModules.Feeds.Settings;
using BlizzTV.InfraStructure.Modules;
using BlizzTV.InfraStructure.Modules.Settings;
using BlizzTV.InfraStructure.Modules.Subscriptions.Catalog;
using BlizzTV.Log;
using BlizzTV.Utility.Extensions;
using BlizzTV.Utility.Imaging;
using BlizzTV.Utility.UI;
using BlizzTV.Assets.i18n;

namespace BlizzTV.EmbeddedModules.Feeds
{
    [ModuleAttributes("Feeds","Feed aggregator.","feed")]
    public class FeedsModule : Module ,ISubscriptionConsumer
    {
        private readonly ModuleNode _moduleNode = new ModuleNode("Feeds");



        private readonly ListItem _rootItem = new ListItem("Feeds") { Icon = new NamedImage("feed", Assets.Images.Icons.Png._16.feed) };
        private Dictionary<string,Feed> _feeds = new Dictionary<string,Feed>(); // list of feeds.
        private System.Timers.Timer _updateTimer = null;
        private readonly Regex _subscriptionConsumerRegex = new Regex("blizztv\\://feed/(?<Name>.*?)/(?<Url>.*)", RegexOptions.Compiled);
        
        private bool _disposed = false;

        public static FeedsModule Instance;

        public FeedsModule()
        {
            FeedsModule.Instance = this;

            this.CanRenderTreeNodes = true;

            this._rootItem.ContextMenus.Add("refresh", new ToolStripMenuItem(i18n.Refresh, Assets.Images.Icons.Png._16.update, new EventHandler(RunManualUpdate)));
            this._rootItem.ContextMenus.Add("markallasread", new ToolStripMenuItem(i18n.MarkAllAsRead, Assets.Images.Icons.Png._16.read, new EventHandler(MenuMarkAllAsReadClicked)));
            this._rootItem.ContextMenus.Add("markallasunread", new ToolStripMenuItem(i18n.MarkAllAsUnread, Assets.Images.Icons.Png._16.unread, new EventHandler(MenuMarkAllAsUnReadClicked)));
            this._rootItem.ContextMenus.Add("settings", new ToolStripMenuItem(i18n.Settings, Assets.Images.Icons.Png._16.settings, new EventHandler(MenuSettingsClicked)));
        }

        public override void Startup()
        {
            this.Refresh();                   
        }

        public override void Refresh()
        {
            this.UpdateFeeds();
            if (this._updateTimer == null) this.SetupUpdateTimer();
        }

        public override bool AddSubscriptionFromUrl(string link) // Tries parsing a drag & dropped link to see if it's a feed and parsable.
        {
            if (Subscriptions.Instance.Dictionary.ContainsKey(link))
            {
                MessageBox.Show(string.Format(i18n.FeedSubscriptionAlreadyExists, Subscriptions.Instance.Dictionary[link].Name), i18n.SubscriptionExists, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            FeedSubscription feedSubscription = new FeedSubscription {Name = "test-feed", Url = link};

            /*using (Feed feed = new Feed(feedSubscription))
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
            }*/

            return false;
        }

        public override ListItem GetRootItem()
        {
            return this._rootItem;
        }

        public override TreeNode GetTreeNode()
        {
            return this._moduleNode;
        }

        private void UpdateFeeds()
        {
            if (this.RefreshingData) return;

            this.RefreshingData = true;
            this.OnDataRefreshStarting(EventArgs.Empty);

            if (this._feeds.Count > 0) // clear previous entries before doing an update.
            {
                this._feeds.Clear();
                this._rootItem.Childs.Clear();
            }

            this._rootItem.SetTitle("Updating feeds..");

            foreach (KeyValuePair<string, FeedSubscription> pair in Subscriptions.Instance.Dictionary)
            {
                Feed feed = new Feed(pair.Value);
                //feed.OnStateChange += OnChildStateChange;
                this._feeds.Add(pair.Value.Url, feed);                
            }

            Workload.WorkloadManager.Instance.Add(this._feeds.Count);

            var stopwatch = new Stopwatch();
            stopwatch.Start();

            int i = 0;
            var tasks = new Task<Feed>[this._feeds.Count];

            foreach(KeyValuePair<string,Feed> pair in this._feeds)
            {
                KeyValuePair<string, Feed> local = pair; 
                tasks[i] = Task.Factory.StartNew(() => TaskProcessFeed(local.Value));
                i++;
            }

            var tasksWaitQueue = tasks;

            while (tasksWaitQueue.Length > 0)
            {
                int taskIndex = Task.WaitAny(tasksWaitQueue);
                tasksWaitQueue = tasksWaitQueue.Where((t) => t != tasksWaitQueue[taskIndex]).ToArray();
                Workload.WorkloadManager.Instance.Step();
            }

            try { Task.WaitAll(tasks); }
            catch(AggregateException aggregateException)
            {
                foreach(var exception in aggregateException.InnerExceptions)
                {
                    LogManager.Instance.Write(LogMessageTypes.Error, string.Format("Feeds module caught an exception while running feed processing task:  {0}", exception));
                }
            }

            Module.UITreeView.AsyncInvokeHandler(() =>
            {
                foreach (Task<Feed> task in tasks)
                {                
                    this._moduleNode.Nodes.Add(task.Result);                
                    foreach(Story story in task.Result.Stories)
                    {
                        task.Result.Nodes.Add(story);
                    }
                //this._rootItem.Childs.Add(task.Result.Url, task.Result);
                //foreach (Story story in task.Result.Stories) { task.Result.Childs.Add(story.Guid, story); }
                }
            });

            stopwatch.Stop();
            TimeSpan ts = stopwatch.Elapsed;
            LogManager.Instance.Write(LogMessageTypes.Trace, string.Format("Updated {0} feeds in {1}.", this._feeds.Count, String.Format("{0:00}:{1:00}:{2:00}.{3:00}", ts.Hours, ts.Minutes, ts.Seconds, ts.Milliseconds/10)));

            this._rootItem.SetTitle("Feeds");
            this.OnDataRefreshCompleted(new DataRefreshCompletedEventArgs(true));
            this.RefreshingData = false;
        }

        private static Feed TaskProcessFeed(Feed feed)
        {
            feed.Update();
            return feed;
        }

        private void OnChildStateChange(object sender, EventArgs e)
        {
            /*if (this._rootItem.State == ((Feed)sender).State) return;

            int unread = this._feeds.Count(pair => pair.Value.State == State.Unread);
            this._rootItem.State = unread > 0 ? State.Unread : State.Read;*/
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

            _updateTimer = new System.Timers.Timer(EmbeddedModules.Feeds.Settings.ModuleSettings.Instance.UpdatePeriod * 60000);
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
                //s.State = State.Read;
            }
        }

        private void MenuMarkAllAsUnReadClicked(object sender, EventArgs e)
        {
            foreach (Story s in this._feeds.SelectMany(pair => pair.Value.Stories))
            {
                //s.State = State.Unread;
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

        public string GetCatalogUrl()
        {
            return "http://www.blizztv.com/catalog/feeds";
        }

        public void ConsumeSubscription(string entryUrl)
        {
            Match match = this._subscriptionConsumerRegex.Match(entryUrl);
            if (!match.Success) return;

            string name = match.Groups["Name"].Value;
            string url = match.Groups["Url"].Value;

            var subscription = new FeedSubscription {Name = name, Url = url};
            Subscriptions.Instance.Add(subscription);
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
                //foreach (KeyValuePair<string,Feed> pair in this._feeds) { pair.Value.Dispose(); }
                this._feeds.Clear();
                this._feeds = null;
            }
            base.Dispose(disposing);
        }

        #endregion
    }
}
