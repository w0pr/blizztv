﻿/*    
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
using BlizzTV.CommonLib.Web;
using BlizzTV.CommonLib.Logger;

namespace BlizzTV.Modules.Videos.Handlers
{
    public class BlipTV : Channel
    {
        public BlipTV(VideoSubscription subscription) : base(subscription) { }

        public override bool Parse()
        {
            try
            {
                string api_url = string.Format("http://{0}.blip.tv/rss", this.Slug); // the api url.
                string response = WebReader.Read(api_url); // read the api response.
                if (response == null) return false;

                XDocument xdoc = XDocument.Parse(response); // parse the api response.
                XNamespace xmlns = "http://blip.tv/dtd/blip/1.0";
                var entries = from item in xdoc.Descendants("item") // get the videos
                              select new
                              {
                                  GUID = item.Element("guid").Value,
                                  Title = item.Element("title").Value,
                                  Link = item.Element("link").Value,
                                  VideoID = item.Element(xmlns + "posts_id").Value
                              };

                int i = 0;

                foreach (var entry in entries) // create the video items.
                {
                    BlipTVVideo v = new BlipTVVideo(entry.Title, entry.GUID, entry.Link, this.Provider);
                    v.OnStyleChange += OnChildStyleChange;
                    v.VideoID = entry.VideoID;
                    this.Videos.Add(v);
                    i++;
                    if (i >= Settings.Instance.NumberOfVideosToQueryChannelFor) break;
                }
                return true;
            }
            catch (Exception e) { Log.Instance.Write(LogMessageTypes.Error, string.Format("VideoChannels Plugin - Blip.TV Channel - Update() Error: \n {0}", e.ToString())); return false; }
        }
    }
}
