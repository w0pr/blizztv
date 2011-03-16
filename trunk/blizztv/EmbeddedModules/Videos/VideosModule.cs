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
using BlizzTV.EmbeddedModules.Videos.Settings;
using BlizzTV.InfraStructure.Modules;
using BlizzTV.InfraStructure.Modules.Settings;
using BlizzTV.InfraStructure.Modules.Subscriptions.Catalog;
using BlizzTV.InfraStructure.Modules.Subscriptions.Providers;
using BlizzTV.Log;
using BlizzTV.Utility.Extensions;
using BlizzTV.Utility.Imaging;

namespace BlizzTV.EmbeddedModules.Videos
{
    [ModuleAttributes("Videos", "Video aggregator.","video")]
    public class VideosModule : Module, ISubscriptionConsumer
    {
        private readonly List<Channel> _channels = new List<Channel>(); // holds references to current stored feeds.
        private readonly ModuleNode _moduleNode = new ModuleNode("Videos"); // the root module node.
        private readonly Regex _subscriptionConsumerRegex = new Regex("blizztv\\://videochannel/(?<Name>.*?)/(?<Provider>.*?)/(?<Slug>.*)", RegexOptions.Compiled); // regex for consuming subscriptions from catalog.
        private System.Timers.Timer _updateTimer;
        private bool _disposed = false;

        public static VideosModule Instance;

        public VideosModule() : base()
        {
            VideosModule.Instance = this;
            this.CanRenderTreeNodes = true;
            this._moduleNode.Icon = new NodeIcon("video", Assets.Images.Icons.Png._16.video);

            this._moduleNode.Menu.Add("refresh", new ToolStripMenuItem(i18n.Refresh, Assets.Images.Icons.Png._16.update, new EventHandler(MenuRefresh)));
            this._moduleNode.Menu.Add("markallaswatched", new ToolStripMenuItem(i18n.MarkAllAsWatched, Assets.Images.Icons.Png._16.read, new EventHandler(MenuMarkAllAsWatchedClicked)));
            this._moduleNode.Menu.Add("markallasunwatched", new ToolStripMenuItem(i18n.MarkAllAsUnwatched, Assets.Images.Icons.Png._16.unread, new EventHandler(MenuMarkAllAsUnWatchedClicked)));
            this._moduleNode.Menu.Add("settings", new ToolStripMenuItem(i18n.Settings, Assets.Images.Icons.Png._16.settings, new EventHandler(MenuSettingsClicked)));
        }

        public override void Startup()
        {
            this.UpdateChannels();
            if (this._updateTimer == null) this.SetupUpdateTimer();
        }

        public override ModuleNode GetModuleNode()
        {
            return this._moduleNode;
        }

        #region data handling

