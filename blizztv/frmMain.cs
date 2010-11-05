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
using System.ComponentModel;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using LibBlizzTV;

namespace BlizzTV
{
    public partial class frmMain : Form
    {
        public frmMain()
        {
            InitializeComponent();
        }

        private void frmMain_Load(object sender, EventArgs e)
        {
            PluginManager pm = PluginManager.Instance;
            foreach (KeyValuePair<string,PluginInfo> pair in pm.Plugins)
            {
                Plugin Plugin = pair.Value.CreateInstance();

                ThreadStart plugin_thread = delegate { RunPlugin(Plugin); };
                Thread t = new Thread(plugin_thread) { IsBackground = true };
                t.Start();
            }
        }

        private void RunPlugin(Plugin p)
        {
            p.OnRegisterListGroup += RegisterListGroup;
            p.OnRegisterListItem += RegisterListItem;
            p.Load();
        }

        private void RegisterListGroup(object sender, ListGroup g)
        {
            if (this.InvokeRequired) BeginInvoke(new MethodInvoker(delegate() { RegisterListGroup(sender, g); }));
            else List.Groups.Add(new ListViewGroup(g.Key,g.Name));
        }

        private void RegisterListItem(object sender, ListItem item,ListGroup group)
        {
            if (this.InvokeRequired) BeginInvoke(new MethodInvoker(delegate() { RegisterListItem(sender, item, group); }));
            else
            {
                ListItemContainer c = new ListItemContainer(item);
                c.Group = List.Groups[group.Key];
                this.List.Items.Add(c);
            }
        }

        private void List_DrawSubItem(object sender, DrawListViewSubItemEventArgs e)
        {
            switch (e.ColumnIndex)
            {
                case 0:
                case 1:
                case 2:
                    e.DrawDefault = true;
                    break;
                default:
                    break;
            }
        }

        private void List_DoubleClick(object sender, EventArgs e)
        {
            if (List.SelectedItems.Count > 0)
            {
                ListItemContainer selection = (ListItemContainer)List.SelectedItems[0];
                selection.DoubleClick(sender, e);
            }
        }

        private void AboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmAbout f = new frmAbout();
            f.ShowDialog();
        }

        /*
            Rectangle Bounds = e.Bounds;
            Bounds.Width = Bounds.Height = 16;
            e.Graphics.DrawImage(item_image, Bounds);
        */
    }
}
