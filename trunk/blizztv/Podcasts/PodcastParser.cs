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

using System.Collections.Generic;
using BlizzTV.Podcasts.Parsers;
using BlizzTV.Utility.Web;

namespace BlizzTV.Podcasts
{
    /// <summary>
    /// Tries to parse podcast-feeds using implemented parsers.
    /// </summary>
    public class PodcastParser
    {
        #region instance

        private static PodcastParser _instance = new PodcastParser();
        public static PodcastParser Instance { get { return _instance; } }

        #endregion 

        private readonly IPodcastFeedParser[] _parsers = new IPodcastFeedParser[] { new Rss2() }; // our supported parsers.

        public bool Parse(string url,ref List<PodcastItem> items)
        {
            if (items == null) items = new List<PodcastItem>();
            if (items.Count > 0) items.Clear();

            WebReader.Result result = WebReader.Read(url);
            if (result.State != WebReader.States.Success) return false; 

            foreach(IPodcastFeedParser parser in this._parsers)
            {
                if (parser.Parse(result.Response, ref items)) return true;
            }

            return false;
        }
    }

    public class PodcastItem
    {
        public string Title { get; private set; }
        public string Id { get; private set; }
        public string Link { get; private set; }
        public string Enclosure { get; private set; }

        public PodcastItem(string title, string id, string link,string enclosure)
        {
            this.Title = title;
            this.Id = id;
            this.Link = link;
            this.Enclosure = enclosure;
        }
    }
}
