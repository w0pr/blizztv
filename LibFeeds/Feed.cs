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
    public class Feed : ListItem
    {
        #region members

        private bool _valid = true; // did the feed parsed all okay?        
        private string _url; // the feed url
        private bool disposed = false;

        public string URL { get { return this._url; } }
        public bool Valid { get { return this._valid; } }

        // TODO: should be a readonly collection.
        public List<Story> Stories = new List<Story>(); // the feed's stories
       
        #endregion

        #region ctor

        public Feed(string Title, string URL)
            : base(Title)
        {
            this._url = URL;
        }

        #endregion

        #region internal logic

        public virtual void Update() // Updates the feed data.
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
                System.Windows.Forms.MessageBox.Show(string.Format("An error occured while updating feed. Feed URL: {0} \n\n[Error Details: {1}]",this.URL, e.Message), "Feeds Plugin Error", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
            }

            if (this._valid)
            {
                int unread = 0; // unread stories count
                foreach (Story s in this.Stories) { if (s.State == ItemState.UNREAD) unread++; }

                if (unread > 0) // if there are unread feed stories
                {
                    this.SetTitle(string.Format(" {0} ({1})", this.Title, unread.ToString()));
                    this.SetState(ItemState.UNREAD); // then mark the feed itself as unread also
                }
            }
        }

        #endregion

        #region de-ctor

        ~Feed() { Dispose(false); }

        private void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing) // managed resources
                {
                    foreach (Story s in this.Stories) { s.Dispose(); }
                    this.Stories.Clear();
                    this.Stories = null;
                }
                disposed = true;
            }
        }

        #endregion
    }
}
