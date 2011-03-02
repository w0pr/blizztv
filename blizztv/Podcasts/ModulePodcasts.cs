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
using System.Windows.Forms;
using System.Timers;
using BlizzTV.Assets.i18n;
using BlizzTV.Configuration;
using BlizzTV.Log;
using BlizzTV.Modules;
using BlizzTV.Modules.Settings;
using BlizzTV.Modules.Subscriptions.Catalog;
using BlizzTV.Utility.Imaging;
using BlizzTV.Utility.UI;

namespace BlizzTV.Podcasts
{
    [ModuleAttributes("Podcasts", "Podcast aggregator.", "podcast")]
    public class ModulePodcasts : Module,ISubscriptionConsumer
    {
        private Dictionary<string, Podcast> _podcasts = new Dictionary<string, Podcast>(); // list of feeds.
        private System.Timers.Timer _updateTimer;
        private readonly Regex _subscriptionConsumerRegex = new Regex("blizztv\\://podcast/(?<Name>.*?)/(?<Url>.*)", RegexOptions.Compiled);

        public static ModulePodcasts Instance;

        public ModulePodcasts()
        {
            ModulePodcasts.Instance = this;

            this.RootListItem = new ListItem("Podcasts")
                                    {
                                        Icon = new NamedImage("podcast", Assets.Images.Icons.Png._16.podcast)
                                    };

            this.RootListItem.ContextMenus.Add("refresh", new ToolStripMenuItem(i18n.Refresh, Assets.Images.Icons.Png._16.update, new EventHandler(RunManualUpdate)));
            this.RootListItem.ContextMenus.Add("markallasread", new ToolStripMenuItem(i18n.MarkAllAsRead, Assets.Images.Icons.Png._16.read, new EventHandler(MenuMarkAllAsReadClicked)));
            this.RootListItem.ContextMenus.Add("markallasunread", new ToolStripMenuItem(i18n.MarkAllAsUnread, Assets.Images.Icons.Png._16.unread, new EventHandler(MenuMarkAllAsUnReadClicked)));
            this.RootListItem.ContextMenus.Add("settings", new ToolStripMenuItem(i18n.Settings, Assets.Images.Icons.Png._16.settings, new EventHandler(MenuSettingsClicked)));
        }

        public override void Run()
        {
            this.UpdatePodcasts();
            this.SetupUpdateTimer();
        }

        public override bool TryDragDrop(string link) // Tries parsing a drag & dropped link to see if it's a podcast and parsable.
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

        private void UpdatePodcasts()
        {
            if (this.Updating) return;

            this.Updating = true;
            this.NotifyUpdateStarted();

            if (this._podcasts.Count > 0) // clear previous entries before doing an update.
            {
                this._podcasts.Clear();
                this.RootListItem.Childs.Clear();
            }

            this.RootListItem.SetTitle("Updating podcasts..");

            foreach (KeyValuePair<string, PodcastSubscription> pair in Subscriptions.Instance.Dictionary)
            {
                Podcast podcast = new Podcast(pair.Value);
                podcast.OnStateChange += OnChildStateChange;
                this._podcasts.Add(pair.Value.Url, podcast);
            }

            Workload.WorkloadManager.Instance.Add(this._podcasts.Count);

            foreach(KeyValuePair<string,Podcast> pair in this._podcasts)
            {
                try
                {
                    pair.Value.Update(); // update the podcast
                    this.RootListItem.Childs.Add(pair.Key, pair.Value);
                    foreach (Episode episode in pair.Value.Episodes) { pair.Value.Childs.Add(episode.Guid, episode); } // register the episodes.
                }
                catch (Exception e) { LogManager.Instance.Write(LogMessageTypes.Error, string.Format("Module podcasts caught an exception while updating podcasts: {0}", e)); }
                Workload.WorkloadManager.Instance.Step();
            }

            this.RootListItem.SetTitle("Podcasts");
            this.NotifyUpdateComplete(new PluginUpdateCompleteEventArgs(true));
            this.Updating = false;
        }

        private void OnChildStateChange(object sender, EventArgs e)
        {
            if (this.RootListItem.State == ((Podcast)sender).State) return;

            int unread = this._podcasts.Count(pair => pair.Value.State == State.Unread);
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
