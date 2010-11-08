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
using System.Drawing;

namespace LibBlizzTV
{
    public class ListItem : IDisposable
    {
        private string _title; 
        private string _key;
        private ItemState _state = ItemState.READ;
        private bool disposed = false;

        public string Title { get { return this._title; } }
        public string Key { get { return this._key; } }
        public ItemState State { get { return this._state; } }

        public ListItem() { this.generate_random_key(); }
        public ListItem(string Title) { this._title = Title; this.generate_random_key(); }

        public virtual void DoubleClick(object sender, EventArgs e) {}
        public virtual void UpdateState() { }

        private void generate_random_key() { this._key = System.Convert.ToBase64String(Guid.NewGuid().ToByteArray()); }

        public delegate void TitleChangedEventHandler(object sender);
        public event TitleChangedEventHandler OnTitleChange;

        public void SetTitle(string Title)
        {
            this._title = Title;
            if (OnTitleChange != null) OnTitleChange(this);
        }

        public delegate void StateChangedEventHandler(object sender);
        public event StateChangedEventHandler OnStateChange;

        protected void SetState(ItemState State)
        {
            this._state = State;
            if (OnStateChange != null) OnStateChange(this);
        }

        /* context menu */
        public delegate void RegisterContextMenuItemEventHandler(object sender, MenuItemEventArgs e);
        public event RegisterContextMenuItemEventHandler OnRegisterContextMenuItem;

        protected void RegisterContextMenuItem(object sender, MenuItemEventArgs e)
        {
            if (OnRegisterContextMenuItem != null) OnRegisterContextMenuItem(this, e);
        }


        ~ListItem() { Dispose(false); }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        private void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing) // managed resources
                {
                    this.OnTitleChange = null;
                    this.OnStateChange = null;
                }
                disposed = true;
            }
        }
    }

    public enum ItemState
    {
        UNREAD,
        READ,
        MARKED
    }
}
