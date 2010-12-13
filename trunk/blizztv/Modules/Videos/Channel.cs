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
using BlizzTV.ModuleLib;

namespace BlizzTV.Modules.Videos
{
    public class Channel:ListItem
    {
        #region members

        private string _name; // the channel name.
        private string _slug; // the channel slug.
        private string _provider; // the channel provider.
        private bool disposed = false;

        public string Name { get { return this._name; } internal set { this._name = value; } }
        public string Slug { get { return this._slug; } internal set { this._slug = value; }  }
        public string Provider { get { return this._provider; } internal set { this._provider = value; } }
                
        public List<Video> Videos = new List<Video>();

        #endregion

        #region ctor

        public Channel(VideoSubscription subscription)
            : base(subscription.Name)
        {
            this._name = subscription.Name;
            this._slug = subscription.Slug;
            this._provider = subscription.Provider;         

             // register context menus.
            this.ContextMenus.Add("markallaswatched", new System.Windows.Forms.ToolStripMenuItem("Mark As Watched", null, new EventHandler(MenuMarkAllAsWatchedClicked))); // mark as read menu.
            this.ContextMenus.Add("markallasunwatched", new System.Windows.Forms.ToolStripMenuItem("Mark As Unwatched", null, new EventHandler(MenuMarkAllAsUnWatchedClicked))); // mark as unread menu.
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
                foreach (Video v in this.Videos) { v.CheckForNotifications(); }
                return true;
            }
            else return false;
        } 

        public virtual bool Parse() { throw new NotImplementedException(); }

        private void MenuMarkAllAsWatchedClicked(object sender, EventArgs e)
        {
            foreach (Video v in this.Videos) { v.Status = Video.Statutes.WATCHED; } // marked all videos as watched.
        }

        private void MenuMarkAllAsUnWatchedClicked(object sender, EventArgs e)
        {
            foreach (Video v in this.Videos) { v.Status = Video.Statutes.UNWATCHED; } // marked all videos as unread.
        }

        protected void OnChildStyleChange(ItemStyle Style)
        {
            if (this.Style == Style) return;

            int unread = 0;
            foreach (Video v in this.Videos) { if (v.Style == ItemStyle.Bold) unread++; }
            if (unread > 0) this.Style = ItemStyle.Bold;
            else this.Style = ItemStyle.Regular;
        }

        #endregion

        #region de-ctor

        protected override void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing) // managed resources
                {
                    foreach (Video v in this.Videos) { v.Dispose(); }
                    this.Videos.Clear();
                    this.Videos = null;
                }
                base.Dispose(disposing);
            }
        }

        #endregion
    }
}
