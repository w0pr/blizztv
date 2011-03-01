/*    
 * Copyright (C) 2010-2011, BlizzTV Project - http://code.google.com/p/blizztv/
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
using System.Windows.Forms;
using BlizzTV.Modules.Settings;
using BlizzTV.Modules.Subscriptions;
using BlizzTV.Modules.Subscriptions.Catalog;

namespace BlizzTV.Videos
{
    public partial class SettingsForm : Form,IModuleSettingsForm
    {
        public SettingsForm()
        {
            InitializeComponent();
            this.ListviewSubscriptions.AfterLabelEdit += OnItemEdit;
        }

        private void SettingsForm_Load(object sender, EventArgs e)
        {
            this.LoadSubscriptions();
            this.LoadSettings();
        }

        private void LoadSubscriptions()
        {
            this.ListviewSubscriptions.Items.Clear();
            foreach (Subscription subscription in Subscriptions.Instance.List) this.ListviewSubscriptions.Items.Add(new ListviewVideoSubscription((VideoSubscription)subscription));
        }

        private void LoadSettings()
        {
            checkBoxEnableNotifications.Checked = Settings.Instance.NotificationsEnabled;
            numericUpDownNumberOfVideosToQueryChannelFor.Value = (decimal)Settings.Instance.NumberOfVideosToQueryChannelFor;
            numericUpDownUpdatePeriod.Value = (decimal)Settings.Instance.UpdatePeriod;
        }

        public void SaveSettings()
        {
            Settings.Instance.NotificationsEnabled = checkBoxEnableNotifications.Checked;
            Settings.Instance.NumberOfVideosToQueryChannelFor = (int)numericUpDownNumberOfVideosToQueryChannelFor.Value;
            Settings.Instance.UpdatePeriod = (int)numericUpDownUpdatePeriod.Value;
            Settings.Instance.Save();
            ModuleVideos.Instance.OnSaveSettings();
        }

        private void buttonCatalog_Click(object sender, EventArgs e)
        {
            var form = new CatalogBrowser(ModuleVideos.Instance);
            form.ShowDialog();
            this.LoadSubscriptions();
        }

        private void buttonAdd_Click(object sender, EventArgs e)
        {
            AddChannelForm f = new AddChannelForm();
            if (f.ShowDialog() == DialogResult.OK)
            {
                Subscriptions.Instance.Add(f.Subscription);
                this.ListviewSubscriptions.Items.Add(new ListviewVideoSubscription(f.Subscription));
            }
        }

        private void buttonRemove_Click(object sender, EventArgs e)
        {
            if (ListviewSubscriptions.SelectedItems.Count > 0)
            {
                ListviewVideoSubscription selection = (ListviewVideoSubscription)ListviewSubscriptions.SelectedItems[0];
                Subscriptions.Instance.Remove(selection.Subscription);
                selection.Remove();
            }
        }

        private void buttonEdit_Click(object sender, EventArgs e)
        {
            if (ListviewSubscriptions.SelectedItems.Count > 0) ListviewSubscriptions.SelectedItems[0].BeginEdit();
        }

        private void ListviewSubscriptions_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2 && ListviewSubscriptions.SelectedItems.Count > 0) ListviewSubscriptions.SelectedItems[0].BeginEdit();
        }

        void OnItemEdit(object sender, LabelEditEventArgs e)
        {
            if (!string.IsNullOrEmpty(e.Label)) Subscriptions.Instance.Rename((ListviewSubscriptions.Items[e.Item] as ListviewVideoSubscription).Subscription, e.Label);
        }
    }

    public class ListviewVideoSubscription : ListViewItem
    {
        private readonly VideoSubscription _videoSubscription;
        public VideoSubscription Subscription { get { return this._videoSubscription; } }

        public ListviewVideoSubscription(VideoSubscription videoSubscription)
        {
            this._videoSubscription = videoSubscription;
            this.Text = videoSubscription.Name;
            this.SubItems.Add(videoSubscription.Provider);
            this.SubItems.Add(videoSubscription.Slug);
        }
    }
}
