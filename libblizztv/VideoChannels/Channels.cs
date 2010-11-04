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
using System.Threading;


namespace LibBlizzTV.VideoChannels
{
    public class Channels
    {
        public Dictionary<string, Channel> List = new Dictionary<string, Channel>();

        private object _lock;
        private Thread _worker;

        public Channels() { }

        public void Load()
        {
            _load_channels();
        }   

        private void _load_channels()
        {
            XDocument xdoc = XDocument.Load("VideoChannels.xml");
            var entries = from videochannel in xdoc.Descendants("VideoChannel")
                          select new
                          {
                              Name = videochannel.Element("Name").Value,
                              Slug = videochannel.Element("Slug").Value,
                              Provider = videochannel.Element("Provider").Value,
                              Game = videochannel.Element("Game").Value
                          };

            foreach (var entry in entries)
            {
                Channel channel = new Channel();
                channel.Name = entry.Name;
                channel.Slug = entry.Slug;
                channel.Provider = entry.Provider;
                switch (entry.Game)
                {
                    case "Starcraft": channel.Game = Game.Starcraft; break;
                    default: break;
                }
                this.List.Add(entry.Slug, channel);
            }
        }

        public void Update()
        {
            _update_channels();
        }

        private void _update_channels()
        {
            foreach (KeyValuePair<string, Channel> pair in this.List)
            {
                pair.Value.Update();
            }
        }
    }
}
