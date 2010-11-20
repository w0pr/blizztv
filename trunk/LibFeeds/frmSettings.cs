using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using LibBlizzTV;

namespace LibFeeds
{
    public partial class frmSettings : Form, IPluginSettingsForm
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
            numericUpDownUpdateFeedsEveryXMinutes.Value = (decimal)(FeedsPlugin.Instance.Settings as Settings).UpdateEveryXMinutes;
        }

        public void SaveSettings()
        {
            (FeedsPlugin.Instance.Settings as Settings).UpdateEveryXMinutes = (int)numericUpDownUpdateFeedsEveryXMinutes.Value;
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
