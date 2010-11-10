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
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using LibBlizzTV;

namespace BlizzTV
{
    public class TreeItem:TreeNode
    {
        #region Members

        private ListItem _item; // the plugin-item bound to.
        private Plugin _plugin; // the plugin itself.
        private Font _bold = new Font("Tahoma", 9, FontStyle.Bold); // font for unread items.
        private Font _regular = new Font("Tahoma", 9, FontStyle.Regular); // font for read items.       
        private bool disposed = false;

        public ListItem Item { get { return this._item; } } // the item getter.
        

        #endregion

        #region ctor

        public TreeItem(Plugin Plugin,ListItem Item)
        {
            this._plugin = Plugin;
            this._item = Item;
            this.Name = _item.Key;

            // register communication events
            this._item.OnTitleChange += TitleChange; // the title-change event
            this._item.OnStateChange += StateChange; // the state-change event            
        }

        #endregion

        #region Logic

        public void Render() // renders the item with title, state and icon information
        {
            this.Text = this._item.Title; // set the inital title
            this.StateChange(this); // set the initial state

            // set the node icon
            if (!this.TreeView.ImageList.Images.ContainsKey(this._plugin.PluginInfo.AssemblyName)) this.TreeView.ImageList.Images.Add(this._plugin.PluginInfo.AssemblyName, this._plugin.PluginInfo.Attributes.Icon); // add the plugin icon to image list in it doesn't exists yet.
            this.ImageIndex = this.TreeView.ImageList.Images.IndexOfKey(this._plugin.PluginInfo.AssemblyName); // use the item's plugin icon.

            // set the node context menus
            this.ContextMenuStrip = new ContextMenuStrip();
            foreach (KeyValuePair<string,ToolStripMenuItem> pair in this._item.ContextMenus)
            {
                this.ContextMenuStrip.Items.Add(pair.Value);
            }
        }

        public void TitleChange(object sender)
        {
            if (this.TreeView.InvokeRequired) this.TreeView.BeginInvoke(new MethodInvoker(delegate() { TitleChange(sender); })); // switch to UI-thread.
            else this.Text = this._item.Title;
        }

        public void StateChange(object sender)
        {
            if (this.TreeView.InvokeRequired) this.TreeView.BeginInvoke(new MethodInvoker(delegate() { StateChange(sender); })); // switch to UI-thread.
            else
            {
                switch (this._item.State)
                {
                    case ItemState.UNREAD:
                        this.NodeFont = _bold;
                        break;
                    case ItemState.READ:
                        this.NodeFont = _regular;
                        break;
                    case ItemState.MARKED: //TODO: Not yet implemented :)
                        break;
                    default:
                        break;
                }
            }
        }

        public void DoubleClicked(object sender, TreeNodeMouseClickEventArgs e) 
        {
            this._item.DoubleClicked(sender,e); // notify the item about the double-click event.
        }

        public void RightClicked(object sender, TreeNodeMouseClickEventArgs e)
        {
            this._item.RightClicked(sender, e); // notify the item about the click event.
        }

        #endregion

        #region de-ctor

        ~TreeItem() { Dispose(false); }

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
                    this._item.OnTitleChange -= TitleChange;
                    this._item.OnStateChange -= StateChange;
                    this._item = null;
                    this._plugin = null;
                    this._bold.Dispose();
                    this._bold = null;
                    this._regular.Dispose();
                    this._regular = null;
                }
                disposed = true;
            }
        }

        #endregion
    }
}
