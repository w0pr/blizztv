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
using BlizzTV.Utility.Extensions;
using BlizzTV.Utility.Imaging;

namespace BlizzTV.InfraStructure.Modules
{
    public class ModuleNode : TreeNode, IDisposable
    {
        private bool _disposed = false;
        private NodeState _state = NodeState.Unknown; // the state of the item.

        public ModuleNode(string text) : base(text)
        {
            this.RememberState = false;
        }

        /// <summary>
        /// The unique guid.
        /// </summary>
        public string Guid { get; protected set; }

        /// <summary>
        /// The icon of the item.
        /// </summary>
        public NamedImage Icon { get; set; }

        /// <summary>
        /// Bound context-menu's to item.
        /// </summary>
        public readonly Dictionary<string, ToolStripMenuItem> Menu = new Dictionary<string, ToolStripMenuItem>();

        /// <summary>
        /// Tells the node to remember contained item's last known-state.
        /// </summary>
        protected bool RememberState { get; set; }

        public NodeState GetState()
        {
            if (this._state != NodeState.Unknown) return this._state;

            string key = string.Format("{0}.{1}", this.GetType(), this.Guid);
            if (!this.RememberState || !StateStorage.Instance.Exists(key)) this.SetState(NodeState.Fresh);
            else
            {
                this.SetState((NodeState) StateStorage.Instance[key]);
                if (this._state == NodeState.Fresh) this.SetState(NodeState.Unread);
                else this.OnStateChanged();
            }
            return this._state;
        }

        public void SetState(NodeState state)
        {
            this._state = state;            

            if (this.RememberState)
            {
                string key = string.Format("{0}.{1}", this.GetType(), this.Guid);
                StateStorage.Instance[key] = (byte)this._state;
            }

            this.OnStateChanged();
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

            Module.UITreeView.AsyncInvokeHandler(() =>
            {
                string iconKey = this.Icon.Name;
                Bitmap image = this.Icon.Image;

                if (this._state == NodeState.Read)
                {
                    iconKey += "GrayScaled";
                    image = image.GrayScale();
                }

                if (!Module.UITreeView.ImageList.Images.ContainsKey(iconKey)) Module.UITreeView.ImageList.Images.Add(iconKey, image);
                this.ImageIndex = this.SelectedImageIndex = Module.UITreeView.ImageList.Images.IndexOfKey(iconKey);
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

        ~ModuleNode() { Dispose(false); }

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
                this.StateChanged = null;
            }
            _disposed = true;
        } 
        #endregion
    }

    /// <summary>
    /// Item states.
    /// </summary>
    public enum NodeState
    {
        Unknown,
        Fresh,
        Unread,
        Read,
        Error
    }
}
