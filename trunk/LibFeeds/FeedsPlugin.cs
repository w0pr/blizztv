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
using System.Timers;
using LibBlizzTV;
using LibBlizzTV.Utils;

namespace LibFeeds
{
    [PluginAttributes("Feeds","Feed aggregator plugin for BlizzTV","feed_16.png")]
    public class FeedsPlugin:Plugin
    {
        #region members

        private ListItem _root_item = new ListItem("Feeds");  // root item on treeview.
        private List<Feed> _feeds = new List<Feed>(); // the feeds list 
        private bool disposed = false;

        #endregion

        #region ctor

        public FeedsPlugin(GlobalSettings gs, PluginSettings ps)
            : base(gs, ps)
        {
            this.Menus.Add("subscriptions", new System.Windows.Forms.ToolStripMenuItem("Subscriptions", null, new EventHandler(MenuSubscriptionsClicked))); // register subscriptions menu.                     
        }

        #endregion

        #region API handlers

        public override void Run()
        {            
            this.RegisterListItem(this._root_item); // register root item.            
            PluginLoadComplete(new PluginLoadCompleteEventArgs(this.UpdateFeeds()));  // parse feeds.    

            // setup update timer for next data updates
            Timer update_timer = new Timer(1000 * 60 * 5);
            update_timer.Elapsed += new ElapsedEventHandler(OnTimerHit);
            update_timer.Enabled = true;
        }

        #endregion

        #region internal logic

        private bool UpdateFeeds()
        {
            bool success = true;

            this._root_item.SetTitle("Updating feeds..");
            if (this._feeds.Count > 0) this.DeleteExistingFeeds(); // clear previous entries before doing an update.

            try
            {
                XDocument xdoc = XDocument.Load("Feeds.xml"); // load the xml.
                var entries = from feed in xdoc.Descendants("Feed") // get the feeds.
                              select new
                              {
                                  Title = feed.Element("Name").Value,
                                  URL = feed.Element("URL").Value,
                              };

                foreach (var entry in entries) // create up the feed items.
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

            if (success) // if parsing of feeds.xml all okay.
            {
                int unread = 0; // feeds with unread stories count.

                foreach (Feed feed in this._feeds) // loop through feeds.
                {
                    feed.Update(); // update the feed.
                    if (feed.Valid)
                    {
                        RegisterListItem(feed, _root_item); // if the feed parsed all okay, regiser the feed-item.
                        foreach (Story story in feed.Stories) { RegisterListItem(story, feed); } // register the story items.
                        if (feed.State == ItemState.UNREAD) unread++;
                    }
                }

                this._root_item.SetTitle(string.Format("Feeds ({0})", unread.ToString()));  // add unread feeds count to root item's title.
            }

            return success;
        }

        private void DeleteExistingFeeds() // removes all current feeds.
        {
            foreach (Feed f in this._feeds) { f.Delete(); } // Delete the feeds.
            this._feeds.Clear(); // remove them from the list.
            GC.Collect();
            GC.WaitForPendingFinalizers();
        }

        private void OnTimerHit(object source, ElapsedEventArgs e)
        {
            PluginDataUpdateComplete(new PluginDataUpdateCompleteEventArgs(UpdateFeeds()));               
        }

        private void MenuSubscriptionsClicked(object sender, EventArgs e) // subscriptions menu handler
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
                    this._root_item.Dispose();
                    this._root_item = null;
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
