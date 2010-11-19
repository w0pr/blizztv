using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
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
            numericUpDownUpdateFeedsEveryXMinutes.Value = (decimal)(StreamsPlugin.Instance.Settings as Settings).UpdateEveryXMinutes;
        }

        private void LoadSubscriptions()
        {
            foreach (KeyValuePair<string, Stream> pair in StreamsPlugin.Instance._streams) { this.AddSubscriptionToListview(pair.Value.Name, pair.Value.Slug, pair.Value.Provider); }
        }

        public void SaveSettings()
        {
            (StreamsPlugin.Instance.Settings as Settings).UpdateEveryXMinutes = (int)numericUpDownUpdateFeedsEveryXMinutes.Value;
            StreamsPlugin.Instance.SaveSettings();
            if (this._streams_list_updated)
            {
                StreamsPlugin.Instance.SaveStreamsXML();
                StreamsPlugin.Instance.UpdateStreams();
            }
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
