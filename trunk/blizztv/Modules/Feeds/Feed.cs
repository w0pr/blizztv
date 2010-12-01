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
 * $Id: Feed.cs 158 2010-11-30 14:06:50Z shalafiraistlin@gmail.com $
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using BlizzTV.ModuleLib;
using BlizzTV.ModuleLib.Utils;

namespace BlizzTV.Modules.Feeds
{
    public class Feed : ListItem
    {
        #region members
        
        private string _url; // the feed url.
        private string _name; // the feed name.
        private bool _valid = true; // did the feed parsed all okay?        
        private bool _commit_on_save = false; // add feed to xml file on save.
        private bool _delete_on_save = false; // remove feed from xml file on save.
        private bool disposed = false;

        public string Name { get { return this._name; } }
        public string URL { get { return this._url; } }
        public bool CommitOnSave { get { return this._commit_on_save; } set { this._commit_on_save = value; } }
        public bool DeleteOnSave { get { return this._delete_on_save; } set { this._delete_on_save = value; } }
        public bool Valid { get { return this._valid; } }

        // TODO: should be a readonly collection.
        public List<Story> Stories = new List<Story>(); // the feed's stories
       
        #endregion

        #region ctor

        public Feed(string Name, string URL)
            : base(Name)
        {
            this._name = Name;
            this._url = URL;

            // register context menus.
            this.ContextMenus.Add("markallasread", new System.Windows.Forms.ToolStripMenuItem("Mark As Read", null, new EventHandler(MenuMarkAllAsReadClicked))); // mark as read menu.
            this.ContextMenus.Add("markallasunread", new System.Windows.Forms.ToolStripMenuItem("Mark As Unread", null, new EventHandler(MenuMarkAllAsUnReadClicked))); // mark as unread menu.
        }

        #endregion

        #region internal logic

        public void Update() // Updates the feed data.
        {
            try
            {
                string response = WebReader.Read(this.URL); // read the feed xml
                if (response != null)
                {
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
                        this.Stories.Add(s);
                    }
                }
                else this._valid = false;
            }
            catch (Exception e)
            {
                this._valid = false;
                Log.Instance.Write(LogMessageTypes.ERROR, string.Format("FeedsPlugin - Feed - Update() Error: \n {0}", e.ToString()));
            }

            if (this._valid)
            {
                int unread = 0; // unread stories count
                foreach (Story s in this.Stories) 
                {
                    if (s.State == ItemState.UNREAD || s.State == ItemState.FRESH) unread++;
                }

                if (unread > 0) // if there are unread feed stories
                {
                    this.SetTitle(string.Format(" {0} ({1})", this.Title, unread.ToString()));
                    this.State = ItemState.UNREAD;
                }
            }
            else
            {
                Story error = new Story("Error parsing feed.", "", "", "");
                error.State = ItemState.ERROR;
                this.Stories.Add(error);
            }
        }

        private void MenuMarkAllAsReadClicked(object sender, EventArgs e)
        {
            foreach (Story s in this.Stories) { s.State = ItemState.READ; } // marked all stories as read.
            this.State = ItemState.UNREAD;
        }

        private void MenuMarkAllAsUnReadClicked(object sender, EventArgs e)
        {
            foreach (Story s in this.Stories) { s.State = ItemState.UNREAD; } // marked all stories as unread.
            this.State = ItemState.READ;
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
