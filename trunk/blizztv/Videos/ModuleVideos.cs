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
using BlizzTV.Assets.i18n;
using BlizzTV.Configuration;
using BlizzTV.Log;
using BlizzTV.Modules;
using BlizzTV.Modules.Settings;
using BlizzTV.Modules.Subscriptions.Catalog;
using BlizzTV.Modules.Subscriptions.Providers;
using BlizzTV.Utility.Imaging;

namespace BlizzTV.Videos
{
    [ModuleAttributes("Videos", "Video aggregator.","video")]
    public class ModuleVideos : Module, ISubscriptionConsumer
    {
        private Dictionary<string,Channel> _channels = new Dictionary<string,Channel>(); // the channels list.
        private System.Timers.Timer _updateTimer;
        private readonly Regex _subscriptionConsumerRegex = new Regex("blizztv\\://videochannel/(?<Name>.*?)/(?<Provider>.*?)/(?<Slug>.*)", RegexOptions.Compiled);
        private bool _disposed = false;

        public static ModuleVideos Instance;

        public ModuleVideos() : base()
        {
            ModuleVideos.Instance = this;
            this.RootListItem = new ListItem("Videos")
                                    {
                                        Icon = new NamedImage("video", Assets.Images.Icons.Png._16.video)
                                    };

            this.RootListItem.ContextMenus.Add("refresh", new ToolStripMenuItem(i18n.Refresh, Assets.Images.Icons.Png._16.update, new EventHandler(RunManualUpdate))); 
            this.RootListItem.ContextMenus.Add("markallaswatched", new ToolStripMenuItem(i18n.MarkAllAsWatched, Assets.Images.Icons.Png._16.read, new EventHandler(MenuMarkAllAsWatchedClicked)));
            this.RootListItem.ContextMenus.Add("markallasunwatched", new ToolStripMenuItem(i18n.MarkAllAsUnwatched, Assets.Images.Icons.Png._16.unread, new EventHandler(MenuMarkAllAsUnWatchedClicked)));
            this.RootListItem.ContextMenus.Add("settings", new ToolStripMenuItem(i18n.Settings, Assets.Images.Icons.Png._16.settings, new EventHandler(MenuSettingsClicked)));
        }

        public override void Run()
        {
            this.UpdateChannels();
            this.SetupUpdateTimer();
        }

        public override Form GetPreferencesForm()
        {
            return new SettingsForm();
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
                            else MessageBox.Show(string.Format(i18n.VideoChannelSubscriptionsAlreadyExists, Subscriptions.Instance.Dictionary[v.Slug].Name), i18n.SubscriptionExists, MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return true;
                        }
                    }
                }
            }

            return false;
        }

        private void UpdateChannels()
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

            var stopwatch = new Stopwatch();
            stopwatch.Start();

            int i = 0;
            var tasks = new Task<Channel>[this._channels.Count];

            foreach (KeyValuePair<string, Channel> pair in this._channels)
            {
                KeyValuePair<string, Channel> local = pair;
                tasks[i] = Task.Factory.StartNew(() => TaskProcessChannel(local.Value));
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
            catch (AggregateException aggregateException)
            {
                foreach (var exception in aggregateException.InnerExceptions)
                {
                    LogManager.Instance.Write(LogMessageTypes.Error, string.Format("Video channels module caught an exception while running channel processing task:  {0}", exception));
                }
            }

            foreach (Task<Channel> task in tasks)
            {
                this.RootListItem.Childs.Add(string.Format("{0}@{1}",task.Result.Slug,task.Result.Provider), task.Result);
                foreach (Video video in task.Result.Videos) { task.Result.Childs.Add(video.Guid, video); }
            }

            /*foreach (KeyValuePair<string, Channel> pair in this._channels) // loop through videos.
            {
                pair.Value.Update(); // update the channel.
                this.RootListItem.Childs.Add(pair.Key, pair.Value);
                foreach (Video v in pair.Value.Videos) { pair.Value.Childs.Add(v.Guid, v); } // register the video items.
                Workload.WorkloadManager.Instance.Step();                    
            }*/

            stopwatch.Stop();
            TimeSpan ts = stopwatch.Elapsed;
            LogManager.Instance.Write(LogMessageTypes.Trace, string.Format("Updated {0} video channels in {1}.", this._channels.Count, String.Format("{0:00}:{1:00}:{2:00}.{3:00}", ts.Hours, ts.Minutes, ts.Seconds, ts.Milliseconds / 10)));

            this.RootListItem.SetTitle("Videos");
            NotifyUpdateComplete(new PluginUpdateCompleteEventArgs(true));
            this.Updating = false;
        }

        private static Channel TaskProcessChannel(Channel channel)
        {
            channel.Update();
            return channel;
        }
        
        private void OnChildStateChange(object sender, EventArgs e)
        {
            if (this.RootListItem.State == ((Channel) sender).State) return;

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

            _updateTimer = new System.Timers.Timer(Settings.Instance.UpdatePeriod * 60000);
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
            ModuleSettingsHostForm f = new ModuleSettingsHostForm(this.Attributes, this.GetPreferencesForm());
            f.ShowDialog();
        }

        public string GetCatalogUrl()
        {
            return "http://www.blizztv.com/catalog/videochannels";
        }

        public void ConsumeSubscription(string entryUrl)
        {
            Match match = this._subscriptionConsumerRegex.Match(entryUrl);
            if (!match.Success) return;

            string name = match.Groups["Name"].Value;
            string provider = match.Groups["Provider"].Value;
            string slug = match.Groups["Slug"].Value;

            var subscription = new VideoSubscription { Name = name, Provider = provider, Slug = slug };
            Subscriptions.Instance.Add(subscription);
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
