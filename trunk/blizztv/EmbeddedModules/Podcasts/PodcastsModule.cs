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
using System.Windows.Forms;
using System.Timers;
using System.Diagnostics;
using BlizzTV.Assets.i18n;
using BlizzTV.Configuration;
using BlizzTV.EmbeddedModules.Podcasts.Settings;
using BlizzTV.InfraStructure.Modules;
using BlizzTV.InfraStructure.Modules.Settings;
using BlizzTV.InfraStructure.Modules.Subscriptions.Catalog;
using BlizzTV.Log;
using BlizzTV.Utility.Imaging;
using BlizzTV.Utility.UI;

namespace BlizzTV.EmbeddedModules.Podcasts
{
    [ModuleAttributes("Podcasts", "Podcast aggregator.", "podcast")]
    public class PodcastsModule : Module,ISubscriptionConsumer
    {
        private readonly ListItem _rootItem = new ListItem("Podcasts") { Icon = new NamedImage("podcast", Assets.Images.Icons.Png._16.podcast) };
        private Dictionary<string, Podcast> _podcasts = new Dictionary<string, Podcast>(); // list of feeds.
        private System.Timers.Timer _updateTimer;
        private readonly Regex _subscriptionConsumerRegex = new Regex("blizztv\\://podcast/(?<Name>.*?)/(?<Url>.*)", RegexOptions.Compiled);

        public static PodcastsModule Instance;

        public PodcastsModule()
        {
            PodcastsModule.Instance = this;

            this._rootItem.ContextMenus.Add("refresh", new ToolStripMenuItem(i18n.Refresh, Assets.Images.Icons.Png._16.update, new EventHandler(RunManualUpdate)));
            this._rootItem.ContextMenus.Add("markallasread", new ToolStripMenuItem(i18n.MarkAllAsRead, Assets.Images.Icons.Png._16.read, new EventHandler(MenuMarkAllAsReadClicked)));
            this._rootItem.ContextMenus.Add("markallasunread", new ToolStripMenuItem(i18n.MarkAllAsUnread, Assets.Images.Icons.Png._16.unread, new EventHandler(MenuMarkAllAsUnReadClicked)));
            this._rootItem.ContextMenus.Add("settings", new ToolStripMenuItem(i18n.Settings, Assets.Images.Icons.Png._16.settings, new EventHandler(MenuSettingsClicked)));
        }

        public override void Refresh()
        {
            this.UpdatePodcasts();
            if (this._updateTimer == null) this.SetupUpdateTimer();
        }

