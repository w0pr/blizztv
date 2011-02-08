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
using BlizzTV.Modules;
using BlizzTV.Notifications;
using BlizzTV.Utility.Imaging;

namespace BlizzTV.Feeds
{
    public class Story : ListItem
    {
        public string FeedName { get; private set; }
        public string Link { get; private set; }
        
        public Story(string feedName, FeedItem item)
            : base(item.Title)
        {
            this.FeedName = feedName;
            this.Guid = item.Title;
            this.Link = item.Link;

            // register context menus.
            this.ContextMenus.Add("markasread", new System.Windows.Forms.ToolStripMenuItem("Mark As Read", Assets.Images.Icons.Png._16.read, new EventHandler(MenuMarkAsReadClicked))); // mark as read menu.
            this.ContextMenus.Add("markasunread", new System.Windows.Forms.ToolStripMenuItem("Mark As Unread", Assets.Images.Icons.Png._16.unread, new EventHandler(MenuMarkAsUnReadClicked))); // mark as unread menu.                            

            this.Icon = new NamedImage("story", Assets.Images.Icons.Png._16.feed);
        }

        public void CheckForNotifications()
        {
            if (Settings.Instance.NotificationsEnabled &&  this.State == State.Fresh) NotificationManager.Instance.Show(this, new NotificationEventArgs(this.Title, string.Format("A new story is available on {0}, click to open it.",this.FeedName), System.Windows.Forms.ToolTipIcon.Info));
        }

        public override void Open(object sender, EventArgs e)
        {
            this.Navigate();
        }

        public override void NotificationClicked()
        {
            this.Navigate();
        }

        private void Navigate()
        {
            System.Diagnostics.Process.Start(this.Link, null); // navigate to story with default web-browser.
            if (this.State != State.Read) this.State = State.Read;  
        }

        public override void RightClicked(object sender, EventArgs e) // manage the context-menus based on our item state.
        {
            // make conditional context-menus invisible.
            this.ContextMenus["markasread"].Visible=false;
            this.ContextMenus["markasunread"].Visible=false;

            switch (this.State)
            {
                case State.Fresh:
                case State.Unread:
                    this.ContextMenus["markasread"].Visible = true; // make mark as read menu visible.
                    break;
                case State.Read:
                    this.ContextMenus["markasunread"].Visible = true; // make mark as unread menu visible.
                    break;
            }
        }

        private void MenuMarkAsReadClicked(object sender, EventArgs e)
        {
            this.State = State.Read;            
        }

        private void MenuMarkAsUnReadClicked(object sender, EventArgs e)
        {
            this.State = State.Unread;
        }
    }
}
