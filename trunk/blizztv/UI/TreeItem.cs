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
using System.Windows.Forms;
using System.Drawing;
using BlizzTV.ModuleLib;
using BlizzTV.Utility.Extensions;

namespace BlizzTV.UI
{
    public class TreeItem:TreeNode
    {
        private ListItem _item; // the module-item bound to.
        private Module _plugin; // the module itself.        
        private bool _disposed = false;

        public ListItem Item { get { return this._item; } }
        public Module Plugin { get { return this._plugin; } }

        public TreeItem(Module plugin, ListItem item)
        {
            this._plugin = plugin;
            this._item = item;
            this.Name = _item.Guid;

            this._item.OnTitleChange += OnTitleChange; // the title-change event.
            this._item.OnStateChange += OnStateChange;
            this._item.OnShowForm += OnShowForm; // the show-form request.
        }

        public void Render() // renders the item with title, state and icon information
        {
            this.Text = this._item.Title; // set the inital title
            this.OnStateChange(this, EventArgs.Empty);
        }

        public void Open(object sender, EventArgs e)
        {
            this._item.Open(sender, e); // notify the item about the double-click event.
        }

        public void RightClicked(object sender, TreeNodeMouseClickEventArgs e)
        {
            this._item.RightClicked(sender, e); // notify the item about the click event.
        }

        private void OnTitleChange(object sender)
        {
            this.TreeView.AsyncInvokeHandler(() =>
            {
                this.Text = this._item.Title;
            });
        }

        private void OnStateChange(object sender, EventArgs e)
        {
            if (this._item.Icon != null)
            {
                this.TreeView.AsyncInvokeHandler(() =>
                {
                    string imageKey = this.Item.Icon.Name;
                    Bitmap image = this.Item.Icon.Image;

                    if (this.Item.State == State.Read)
                    {
                        imageKey += "GrayScale";
                        image = image.GrayScale();
                    }
    
                    if (!this.TreeView.ImageList.Images.ContainsKey(imageKey)) this.TreeView.ImageList.Images.Add(imageKey, image); // add the item image to imagelist if not exists already.         
                    this.ImageIndex = this.SelectedImageIndex = this.TreeView.ImageList.Images.IndexOfKey(imageKey); // use the item icon.
                });
            }
        }

        private void OnShowForm(Form form, bool isModal)
        {
            this.TreeView.AsyncInvokeHandler(() =>
            {
                if (isModal) form.ShowDialog();
                else form.Show();
            });
        }

        #region de-ctor

        ~TreeItem() { Dispose(false); }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        private void Dispose(bool disposing)
        {
            if (!this._disposed)
            {
                if (disposing) // managed resources
                {
                    this._item.OnTitleChange -= OnTitleChange;
                    this._item = null;
                    this._plugin = null;
                }
                _disposed = true;
            }
        }

        #endregion
    }
}
