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
using LibBlizzTV.Utils;

namespace LibFeeds
{
    [Plugin("Feeds","Feed aggregator plugin for BlizzTV","feed_16.png")]
    public class FeedsPlugin:Plugin
    {
        #region members

        ListItem root = new ListItem("Feeds");  // root item on treeview.
        private List<Feed> _feeds = new List<Feed>(); // the feeds list 
        private bool disposed = false;

        #endregion

        #region ctor

        public FeedsPlugin() {}

        #endregion

        #region API handlers

        public override void Load(PluginSettings ps)
        {            
            FeedsPlugin.PluginSettings = ps; 
           
            this.RegisterListItem(this.root); // register root item.
            this.RegisterPluginMenuItem(this, new NewMenuItemEventArgs("Subscriptions", new EventHandler(MenuSubscriptionsClicked))); // register subscriptions menu.           

            PluginLoadComplete(new PluginLoadCompleteEventArgs(this.ParseFeeds()));  // parse feeds.    
        }

        #endregion

        #region internal logic

        private bool ParseFeeds()
        {
            bool success = true; 

            try
            {
                XDocument xdoc = XDocument.Load("Feeds.xml"); // load the xml
                var entries = from feed in xdoc.Descendants("Feed") // get the feeds
                              select new
                              {
                                  Title = feed.Element("Name").Value,
                                  URL = feed.Element("URL").Value,
                              };

                foreach (var entry in entries) // create up the feed items
                {
                    Feed f = new Feed(entry.Title, entry.URL);
                    this._feeds.Add(f); 
                }
            }
            catch (Exception e)
            {
                success = false;
                Log.Instance.Write(LogMessageTypes.ERROR, string.Format("FeedsPlugin ParseFeeds() Error: \n {0}", e.ToString()));
                System.Windows.Forms.MessageBox.Show(string.Format("An error occured while parsing your feeds.xml. Please correct the error and re-start the plugin. \n\n[Error Details: {0}]", e.Message), "Feeds Plugin Error", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
            }

            if (success) // if parsing of feeds.xml all okay
            {
                int unread = 0; // feeds with unread storyies count

                foreach (Feed feed in this._feeds) // loop through feeds
                {
                    feed.Update(); // update the feed
                    if (feed.Valid) RegisterListItem(feed, root); // if the feed parsed all okay, regiser the feed-item.

                    foreach (Story story in feed.Stories) { RegisterListItem(story, feed); } // register the story item.
                    if (feed.State == ItemState.UNREAD) unread++;
                }

                if (unread > 0) { root.SetTitle(string.Format("{0} ({1})", root.Title, unread.ToString())); }
            }

            return success;
        }

        public void MenuSubscriptionsClicked(object sender, EventArgs e) // subscriptions menu handler
        {
            frmDataEditor f = new frmDataEditor("Feeds.xml", "Feed");
            f.Show();
        }

        #endregion

        #region de-ctor

        ~FeedsPlugin() { Dispose(false); }

        private void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing) // managed resources
                {
                    FeedsPlugin.PluginSettings = null;
                    this.root.Dispose();
                    this.root = null;
                    foreach (Feed f in this._feeds) { f.Dispose(); }
                    this._feeds.Clear();
                    this._feeds = null;
                }
                disposed = true;
            }
        }

        #endregion
    }
}
