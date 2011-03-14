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
    public class FeedsModule : Module , ISubscriptionConsumer
    {
        private bool _disposed = false;
        private readonly List<Feed> _feeds = new List<Feed>(); // holds references to current stored feeds.
        private readonly ModuleNode _moduleNode = new ModuleNode("Feeds");
        private System.Timers.Timer _updateTimer = null;
        private readonly Regex _subscriptionConsumerRegex = new Regex("blizztv\\://feed/(?<Name>.*?)/(?<Url>.*)", RegexOptions.Compiled);       

        public static FeedsModule Instance;

        public FeedsModule()
        {
            FeedsModule.Instance = this;

            this.CanRenderTreeNodes = true;

            this._moduleNode.Icon = new NamedImage("feed", Assets.Images.Icons.Png._16.feed);

            this._moduleNode.Menu.Add("refresh", new ToolStripMenuItem(i18n.Refresh, Assets.Images.Icons.Png._16.update, new EventHandler(RunManualUpdate)));
            this._moduleNode.Menu.Add("markallasread", new ToolStripMenuItem(i18n.MarkAllAsRead, Assets.Images.Icons.Png._16.read, new EventHandler(MenuMarkAllAsReadClicked)));
            this._moduleNode.Menu.Add("markallasunread", new ToolStripMenuItem(i18n.MarkAllAsUnread, Assets.Images.Icons.Png._16.unread, new EventHandler(MenuMarkAllAsUnReadClicked)));
            this._moduleNode.Menu.Add("settings", new ToolStripMenuItem(i18n.Settings, Assets.Images.Icons.Png._16.settings, new EventHandler(MenuSettingsClicked)));
        }

        public override void Startup()
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

            var feedSubscription = new FeedSubscription {Name = "test-feed", Url = link};

            using (var feed = new Feed(feedSubscription))
            {
                if (feed.IsValid())
                {
                    var subscription = new FeedSubscription();
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

        public override ModuleNode GetModuleNode()
        {
            return this._moduleNode;
        }

        private void UpdateFeeds()
        {
            if (this.RefreshingData) return;
            this.RefreshingData = true;

            Module.UITreeView.AsyncInvokeHandler(() => { this._moduleNode.Text = @"Updating feeds.."; });

            this._feeds.Clear();

            Workload.WorkloadManager.Instance.Add(Subscriptions.Instance.Dictionary.Count);

            var stopwatch = new Stopwatch();
            stopwatch.Start();

            int i = 0;
            var tasks = new Task<Feed>[Subscriptions.Instance.Dictionary.Count];

            foreach(KeyValuePair<string,FeedSubscription> pair in Subscriptions.Instance.Dictionary)
            {
                var feed = new Feed(pair.Value);
                this._feeds.Add(feed);
                feed.StateChanged += OnChildStateChanged;
                tasks[i] = Task.Factory.StartNew(() => TaskProcessFeed(feed));
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
                Module.UITreeView.BeginUpdate();
                if (this._moduleNode.Nodes.Count > 0) this._moduleNode.Nodes.Clear();
                foreach (Task<Feed> task in tasks)
                {                
                    this._moduleNode.Nodes.Add(task.Result);      
                    foreach(Story story in task.Result.Stories)
                    {
                        task.Result.Nodes.Add(story);
                    }
                }
                Module.UITreeView.EndUpdate();
                this._moduleNode.Text = @"Feeds";
            });

            stopwatch.Stop();
            TimeSpan ts = stopwatch.Elapsed;
            LogManager.Instance.Write(LogMessageTypes.Trace, string.Format("Updated {0} feeds in {1}.", this._feeds.Count, String.Format("{0:00}:{1:00}:{2:00}.{3:00}", ts.Hours, ts.Minutes, ts.Seconds, ts.Milliseconds / 10)));

            this.RefreshingData = false;
        }

        private static Feed TaskProcessFeed(Feed feed)
        {
            feed.Update();
            return feed;
        }

        private void OnChildStateChanged(object sender, EventArgs e)
        {
            if (this._moduleNode.State == ((Feed)sender).State) return;

            int unread = this._feeds.Count(feed => feed.State == State.Unread);
            this._moduleNode.State = unread > 0 ? State.Unread : State.Read;
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

            _updateTimer = new System.Timers.Timer(ModuleSettings.Instance.UpdatePeriod * 60000);
            _updateTimer.Elapsed += OnTimerHit;
            _updateTimer.Enabled = true;
        }

        private void OnTimerHit(object source, ElapsedEventArgs e)
        {
            if (!RuntimeConfiguration.Instance.InSleepMode) this.UpdateFeeds();
        }

        private void MenuMarkAllAsReadClicked(object sender, EventArgs e)
        {
            foreach (Story story in from Feed feed in this._moduleNode.Nodes from Story story in feed.Nodes select story)
            {
                story.State = State.Read;
            }
        }

        private void MenuMarkAllAsUnReadClicked(object sender, EventArgs e)
        {
            foreach (Story story in from Feed feed in this._moduleNode.Nodes from Story story in feed.Nodes select story)
            {
                story.State = State.Unread;
            }
        }

        private void MenuSettingsClicked(object sender, EventArgs e)
        {
            var f = new ModuleSettingsHostForm(this.Attributes, this.GetPreferencesForm());
            f.ShowDialog();
        }

        private void RunManualUpdate(object sender, EventArgs e)
        {
            var thread = new System.Threading.Thread(this.UpdateFeeds) {IsBackground = true};
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
                foreach (Feed feed in this._moduleNode.Nodes) { feed.Dispose(); }
                this._moduleNode.Nodes.Clear();
            }
            base.Dispose(disposing);
        }

        #endregion
    }
}
