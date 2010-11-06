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

namespace LibFeeds
{
    [Plugin("LibFeeds","Feed aggregator plugin for BlizzTV","feed_16.png")]
    public class FeedsPlugin:Plugin
    {
        private List<Feed> _feeds = new List<Feed>();

        public FeedsPlugin() {}

        public override void Load(PluginSettings ps)
        {
            FeedsPlugin.PluginSettings = ps;

            ListItem root = new ListItem("Feeds");
            RegisterListItem(root);            

            XDocument xdoc = XDocument.Load("Feeds.xml");
            var entries = from feed in xdoc.Descendants("Feed")
                          select new
                          {
                              Title = feed.Element("Name").Value,
                              URL = feed.Element("URL").Value,
                          };


            foreach (var entry in entries)
            {
                Feed f = new Feed();
                f.Title = entry.Title;
                f.URL = entry.URL;
                this._feeds.Add(f);
            }

            foreach (Feed feed in this._feeds)
            {
                RegisterListItem(feed,root);
                feed.Update();                
                foreach (Story story in feed.Stories)
                {
                    RegisterListItem(story, feed);
                }                
            }

            PluginLoadComplete(new PluginLoadCompleteEventArgs(true));            
        }
    }
}
