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
using System.Linq;
using System.Xml.Linq;
using BlizzTV.InfraStructure.Modules;
using BlizzTV.Log;
using BlizzTV.Utility.Extensions;
using BlizzTV.Utility.Web;

namespace BlizzTV.EmbeddedModules.Videos.Parsers
{
    public class BlipTv : Channel
    {
        public BlipTv(VideoSubscription subscription) : base(subscription) { }

        public override bool Parse()
        {
            try
            {
                var apiUrl = string.Format("http://{0}.blip.tv/rss", this.Slug); // the api url.
                WebReader.Result result = WebReader.Read(apiUrl); // read the api response.
                if (result.State != WebReader.States.Success) return false;

                var xdoc = XDocument.Parse(result.Response); // parse the api response.
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
                    var video = new Video.BlipTv(this.Text, entry.Title, entry.GUID, entry.Link, this.Provider);
                    video.StateChanged += OnChildStateChanged;
                    video.VideoId = entry.VideoID;
                    this.Videos.Add(video);
                    i++;
                    if (i >= Settings.ModuleSettings.Instance.NumberOfVideosToQueryChannelFor) break;
                }
                return true;
            }
            catch (Exception e) { LogManager.Instance.Write(LogMessageTypes.Error, string.Format("Module video's BlipTV parser caught an exception: {0}", e)); return false; }
        }
    }
}
