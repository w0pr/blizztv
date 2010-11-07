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

namespace LibVideoChannels
{
    [Plugin("LibVideoChannels", "Video channel aggregator plugin for BlizzTV","video_16.png")]
    public class VideoChannelsPlugin:Plugin
    {
        private List<Channel> _channels = new List<Channel>();

        public VideoChannelsPlugin() { }

        public override void Load(PluginSettings ps)
        {
            VideoChannelsPlugin.PluginSettings = ps;

            ListItem root = new ListItem("Videos");
            RegisterListItem(root);            

            XDocument xdoc = XDocument.Load("VideoChannels.xml");
            var entries = from videochannel in xdoc.Descendants("VideoChannel")
                          select new
                          {
                              Name = videochannel.Element("Name").Value,
                              Slug = videochannel.Element("Slug").Value,
                              Provider = videochannel.Element("Provider").Value,
                          };

            foreach (var entry in entries)
            {
                Channel c = new Channel(entry.Name,entry.Slug,entry.Provider);
                this._channels.Add(c);
            }

            int unread = 0;

            foreach (Channel channel in this._channels)
            {
                RegisterListItem(channel,root);
                channel.Update();
                foreach (Video v in channel.Videos)
                {
                    RegisterListItem(v, channel);
                }
                if (channel.State == ItemState.UNREAD) unread++;
            }

            if (unread > 0)
            {
                root.SetTitle(string.Format("{0} ({1})", root.Title, unread.ToString()));
            }

            PluginLoadComplete(new PluginLoadCompleteEventArgs(true));
        }
    }
}
