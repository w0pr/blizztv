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
using System.Diagnostics;
using System.Windows.Forms;
using BlizzTV.Assets.i18n;
using BlizzTV.Configuration;
using BlizzTV.EmbeddedModules.Streams.Settings;
using BlizzTV.InfraStructure.Modules;
using BlizzTV.InfraStructure.Modules.Settings;
using BlizzTV.InfraStructure.Modules.Subscriptions.Catalog;
using BlizzTV.InfraStructure.Modules.Subscriptions.Providers;
using BlizzTV.Log;
using BlizzTV.Utility.Extensions;
using BlizzTV.Utility.Imaging;
using BlizzTV.Utility.UI;

namespace BlizzTV.EmbeddedModules.Streams
{
    [ModuleAttributes("Streams", "Live-stream aggregator.","stream")]
    public class StreamsModule : Module , ISubscriptionConsumer
    {
        private readonly ModuleNode _moduleNode = new ModuleNode("Streams"); // the root module node.
        private readonly Regex _subscriptionConsumerRegex = new Regex("blizztv\\://stream/(?<Name>.*?)/(?<Provider>.*?)/(?<Slug>.*)", RegexOptions.Compiled); // regex for consuming subscriptions from catalog.
        private System.Timers.Timer _updateTimer;
        private bool _disposed = false;

        public static StreamsModule Instance;

        public StreamsModule()
        {
            StreamsModule.Instance = this;

            this.CanRenderTreeNodes = true;

            this._moduleNode.Icon = new NodeIcon("stream", Assets.Images.Icons.Png._16.stream);

            this._moduleNode.Menu.Add("refresh", new ToolStripMenuItem(i18n.Refresh, Assets.Images.Icons.Png._16.update, new EventHandler(MenuRefresh)));
            this._moduleNode.Menu.Add("settings", new ToolStripMenuItem(i18n.Settings, Assets.Images.Icons.Png._16.settings, new EventHandler(MenuSettingsClicked)));
        }

        public override void Startup()
        {
            this.UpdateStreams();
            if (this._updateTimer == null) this.SetupUpdateTimer();
        }

        public override ModuleNode GetModuleNode()
        {
            return this._moduleNode;
        }

        #region data handling

        private void UpdateStreams()
        {
            if (this.RefreshingData) return;
            this.RefreshingData = true;

            Module.UITreeView.AsyncInvokeHandler(() => { this._moduleNode.Text = @"Updating streams.."; });

            int availableCount = 0; // available live streams count
            Workload.WorkloadManager.Instance.Add(Subscriptions.Instance.Dictionary.Count);

            var stopwatch = new Stopwatch();
            stopwatch.Start();

            int i = 0;
            var tasks = new Task<Stream>[Subscriptions.Instance.Dictionary.Count];

            foreach (KeyValuePair<string, StreamSubscription> pair in Subscriptions.Instance.Dictionary)
            {
                var stream = StreamFactory.CreateStream(pair.Value);
                tasks[i] = Task.Factory.StartNew(() => TaskProcessStream(stream));
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
                    LogManager.Instance.Write(LogMessageTypes.Error, string.Format("Streams module caught an exception while running stream processing task:  {0}", exception));
                }
            }

            Module.UITreeView.AsyncInvokeHandler(() =>
            {
                Module.UITreeView.BeginUpdate();

                if (this._moduleNode.Nodes.Count > 0) this._moduleNode.Nodes.Clear();
                foreach (Task<Stream> task in tasks)
                {
                    if (task.Result.IsLive)
                    {
                        task.Result.Text = string.Format("{0} ({1})", task.Result.Text, task.Result.ViewerCount); // put stream viewers count on title.
                        availableCount++;
                        this._moduleNode.Nodes.Add(task.Result);
                    }
                }

                Module.UITreeView.EndUpdate();
                Module.UITreeView.AsyncInvokeHandler(() =>
                {
                    if (availableCount > 0) this._moduleNode.Text = string.Format(@"Streams ({0})", availableCount);
                    else
                    {
                        this._moduleNode.Text = @"Streams";
                        this._moduleNode.State = State.Read;
                    }
                });
            });

            stopwatch.Stop();
            TimeSpan ts = stopwatch.Elapsed;
            LogManager.Instance.Write(LogMessageTypes.Trace, string.Format("Updated {0} streams in {1}.", Subscriptions.Instance.Dictionary.Count, String.Format("{0:00}:{1:00}:{2:00}.{3:00}", ts.Hours, ts.Minutes, ts.Seconds, ts.Milliseconds / 10)));

            this.RefreshingData = false;
        }

        private static Stream TaskProcessStream(Stream stream)
        {
            stream.Update();
            return stream;
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
            if (!RuntimeConfiguration.Instance.InSleepMode) UpdateStreams();
        }

        #endregion

        #region menu handling

        private void MenuRefresh(object sender, EventArgs e)
        {
            var thread = new System.Threading.Thread(UpdateStreams) { IsBackground = true };
            thread.Start();
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
            return "http://www.blizztv.com/catalog/streams";
        }

        public void ConsumeSubscription(string entryUrl)
        {
            Match match = this._subscriptionConsumerRegex.Match(entryUrl);
            if (!match.Success) return;

            string name = match.Groups["Name"].Value;
            string provider = match.Groups["Provider"].Value;
            string slug = match.Groups["Slug"].Value;

            var subscription = new StreamSubscription {Name = name, Provider = provider, Slug = slug};
            Subscriptions.Instance.Add(subscription);
        }

        public override bool AddSubscriptionFromUrl(string link)
        {
            foreach (KeyValuePair<string, Provider> pair in Providers.Instance.Dictionary)
            {
                if (((StreamProvider)pair.Value).LinkValid(link))
                {
                    var streamSubscription = new StreamSubscription();
                    streamSubscription.Slug = (pair.Value as StreamProvider).GetSlug(link);
                    streamSubscription.Provider = pair.Value.Name;
                    streamSubscription.Name = (pair.Value as StreamProvider).GetSlug(link);

                    if (streamSubscription.Provider.ToLower() == "own3dtv")
                    {
                        string name = "";
                        if (InputBox.Show("Add New Stream", "Please enter name for the new stream", ref name) == System.Windows.Forms.DialogResult.OK) streamSubscription.Name = name;
                        else return false;
                    }

                    if (Subscriptions.Instance.Add(streamSubscription)) this.MenuRefresh(this, new EventArgs());
                    else MessageBox.Show(string.Format("The stream already exists in your subscriptions named as '{0}'.", Subscriptions.Instance.Dictionary[streamSubscription.Slug].Name), "Subscription Exists", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                    return true;
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