        private void UpdateChannels()
        {
            if (this.RefreshingData) return;
            this.RefreshingData = true;

            Module.UITreeView.AsyncInvokeHandler(() => { this._moduleNode.Text = @"Updating channels.."; });
            this._channels.Clear();

            Workload.WorkloadManager.Instance.Add(Subscriptions.Instance.Dictionary.Count);

            var stopwatch = new Stopwatch();
            stopwatch.Start();

            int i = 0;
            var tasks = new Task<Channel>[Subscriptions.Instance.Dictionary.Count];

            foreach (KeyValuePair<string, VideoSubscription> pair in Subscriptions.Instance.Dictionary)
            {
                var channel = ChannelFactory.CreateChannel(pair.Value);
                this._channels.Add(channel);
                channel.StateChanged += OnChildStateChanged;
                tasks[i] = Task.Factory.StartNew(() => TaskProcessChannel(channel));
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

            Module.UITreeView.AsyncInvokeHandler(() =>
            {
                Module.UITreeView.BeginUpdate();

                if (this._moduleNode.Nodes.Count > 0) this._moduleNode.Nodes.Clear();
                foreach (Task<Channel> task in tasks)
                {
                    this._moduleNode.Nodes.Add(task.Result);
                    foreach (Video video in task.Result.Videos)
                    {
                        task.Result.Nodes.Add(video);
                    }
                }

                Module.UITreeView.EndUpdate();
                this._moduleNode.Text = @"Videos";
            });

            stopwatch.Stop();
            TimeSpan ts = stopwatch.Elapsed;
            LogManager.Instance.Write(LogMessageTypes.Trace, string.Format("Updated {0} video channels in {1}.", this._channels.Count, String.Format("{0:00}:{1:00}:{2:00}.{3:00}", ts.Hours, ts.Minutes, ts.Seconds, ts.Milliseconds / 10)));

            this.RefreshingData = false;
        }

        private static Channel TaskProcessChannel(Channel channel)
        {
            channel.Update();
            return channel;
        }

        private void OnChildStateChanged(object sender, EventArgs e)
        {
            if (this._moduleNode.State == ((Channel)sender).State) return;

            int unread = this._channels.Count(channel => channel.State == State.Unread);
            this._moduleNode.State = unread > 0 ? State.Unread : State.Read;
        }

        private void SetupUpdateTimer()
        {
            if (this._updateTimer != null)
            {
                this._updateTimer.Enabled = false;
                this._updateTimer.Elapsed -= OnTimerHit;
                this._updateTimer = null;
            }

            _updateTimer = new System.Timers.Timer(EmbeddedModules.Videos.Settings.ModuleSettings.Instance.UpdatePeriod * 60000);
            _updateTimer.Elapsed += OnTimerHit;
            _updateTimer.Enabled = true;
        }

        private void OnTimerHit(object source, ElapsedEventArgs e)
        {
            if (!RuntimeConfiguration.Instance.InSleepMode) UpdateChannels();
        }

        #endregion

        #region menu handling

        private void MenuRefresh(object sender, EventArgs e)
        {
            var thread = new System.Threading.Thread(UpdateChannels) {IsBackground = true};
            thread.Start();
        }

        private void MenuMarkAllAsWatchedClicked(object sender, EventArgs e)
        {
            foreach (Video video in from Channel channel in this._moduleNode.Nodes from Video video in channel.Nodes select video)
            {
                video.State = State.Read;
            }
        }

        private void MenuMarkAllAsUnWatchedClicked(object sender, EventArgs e)
        {
            foreach (Video video in from Channel channel in this._moduleNode.Nodes from Video video in channel.Nodes select video)
            {
                video.State = State.Unread;
            }
        }

        private void MenuSettingsClicked(object sender, EventArgs e)
        {
            var f = new ModuleSettingsHostForm(this.Attributes, this.GetPreferencesForm());
            f.ShowDialog();
        }

        #endregion

        #region settings handling

        public override Form GetPreferencesForm()
        {
            return new SettingsForm();
        }

        public void OnSaveSettings()
        {
            this.SetupUpdateTimer();
        }

        #endregion

        #region catalog handling

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

        public override bool AddSubscriptionFromUrl(string link)
        {
            foreach (KeyValuePair<string, Provider> pair in Providers.Instance.Dictionary)
            {
                if (((VideoProvider)pair.Value).LinkValid(link))
                {
                    var videoSubscription = new VideoSubscription();
                    videoSubscription.Name = videoSubscription.Slug = (pair.Value as VideoProvider).GetSlug(link);
                    videoSubscription.Provider = pair.Value.Name;

                    using (Channel channel = ChannelFactory.CreateChannel(videoSubscription))
                    {
                        if (channel.IsValid())
                        {
                            if (Subscriptions.Instance.Add(videoSubscription)) this.MenuRefresh(this, new EventArgs());
                            else MessageBox.Show(string.Format(i18n.VideoChannelSubscriptionsAlreadyExists, Subscriptions.Instance.Dictionary[videoSubscription.Slug].Name), i18n.SubscriptionExists, MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return true;
                        }
                    }
                }
            }

            return false;
        }

        #endregion

        #region de-ctor

        // IDisposable pattern: http://msdn.microsoft.com/en-us/library/fs2xkftw%28VS.80%29.aspx

        protected override void Dispose(bool disposing)
        {
            if (this._disposed) return; // if already disposed, just return

            try
            {
                if (disposing) // only dispose managed resources if we're called from directly or in-directly from user code.
                {
                    this._updateTimer.Elapsed -= OnTimerHit;
                    this._updateTimer.Dispose();
                    this._channels.Clear();
                    this._moduleNode.Nodes.Clear();
                }

                this._disposed = true;
            }
            finally // let base-class to dispose also.
            {
                base.Dispose(disposing);
            }
        }

        #endregion
    }
}
