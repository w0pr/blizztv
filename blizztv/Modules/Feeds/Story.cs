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
using BlizzTV.CommonLib.Utils;
using BlizzTV.ModuleLib;
using BlizzTV.ModuleLib.StatusStorage;
using BlizzTV.CommonLib.Notifications;

namespace BlizzTV.Modules.Feeds
{
    public class Story : ListItem
    {
        public string Link { get; private set; }
        
        public Story(FeedItem item)
            : base(item.Title)
        {
            this.Guid = item.Title;
            this.Link = item.Link;

            // register context menus.
            this.ContextMenus.Add("markasread", new System.Windows.Forms.ToolStripMenuItem("Mark As Read", Assets.Images.Icons.Png._16.read, new EventHandler(MenuMarkAsReadClicked))); // mark as read menu.
            this.ContextMenus.Add("markasunread", new System.Windows.Forms.ToolStripMenuItem("Mark As Unread", Assets.Images.Icons.Png._16.unread, new EventHandler(MenuMarkAsUnReadClicked))); // mark as unread menu.                            

            this.Icon = new NamedImage("story", Assets.Images.Icons.Png._16.feed);
        }

        public void CheckForNotifications()
        {
            if (Settings.Instance.NotificationsEnabled &&  this.State == ModuleLib.State.Fresh) NotificationManager.Instance.Show(this, new NotificationEventArgs(this.Title, "Click to read.", System.Windows.Forms.ToolTipIcon.Info));
        }

        public override void DoubleClicked(object sender, EventArgs e)
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
            if (this.State != ModuleLib.State.Read) this.State = ModuleLib.State.Read;  
        }

        public override void RightClicked(object sender, EventArgs e) // manage the context-menus based on our item state.
        {
            // make conditional context-menus invisible.
            this.ContextMenus["markasread"].Visible=false;
            this.ContextMenus["markasunread"].Visible=false;

            switch (this.State)
            {
                case ModuleLib.State.Fresh:
                case ModuleLib.State.Unread:
                    this.ContextMenus["markasread"].Visible = true; // make mark as read menu visible.
                    break;
                case ModuleLib.State.Read:
                    this.ContextMenus["markasunread"].Visible = true; // make mark as unread menu visible.
                    break;
            }
        }

        private void MenuMarkAsReadClicked(object sender, EventArgs e)
        {
            this.State = ModuleLib.State.Read;            
        }

        private void MenuMarkAsUnReadClicked(object sender, EventArgs e)
        {
            this.State = ModuleLib.State.Unread;
        }
    }
}
