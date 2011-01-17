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
using BlizzTV.CommonLib.Utils;
using BlizzTV.CommonLib.Web;
using BlizzTV.CommonLib.Logger;
using BlizzTV.ModuleLib;

namespace BlizzTV.Modules.Feeds
{
    public class Feed : ListItem
    {
        private bool _disposed = false;

        public string Name { get; private set; }
        public string Url { get; private set; }

        public List<Story> Stories = new List<Story>(); 

        public Feed(FeedSubscription subscription)
            : base(subscription.Name)
        {
            this.Name = subscription.Name;
            this.Url = subscription.Url;

            // register context menus.
            this.ContextMenus.Add("markallasread", new System.Windows.Forms.ToolStripMenuItem("Mark As Read", Assets.Images.Icons.Png._16.read, new EventHandler(MenuMarkAllAsReadClicked))); // mark as read menu.
            this.ContextMenus.Add("markallasunread", new System.Windows.Forms.ToolStripMenuItem("Mark As Unread", Assets.Images.Icons.Png._16.unread, new EventHandler(MenuMarkAllAsUnReadClicked))); // mark as unread menu.

            this.Icon = new NamedImage("feed", Assets.Images.Icons.Png._16.feed);
        }

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
            return false;
        }

        private bool Parse()
        {
            List<FeedItem> items = null;
            if (!FeedParser.Instance.Parse(this.Url, ref items))
            {
                this.State = ModuleLib.State.Error;
                this.Icon = new NamedImage("error", Assets.Images.Icons.Png._16.error);
                return false;
            }

            foreach (FeedItem item in items)
            {
                try
                {
                    Story story = new Story(this.Title, item);
                    story.OnStateChange += OnChildStateChange;
                    this.Stories.Add(story);
                }
                catch (Exception e) { Log.Instance.Write(LogMessageTypes.Error, string.Format("Feed-Parse Error: {0}", e)); }
            }
            return true;
        }

        private void OnChildStateChange(object sender, EventArgs e)
        {
            if (this.State == (sender as Story).State) return;
            int unread = this.Stories.Count(s => s.State == ModuleLib.State.Fresh || s.State == ModuleLib.State.Unread);
            this.State = unread > 0 ? ModuleLib.State.Unread : ModuleLib.State.Read;
        }

        private void MenuMarkAllAsReadClicked(object sender, EventArgs e)
        {
            foreach (Story s in this.Stories) { s.State = ModuleLib.State.Read; } // marked all stories as read.
        }

        private void MenuMarkAllAsUnReadClicked(object sender, EventArgs e)
        {
            foreach (Story s in this.Stories) { s.State = ModuleLib.State.Unread; } // marked all stories as unread.
        }

        #region de-ctor

        ~Feed() { Dispose(false); }

        protected override void Dispose(bool disposing)
        {
            if (this._disposed) return;
            if (disposing) // managed resources
            {
                foreach (Story s in this.Stories) { s.Dispose(); }
                this.Stories.Clear();
                this.Stories = null;
            }
            base.Dispose(disposing);
        }

        #endregion
    }
}
