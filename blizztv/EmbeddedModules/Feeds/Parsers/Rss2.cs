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

namespace BlizzTV.EmbeddedModules.Feeds.Parsers
{
    /// <summary>
    /// Parses RSS 2.0 feeds.
    /// </summary>
    public class Rss2 : IFeedParser
    {
        public bool Parse(string xml, ref List<FeedItem> items, string linkFallback = "")
        {
            try
            {
                var xdoc = XDocument.Parse(xml);

                if (xdoc.Root == null) return false;
                XNamespace defaultNS = xdoc.Root.GetDefaultNamespace();

                var entries = from item in xdoc.Descendants(defaultNS + "item")
                    select new
                    {
                        Id = (string) item.Element(defaultNS + "guid") ?? "",
                        Title = (string) item.Element(defaultNS + "title") ?? "",
                        Link = (string) item.Element(defaultNS + "link") ?? "",
                    };

                if (items == null) items = new List<FeedItem>();
                items.AddRange(entries.Select(entry => new FeedItem(entry.Title, entry.Id, entry.Link)));
                return items.Count > 0;
            }
            catch (Exception) { return false; } // supress the exceptions as the method can also be used for checking a feed if it's compatible with the standart.            
        }
    }
}
