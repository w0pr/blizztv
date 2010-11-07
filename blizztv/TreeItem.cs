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
        private ListItem _item;
        private Plugin _plugin;

        public ListItem Item { get { return this._item; } }
        
        public TreeItem(Plugin Plugin,ListItem Item)
        {
            this._plugin = Plugin;
            this._item = Item;
            this.Name = _item.Key;

            this._item.OnTitleChange += TitleChange;
            this._item.OnStateChange += StateChange;

        }

        public void Render()
        {
            this.Text = this._item.Title; // set the inital title
            this.StateChange(this); // set the initial state

            if (!this.TreeView.ImageList.Images.ContainsKey(this._plugin.PluginInfo.AssemblyName)) this.TreeView.ImageList.Images.Add(this._plugin.PluginInfo.AssemblyName, this._plugin.PluginInfo.Attributes.Icon); // add the plugin icon to image list in it doesn't exists
            this.ImageIndex = this.TreeView.ImageList.Images.IndexOfKey(this._plugin.PluginInfo.AssemblyName); // render the icon
        }

        public void TitleChange(object sender)
        {
            if (this.TreeView.InvokeRequired) this.TreeView.BeginInvoke(new MethodInvoker(delegate() { TitleChange(sender); }));
            else this.Text = this._item.Title;
        }

        public void StateChange(object sender)
        {
            if (this.TreeView.InvokeRequired) this.TreeView.BeginInvoke(new MethodInvoker(delegate() { StateChange(sender); }));
            else
            {
                switch (this._item.State)
                {
                    case ItemState.UNREAD:
                        this.NodeFont = new Font(SystemFonts.DefaultFont.FontFamily, SystemFonts.DefaultFont.Size, FontStyle.Bold);
                        break;
                    case ItemState.READ:
                        this.NodeFont = new Font(SystemFonts.DefaultFont.FontFamily, SystemFonts.DefaultFont.Size, FontStyle.Regular);
                        break;
                    case ItemState.MARKED:
                        break;
                    default:
                        break;
                }
            }
        }

        public void DoubleClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            this._item.DoubleClick(sender,e);
        }
    }
}
