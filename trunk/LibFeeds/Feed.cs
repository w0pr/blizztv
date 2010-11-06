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

namespace LibFeeds
{
    public class Feed : ListItem
    {
        public string URL;
        public List<Story> Stories = new List<Story>();

        public virtual void Update()
        {
            string response = WebReader.Read(this.URL);
            XDocument xdoc = XDocument.Parse(response);

            var entries = from item in xdoc.Descendants("item")
                          select new
                          {
                              GUID = item.Element("guid").Value,
                              Title = item.Element("title").Value,
                              Link = item.Element("link").Value,
                              Description=item.Element("description").Value
                          };

            foreach (var entry in entries)
            {
                Story s = new Story();
                s.GUID = entry.GUID;
                s.Link = entry.Link;
                s.Title = entry.Title;
                s.Description = entry.Description;
                this.Stories.Add(s);
            }
        }
    }
}
