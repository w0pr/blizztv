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
using System.Reflection;
using LibBlizzTV;
using BlizzTV.Updates;

namespace BlizzTV
{
    public partial class frmAbout : Form
    {
        public frmAbout()
        {
            InitializeComponent();
        }

        private void buttonOK_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmAbout_Load(object sender, EventArgs e)
        {            
            ListviewPlugins.Items.Add(new ListviewModulesItem(Assembly.GetExecutingAssembly().GetName().Name, Assembly.GetExecutingAssembly().GetName().Version.ToString(), "The user-interface."));
            ListviewPlugins.Items.Add(new ListviewModulesItem(PluginManager.Instance.AssemblyName, PluginManager.Instance.AssemblyVersion, "The core."));

            foreach (KeyValuePair<string, PluginInfo> pair in PluginManager.Instance.AvailablePlugins)
            {
                ListviewPlugins.Items.Add(new ListviewModulesItem(pair.Value.AssemblyName, pair.Value.AssemblyVersion, pair.Value.Attributes.Description));
            }

            ListviewPlugins.AutoResizeColumns(ColumnHeaderAutoResizeStyle.ColumnContent);
        }

        private void LinkLabelBlizzTV_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start("http://www.blizztv.com", null);
        }

        private void LinkFlattr_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("http://flattr.com/thing/86300/BlizzTV", null);
        }

        private void LinkPaypal_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("https://www.paypal.com/cgi-bin/webscr?cmd=_s-xclick&hosted_button_id=PQ3D5PMB85L34", null);
        }

        private bool _enough_dots = false;
        private void more_dots(object sender, KeyEventArgs e) { if (e.Alt && e.Control) this._enough_dots = true; }
        private void even_more_dots(object sender, EventArgs e) { if (this._enough_dots) { this.Width = 640; this.Height = 385; this.Player.Visible = true; this.Player.Dock = DockStyle.Fill; this.Player.LoadMovie(0, "http://www.youtube.com/v/bvwFcfQWOGY?fs=1&autoplay=1&hl=en_US"); } _enough_dots = false; }
        private void wipe(object sender, KeyEventArgs e) { this._enough_dots = false; }
    }

    class ListviewModulesItem : ListViewItem
    {
        public ListviewModulesItem(string Name, string Version, string Description)
        {
            this.Text = Name;
            this.SubItems.Add(new ListViewSubItem(this, Version));
            this.SubItems.Add(new ListViewSubItem(this, Description));
        }
    }
}
