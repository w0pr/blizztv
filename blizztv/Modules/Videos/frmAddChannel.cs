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
using BlizzTV.ModuleLib.Subscriptions.Providers;

namespace BlizzTV.Modules.Videos
{
    public partial class frmAddChannel : Form
    {
        public frmAddChannel()
        {
            InitializeComponent();
        }

        private void frmAddChannel_Load(object sender, EventArgs e)
        {
            foreach (KeyValuePair<string, IProvider> pair in Providers.Instance.List) { comboBoxProviders.Items.Add(pair.Value.Name); }
            comboBoxProviders.SelectedIndex = 0;
        }

        private void buttonOK_Click(object sender, EventArgs e)
        {            
            if (txtName.Text.Trim() != "" && txtSlug.Text.Trim() != "")
            {
                if (!VideosPlugin.Instance._channels.ContainsKey(txtName.Text))
                {
                    using (Channel c = ChannelFactory.CreateChannel(txtName.Text, txtSlug.Text, comboBoxProviders.SelectedItem.ToString()))
                    {
                        c.Update();
                        if (c.Valid)
                        {
                            this.AddVideoChannel(txtName.Text, txtSlug.Text, comboBoxProviders.SelectedItem.ToString());
                            this.Close();
                        }
                        else MessageBox.Show("There was an error parsing the video channel feed. Please check the channel slug and retry.", "Error parsing video channel", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else MessageBox.Show(string.Format("A channel already exists with name '{0}', please choose another name and retry.", txtName.Text), "Key exists", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else MessageBox.Show("Please fill the channel name and slug fields!", "All fields required", MessageBoxButtons.OK, MessageBoxIcon.Error);            
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        public delegate void AddVideoChannelEventHandler(string Name, string Slug, string Provider);
        public event AddVideoChannelEventHandler OnAddVideoChannel;

        private void AddVideoChannel(string Name, string Slug, string Provider)
        {
            if (OnAddVideoChannel != null) OnAddVideoChannel(Name, Slug, Provider);
        }
    }
}
