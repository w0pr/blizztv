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

namespace BlizzTV.Modules.Feeds
{
    public partial class frmSettings : Form, IModuleSettingsForm
    {
        private bool _feeds_list_updated = false;

        public frmSettings()
        {
            InitializeComponent();
        }

        private void frmSettings_Load(object sender, EventArgs e)
        {
            this.LoadSubscriptions();
            this.LoadSettings();
        }

        private void AddSubscriptionToListview(string Name, string URL)
        {
            ListViewItem item = new ListViewItem(Name);
            item.SubItems.Add(URL);
            this.ListviewSubscriptions.Items.Add(item);
        }

        private void LoadSubscriptions()
        {
            foreach (KeyValuePair<string,Feed> pair in FeedsPlugin.Instance._feeds) { this.AddSubscriptionToListview(pair.Value.Name, pair.Value.URL); }
        }

        private void LoadSettings()
        {
            numericUpDownUpdateFeedsEveryXMinutes.Value = (decimal)Settings.Instance.UpdateEveryXMinutes;
        }

        public void SaveSettings()
        {
            Settings.Instance.UpdateEveryXMinutes = (int)numericUpDownUpdateFeedsEveryXMinutes.Value;
            FeedsPlugin.Instance.SaveSettings();
            if (this._feeds_list_updated) { FeedsPlugin.Instance.SaveFeedsXML(); }
        }

        private void buttonAdd_Click(object sender, EventArgs e)
        {
            frmAddFeed f = new frmAddFeed();
            f.OnAddFeed += OnAddFeed;
            f.ShowDialog();
        }

        private void buttonRemove_Click(object sender, EventArgs e)
        {
            if (ListviewSubscriptions.SelectedItems.Count > 0)
            {
                this._feeds_list_updated = true;
                ListViewItem selection = ListviewSubscriptions.SelectedItems[0];
                FeedsPlugin.Instance._feeds[selection.Text].DeleteOnSave = true;                
                selection.Remove();
            }
        }

        private void OnAddFeed(string Name, string URL)
        {
            this._feeds_list_updated=true;
            this.AddSubscriptionToListview(Name, URL); // add to listview.
            Feed f = new Feed(Name, URL);
            f.CommitOnSave = true;
            FeedsPlugin.Instance._feeds.Add(Name,f); // add to our feeds list.
        }
    }
}
