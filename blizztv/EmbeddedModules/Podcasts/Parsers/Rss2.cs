/*    
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

namespace BlizzTV.EmbeddedModules.Podcasts.Parsers
{
    /// <summary>
    /// Parses RSS 2.0 based podcast-feeds.
    /// </summary>
    public class Rss2:IPodcastFeedParser
    {
        public bool Parse(string xml, ref List<PodcastItem> items)
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
                                             Enclosure = (XElement) item.Element(defaultNS + "enclosure") ?? null
                                         };

                if (items == null) items = new List<PodcastItem>();
                items.AddRange(entries.Where(entry => entry.Enclosure != null).Select(entry => new PodcastItem(entry.Title, entry.Id, entry.Link, entry.Enclosure.Attribute("url").Value)));
                return items.Count > 0;
            }
            catch (Exception) { return false; } // supress the exceptions as the method can also be used for checking a podcast-feed if it's compatible with the standart.
        }
    }
}
