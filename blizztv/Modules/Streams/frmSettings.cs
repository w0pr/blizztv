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
using BlizzTV.ModuleLib;
using BlizzTV.ModuleLib.Settings;
using BlizzTV.ModuleLib.Subscriptions;

namespace BlizzTV.Modules.Streams
{
    public partial class frmSettings : Form, IModuleSettingsForm
    {
        public frmSettings()
        {
            InitializeComponent();
        }

        private void frmSettings_Load(object sender, EventArgs e)
        {
            this.LoadSubscriptions();
            this.LoadSettings();
        }

        private void LoadSubscriptions()
        {
            foreach (ISubscription subscription in Subscriptions.Instance.List) this.ListviewSubscriptions.Items.Add(new ListviewStreamSubscription((StreamSubscription)subscription));
        }

        private void LoadSettings()
        {
            numericUpDownUpdateFeedsEveryXMinutes.Value = (decimal)Settings.Instance.UpdateEveryXMinutes;
            checkBoxAutomaticallyOpenChatForAvailableStreams.Checked = Settings.Instance.AutomaticallyOpenChatForAvailableStreams;
            txtStreamChatWindowWidth.Text = Settings.Instance.StreamChatWindowWidth.ToString();
            txtStreamChatWindowHeight.Text = Settings.Instance.StreamChatWindowHeight.ToString();
        }

        public void SaveSettings()
        {
            Settings.Instance.UpdateEveryXMinutes = (int)numericUpDownUpdateFeedsEveryXMinutes.Value;
            Settings.Instance.AutomaticallyOpenChatForAvailableStreams = checkBoxAutomaticallyOpenChatForAvailableStreams.Checked;
            Settings.Instance.StreamChatWindowWidth = int.Parse(txtStreamChatWindowWidth.Text);
            Settings.Instance.StreamChatWindowHeight = int.Parse(txtStreamChatWindowHeight.Text);
            Settings.Instance.Save();
            StreamsPlugin.Instance.OnSaveSettings();
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
                ListviewStreamSubscription selection = (ListviewStreamSubscription)ListviewSubscriptions.SelectedItems[0];
                Subscriptions.Instance.Remove(selection.Subscription);
                selection.Remove();
            }
        }

        private void OnAddStream(string Name, string Slug, string Provider)
        {
            StreamSubscription s = new StreamSubscription();
            s.Name = Name;
            s.Slug = Slug;
            s.Provider = Provider;
            Subscriptions.Instance.Add(s);
            this.ListviewSubscriptions.Items.Add(new ListviewStreamSubscription(s));
        }
    }

    public class ListviewStreamSubscription : ListViewItem
    {
        private StreamSubscription _streamSubscription;
        public StreamSubscription Subscription { get { return this._streamSubscription; } }

        public ListviewStreamSubscription(StreamSubscription streamSubscription)
        {
            this._streamSubscription = streamSubscription;
            this.Text = streamSubscription.Name;
            this.SubItems.Add(streamSubscription.Provider);
            this.SubItems.Add(streamSubscription.Slug);
        }
    }
}
