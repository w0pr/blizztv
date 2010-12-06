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
using BlizzTV.CommonLib.Web;
using BlizzTV.CommonLib.Logger;
using BlizzTV.ModuleLib;

namespace BlizzTV.Modules.Feeds
{
    public class Feed : ListItem
    {
        #region members
        
        private string _url; // the feed url.
        private string _name; // the feed name.   
        private bool disposed = false;

        public string Name { get { return this._name; } }
        public string URL { get { return this._url; } }

        // TODO: should be a readonly collection.
        public List<Story> Stories = new List<Story>(); // the feed's stories
       
        #endregion

        #region ctor

        public Feed(FeedSubscription subscription)
            : base(subscription.Name)
        {
            this._name = subscription.Name;
            this._url = subscription.URL;

            // register context menus.
            this.ContextMenus.Add("markallasread", new System.Windows.Forms.ToolStripMenuItem("Mark As Read", null, new EventHandler(MenuMarkAllAsReadClicked))); // mark as read menu.
            this.ContextMenus.Add("markallasunread", new System.Windows.Forms.ToolStripMenuItem("Mark As Unread", null, new EventHandler(MenuMarkAllAsUnReadClicked))); // mark as unread menu.
        }

        #endregion

        #region internal logic

        public bool IsValid()
        {
            return this.Parse();
        }

        public bool Update() 
        {
            if (this.Parse())
            {
                foreach (Story s in this.Stories) { s.CheckForNotifications(); }
                return true;
            }
            else return false;
        }

        private bool Parse()
        {
            try
            {
                string response = WebReader.Read(this.URL); // read the feed xml
                if (response == null) return false;

                XDocument xdoc = XDocument.Parse(response); // parse the xml

                var entries = from item in xdoc.Descendants("item") // get the stories
                              select new
                              {
                                  GUID = item.Element("guid").Value,
                                  Title = item.Element("title").Value,
                                  Link = item.Element("link").Value,
                                  Description = item.Element("description").Value
                              };

                foreach (var entry in entries) // create the story-item's.
                {
                    Story s = new Story(entry.Title, entry.GUID, entry.Link, entry.Description);
                    s.OnStyleChange += ChildStyleChange;
                    this.Stories.Add(s);
                }
                return true;
            }
            catch (Exception e) { Log.Instance.Write(LogMessageTypes.ERROR, string.Format("FeedsPlugin - Feed - Update() Error: \n {0}", e.ToString())); return false; }
        }

        void ChildStyleChange(ItemStyle Style)
        {
            if (this.Style == Style) return;

            int unread = 0;
            foreach (Story s in this.Stories) { if (s.Style == ItemStyle.BOLD) unread++; }
            if (unread > 0) this.Style = ItemStyle.BOLD;
            else this.Style = ItemStyle.REGULAR;
        }

        private void MenuMarkAllAsReadClicked(object sender, EventArgs e)
        {
            foreach (Story s in this.Stories) { s.Status = Story.Statutes.READ; } // marked all stories as read.
        }

        private void MenuMarkAllAsUnReadClicked(object sender, EventArgs e)
        {
            foreach (Story s in this.Stories) { s.Status = Story.Statutes.UNREAD; } // marked all stories as unread.
        }

        #endregion

        #region de-ctor

        ~Feed() { Dispose(false); }

        protected override void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing) // managed resources
                {
                    foreach (Story s in this.Stories) { s.Dispose(); }
                    this.Stories.Clear();
                    this.Stories = null;
                }
                base.Dispose(disposing);
            }
        }

        #endregion
    }
}
