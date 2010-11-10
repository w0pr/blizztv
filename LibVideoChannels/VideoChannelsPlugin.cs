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
using LibBlizzTV;
using LibBlizzTV.Utils;

namespace LibVideoChannels
{
    [Plugin("Video Channels", "Video channel aggregator plugin for BlizzTV","video_16.png")]
    public class VideoChannelsPlugin:Plugin
    {
        #region members

        private ListItem _root_item = new ListItem("Videos"); // root item on treeview.
        private List<Channel> _channels = new List<Channel>(); // the channels list.
        private bool disposed = false;

        #endregion

        #region ctor

        public VideoChannelsPlugin()
        {
            this.Menus.Add("subscriptions", new System.Windows.Forms.ToolStripMenuItem("Subscriptions", null, new EventHandler(MenuSubscriptionsClicked))); // register subscriptions menu.
        }

        #endregion

        #region API handlers

        public override void Load(PluginSettings ps)
        {
            VideoChannelsPlugin.PluginSettings = ps;

            this.RegisterListItem(_root_item); // register root item.            

            PluginLoadComplete(new PluginLoadCompleteEventArgs(ParseChannels())); // parse channels
        }

        #endregion

        #region internal logic

        private bool ParseChannels()
        {
            bool success = true;

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

                foreach (Channel channel in this._channels) // loop through videos.
                {
                    channel.Update(); // update the channel.
                    if (channel.Valid)
                    {
                        RegisterListItem(channel, _root_item);  // if the channel parsed all okay, regiser the channel-item.                    
                        foreach (Video v in channel.Videos) { RegisterListItem(v, channel); } // register the video items.
                        if (channel.State == ItemState.UNREAD) unread++;
                    }
                }

                if (unread > 0) { _root_item.SetTitle(string.Format("{0} ({1})", _root_item.Title, unread.ToString())); } // add non-watched channels count to root item's title.
            }
            return success;
        }

        public void MenuSubscriptionsClicked(object sender, EventArgs e) // subscriptions menu handler
        {
            frmDataEditor f = new frmDataEditor("VideoChannels.xml", "VideoChannel");
            f.Show();
        }

        #endregion

        #region de-ctor

        ~VideoChannelsPlugin() { Dispose(false); }

        private void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing) // managed resources
                {
                    foreach (Channel c in this._channels) { c.Dispose(); }
                    this._channels.Clear();
                    this._channels = null;
                }
                disposed = true;
            }
        }

        #endregion
    }
}
