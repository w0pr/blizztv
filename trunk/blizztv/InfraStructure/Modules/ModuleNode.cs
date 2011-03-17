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
 * $Id: Feed.cs 466 2011-03-11 14:59:17Z shalafiraistlin@gmail.com $
 */

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using BlizzTV.InfraStructure.Modules.Storage;
using BlizzTV.Notifications;
using BlizzTV.Utility.Extensions;
using BlizzTV.Utility.Imaging;

namespace BlizzTV.InfraStructure.Modules
{
    public class ModuleNode : TreeNode, INotificationRequester, IDisposable
    {
        private State _state = State.Unknown; // the state of the item.
        private NodeIcon _icon; // the icon.
        private bool _disposed = false;

        /// <summary>
        /// The unique guid.
        /// </summary>
        public string Guid { get; protected set; }

        /// <summary>
        /// Tells the node to remember contained item's last known-state.
        /// </summary>
        protected bool RememberState { private get; set; }

        /// <summary>
        /// Bound context-menu's to item.
        /// </summary>
        public readonly Dictionary<string, ToolStripMenuItem> Menu = new Dictionary<string, ToolStripMenuItem>();

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

                    if (!this.RememberState || !StateStorage.Instance.Exists(key)) this.State = State.Fresh; // if key does not exists in state-storage already, then it's just fresh.                    
                    else // if already exists on state-storage
                    {
                        this._state = (State)StateStorage.Instance[key]; // get the last state from storage.
                        if (this._state == State.Fresh) this.State = State.Unread; // if last state was fresh, change it unread.
                        else { this.OnStateChanged(); }
                    }
                }
                return this._state;
            }
            set
            {
                this._state = value;
                if (this.RememberState) StateStorage.Instance[string.Format("{0}.{1}", this.GetType(), this.Guid)] = (byte)this._state; // set the new state.
                this.OnStateChanged(); // notify about the state change.
            }
        }

        /// <summary>
        /// The icon of the item.
        /// </summary>
        public NodeIcon Icon {
            get { return this._icon; }
            set
            {
                this._icon = value;
                if (value == null) return;
                Module.UITreeView.AsyncInvokeHandler(() =>
                {
                    if (!Module.UITreeView.ImageList.Images.ContainsKey(this._icon.Key)) Module.UITreeView.ImageList.Images.Add(this._icon.Key, this._icon.Image);
                    this.ImageIndex = this.SelectedImageIndex = Module.UITreeView.ImageList.Images.IndexOfKey(this._icon.Key);
                });
            } 
        }

        public ModuleNode(string text)
            : base(text)
        {
            this.RememberState = false; 
        }

        /// <summary>
        /// Notifies observers about a state change.
        /// </summary>
        public EventHandler<EventArgs> StateChanged;

        /// <summary>
        /// Lets item to notify its observers about a state change.
        /// </summary>
        /// <param name="e"></param>
        private void OnStateChanged()
        {
            EventHandler<EventArgs> handler = StateChanged;
            if (handler != null) handler(this, EventArgs.Empty);

            if (this.Icon == null) return;

            this.Icon.Mode = this._state == State.Read ? NodeIcon.ImageMode.GrayScaled : NodeIcon.ImageMode.Normal;

            Module.UITreeView.AsyncInvokeHandler(() =>
            {
                if (!Module.UITreeView.ImageList.Images.ContainsKey(this.Icon.Key)) Module.UITreeView.ImageList.Images.Add(this.Icon.Key, this.Icon.Image);
                this.ImageIndex = this.SelectedImageIndex = Module.UITreeView.ImageList.Images.IndexOfKey(this.Icon.Key);
            });
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

        #region de-ctor

        // IDisposable pattern: http://msdn.microsoft.com/en-us/library/fs2xkftw%28VS.80%29.aspx

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this); // Take object out the finalization queue to prevent finalization code for it from executing a second time.
        }

        protected virtual void Dispose(bool disposing)
        {
            if (this._disposed) return; // if already disposed, just return.

            if (disposing) // only dispose managed resources if we're called from directly or in-directly from user code.
            {
                this.StateChanged = null;
                this.Icon.Dispose();
                this.Icon = null;
            }

            this._disposed = true;
        }

        ~ModuleNode() { Dispose(false); } // finalizer called by the runtime. we should only dispose unmanaged objects and should NOT reference managed ones.
        
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
