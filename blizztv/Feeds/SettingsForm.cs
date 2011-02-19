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

namespace BlizzTV.Feeds
{
    public partial class SettingsForm : Form, IModuleSettingsForm
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
            foreach (Subscription subscription in Subscriptions.Instance.List) this.ListviewSubscriptions.Items.Add(new ListviewFeedSubscription((FeedSubscription)subscription));
        }

        private void LoadSettings()
        {
            checkBoxEnableNotifications.Checked = Settings.Instance.NotificationsEnabled;
            numericUpDownUpdatePeriod.Value = (decimal)Settings.Instance.UpdatePeriod;
        }

        public void SaveSettings()
        {
            Settings.Instance.UpdatePeriod = (int)numericUpDownUpdatePeriod.Value;
            Settings.Instance.NotificationsEnabled = checkBoxEnableNotifications.Checked;
            Settings.Instance.Save();
            ModuleFeeds.Instance.OnSaveSettings();
        }

        private void buttonAdd_Click(object sender, EventArgs e)
        {
            AddFeedForm f = new AddFeedForm();
            if (f.ShowDialog() != DialogResult.OK) return;

            Subscriptions.Instance.Add(f.Subscription);
            this.ListviewSubscriptions.Items.Add(new ListviewFeedSubscription(f.Subscription));
        }

        private void buttonRemove_Click(object sender, EventArgs e)
        {
            if (ListviewSubscriptions.SelectedItems.Count <= 0) return;

            ListviewFeedSubscription selection = (ListviewFeedSubscription)ListviewSubscriptions.SelectedItems[0];
            Subscriptions.Instance.Remove(selection.Subscription);        
            selection.Remove();
        }

        private void buttonCatalog_Click(object sender, EventArgs e)
        {
            if (Catalog.Instance.ShowDialog()) this.LoadSubscriptions();
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
            if (!string.IsNullOrEmpty(e.Label)) Subscriptions.Instance.Rename(((ListviewFeedSubscription) ListviewSubscriptions.Items[e.Item]).Subscription, e.Label);
        }
    }

    public class ListviewFeedSubscription:ListViewItem
    {
        private readonly FeedSubscription _feedSubscription;
        public FeedSubscription Subscription { get { return this._feedSubscription; } }

        public ListviewFeedSubscription(FeedSubscription feedSubscription)
        {
            this._feedSubscription = feedSubscription;
            this.Text = feedSubscription.Name;
            this.SubItems.Add(feedSubscription.Url);
        }
    }
}
