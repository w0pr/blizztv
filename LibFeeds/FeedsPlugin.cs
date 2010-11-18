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
using System.Xml.XPath;
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
        internal Dictionary<string,Feed> _feeds = new Dictionary<string,Feed>(); // the feeds list 
        private Timer _update_timer;
        private bool disposed = false;

        public static FeedsPlugin Instance;

        #endregion

        #region ctor

        public FeedsPlugin(PluginSettings ps)
            : base(ps)
        {
            FeedsPlugin.Instance = this;
        }

        #endregion

        #region API handlers

        public override void Run()
        {            
            this.RegisterListItem(this._root_item); // register root item.            
            PluginLoadComplete(new PluginLoadCompleteEventArgs(this.UpdateFeeds()));  // parse feeds.    

            // setup update timer for next data updates
            _update_timer = new Timer((Settings as Settings).UpdateEveryXMinutes * 60000);
            _update_timer.Elapsed += new ElapsedEventHandler(OnTimerHit);
            _update_timer.Enabled = true;
        }

        public override System.Windows.Forms.Form GetPreferencesForm()
        {
            return new frmSettings();
        }

        #endregion

        #region internal logic

        internal bool UpdateFeeds()
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
                                  Title = feed.Attribute("Name").Value,
                                  URL = feed.Element("URL").Value,
                              };

                foreach (var entry in entries) // create up the feed items.
                {
                    Feed f = new Feed(entry.Title, entry.URL);
                    this._feeds.Add(f.Name, f);
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

                this.AddWorkload(this._feeds.Count);

                foreach (KeyValuePair<string,Feed> pair in this._feeds) // loop through feeds.
                {
                    pair.Value.Update(); // update the feed.
                    RegisterListItem(pair.Value, _root_item); // if the feed parsed all okay, regiser the feed-item.
                    foreach (Story story in pair.Value.Stories) { RegisterListItem(story, pair.Value); } // register the story items.
                    if (pair.Value.State == ItemState.UNREAD) unread++;
                    this.StepWorkload();
                }

                this._root_item.SetTitle(string.Format("Feeds ({0})", unread.ToString()));  // add unread feeds count to root item's title.
            }

            return success;
        }

        internal void SaveFeedsXML()
        {
            try
            {
                foreach (KeyValuePair<string,Feed> pair in this._feeds)
                {
                    if (pair.Value.CommitOnSave)
                    {
                        XDocument xdoc = XDocument.Load("Feeds.xml");
                        xdoc.Element("Feeds").Add(new XElement("Feed", new XAttribute("Name", pair.Value.Name), new XElement("URL", pair.Value.URL)));
                        xdoc.Save("Feeds.xml");
                    }
                    else if (pair.Value.DeleteOnSave)
                    {
                        XDocument xdoc = XDocument.Load("Feeds.xml");
                        xdoc.XPathSelectElement(string.Format("Feeds/Feed[@Name='{0}']", pair.Value.Name)).Remove();
                        xdoc.Save("Feeds.xml");
                    }
                }
            }
            catch (Exception e)
            {
                Log.Instance.Write(LogMessageTypes.ERROR, string.Format("FeedsPlugin SaveFeedsXML() Error: \n {0}", e.ToString()));
            }
        }

        private void DeleteExistingFeeds() // removes all current feeds.
        {
            foreach (KeyValuePair<string,Feed> pair in this._feeds) { pair.Value.Delete(); } // Delete the feeds.
            this._feeds.Clear(); // remove them from the list.
            GC.Collect();
            GC.WaitForPendingFinalizers();
        }

        private void OnTimerHit(object source, ElapsedEventArgs e)
        {
            PluginDataUpdateComplete(new PluginDataUpdateCompleteEventArgs(UpdateFeeds()));               
        }

        #endregion

        #region de-ctor

        protected override void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing) // managed resources
                {
                    this._update_timer.Enabled = false;
                    this._update_timer.Elapsed -= OnTimerHit;
                    this._update_timer.Dispose();
                    this._update_timer = null;
                    this._root_item.Dispose();
                    this._root_item = null;
                    foreach (KeyValuePair<string,Feed> pair in this._feeds) { pair.Value.Dispose(); }
                    this._feeds.Clear();
                    this._feeds = null;
                }
                base.Dispose(disposing);
            }            
        }

        #endregion
    }
}
