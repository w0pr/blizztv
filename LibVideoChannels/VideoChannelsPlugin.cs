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
using System.Text;
using System.Timers;
using LibBlizzTV;
using LibBlizzTV.Utils;

namespace LibVideoChannels
{
    [PluginAttributes("Video Channels", "Video channel aggregator plugin for BlizzTV","video_16.png")]
    public class VideoChannelsPlugin:Plugin
    {
        #region members

        private ListItem _root_item = new ListItem("Videos"); // root item on treeview.
        private List<Channel> _channels = new List<Channel>(); // the channels list.
        private Timer _update_timer = new Timer(1000 * 60 * 5);
        private bool disposed = false;

        public static Plugin Instance;

        #endregion

        #region ctor

        public VideoChannelsPlugin(PluginSettings ps)
            : base(ps)
        {
            VideoChannelsPlugin.Instance = this;
            this.Menus.Add("subscriptions", new System.Windows.Forms.ToolStripMenuItem("Subscriptions", null, new EventHandler(MenuSubscriptionsClicked))); // register subscriptions menu.
        }

        #endregion

        #region API handlers

        public override void Run()
        {
            this.RegisterListItem(_root_item); // register root item.            
            PluginLoadComplete(new PluginLoadCompleteEventArgs(UpdateChannels())); // parse channels

            // setup update timer for next data updates
            _update_timer.Elapsed += new ElapsedEventHandler(OnTimerHit);
            _update_timer.Enabled = true;
        }

        public override System.Windows.Forms.Form GetPreferencesForm()
        {
            return new frmSettings();
        }

        #endregion

        #region internal logic

        private bool UpdateChannels()
        {
            bool success = true;

            this._root_item.SetTitle("Updating videos..");
            if (this._channels.Count > 0) this.DeleteExistingChannels(); // clear previous entries before doing an update.

            try
            {
                XDocument xdoc = XDocument.Load("VideoChannels.xml"); // load the xml.
                var entries = from videochannel in xdoc.Descendants("VideoChannel") // get the channels.
                              select new
                              {
                                  Name = videochannel.Element("Name").Value,
                                  Slug = videochannel.Element("Slug").Value,
                                  Provider = videochannel.Element("Provider").Value,
                              };

                foreach (var entry in entries) // create up the channel items.
                {
                    Channel c = new Channel(entry.Name, entry.Slug, entry.Provider);
                    this._channels.Add(c);
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

                foreach (Channel channel in this._channels) // loop through videos.
                {
                    channel.Update(); // update the channel.
                    RegisterListItem(channel, _root_item);  // if the channel parsed all okay, regiser the channel-item.                    
                    foreach (Video v in channel.Videos) { RegisterListItem(v, channel); } // register the video items.
                    if (channel.State == ItemState.UNREAD) unread++;
                    this.StepWorkload();
                }

                _root_item.SetTitle(string.Format("Videos ({0})", unread.ToString()));  // add non-watched channels count to root item's title.
            }
            return success;
        }

        private void DeleteExistingChannels() // removes all current feeds.
        {
            foreach (Channel c in this._channels) { c.Delete(); } // Delete the feeds.
            this._channels.Clear(); // remove them from the list.
            GC.Collect();
            GC.WaitForPendingFinalizers();
        }

        private void OnTimerHit(object source, ElapsedEventArgs e)
        {
            PluginDataUpdateComplete(new PluginDataUpdateCompleteEventArgs(UpdateChannels()));
        }

        public void MenuSubscriptionsClicked(object sender, EventArgs e) // subscriptions menu handler
        {
            frmDataEditor f = new frmDataEditor("VideoChannels.xml", "VideoChannel");
            f.Show();
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
                    foreach (Channel c in this._channels) { c.Dispose(); }
                    this._channels.Clear();
                    this._channels = null;
                }
                base.Dispose(disposing);
            }
        }

        #endregion
    }
}
