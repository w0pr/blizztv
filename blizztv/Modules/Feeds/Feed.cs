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

        public List<Story> Stories = new List<Story>(); // the feed's stories -- TODO: should be a readonly collection.

        public Feed(FeedSubscription subscription)
            : base(subscription.Name)
        {
            this.Name = subscription.Name;
            this.Url = subscription.Url;

            // register context menus.
            this.ContextMenus.Add("markallasread", new System.Windows.Forms.ToolStripMenuItem("Mark As Read", null, new EventHandler(MenuMarkAllAsReadClicked))); // mark as read menu.
            this.ContextMenus.Add("markallasunread", new System.Windows.Forms.ToolStripMenuItem("Mark As Unread", null, new EventHandler(MenuMarkAllAsUnReadClicked))); // mark as unread menu.

            this.Icon = new NamedImage("feed_16", Assets.Images.Icons.Png._16.feed);
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
            List<FeedItem> items=null;
            if (FeedParser.Instance.Parse(this.Url, ref items))
            {
                foreach (FeedItem item in items)
                {
                    try
                    {
                        Story story = new Story(item);
                        story.OnStyleChange += ChildStyleChange;
                        this.Stories.Add(story);
                    }
                    catch (Exception e) { Log.Instance.Write(LogMessageTypes.Error, string.Format("Feed-Parse Error: {0}", e)); }
                }
                return true;
            }
            return false;     
        }

        void ChildStyleChange(ItemStyle style)
        {
            if (this.Style == style) return;

            int unread = this.Stories.Count(s => s.Style == ItemStyle.Bold);
            this.Style = unread > 0 ? ItemStyle.Bold : ItemStyle.Regular;
        }

        private void MenuMarkAllAsReadClicked(object sender, EventArgs e)
        {
            foreach (Story s in this.Stories) { s.Status = Story.Statutes.Read; } // marked all stories as read.
        }

        private void MenuMarkAllAsUnReadClicked(object sender, EventArgs e)
        {
            foreach (Story s in this.Stories) { s.Status = Story.Statutes.Unread; } // marked all stories as unread.
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
