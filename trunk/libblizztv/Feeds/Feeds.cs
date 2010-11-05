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

namespace LibBlizzTV.Feeds
{
    public class Feeds
    {
        public Dictionary<string, Feed> List = new Dictionary<string, Feed>();

        public void Load()
        {
            XDocument xdoc = XDocument.Load("Feeds.xml");
            var entries = from feed in xdoc.Descendants("Feed")
                          select new
                          {
                              Name = feed.Element("Name").Value,
                              Url = feed.Element("Url").Value,
                          };

            foreach (var entry in entries)
            {
                Feed f = new Feed();
                f.Name = entry.Name;
                f.Url = entry.Url;
                this.List.Add(f.Name, f);
            }
        }

        public void Update()
        {
            foreach (KeyValuePair<string, Feed> pair in this.List)
            {
                pair.Value.Update();
            }
        }
    }
}
