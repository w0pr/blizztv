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
using System.Collections.Generic;
using System.Drawing;
using BlizzTV.CommonLib.Notifications;

namespace BlizzTV.ModuleLib
{
    /// <summary>
    /// A list item that can be rendered on main form's treeview.
    /// </summary>
    public class ListItem : INotificationRequester, IDisposable
    {
        #region members

        private string _title; 
        private string _key;
        private ItemStyle _style = ItemStyle.REGULAR;
        private bool disposed = false;

        /// <summary>
        /// The title.
        /// </summary>
        public string Title { get { return this._title; } }
        
        /// <summary>
        /// The key.
        /// </summary>
        public string Key { get { return this._key; } }

        public Bitmap Icon { get; protected set; }

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
        public ItemStyle Style
        {
            get
            {
                return this._style;
            }
            set
            {
                this._style = value;
                if (OnStyleChange != null) OnStyleChange(value);
            }
        }


        #endregion

        #region ctor

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Title"></param>
        /// <param name="StateTracked"></param>
        public ListItem(string Title) { this._title = Title; this.generate_unique_random_key(); } // generate an unique-random key for the item.

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
        public virtual void NotificationClicked() { }

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
        public delegate void StyleChangedEventHandler(ItemStyle Style);

        /// <summary>
        /// 
        /// </summary>
        public event StyleChangedEventHandler OnStyleChange;

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
    public enum ItemStyle
    {
        BOLD,
        REGULAR,
    }


	#endregion
}
