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
 * $Id: ListItem.cs 158 2010-11-30 14:06:50Z shalafiraistlin@gmail.com $
 */

using System;
using System.Collections.Generic;
using BlizzTV.CommonLib.Storage;

namespace BlizzTV.ModuleLib
{
    /// <summary>
    /// A list item that can be rendered on main form's treeview.
    /// </summary>
    public class ListItem : IDisposable
    {
        #region members

        private string _title; 
        private string _key;
        private string _guid; // the story-guid.
        private bool _state_tracked;
        private ItemState _state = ItemState.UNKNOWN;
        private bool disposed = false;

        /// <summary>
        /// The title.
        /// </summary>
        public string Title { get { return this._title; } }

        /// <summary>
        /// 
        /// </summary>
        public string GUID { get { return this._guid; } protected set { this._guid = value; } }

        /// <summary>
        /// The key.
        /// </summary>
        public string Key { get { return this._key; } }

        /// <summary>
        /// Context Menus for Item
        /// </summary>
        public Dictionary<string,System.Windows.Forms.ToolStripMenuItem> ContextMenus = new Dictionary<string,System.Windows.Forms.ToolStripMenuItem>();

        /// <summary>
        /// The visible list childs.
        /// </summary>
        public Dictionary<string, ListItem> Childs = new Dictionary<string, ListItem>();

        /// <summary>
        /// 
        /// </summary>
        public ItemState State
        {
            get
            {
                if (this._state_tracked)
                {
                    if (this._state == ItemState.UNKNOWN)
                    {
                        if (PersistantStorage.Instance.EntryExists("state", this._guid))
                        {
                            ItemState state = (ItemState)PersistantStorage.Instance.GetByte("state", this._guid);
                            if (state == ItemState.FRESH) return (this.State = ItemState.UNREAD);
                            else return state;
                        }
                        else return (this.State = ItemState.FRESH);
                    }
                    else return this._state;
                }
                else return this._state;
            }
            set
            {
                this._state = (ItemState)value;
                if (this._state != ItemState.ERROR)
                {
                    if(this._state_tracked) PersistantStorage.Instance.PutByte("state", this._guid, (byte)value);
                    if ((ItemState)value == ItemState.READ || (ItemState)value == ItemState.UNREAD) if (this.OnStateChange != null) this.OnStateChange((ItemState)value);
                }
            }
        }


        #endregion

        #region ctor

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Title"></param>
        /// <param name="StateTracked"></param>
        public ListItem(string Title, bool StateTracked = false) { this._title = Title; this._state_tracked=StateTracked; this.generate_unique_random_key(); } // generate an unique-random key for the item.

        #endregion

        #region The list-item API & events

        /// <summary>
        /// The double-click event handler.
        /// </summary>
        /// <remarks>Should be implemented if the item wants to be notified of double-click's.</remarks>
        /// <param name="sender">The sender object.</param>
        /// <param name="e">The event parameters.</param>
        public virtual void DoubleClicked(object sender, EventArgs e) { }

        /// <summary>
        /// Fires when an item's right clicked.
        /// </summary>
        /// <param name="sender">The sender object.</param>
        /// <param name="e">The event parameters.</param>
        public virtual void RightClicked(object sender, EventArgs e) { }


        /// <summary>
        /// Fires when a notify balloon about the item is clicked.
        /// </summary>
        /// <param name="sender">The sender object.</param>
        /// <param name="e">The event parameters.</param>
        public virtual void BalloonClicked(object sender, EventArgs e) { }

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
        /// <remarks>Can be overridden though you should still call base.SetTitle().</remarks>  
        /// <param name="Title">The new title.</param>
        public virtual void SetTitle(string Title)
        {
            this._title = Title;
            if (OnTitleChange != null) OnTitleChange(this); // notify observers.
        }

        /// <summary>
        /// 
        /// </summary>
        public delegate void StateChangedEventHandler(ItemState State);

        /// <summary>
        /// 
        /// </summary>
        public event StateChangedEventHandler OnStateChange;

        /// <summary>
        /// 
        /// </summary>
        public delegate void ShowFormEventHandler(System.Windows.Forms.Form Form, bool IsModal);

        /// <summary>
        /// 
        /// </summary>
        public event ShowFormEventHandler OnShowForm;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Form"></param>
        /// <param name="IsModal"></param>
        public void ShowForm(System.Windows.Forms.Form Form,bool IsModal=false)
        {
            if (OnShowForm != null) OnShowForm(Form,IsModal);
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

        /// <summary>
        /// Actual dispose function, can be overriden.
        /// </summary>
        /// <param name="disposing"></param>
        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing) // managed resources
                {
                    this.OnTitleChange = null;
                }
                disposed = true;
            }
        } 
        #endregion
    }

    #region item-state enum

    /// <summary>
    /// 
    /// </summary>
    public enum ItemState
    {
        /// <summary>
        /// 
        /// </summary>
        UNKNOWN,
        /// <summary>
        /// 
        /// </summary>
        FRESH,
        /// <summary>
        /// 
        /// </summary>
        UNREAD,
        /// <summary>
        /// 
        /// </summary>
        READ,
        /// <summary>
        /// 
        /// </summary>
        ERROR
    }


	#endregion
}
