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
using LibBlizzTV;

namespace LibStreams
{
    public partial class frmSettings : Form, IPluginSettingsForm
    {
        private bool _streams_list_updated = false;

        public frmSettings()
        {
            InitializeComponent();
        }

        private void frmSettings_Load(object sender, EventArgs e)
        {
            this.LoadSubscriptions();
            this.LoadSettings();
        }

        private void AddSubscriptionToListview(string Name, string Slug,string Provider)
        {
            ListViewItem item = new ListViewItem(Name);
            item.SubItems.Add(Provider);
            item.SubItems.Add(Slug);
            this.ListviewSubscriptions.Items.Add(item);
        }

        private void LoadSettings()
        {
            numericUpDownUpdateFeedsEveryXMinutes.Value = (decimal)Settings.Instance.UpdateEveryXMinutes;
            checkBoxAutomaticallyOpenChatForAvailableStreams.Checked = Settings.Instance.AutomaticallyOpenChatForAvailableStreams;
            txtStreamChatWindowWidth.Text = Settings.Instance.StreamChatWindowWidth.ToString();
            txtStreamChatWindowHeight.Text = Settings.Instance.StreamChatWindowHeight.ToString();
        }

        private void LoadSubscriptions()
        {
            foreach (KeyValuePair<string, Stream> pair in StreamsPlugin.Instance._streams) { this.AddSubscriptionToListview(pair.Value.Name, pair.Value.Slug, pair.Value.Provider); }
        }

        public void SaveSettings()
        {
            Settings.Instance.UpdateEveryXMinutes = (int)numericUpDownUpdateFeedsEveryXMinutes.Value;
            Settings.Instance.AutomaticallyOpenChatForAvailableStreams = checkBoxAutomaticallyOpenChatForAvailableStreams.Checked;
            Settings.Instance.StreamChatWindowWidth = int.Parse(txtStreamChatWindowWidth.Text);
            Settings.Instance.StreamChatWindowHeight = int.Parse(txtStreamChatWindowHeight.Text);

            StreamsPlugin.Instance.SaveSettings();
            if (this._streams_list_updated) { StreamsPlugin.Instance.SaveStreamsXML(); }
        }

        private void buttonAdd_Click(object sender, EventArgs e)
        {
            frmAddStream f = new frmAddStream();
            f.OnAddStream += OnAddStream;
            f.ShowDialog();
        }

        private void buttonRemove_Click(object sender, EventArgs e)
        {
            if (ListviewSubscriptions.SelectedItems.Count > 0)
            {
                this._streams_list_updated = true;
                ListViewItem selection = ListviewSubscriptions.SelectedItems[0];
                StreamsPlugin.Instance._streams[selection.Text].DeleteOnSave = true;
                selection.Remove();
            }
        }

        private void OnAddStream(string Name, string Slug, string Provider)
        {
            this._streams_list_updated = true;
            this.AddSubscriptionToListview(Name, Slug, Provider);
            Stream s = StreamFactory.CreateStream(Name, Slug, Provider);
            s.CommitOnSave = true;
            StreamsPlugin.Instance._streams.Add(Name, s);
        }
    }
}
