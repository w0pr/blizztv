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
    public class ListItemContainer:ListViewItem
    {
        private ListItem _item;
        private Plugin _plugin;

        public ListItemContainer(Plugin Plugin,ListItem Item)
        {
            this._plugin = Plugin;
            this._item = Item;
            this.SubItems.Add(this._item.Title);
        }

        public void DoubleClick(object sender, EventArgs e)
        {
            this._item.DoubleClick(sender,e);
        }

        public void DrawSubItem(object sender, DrawListViewSubItemEventArgs e)
        {
            switch (e.ColumnIndex)
            {
                case 0:
                    Rectangle Bounds = e.Bounds;
                    Bounds.Width = Bounds.Height = 16;
                    e.Graphics.DrawImage(this._plugin.PluginInfo.Attributes.Icon, Bounds);
                    break;
                case 1:
                case 2:
                    e.DrawDefault = true;
                    break;
                default:
                    break;
            }
        }
    }
}
