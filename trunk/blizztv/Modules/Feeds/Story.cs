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
using BlizzTV.ModuleLib;
using BlizzTV.ModuleLib.StatusStorage;
using BlizzTV.ModuleLib.Notifications;

namespace BlizzTV.Modules.Feeds
{
    public class Story : ListItem
    {
        #region members

        private string _link; // the story-link .
        private string _description; // the story-excerpt.
        private string _guid; // the story-guid.
        private Statutes _status = Statutes.UNKNOWN;

        public string Link { get { return this._link; } }
        public string Description { get { return this._description; } }
        public string GUID { get { return this._guid; } protected set { this._guid = value; } }

        public Statutes Status
        {
            get
            {
                if (this._status == Statutes.UNKNOWN)
                {
                    if (!StatusStorage.Instance.Exists(string.Format("story.{0}", this.GUID))) this.Status = Statutes.FRESH;
                    else
                    {
                        this._status = (Statutes)StatusStorage.Instance[string.Format("story.{0}", this.GUID)];
                        if (this._status == Statutes.FRESH) this.Status = Statutes.UNREAD;
                    }
                }
                return this._status;
            }
            set
            {
                this._status = value;
                StatusStorage.Instance[string.Format("story.{0}", this.GUID)] = (byte)this._status;
                switch (this._status)
                {
                    case Statutes.FRESH:
                    case Statutes.UNREAD:
                        this.Style = ItemStyle.BOLD;
                        break;
                    case Statutes.READ:
                        this.Style = ItemStyle.REGULAR;
                        break;
                    default:
                        break;
                }
            }
        }

        #endregion

        #region ctor

        public Story(string Title, string GUID, string Link, string Description)
            : base(Title)
        {
            this.GUID = GUID;
            this._link = Link;
            this._description = Description;

            // register context menus.
            this.ContextMenus.Add("markasread",new System.Windows.Forms.ToolStripMenuItem("Mark As Read", null, new EventHandler(MenuMarkAsReadClicked))); // mark as read menu.
            this.ContextMenus.Add("markasunread", new System.Windows.Forms.ToolStripMenuItem("Mark As Unread", null, new EventHandler(MenuMarkAsUnReadClicked))); // mark as unread menu.                

            if(this.Status== Statutes.FRESH) Notifications.Instance.Show(this, this.Title, "Click to read.", System.Windows.Forms.ToolTipIcon.Info);
        }        

        #endregion

        #region internal logic

        public override void DoubleClicked(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start(this.Link, null); // navigate to story with default web-browser.
            this.Status = Statutes.READ;
        }

        public override void BalloonClicked(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start(this.Link, null); // navigate to story with default web-browser.
            this.Status = Statutes.READ;
        }

        public override void RightClicked(object sender, EventArgs e) // manage the context-menus based on our item state.
        {
            // make conditional context-menus invisible.
            this.ContextMenus["markasread"].Visible=false;
            this.ContextMenus["markasunread"].Visible=false;

            switch (this.Status) // switch on the item state.
	        {
		        case Statutes.FRESH:
                case Statutes.UNREAD:
                    this.ContextMenus["markasread"].Visible=true; // make mark as read menu visible.
                    break;
                case Statutes.READ:
                    this.ContextMenus["markasunread"].Visible = true; // make mark as unread menu visible.
                    break;
            }
        }

        private void MenuMarkAsReadClicked(object sender, EventArgs e)
        {
            this.Status = Statutes.READ; // set the story state as read.
        }

        private void MenuMarkAsUnReadClicked(object sender, EventArgs e)
        {
            this.Status = Statutes.UNREAD; // set the story state as unread.
        }

        #endregion

        public enum Statutes
        {
            UNKNOWN,
            FRESH,
            UNREAD,
            READ
        }
    }
}
