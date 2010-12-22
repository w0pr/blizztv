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
        private Statutes _status = Statutes.Unknown;

        public string Link { get; private set; }
        public string Guid { get; protected set; }

        public Statutes Status
        {
            get
            {
                if (this._status == Statutes.Unknown)
                {
                    if (!StatusStorage.Instance.Exists(string.Format("story.{0}", this.Guid))) this.Status = Statutes.Fresh;
                    else
                    {
                        this._status = (Statutes)StatusStorage.Instance[string.Format("story.{0}", this.Guid)];
                        if (this._status == Statutes.Fresh) this.Status = Statutes.Unread;
                        else if (this._status == Statutes.Unread) this.Style = ItemStyle.Bold;
                    }
                }
                else
                {
                    switch (this._status)
                    {
                        case Statutes.Fresh:
                        case Statutes.Unread:
                            if (this.Style != ItemStyle.Bold) this.Style = ItemStyle.Bold;
                            break;                        
                        case Statutes.Read:
                            if (this.Style != ItemStyle.Regular) this.Style = ItemStyle.Regular;
                            break;
                    }
                }
                return this._status;
            }
            set
            {
                this._status = value;
                StatusStorage.Instance[string.Format("story.{0}", this.Guid)] = (byte)this._status;
                switch (this._status)
                {
                    case Statutes.Fresh:
                    case Statutes.Unread:
                        this.Style = ItemStyle.Bold;
                        break;
                    case Statutes.Read:
                        this.Style = ItemStyle.Regular;
                        break;
                    default:
                        break;
                }
            }
        }

        public Story(FeedItem item)
            : base(item.Title)
        {
            this.Guid = item.Title;
            this.Link = item.Link;

            // register context menus.
            this.ContextMenus.Add("markasread",new System.Windows.Forms.ToolStripMenuItem("Mark As Read", null, new EventHandler(MenuMarkAsReadClicked))); // mark as read menu.
            this.ContextMenus.Add("markasunread", new System.Windows.Forms.ToolStripMenuItem("Mark As Unread", null, new EventHandler(MenuMarkAsUnReadClicked))); // mark as unread menu.                            

            this.Icon = new NamedImage("story_16", Assets.Images.Icons.Png._16.feed);
        }

        public void CheckForNotifications()
        {
            if (this.Status == Statutes.Fresh) NotificationManager.Instance.Show(this, new NotificationEventArgs(this.Title, "Click to read.", System.Windows.Forms.ToolTipIcon.Info));
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
            if(this.Status!= Statutes.Read) this.Status = Statutes.Read;            
        }

        public override void RightClicked(object sender, EventArgs e) // manage the context-menus based on our item state.
        {
            // make conditional context-menus invisible.
            this.ContextMenus["markasread"].Visible=false;
            this.ContextMenus["markasunread"].Visible=false;

            switch (this.Status) // switch on the item state.
	        {
		        case Statutes.Fresh:
                case Statutes.Unread:
                    this.ContextMenus["markasread"].Visible=true; // make mark as read menu visible.
                    break;
                case Statutes.Read:
                    this.ContextMenus["markasunread"].Visible = true; // make mark as unread menu visible.
                    break;
            }
        }

        private void MenuMarkAsReadClicked(object sender, EventArgs e)
        {
            this.Status = Statutes.Read; // set the story state as read.
        }

        private void MenuMarkAsUnReadClicked(object sender, EventArgs e)
        {
            this.Status = Statutes.Unread; // set the story state as unread.
        }

        public enum Statutes
        {
            Unknown,
            Fresh,
            Unread,
            Read
        }
    }
}
