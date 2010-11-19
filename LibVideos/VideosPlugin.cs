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

namespace LibVideos
{
    [PluginAttributes("Video Channels", "Video channel aggregator plugin for BlizzTV","video_16.png")]
    public class VideosPlugin:Plugin
    {
        #region members

        private ListItem _root_item = new ListItem("Videos"); // root item on treeview.
        internal Dictionary<string,Channel> _channels = new Dictionary<string,Channel>(); // the channels list.
        private Timer _update_timer;
        private bool disposed = false;

        public static VideosPlugin Instance;

        #endregion

        #region ctor

        public VideosPlugin(PluginSettings ps)
            : base(ps)
        {
            VideosPlugin.Instance = this;
        }

        #endregion

        #region API handlers

        public override void Run()
        {
            this.RegisterListItem(_root_item); // register root item.            
            PluginLoadComplete(new PluginLoadCompleteEventArgs(UpdateChannels())); // parse channels

            // setup update timer for next data updates
            _update_timer = new Timer((Settings as Settings).UpdateEveryXMinutes * 60000);
            _update_timer.Elapsed += new ElapsedEventHandler(OnTimerHit);
            _update_timer.Enabled = true;
        }

        public override System.Windows.Forms.Form GetPreferencesForm()
        {
            return new frmSettings();
        }

        #endregion

        #region internal logic

        internal bool UpdateChannels()
        {
            bool success = true;

            this._root_item.SetTitle("Updating videos..");
            if (this._channels.Count > 0) this.DeleteExistingChannels(); // clear previous entries before doing an update.

            try
            {
                XDocument xdoc = XDocument.Load("VideoChannels.xml"); // load the xml.
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
                    this._channels.Add(entry.Name,c);
                }
            }
            catch (Exception e)
            {
                success = false;
                Log.Instance.Write(LogMessageTypes.ERROR, string.Format("VideoChannelsPlugin ParseChannels() Error: \n {0}", e.ToString()));
                System.Windows.Forms.MessageBox.Show(string.Format("An error occured while parsing your videochannels.xml. Please correct the error and re-start the plugin. \n\n[Error Details: {0}]", e.Message), "Video Channels Plugin Error", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
            }

            if (success) // if parsing of videochannels.xml all okay.
            {
                int unread = 0; // channels with un-watched videos count.

                this.AddWorkload(this._channels.Count);

                foreach (KeyValuePair<string,Channel> pair in this._channels) // loop through videos.
                {
                    pair.Value.Update(); // update the channel.
                    RegisterListItem(pair.Value, _root_item);  // if the channel parsed all okay, regiser the channel-item.                    
                    foreach (Video v in pair.Value.Videos) { RegisterListItem(v, pair.Value); } // register the video items.
                    if (pair.Value.State == ItemState.UNREAD) unread++;
                    this.StepWorkload();
                }

                _root_item.SetTitle(string.Format("Videos ({0})", unread.ToString()));  // add non-watched channels count to root item's title.
            }
            return success;
        }

        internal void SaveChannelsXML()
        {
            try
            {
                foreach (KeyValuePair<string, Channel> pair in this._channels)
                {
                    if (pair.Value.CommitOnSave)
                    {
                        XDocument xdoc = XDocument.Load("VideoChannels.xml");
                        xdoc.Element("Channels").Add(new XElement("Channel", new XAttribute("Name", pair.Value.Name), new XElement("Slug", pair.Value.Slug), new XElement("Provider", pair.Value.Provider)));
                        xdoc.Save("VideoChannels.xml");
                    }
                    else if (pair.Value.DeleteOnSave)
                    {
                        XDocument xdoc = XDocument.Load("VideoChannels.xml");
                        xdoc.XPathSelectElement(string.Format("Channels/Channel[@Name='{0}']", pair.Value.Name)).Remove();
                        xdoc.Save("VideoChannels.xml");
                    }
                }
            }
            catch (Exception e)
            {
                Log.Instance.Write(LogMessageTypes.ERROR, string.Format("FeedsPlugin SaveFeedsXML() Error: \n {0}", e.ToString()));
            }
        }

        private void DeleteExistingChannels() // removes all current feeds.
        {
            foreach (KeyValuePair<string,Channel> pair in this._channels) { pair.Value.Delete(); } // Delete the feeds.
            this._channels.Clear(); // remove them from the list.
            GC.Collect();
            GC.WaitForPendingFinalizers();
        }

        private void OnTimerHit(object source, ElapsedEventArgs e)
        {
            PluginDataUpdateComplete(new PluginDataUpdateCompleteEventArgs(UpdateChannels()));
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
