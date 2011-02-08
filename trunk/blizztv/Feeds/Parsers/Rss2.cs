﻿/*    
 * Copyright (C) 2010-2011, BlizzTV Project - http://code.google.com/p/blizztv/
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

namespace BlizzTV.Feeds.Parsers
{
    public class Rss2:IFeedParser
    {
        public bool Parse(string xml, ref List<FeedItem> items, string linkFallback = "")
        {
            try
            {
                XDocument xdoc = XDocument.Parse(xml);

                var entries = from item in xdoc.Descendants("item")
                              select new
                              {
                                  Id = item.Element("guid").Value,
                                  Title = item.Element("title").Value,
                                  Link = item.Element("link").Value,
                              };

                foreach (var entry in entries) // create the story-item's.
                {
                    items.Add(new FeedItem(entry.Title, entry.Id, entry.Link));
                }

                if (items.Count > 0) return true;
            }
            catch (Exception) { }
            return false;
        }
    }
}
