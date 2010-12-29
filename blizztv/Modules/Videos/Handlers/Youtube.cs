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
using System.Linq;
using System.Xml.Linq;
using BlizzTV.CommonLib.Logger;
using BlizzTV.CommonLib.Web;

namespace BlizzTV.Modules.Videos.Handlers
{
    public class Youtube : Channel
    {
        public Youtube(VideoSubscription subscription) : base(subscription) { }

        public override bool Parse()
        {
            try
            {
                string apiUrl = string.Format("http://gdata.youtube.com/feeds/api/users/{0}/uploads?alt=rss&max-results={1}", this.Slug, Settings.Instance.NumberOfVideosToQueryChannelFor); // the api url.
                string response = WebReader.Read(apiUrl); // read the api response.
                if (response == null) return false;

                XDocument xdoc = XDocument.Parse(response); // parse the api response.
                var entries = from item in xdoc.Descendants("item") // get the videos
                              select new
                              {
                                  GUID = item.Element("guid").Value,
                                  Title = item.Element("title").Value,
                                  Link = item.Element("link").Value
                              };

                foreach (var entry in entries) // create the video items.
                {
                    YoutubeVideo v = new YoutubeVideo(entry.Title, entry.GUID, entry.Link, this.Provider);
                    v.OnStateChange +=  OnChildStateChange;
                    this.Videos.Add(v);
                }

                return true;
            }
            catch (Exception e) { Log.Instance.Write(LogMessageTypes.Error, string.Format("VideoChannels Plugin - Youtube Channel - Update() Error: \n {0}", e)); return false; }
        }
    }
}
