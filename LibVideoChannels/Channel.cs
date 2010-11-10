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
using LibBlizzTV;
using LibBlizzTV.Utils;

namespace LibVideoChannels
{
    public class Channel:ListItem
    {
        #region members

        private bool _valid = true; // did the feed parsed all okay?
        private string _slug; // the channel slug
        private string _provider; // the channel provider
        private bool disposed = false;

        public bool Valid { get { return this._valid; } }
        public string Slug { get { return this._slug; } }
        public string Provider { get { return this._provider; } }
                
        public List<Video> Videos = new List<Video>();

        #endregion

        #region ctor

        public Channel(string Title, string Slug, String Provider)
            : base(Title)
        {
            this._slug = Slug;
            this._provider = Provider;
        }

        #endregion

        #region internal logic

        public void Update() // Update the channel data.
        {
            try
            {
                string api_url = string.Format(@"http://gdata.youtube.com/feeds/api/users/{0}/uploads?alt=rss&max-results=5", this.Slug); // the api url.
                string response = WebReader.Read(api_url); // read the api response.
                if (response != null)
                {
                    XDocument xdoc = XDocument.Parse(response); // parse the api response.
                    var entries = from item in xdoc.Descendants("item") // get the videos
                                  select new
                                  {
                                      GUID = item.Element("guid").Value,
                                      Title = item.Element("title").Value,
                                      Link = item.Element("link").Value
                                  };

                    foreach (var entry in entries) // create the video items.
                    {
                        Video v = new Video(entry.Title, entry.GUID, entry.Link, this.Provider);
                        this.Videos.Add(v);
                    }
                }
                else this._valid = false;
            }
            catch (Exception e)
            {
                this._valid = false;
                Log.Instance.Write(LogMessageTypes.ERROR, string.Format("VideoChannels Plugin - Channel - Update() Error: \n {0}", e.ToString()));
                System.Windows.Forms.MessageBox.Show(string.Format("An error occured while updating video channel. Channel Name: {0} \n\n[Error Details: {1}]", this.Slug, e.Message), "Video Channels Plugin Error", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
            }

            if (this._valid)
            {
                int unread = 0; // non-watched videos count.
                foreach (Video v in this.Videos) { if (v.State == ItemState.UNREAD) unread++; }

                if (unread > 0) // if there non-watched channel videos.
                {
                    this.SetTitle(string.Format("{0} ({1})", this.Title, unread.ToString()));
                    this.SetState(ItemState.UNREAD); // then mark the channel itself as unread also
                }
            }
        }

        #endregion

        #region de-ctor

        ~Channel() { Dispose(false); }

        private void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing) // managed resources
                {
                    foreach (Video v in this.Videos) { v.Dispose(); }
                    this.Videos.Clear();
                    this.Videos = null;
                }
                disposed = true;
            }
        }

        #endregion
    }
}
