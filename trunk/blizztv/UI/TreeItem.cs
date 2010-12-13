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
using BlizzTV.CommonLib.UI;
using BlizzTV.ModuleLib;

namespace BlizzTV.UI
{
    public class TreeItem:TreeNode
    {
        private static readonly Font Bold = new Font("Tahoma", 9, FontStyle.Bold); // font for unread items.
        private static readonly Font Regular = new Font("Tahoma", 9, FontStyle.Regular); // font for read items.       

        private ListItem _item; // the module-item bound to.
        private Module _plugin; // the module itself.        
        private bool _disposed = false;

        public ListItem Item { get { return this._item; } }
        public Module Plugin { get { return this._plugin; } }

        public TreeItem(Module plugin, ListItem item)
        {
            this._plugin = plugin;
            this._item = item;
            this.Name = _item.Key;

            this._item.OnTitleChange += OnTitleChange; // the title-change event.
            this._item.OnStyleChange += OnStyleChange; // the style-change event.
            this._item.OnShowForm += OnShowForm; // the show-form request.
        }

        public void Render() // renders the item with title, state and icon information
        {
            this.Text = this._item.Title; // set the inital title
            this.OnStyleChange(this._item.Style);

            // set the node icon
            if (!this.TreeView.ImageList.Images.ContainsKey(this._plugin.Attributes.Name)) this.TreeView.ImageList.Images.Add(this._plugin.Attributes.Name, this._plugin.Attributes.Icon); // add the plugin icon to image list in it doesn't exists yet.
            this.ImageIndex = this.TreeView.ImageList.Images.IndexOfKey(this._plugin.Attributes.Name); // use the item's plugin icon.
        }

        public void DoubleClicked(object sender, TreeNodeMouseClickEventArgs e)
        {
            this._item.DoubleClicked(sender, e); // notify the item about the double-click event.
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

        private void OnStyleChange(ItemStyle style)
        {
            this.TreeView.AsyncInvokeHandler(() =>
                {
                    switch (style)
                    {
                        case ItemStyle.Bold:
                            this.NodeFont = TreeItem.Bold;                            
                            break;
                        case ItemStyle.Regular:
                            this.NodeFont = TreeItem.Regular;
                            break;
                    }
                });
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
                    this._item.OnStyleChange -= OnStyleChange;
                    this._item = null;
                    this._plugin = null;
                }
                _disposed = true;
            }
        }

        #endregion
    }
}
