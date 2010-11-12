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
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LibBlizzTV;

namespace LibFeeds
{
    public class Story : ListItem
    {
        #region members

        private string _guid; // the story-guid.
        private string _link; // the story-link .
        private string _description; // the story-excerpt.

        public string GUID { get { return this._guid; } }
        public string Link { get { return this._link; } }
        public string Description { get { return this._description; } }

        #endregion

        #region ctor

        public Story(string Title, string GUID, string Link, string Description)
            : base(Title)
        {
            this._guid = GUID;
            this._link = Link;
            this._description = Description;

            // check the persistent storage for if the story is read before.
            if (Plugin.Instance.Storage.KeyExists("state",this.GUID)) this.SetState((ItemState)Plugin.Instance.Storage.Get("state",this.GUID));  
            else this.SetState(ItemState.UNREAD);

            // register context menus.
            this.ContextMenus.Add("markasread",new System.Windows.Forms.ToolStripMenuItem("Mark As Read", null, new EventHandler(MenuMarkAsReadClicked))); // mark as read menu.
            this.ContextMenus.Add("markasunread", new System.Windows.Forms.ToolStripMenuItem("Mark As Unread", null, new EventHandler(MenuMarkAsUnReadClicked))); // mark as unread menu.
        }

        #endregion

        #region internal logic

        public override void SetState(ItemState State) // override setstate function so that we can commit our state to storage.
        {
            base.SetState(State); // let the base function also do it's own job.
            Plugin.Instance.Storage.Put("state",this.GUID, (byte)this.State); // commit it to persistent storage.
        }

        public override void DoubleClicked(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start(this.Link, null); // navigate to story with default web-browser.
            this.SetState(ItemState.READ); // set the story state as read.
        }

        public override void RightClicked(object sender, EventArgs e) // manage the context-menus based on our item state.
        {
            // make conditional context-menus invisible.
            this.ContextMenus["markasread"].Visible=false;
            this.ContextMenus["markasunread"].Visible=false;

            switch (this.State) // switch on the item state.
	        {
		        case ItemState.UNREAD:
                    this.ContextMenus["markasread"].Visible=true; // make mark as read menu visible.
                    break;
                case ItemState.READ:
                    this.ContextMenus["markasunread"].Visible = true; // make mark as unread menu visible.
                    break;
                case ItemState.MARKED:
                    break;
            }
        }

        private void MenuMarkAsReadClicked(object sender, EventArgs e)
        {
            this.SetState(ItemState.READ); // set the story state as read.          
        }

        private void MenuMarkAsUnReadClicked(object sender, EventArgs e)
        {
            this.SetState(ItemState.UNREAD); // set the story state as unread.          
        }

        #endregion
    }
}
