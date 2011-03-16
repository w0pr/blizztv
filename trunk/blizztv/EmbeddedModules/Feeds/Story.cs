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
using System.Windows.Forms;
using BlizzTV.EmbeddedModules.Feeds.Parsers;
using BlizzTV.InfraStructure.Modules;
using BlizzTV.Notifications;
using BlizzTV.Utility.Imaging;
using BlizzTV.Assets.i18n;

namespace BlizzTV.EmbeddedModules.Feeds
{
    /// <summary>
    /// Feed story.
    /// </summary>
    public class Story : ModuleNode
    {
        public string FeedName { get; private set; }
        public string Link { get; private set; }
        
        public Story(string feedName, FeedItem item)
            : base(item.Title)
        {            
            this.FeedName = feedName;
            this.Guid = item.Id;
            this.Link = item.Link;

            this.Icon = new NodeIcon("story", Assets.Images.Icons.Png._16.feed);
            this.RememberState = true;

            this.Menu.Add("markasread", new ToolStripMenuItem(i18n.MarkAsRead, Assets.Images.Icons.Png._16.read, new EventHandler(MenuMarkAsReadClicked)));
            this.Menu.Add("markasunread", new ToolStripMenuItem(i18n.MarkAsUnread, Assets.Images.Icons.Png._16.unread, new EventHandler(MenuMarkAsUnReadClicked)));             
        }

        public void CheckForNotifications()
        {
            if (Settings.ModuleSettings.Instance.NotificationsEnabled &&  this.State == State.Fresh) NotificationManager.Instance.Show(this, new NotificationEventArgs(this.Text, string.Format("A new story is available on {0}, click to open it.",this.FeedName), System.Windows.Forms.ToolTipIcon.Info));
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
            this.Menu["markasread"].Enabled = false;
            this.Menu["markasunread"].Enabled = false;

            switch (this.State)
            {
                case State.Fresh:
                case State.Unread:
                    this.Menu["markasread"].Enabled = true;
                    break;
                case State.Read:
                    this.Menu["markasunread"].Enabled = true;
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
