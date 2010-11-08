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
        private bool disposed = false;

        public FeedsPlugin() {}

        public override void Load(PluginSettings ps)
        {
            FeedsPlugin.PluginSettings = ps;

            ListItem root = new ListItem("Feeds");  // register root item feeds
            this.RegisterListItem(root);

            this.RegisterPluginMenuItem(this, new MenuItemEventArgs("Subscriptions", new EventHandler(MenuSubscriptionsClicked)));

            XDocument xdoc = XDocument.Load("Feeds.xml");
            var entries = from feed in xdoc.Descendants("Feed")
                          select new
                          {
                              Title = feed.Element("Name").Value,
                              URL = feed.Element("URL").Value,
                          };


            foreach (var entry in entries)
            {
                Feed f = new Feed(entry.Title,entry.URL);
                this._feeds.Add(f);
            }

            int unread = 0;

            foreach (Feed feed in this._feeds)
            {
                RegisterListItem(feed,root);
                feed.Update();                
                foreach (Story story in feed.Stories)
                {
                    RegisterListItem(story, feed);
                }

                if (feed.State == ItemState.UNREAD) unread++;
            }

            if (unread > 0)
            {
                root.SetTitle(string.Format("{0} ({1})",root.Title,unread.ToString()));
            }

            PluginLoadComplete(new PluginLoadCompleteEventArgs(true));            
        }

        public void MenuSubscriptionsClicked(object sender, EventArgs e)
        {
            frmDataEditor f = new frmDataEditor("Feeds.xml", "Feed");
            f.Show();
        }

        ~FeedsPlugin() { Dispose(false); }

        private void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing) // managed resources
                {
                    FeedsPlugin.PluginSettings = null;
                    foreach (Feed f in this._feeds) { f.Dispose(); }
                    this._feeds.Clear();
                    this._feeds = null;
                }
                disposed = true;
            }
        }
    }
}
