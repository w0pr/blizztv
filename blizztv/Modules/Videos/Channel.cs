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
using BlizzTV.ModuleLib;

namespace BlizzTV.Modules.Videos
{
    public class Channel:ListItem
    {
        private bool _disposed = false;

        public string Name { get; internal set; }
        public string Slug { get; internal set; }
        public string Provider { get; internal set; }

        public List<Video> Videos = new List<Video>();

        public Channel(VideoSubscription subscription)
            : base(subscription.Name)
        {
            this.Name = subscription.Name;
            this.Slug = subscription.Slug;
            this.Provider = subscription.Provider;         

             // register context menus.
            this.ContextMenus.Add("markallaswatched", new System.Windows.Forms.ToolStripMenuItem("Mark As Watched", null, new EventHandler(MenuMarkAllAsWatchedClicked))); // mark as read menu.
            this.ContextMenus.Add("markallasunwatched", new System.Windows.Forms.ToolStripMenuItem("Mark As Unwatched", null, new EventHandler(MenuMarkAllAsUnWatchedClicked))); // mark as unread menu.
        }

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
            return false;
        }

        public virtual bool Parse() { throw new NotImplementedException(); }

        private void MenuMarkAllAsWatchedClicked(object sender, EventArgs e)
        {
            foreach (Video v in this.Videos) { v.Status = Video.Statutes.Watched; } // marked all videos as watched.
        }

        private void MenuMarkAllAsUnWatchedClicked(object sender, EventArgs e)
        {
            foreach (Video v in this.Videos) { v.Status = Video.Statutes.Unwatched; } // marked all videos as unread.
        }

        protected void OnChildStyleChange(ItemStyle style)
        {
            if (this.Style == style) return;

            int unread = this.Videos.Count(v => v.Style == ItemStyle.Bold);
            this.Style = unread > 0 ? ItemStyle.Bold : ItemStyle.Regular;
        }

        #region de-ctor

        protected override void Dispose(bool disposing)
        {
            if (this._disposed) return;
            if (disposing) // managed resources
            {
                foreach (Video v in this.Videos) { v.Dispose(); }
                this.Videos.Clear();
                this.Videos = null;
            }
            base.Dispose(disposing);
        }

        #endregion
    }
}
