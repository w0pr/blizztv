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
    public class ListItem : INotificationRequester, IDisposable
    {
        private string _title; 
        private string _key;
        private ItemStyle _style = ItemStyle.Regular;
        private bool _disposed = false;

        public string Title { get { return this._title; } }        
        public string Key { get { return this._key; } }
        public Bitmap Icon { get; protected set; }

        public Dictionary<string,System.Windows.Forms.ToolStripMenuItem> ContextMenus = new Dictionary<string,System.Windows.Forms.ToolStripMenuItem>();
        public Dictionary<string, ListItem> Childs = new Dictionary<string, ListItem>();

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

        public ListItem(string title) { this._title = title; this.GenerateUniqueRandomKey(); } // generate an unique-random key for the item.

        public virtual void DoubleClicked(object sender, EventArgs e) { }
        public virtual void RightClicked(object sender, EventArgs e) { }
        public virtual void NotificationClicked() { }

        public delegate void TitleChangedEventHandler(object sender);
        public event TitleChangedEventHandler OnTitleChange;
        public virtual void SetTitle(string title)
        {
            this._title = title;
            if (OnTitleChange != null) OnTitleChange(this); // notify observers.
        }

        public delegate void StyleChangedEventHandler(ItemStyle style);
        public event StyleChangedEventHandler OnStyleChange;

        public delegate void ShowFormEventHandler(System.Windows.Forms.Form form, bool isModal);
        public event ShowFormEventHandler OnShowForm;
        public void ShowForm(System.Windows.Forms.Form form,bool isModal=false)
        {
            if (OnShowForm != null) OnShowForm(form,isModal);
        }


        private void GenerateUniqueRandomKey() { this._key = Convert.ToBase64String(Guid.NewGuid().ToByteArray()); } // generates an unique-random key for the item.
        
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
                this.OnStyleChange = null;
                this.OnShowForm = null;
            }
            _disposed = true;
        } 
        #endregion
    }

    public enum ItemStyle
    {
        Bold,
        Regular,
    }
}
