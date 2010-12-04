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
using BlizzTV.ModuleLib;
using BlizzTV.ModuleLib.Notifications;
using BlizzTV.ModuleLib.Subscriptions;

namespace BlizzTV.Modules.Streams
{
    [ModuleAttributes("Streams", "Live stream aggregator plugin.","stream_16")]
    public class StreamsPlugin:Module
    {
        #region members

        internal Dictionary<string,Stream> _streams = new Dictionary<string,Stream>();
        private Timer _update_timer;
        private bool _updating = false;
        private bool disposed = false;

        public static StreamsPlugin Instance;

        #endregion

        #region ctor

        public StreamsPlugin():base()
        {
            StreamsPlugin.Instance = this;
            this.RootListItem = new ListItem("Streams");

            // register context-menu's.
            this.RootListItem.ContextMenus.Add("manualupdate", new System.Windows.Forms.ToolStripMenuItem("Update Streams", null, new EventHandler(RunManualUpdate))); // mark as unread menu.
        }

        #endregion

        #region API handlers

        public override void Run()
        {
            this.UpdateStreams();
            this.SetupUpdateTimer();
        }

        public override System.Windows.Forms.Form GetPreferencesForm()
        {
            return new frmSettings();
        }

        #endregion

        #region internal logic

        internal void UpdateStreams()
        {
            if (!this._updating)
            {
                this._updating = true;
                this.NotifyUpdateStarted();

                if (this._streams.Count > 0)// clear previous entries before doing an update.
                {
                    this._streams.Clear();
                    this.RootListItem.Childs.Clear();
                }

                this.RootListItem.SetTitle("Updating streams..");

                foreach (ISubscription subscription in Subscriptions.Instance.List)
                {
                    StreamSubscription feedSubscription = (StreamSubscription)subscription;
                    Stream s = StreamFactory.CreateStream(feedSubscription.Name, feedSubscription.Slug, feedSubscription.Provider);
                    this._streams.Add(s.Name, s);
                }

                int available_count = 0; // available live streams count
                this.AddWorkload(this._streams.Count);

                foreach (KeyValuePair<string, Stream> pair in this._streams) // loop through all streams
                {
                    try
                    {
                        pair.Value.Update(); // update the stream
                        if (pair.Value.IsLive) // if it's live
                        {
                            pair.Value.SetTitle(string.Format("{0} ({1})", pair.Value.Title, pair.Value.ViewerCount)); // put stream viewers count on title.
                            available_count++; // increment available live streams count.
                            this.RootListItem.Childs.Add(pair.Key, pair.Value);
                        }
                        this.StepWorkload();
                    }
                    catch (Exception e) { Log.Instance.Write(LogMessageTypes.ERROR, string.Format("StreamsPlugin ParseStreams() Error: \n {0}", e.ToString())); } // catch errors for inner stream-handlers.
                }

                this.RootListItem.SetTitle(string.Format("Streams ({0})", available_count));  // put available streams count on root object's title.

                NotifyUpdateComplete(new PluginUpdateCompleteEventArgs(true));
                this._updating = false;
            }
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
            if (!GlobalSettings.Instance.InSleepMode) UpdateStreams();
        }

        private void RunManualUpdate(object sender, EventArgs e)
        {
            System.Threading.Thread t = new System.Threading.Thread(delegate() { UpdateStreams(); }) 
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
                    foreach (KeyValuePair<string,Stream> pair in this._streams) { pair.Value.Dispose(); }
                    this._streams.Clear();
                    this._streams = null;
                }
                base.Dispose(disposing);
            }
        }

        #endregion
    }
}
