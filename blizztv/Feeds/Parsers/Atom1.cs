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
 * 
 * $Id$
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

namespace BlizzTV.Feeds.Parsers
{
    public class Atom1:IFeedParser
    {
        public bool Parse(string xml, ref List<FeedItem> items, string linkFallback = "")
        {
            try
            {
                XDocument xdoc = XDocument.Parse(xml);
                XNamespace xmlns = "http://www.w3.org/2005/Atom";

                var entries = from entry in xdoc.Descendants(xmlns + "entry")
                              select new
                              {
                                  Id = entry.Element(xmlns + "id").Value,
                                  Title = entry.Element(xmlns + "title").Value,
                                  Link = (entry.Element(xmlns + "link") != null) ? entry.Element(xmlns + "link").Attribute("href").Value : String.Empty
                              };

                foreach (var entry in entries) // create the story-item's.
                {
                    items.Add(new FeedItem(entry.Title, entry.Id, String.IsNullOrEmpty(linkFallback) ? entry.Link : string.Format("{0}{1}", linkFallback, entry.Id)));
                }

                if (items.Count > 0) return true;
            }
            catch (Exception) { }
            return false;
        }
    }
}
