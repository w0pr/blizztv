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
 * $Id: frmSettings.cs 158 2010-11-30 14:06:50Z shalafiraistlin@gmail.com $
 */

using System;
using System.Collections.Generic;
using System.Windows.Forms;
using BlizzTV.Module;

namespace BlizzTV.Modules.Videos
{
    public partial class frmSettings : Form,IPluginSettingsForm
    {
        private bool _video_channels_list_updated = false;

        public frmSettings()
        {
            InitializeComponent();
        }

        private void frmSettings_Load(object sender, EventArgs e)
        {
            this.LoadSubscriptions();
            this.LoadSettings();
        }

        private void AddSubscriptionToListview(string Name, string Slug, string Provider)
        {
            ListViewItem item = new ListViewItem(Name);
            item.SubItems.Add(Provider);
            item.SubItems.Add(Slug);
            this.ListviewSubscriptions.Items.Add(item);
        }

        private void LoadSettings()
        {
            numericUpDownNumberOfVideosToQueryChannelFor.Value = (decimal)Settings.Instance.NumberOfVideosToQueryChannelFor;
            numericUpDownUpdateFeedsEveryXMinutes.Value = (decimal)Settings.Instance.UpdateEveryXMinutes;
        }

        private void LoadSubscriptions()
        {
            foreach (KeyValuePair<string, Channel> pair in VideosPlugin.Instance._channels) { this.AddSubscriptionToListview(pair.Value.Name, pair.Value.Slug, pair.Value.Provider); }
        }

        public void SaveSettings()
        {
            Settings.Instance.NumberOfVideosToQueryChannelFor = (int)numericUpDownNumberOfVideosToQueryChannelFor.Value;
            Settings.Instance.UpdateEveryXMinutes = (int)numericUpDownUpdateFeedsEveryXMinutes.Value;
            VideosPlugin.Instance.SaveSettings();

            if (this._video_channels_list_updated) { VideosPlugin.Instance.SaveChannelsXML(); }
        }

        private void buttonAdd_Click(object sender, EventArgs e)
        {
            frmAddChannel f = new frmAddChannel();
            f.OnAddVideoChannel += OnAddVideoChannel;
            f.ShowDialog();
        }

        private void buttonRemove_Click(object sender, EventArgs e)
        {
            if (ListviewSubscriptions.SelectedItems.Count > 0)
            {
                this._video_channels_list_updated = true;
                ListViewItem selection = ListviewSubscriptions.SelectedItems[0];
                VideosPlugin.Instance._channels[selection.Text].DeleteOnSave = true;
                selection.Remove();
            }
        }

        private void OnAddVideoChannel(string Name, string Slug, string Provider)
        {
            this._video_channels_list_updated = true;
            this.AddSubscriptionToListview(Name, Slug, Provider);
            Channel c = ChannelFactory.CreateChannel(Name, Slug, Provider);
            c.CommitOnSave = true;
            VideosPlugin.Instance._channels.Add(Name, c);
        }
    }
}
