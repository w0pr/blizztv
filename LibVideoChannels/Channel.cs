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
using LibBlizzTV;
using LibBlizzTV.Utils;

namespace LibVideoChannels
{
    public class Channel:ListItem
    {
        private string _slug;
        private string _provider;               

        public string Slug { get { return this._slug; } set { this._slug = value; } }
        public string Provider { get { return this._provider; } set { this._provider = value; } }
                
        public List<Video> Videos = new List<Video>();

        public Channel(string Title, string Slug, String Provider)
            : base(Title)
        {
            this.Slug = Slug;
            this.Provider = Provider;
        }

        public void Update() 
        {
            string api_url = string.Format(@"http://gdata.youtube.com/feeds/api/users/{0}/uploads?alt=rss&max-results=5", this.Slug);
            string response = WebReader.Read(api_url);

            XDocument xdoc = XDocument.Parse(response);
            var entries = from item in xdoc.Descendants("item")
                          select new
                          {
                              GUID = item.Element("guid").Value,
                              Title = item.Element("title").Value,
                              Link = item.Element("link").Value
                          };

            foreach (var entry in entries)
            {                
                Video v = new Video(entry.Title,entry.GUID,entry.Link,this.Provider);
                this.Videos.Add(v);
            }

            int unread = 0;
            foreach (Video v in this.Videos) { if (v.State == ItemState.UNREAD) unread++; }

            if (unread > 0)
            {
                this.SetTitle(string.Format("{0} ({1})",this.Title,unread.ToString()));
                this.SetState(ItemState.UNREAD);
            }
        }        
    }
}
