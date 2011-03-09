/*    
 * Copyright (C) 2010-2011, BlizzTV Project - http://code.google.com/p/blizztv/
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
using System.Windows.Forms;
using BlizzTV.Assets.i18n;
using BlizzTV.InfraStructure.Modules;
using BlizzTV.Utility.Imaging;

namespace BlizzTV.EmbeddedModules.Videos
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

            this.ContextMenus.Add("markaswatched", new ToolStripMenuItem(i18n.MarkAsWatched, Assets.Images.Icons.Png._16.read, new EventHandler(MenuMarkAllAsWatchedClicked))); 
            this.ContextMenus.Add("markasunwatched", new ToolStripMenuItem(i18n.MarkAsUnwatched, Assets.Images.Icons.Png._16.unread, new EventHandler(MenuMarkAllAsUnWatchedClicked))); 

            this.Icon = new NamedImage("video", Assets.Images.Icons.Png._16.video);
        }

        public bool IsValid()
        {
            return this.Parse();
        }

        public bool Update()
        {
            if (!this.Parse())
            {
                this.State = State.Error;
                this.Icon = new NamedImage("error", Assets.Images.Icons.Png._16.error);
                return false;
            }

            foreach (Video v in this.Videos) { v.CheckForNotifications(); }
            return true;
        }

        public virtual bool Parse() { throw new NotImplementedException(); }

        private void MenuMarkAllAsWatchedClicked(object sender, EventArgs e)
        {
            foreach (Video v in this.Videos) { v.State = State.Read; } // marked all videos as watched.
        }

        private void MenuMarkAllAsUnWatchedClicked(object sender, EventArgs e)
        {
            foreach (Video v in this.Videos) { v.State = State.Unread; } // marked all videos as unread.
        }

        protected void OnChildStateChange(object sender, EventArgs e)
        {
            if (this.State == ((Video) sender).State) return;

            int unread = this.Videos.Count(s => s.State == State.Fresh || s.State == State.Unread);
            this.State = unread > 0 ? State.Unread : State.Read;
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
