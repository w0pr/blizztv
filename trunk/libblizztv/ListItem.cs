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
    /// <summary>
    /// A list item that can be rendered on main form's treeview.
    /// </summary>
    public class ListItem : IDisposable
    {
        #region members

        private string _title; 
        private string _key;
        private ItemState _state = ItemState.READ;
        private bool disposed = false;

        /// <summary>
        /// The title.
        /// </summary>
        public string Title { get { return this._title; } }
        /// <summary>
        /// The key.
        /// </summary>
        public string Key { get { return this._key; } }
        /// <summary>
        /// The state.
        /// </summary>
        public ItemState State { get { return this._state; } }

        #endregion

        #region ctor

        /// <summary>
        /// Constructs a new list item with given title.
        /// </summary>
        /// <param name="Title">The title.</param>
        public ListItem(string Title) { this._title = Title; this.generate_unique_random_key(); } // generate an unique-random key for the item.

        #endregion

        #region The list-item API & events

        /// <summary>
        /// The double-click event handler.
        /// </summary>
        /// <remarks>Should be implemented if the item wants to be notified of double-click's.</remarks>
        /// <param name="sender">The ender object.</param>
        /// <param name="e">The event paremeters.</param>
        public virtual void DoubleClick(object sender, EventArgs e) { }

        /// <summary>
        /// Title change event handler delegate.
        /// </summary>
        /// <param name="sender">The sender object.</param>
        public delegate void TitleChangedEventHandler(object sender);
        /// <summary>
        /// Title change event handler
        /// </summary>
        public event TitleChangedEventHandler OnTitleChange;
        /// <summary>
        /// Set's title of the item.
        /// </summary>
        /// <param name="Title">The new title.</param>
        public void SetTitle(string Title)
        {
            this._title = Title;
            if (OnTitleChange != null) OnTitleChange(this); // notify observers.
        }

        /// <summary>
        /// State change event handler delegate.
        /// </summary>
        /// <param name="sender">The sender object.</param>
        public delegate void StateChangedEventHandler(object sender);
        /// <summary>
        /// State change event handler.
        /// </summary>
        public event StateChangedEventHandler OnStateChange;
        /// <summary>
        /// Set's state of the item.
        /// </summary>
        /// <param name="State">The new state.</param>
        protected void SetState(ItemState State)
        {
            this._state = State;
            if (OnStateChange != null) OnStateChange(this); // notify observers.
        }

        /// <summary>
        /// RegisterContextMenuItem event handler delegate.
        /// </summary>
        /// <param name="sender">The sender object.</param>
        /// <param name="e"><see cref="NewMenuItemEventArgs"/></param>
        public delegate void RegisterContextMenuItemEventHandler(object sender, NewMenuItemEventArgs e);
        /// <summary>
        /// RegisterContextMenuItem event handler.
        /// </summary>
        public event RegisterContextMenuItemEventHandler OnRegisterContextMenuItem;
        /// <summary>
        /// Registers a new context-menu for the item.
        /// </summary>
        /// <param name="sender">The sender object.</param>
        /// <param name="e"><see cref="NewMenuItemEventArgs"/></param>
        protected void RegisterContextMenuItem(object sender, NewMenuItemEventArgs e)
        {
            if (OnRegisterContextMenuItem != null) OnRegisterContextMenuItem(this, e); // notify observers.
        }
        
        #endregion

        #region internal logic

        private void generate_unique_random_key() { this._key = System.Convert.ToBase64String(Guid.NewGuid().ToByteArray()); } // generates an unique-random key for the item.
        
        #endregion

        #region de-ctor

        /// <summary>
        /// De-constructor.
        /// </summary>
        ~ListItem() { Dispose(false); }

        /// <summary>
        /// Disposes the object.
        /// </summary>
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
        #endregion
    }

    #region item-state enum
		
    /// <summary>
    /// Available item states.
    /// </summary>
    public enum ItemState
    {
        /// <summary>
        /// Unread or non-viewed item.
        /// </summary>
        UNREAD,
        /// <summary>
        /// Read or vieweed item.
        /// </summary>
        READ,
        /// <summary>
        /// Marked item.
        /// </summary>
        MARKED
    } 

	#endregion
}
