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
using System.Linq;
using System.Xml.Linq;
using System.Xml.XPath;
using System.Text;
using System.Timers;
using LibBlizzTV;
using LibBlizzTV.Utils;
using LibBlizzTV.Settings;
using LibBlizzTV.Notifications;

namespace LibVideos
{
    [PluginAttributes("Videos", "Video aggregator plugin.","video_16.png")]
    public class VideosPlugin:Plugin
    {
        #region members

        private string _xml_file = @"plugins\xml\videos\channels.xml";
        internal Dictionary<string,Channel> _channels = new Dictionary<string,Channel>(); // the channels list.
        private Timer _update_timer;
        private bool _updating = false;
        private bool disposed = false;

        public static VideosPlugin Instance;

        #endregion

        #region ctor

        public VideosPlugin() : base()
        {
            VideosPlugin.Instance = this;
            this.RootListItem = new ListItem("Videos");

            // register context menu's.
            this.RootListItem.ContextMenus.Add("manualupdate", new System.Windows.Forms.ToolStripMenuItem("Update Channels", null, new EventHandler(RunManualUpdate))); // mark as unread menu.
            this.RootListItem.ContextMenus.Add("markallaswatched", new System.Windows.Forms.ToolStripMenuItem("Mark All As Watched", null, new EventHandler(MenuMarkAllAsWatchedClicked))); // mark as read menu.
            this.RootListItem.ContextMenus.Add("markallasunwatched", new System.Windows.Forms.ToolStripMenuItem("Mark All As Unwatched", null, new EventHandler(MenuMarkAllAsUnWatchedClicked))); // mark as unread menu.
        }

        #endregion

        #region API handlers

        public override void Run()
        {
            this.UpdateChannels();
            this.SetupUpdateTimer();
        }

        public override System.Windows.Forms.Form GetPreferencesForm()
        {
            return new frmSettings();
        }

        #endregion

        #region internal logic

        internal void UpdateChannels()
        {
            bool success = true;

            if (!this._updating)
            {
                this._updating = true;
                this.NotifyUpdateStarted();

                if (this._channels.Count > 0)  // clear previous entries before doing an update.
                {
                    this._channels.Clear();
                    this.RootListItem.Childs.Clear();
                }

                this.RootListItem.SetTitle("Updating videos..");

                try
                {
                    XDocument xdoc = XDocument.Load(this._xml_file); // load the xml.
                    var entries = from channel in xdoc.Descendants("Channel") // get the channels.
                                  select new
                                  {
                                      Name = channel.Attribute("Name").Value,
                                      Slug = channel.Element("Slug").Value,
                                      Provider = channel.Element("Provider").Value,
                                  };

                    foreach (var entry in entries) // create up the channel items.
                    {
                        Channel c = ChannelFactory.CreateChannel(entry.Name, entry.Slug, entry.Provider);
                        this._channels.Add(entry.Name, c);
                    }
                }
                catch (Exception e)
                {
                    success = false;
                    Log.Instance.Write(LogMessageTypes.ERROR, string.Format("VideoChannelsPlugin ParseChannels() Error: \n {0}", e.ToString()));
                    System.Windows.Forms.MessageBox.Show(string.Format("An error occured while parsing your channels.xml. Please correct the error and re-start the plugin. \n\n[Error Details: {0}]", e.Message), "Video Channels Plugin Error", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                }

                if (success) // if parsing of videochannels.xml all okay.
                {
                    int unread = 0; // channels with un-watched videos count.

                    this.AddWorkload(this._channels.Count);

                    foreach (KeyValuePair<string, Channel> pair in this._channels) // loop through videos.
                    {
                        pair.Value.Update(); // update the channel.
                        this.RootListItem.Childs.Add(pair.Key, pair.Value);
                        foreach (Video v in pair.Value.Videos) { pair.Value.Childs.Add(v.GUID, v); } // register the video items.
                        if (pair.Value.State == ItemState.UNREAD) unread++;
                        this.StepWorkload();
                    }

                    this.RootListItem.SetTitle(string.Format("Videos ({0})", unread.ToString()));  // add non-watched channels count to root item's title.

                    foreach (KeyValuePair<string, Channel> pair in this._channels)
                    {
                        foreach (KeyValuePair<string, ListItem> child_pair in pair.Value.Childs)
                        {
                            if (child_pair.Value.State == ItemState.FRESH)
                            {
                                Notifications.Instance.Show(child_pair.Value, child_pair.Value.Title, "Click to watch.", System.Windows.Forms.ToolTipIcon.Info);
                                break;
                            }
                        }
                    }
                }
                NotifyUpdateComplete(new PluginUpdateCompleteEventArgs(success));
                this._updating = false;
            }
        }

        internal void SaveChannelsXML()
        {
            try
            {
                foreach (KeyValuePair<string, Channel> pair in this._channels)
                {
                    if (pair.Value.CommitOnSave)
                    {
                        XDocument xdoc = XDocument.Load(this._xml_file);
                        xdoc.Element("Channels").Add(new XElement("Channel", new XAttribute("Name", pair.Value.Name), new XElement("Slug", pair.Value.Slug), new XElement("Provider", pair.Value.Provider)));
                        pair.Value.CommitOnSave = false;
                        xdoc.Save(this._xml_file);
                    }
                    else if (pair.Value.DeleteOnSave)
                    {
                        XDocument xdoc = XDocument.Load(this._xml_file);
                        xdoc.XPathSelectElement(string.Format("Channels/Channel[@Name='{0}']", pair.Value.Name)).Remove();
                        pair.Value.DeleteOnSave = false;
                        xdoc.Save(this._xml_file);
                    }
                }
                this.RunManualUpdate(this, EventArgs.Empty);
            }
            catch (Exception e)
            {
                Log.Instance.Write(LogMessageTypes.ERROR, string.Format("FeedsPlugin SaveFeedsXML() Error: \n {0}", e.ToString()));
            }
        }

        public void SaveSettings()
        {
            Settings.Instance.Save();
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
            if (!Global.Instance.InSleepMode) UpdateChannels();
        }

        private void RunManualUpdate(object sender, EventArgs e)
        {
            System.Threading.Thread t = new System.Threading.Thread(delegate() { UpdateChannels(); }) 
            { IsBackground = true, Name = string.Format("plugin-{0}-{1}", this.Attributes.Name, DateTime.Now.TimeOfDay.ToString()) };
            t.Start();
        }

        private void MenuMarkAllAsWatchedClicked(object sender, EventArgs e)
        {
            foreach (KeyValuePair<string, Channel> pair in this._channels)
            {
                pair.Value.State = ItemState.READ;
                foreach (Video v in pair.Value.Videos) { v.State = ItemState.READ; }
            }
        }

        private void MenuMarkAllAsUnWatchedClicked(object sender, EventArgs e)
        {
            foreach (KeyValuePair<string, Channel> pair in this._channels)
            {
                pair.Value.State = ItemState.UNREAD;
                foreach (Video v in pair.Value.Videos) { v.State = ItemState.UNREAD; }
            }
        }

        #endregion

        #region de-ctor

        protected override void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing) // managed resources
                {
                    if (this._update_timer != null)
                    {
                        this._update_timer.Enabled = false;
                        this._update_timer.Elapsed -= OnTimerHit;
                        this._update_timer.Dispose();
                        this._update_timer = null;
                    }
                    foreach (KeyValuePair<string,Channel> pair in this._channels) { pair.Value.Dispose(); }
                    this._channels.Clear();
                    this._channels = null;
                }
                base.Dispose(disposing);
            }
        }

        #endregion
    }
}
