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
using BlizzTV.ModuleLib.StatusStorage;
using BlizzTV.Notifications;
using BlizzTV.Utility.Imaging;

namespace BlizzTV.ModuleLib
{
    public class ListItem : INotificationRequester, IDisposable
    {
        private string _title; 
        private State _state = State.Unknown;
        private NamedImage _icon = null;
        private bool _disposed = false;

        public string Title { get { return this._title; } }

        public string Guid { get; protected set; }

        public State State
        {
            get
            {
                if (this._state == ModuleLib.State.Unknown)
                {
                    string key = string.Format("{0}.{1}", this.GetType().ToString(), this.Guid);
                    if (string.IsNullOrEmpty(this.Guid) || !StatusStorage.StatusStorage.Instance.Exists(key)) this.State = ModuleLib.State.Fresh;
                    else
                    {
                        this._state = (State)StatusStorage.StatusStorage.Instance[key];
                        if (this._state == ModuleLib.State.Fresh) this.State = ModuleLib.State.Unread;
                        else { if (this.OnStateChange != null) this.OnStateChange(this, EventArgs.Empty); }
                    }
                }
                return this._state;
            }
            set
            {
                this._state = value;
                string key = string.Format("{0}.{1}", this.GetType().ToString(), this.Guid);
                if (!string.IsNullOrEmpty(this.Guid)) StatusStorage.StatusStorage.Instance[key] = (byte)this._state;
                if (this.OnStateChange != null) this.OnStateChange(this,EventArgs.Empty);
            }
        }
        
        public NamedImage Icon
        {
            get { return this._icon; }
            set { if (this._icon != value) this._icon = value; }
        }    

        public Dictionary<string, System.Windows.Forms.ToolStripMenuItem> ContextMenus = new Dictionary<string, System.Windows.Forms.ToolStripMenuItem>();
        public Dictionary<string, ListItem> Childs = new Dictionary<string, ListItem>();

        public ListItem(string title)
        {
            this._title = title;
        }

        public virtual void Open(object sender, EventArgs e) { }
        public virtual void RightClicked(object sender, EventArgs e) { }
        public virtual void NotificationClicked() { }

        public delegate void TitleChangedEventHandler(object sender);
        public event TitleChangedEventHandler OnTitleChange;
        public virtual void SetTitle(string title)
        {
            this._title = title;
            if (OnTitleChange != null) OnTitleChange(this); // notify observers.
        }

        public EventHandler OnStateChange;

        public delegate void ShowFormEventHandler(System.Windows.Forms.Form form, bool isModal);
        public event ShowFormEventHandler OnShowForm;
        public void ShowForm(System.Windows.Forms.Form form,bool isModal=false)
        {
            if (OnShowForm != null) OnShowForm(form,isModal);
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

    public enum State
    {
        Unknown,
        Fresh,
        Unread,
        Read,
        Error
    }
}
