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
        private Font font = new Font("Helvetica", 8, FontStyle.Bold);

        public ListItem Item { get { return this._item; } }
        
        public TreeItem(Plugin Plugin,ListItem Item)
        {
            this._plugin = Plugin;
            this._item = Item;
            this.Name = _item.Key;
            this.Text = this._item.Title;
        }

        public void DoubleClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            this._item.DoubleClick(sender,e);
        }

        public void DrawNode(object sender, DrawTreeNodeEventArgs e)
        {
            Rectangle IconBounds = e.Bounds;
            Rectangle TextBounds = new Rectangle(e.Bounds.X+16, e.Bounds.Y, e.Bounds.Width, e.Bounds.Height);
            IconBounds.Width = IconBounds.Height = 16;

            e.Graphics.DrawImage(this._plugin.PluginInfo.Attributes.Icon, IconBounds);
            e.Graphics.DrawString(this._item.Title, new Font("Verdana",8.25f), new SolidBrush(Color.Black), TextBounds);

            if ((e.State & TreeNodeStates.Focused) != 0)
            {
                using (Pen focusPen = new Pen(Color.Black))
                {
                    focusPen.DashStyle = System.Drawing.Drawing2D.DashStyle.Dot;
                    Rectangle focusBounds = new Rectangle(e.Bounds.X, e.Bounds.Y, e.Bounds.Width + 16, e.Bounds.Height);
                    focusBounds.Size = new Size(focusBounds.Width - 1,focusBounds.Height - 1);
                    e.Graphics.DrawRectangle(focusPen, focusBounds);
                }
            }
        }
    }
}
