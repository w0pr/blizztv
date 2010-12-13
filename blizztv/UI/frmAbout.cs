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
using System.Collections.Generic;
using System.Windows.Forms;
using System.Reflection;
using BlizzTV.CommonLib.UI;
using BlizzTV.ModuleLib;

namespace BlizzTV.UI
{
    public partial class frmAbout : Form
    {
        public frmAbout()
        {
            InitializeComponent();
        }

        private void frmAbout_Load(object sender, EventArgs e)
        {
            this.LabelVersion.Text = Assembly.GetEntryAssembly().GetName().Version.ToString();

            foreach (KeyValuePair<string, ModuleInfo> pair in ModuleManager.Instance.AvailablePlugins) // load the available modules list
            {
                ListviewModuleItem item = new ListviewModuleItem(pair.Value);
                this.ListviewModules.SmallImageList.Images.Add(pair.Value.Attributes.Name, pair.Value.Attributes.Icon);
                this.ListviewModules.Items.Add(item);
            }
        }

        private void buttonOK_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void buttonChangelog_Click(object sender, EventArgs e) { System.Diagnostics.Process.Start("http://code.google.com/p/blizztv/wiki/Changelog", null); }	
        private void LinkBlizzTV_Clicked(object sender, LinkLabelLinkClickedEventArgs e) { System.Diagnostics.Process.Start("http://www.blizztv.com", null); }
        private void LinkFlattr_Click(object sender, EventArgs e) { System.Diagnostics.Process.Start("http://flattr.com/thing/86300/BlizzTV", null); }
        private void LinkPaypal_Click(object sender, EventArgs e) { System.Diagnostics.Process.Start("https://www.paypal.com/cgi-bin/webscr?cmd=_s-xclick&hosted_button_id=PQ3D5PMB85L34", null); }
        			
        private bool _enoughDots = false;		
        private void MoreDots(object sender, KeyEventArgs e) { if (e.Alt && e.Control) this._enoughDots = true; }		
        private void EvenMoreDots(object sender, EventArgs e) { if (this._enoughDots) { this.Text = "Save the murlocs!"; this.Width = 640; this.Height = 385; this.Player.Visible = true; this.Player.Dock = DockStyle.Fill; this.Player.LoadMovie(0, "http://www.youtube.com/v/bvwFcfQWOGY?fs=1&autoplay=1&hl=en_US"); } _enoughDots = false; }	
    }
}
