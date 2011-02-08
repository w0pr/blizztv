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
using System.Collections.Generic;
using BlizzTV.Notifications;
using BlizzTV.Utility.Imaging;
using BlizzTV.Modules.StateStorage;

namespace BlizzTV.Modules
{
    /// <summary>
    /// Provides a renderable list item.
    /// </summary>
    public class ListItem : INotificationRequester, IDisposable
    {
        private State _state = State.Unknown; // the state of the item.
        private NamedImage _icon = null; // the item icon.
        private bool _disposed = false;

        /// <summary>
        /// The title.
        /// </summary>
        public string Title { get; private set; }

        /// <summary>
        /// The unique guid.
        /// </summary>
        public string Guid { get; protected set; }

        /// <summary>
        /// The item state.
        /// </summary>
        public State State
        {
            get
            {
                if (this._state == State.Unknown)
                {
                    string key = string.Format("{0}.{1}", this.GetType(), this.Guid);

                    if (string.IsNullOrEmpty(this.Guid) || !StateStorage.StateStorage.Instance.Exists(key)) this.State = State.Fresh; // if key does not exists in state-storage already, then it's just fresh.                    
                    else // if already exists on state-storage
                    {
                        this._state = (State)StateStorage.StateStorage.Instance[key]; // get the last state from storage.
                        if (this._state == State.Fresh) this.State = State.Unread; // if last state was fresh, change it unread.
                        else { if (this.OnStateChange != null) this.OnStateChange(this, EventArgs.Empty); }
                    }
                }
                return this._state;
            }
            set
            {
                this._state = value;
                string key = string.Format("{0}.{1}", this.GetType(), this.Guid);
                if (!string.IsNullOrEmpty(this.Guid)) StateStorage.StateStorage.Instance[key] = (byte)this._state; // set the new state.
                if (this.OnStateChange != null) this.OnStateChange(this,EventArgs.Empty); // notify about the state change.
            }
        }
        
        /// <summary>
        /// The icon of the item.
        /// </summary>
        public NamedImage Icon
        {
            get { return this._icon; }
            set { if (this._icon != value) this._icon = value; }
        }    

        /// <summary>
        /// Bound context-menu's to item.
        /// </summary>
        public readonly Dictionary<string, System.Windows.Forms.ToolStripMenuItem> ContextMenus = new Dictionary<string, System.Windows.Forms.ToolStripMenuItem>();

        /// <summary>
        /// Childsof the item.
        /// </summary>
        public readonly Dictionary<string, ListItem> Childs = new Dictionary<string, ListItem>();

        public ListItem(string title)
        {
            this.Title = title;
        }

        /// <summary>
        /// Executes an open command on item.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The args.</param>
        public virtual void Open(object sender, EventArgs e) { }

        /// <summary>
        /// Notifies about a right-click on item.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The args.</param>
        public virtual void RightClicked(object sender, EventArgs e) { }

        /// <summary>
        /// Notifies about a notification click of the item.
        /// </summary>
        public virtual void NotificationClicked() { }

        public delegate void TitleChangedEventHandler(object sender);
        public event TitleChangedEventHandler OnTitleChange;
        
        /// <summary>
        /// Sets the title of the item.
        /// </summary>
        /// <param name="title">The new title.</param>
        public virtual void SetTitle(string title)
        {
            this.Title = title;
            if (OnTitleChange != null) OnTitleChange(this); // Notify observers.
        }

        /// <summary>
        /// Notifies about a state change on the item.
        /// </summary>
        public EventHandler OnStateChange;

        public delegate void ShowFormEventHandler(System.Windows.Forms.Form form, bool isModal);
        public event ShowFormEventHandler OnShowForm;


        /* TODO: should be not implemented in this way.. */
        protected void ShowForm(System.Windows.Forms.Form form, bool isModal = false)
        {
            if (OnShowForm != null) OnShowForm(form, isModal);
        }

        #region de-ctor

        ~ListItem() { Dispose(false); }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (this._disposed) return;
            if (disposing) // managed resources
            {
                this.OnTitleChange = null;
                this.OnShowForm = null;
            }
            _disposed = true;
        } 
        #endregion
    }

    /// <summary>
    /// Item states.
    /// </summary>
    public enum State
    {
        Unknown,
        Fresh,
        Unread,
        Read,
        Error
    }
}