        public override bool AddSubscriptionFromUrl(string link) // Tries parsing a drag & dropped link to see if it's a podcast and parsable.
        {
            if (Subscriptions.Instance.Dictionary.ContainsKey(link))
            {
                MessageBox.Show(string.Format(i18n.PodcastSubscriptionAlreadyExists, Subscriptions.Instance.Dictionary[link].Name), i18n.SubscriptionExists, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            PodcastSubscription podcastSubscription = new PodcastSubscription { Name = "test-podcast", Url = link };

            using (Podcast podcast = new Podcast(podcastSubscription))
            {
                if (podcast.IsValid())
                {
                    PodcastSubscription subscription = new PodcastSubscription();
                    string podcastName = "";

                    if (InputBox.Show(i18n.AddNewPodcastTitle, i18n.AddNewPodcastMessage, ref podcastName) == DialogResult.OK)
                    {
                        subscription.Name = podcastName;
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

        public override ListItem GetRootItem()
        {
            return this._rootItem;
        }

        private void UpdatePodcasts()
        {
            if (this.RefreshingData) return;

            this.RefreshingData = true;
            this.OnDataRefreshStarting(EventArgs.Empty);

            if (this._podcasts.Count > 0) // clear previous entries before doing an update.
            {
                this._podcasts.Clear();
                this._rootItem.Childs.Clear();
            }

            this._rootItem.SetTitle("Updating podcasts..");

            foreach (KeyValuePair<string, PodcastSubscription> pair in Subscriptions.Instance.Dictionary)
            {
                Podcast podcast = new Podcast(pair.Value);
                podcast.OnStateChange += OnChildStateChange;
                this._podcasts.Add(pair.Value.Url, podcast);
            }

            Workload.WorkloadManager.Instance.Add(this._podcasts.Count);

            var stopwatch = new Stopwatch();
            stopwatch.Start();

            int i = 0;
            var tasks = new Task<Podcast>[this._podcasts.Count];

            foreach(KeyValuePair<string,Podcast> pair in this._podcasts)
            {
                KeyValuePair<string, Podcast> local = pair;
                tasks[i] = Task.Factory.StartNew(() => TaskProcessPodcast(local.Value));
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
                    LogManager.Instance.Write(LogMessageTypes.Error, string.Format("Podcasts module caught an exception while running podcast processing task:  {0}", exception));
                }
            }

            foreach (Task<Podcast> task in tasks)
            {
                this._rootItem.Childs.Add(task.Result.Url, task.Result);
                foreach (Episode episode in task.Result.Episodes) { task.Result.Childs.Add(episode.Guid, episode); }
            }

            /*foreach(KeyValuePair<string,Podcast> pair in this._podcasts)
            {
                try
                {
                    pair.Value.Update(); // update the podcast
                    this.RootListItem.Childs.Add(pair.Key, pair.Value);
                    foreach (Episode episode in pair.Value.Episodes) { pair.Value.Childs.Add(episode.Guid, episode); } // register the episodes.
                }
                catch (Exception e) { LogManager.Instance.Write(LogMessageTypes.Error, string.Format("Module podcasts caught an exception while updating podcasts: {0}", e)); }
                Workload.WorkloadManager.Instance.Step();
            }*/

            stopwatch.Stop();
            TimeSpan ts = stopwatch.Elapsed;
            LogManager.Instance.Write(LogMessageTypes.Trace, string.Format("Updated {0} podcasts in {1}.", this._podcasts.Count, String.Format("{0:00}:{1:00}:{2:00}.{3:00}", ts.Hours, ts.Minutes, ts.Seconds, ts.Milliseconds / 10)));

            this._rootItem.SetTitle("Podcasts");
            this.OnDataRefreshCompleted(new DataRefreshCompletedEventArgs(true));
            this.RefreshingData = false;
        }

        private static Podcast TaskProcessPodcast(Podcast podcast)
        {
            podcast.Update();
            return podcast;
        }

        private void OnChildStateChange(object sender, EventArgs e)
        {
            if (this._rootItem.State == ((Podcast)sender).State) return;

            int unread = this._podcasts.Count(pair => pair.Value.State == State.Unread);
            this._rootItem.State = unread > 0 ? State.Unread : State.Read;
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

            _updateTimer = new System.Timers.Timer(EmbeddedModules.Podcasts.Settings.ModuleSettings.Instance.UpdatePeriod * 60000);
            _updateTimer.Elapsed += OnTimerHit;
            _updateTimer.Enabled = true;
        }

        private void OnTimerHit(object source, ElapsedEventArgs e)
        {
            if (!RuntimeConfiguration.Instance.InSleepMode) this.UpdatePodcasts();
        }

        private void MenuMarkAllAsReadClicked(object sender, EventArgs e)
        {
            foreach (Episode episode in this._podcasts.SelectMany(pair => pair.Value.Episodes))
            {
                episode.State = State.Read;
            }
        }

        private void MenuMarkAllAsUnReadClicked(object sender, EventArgs e)
        {
            foreach (Episode episode in this._podcasts.SelectMany(pair => pair.Value.Episodes))
            {
                episode.State = State.Unread;
            }
        }

        private void MenuSettingsClicked(object sender, EventArgs e)
        {
            ModuleSettingsHostForm f = new ModuleSettingsHostForm(this.Attributes, this.GetPreferencesForm());
            f.ShowDialog();
        }

        private void RunManualUpdate(object sender, EventArgs e)
        {
            System.Threading.Thread thread = new System.Threading.Thread(this.UpdatePodcasts) { IsBackground = true };
            thread.Start();
        }

        public string GetCatalogUrl()
        {
            return "http://www.blizztv.com/catalog/podcasts";
        }

        public void ConsumeSubscription(string entryUrl)
        {
            Match match = this._subscriptionConsumerRegex.Match(entryUrl);
            if (!match.Success) return;

            string name = match.Groups["Name"].Value;
            string url = match.Groups["Url"].Value;

            var subscription = new PodcastSubscription { Name = name, Url = url };
            Subscriptions.Instance.Add(subscription);
        }
    }
}
