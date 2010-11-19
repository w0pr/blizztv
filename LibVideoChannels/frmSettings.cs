using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using LibBlizzTV;

namespace LibVideoChannels
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
            numericUpDownNumberOfVideosToQueryChannelFor.Value = (decimal)(VideoChannelsPlugin.Instance.Settings as Settings).NumberOfVideosToQueryChannelFor;
            numericUpDownUpdateFeedsEveryXMinutes.Value = (decimal)(VideoChannelsPlugin.Instance.Settings as Settings).UpdateEveryXMinutes;
        }

        private void LoadSubscriptions()
        {
            foreach (KeyValuePair<string, Channel> pair in VideoChannelsPlugin.Instance._channels) { this.AddSubscriptionToListview(pair.Value.Name, pair.Value.Slug, pair.Value.Provider); }
        }

        public void SaveSettings()
        {
            (VideoChannelsPlugin.Instance.Settings as Settings).NumberOfVideosToQueryChannelFor = (int)numericUpDownNumberOfVideosToQueryChannelFor.Value;
            (VideoChannelsPlugin.Instance.Settings as Settings).UpdateEveryXMinutes = (int)numericUpDownUpdateFeedsEveryXMinutes.Value;
            VideoChannelsPlugin.Instance.SaveSettings();

            if (this._video_channels_list_updated)
            {
                VideoChannelsPlugin.Instance.SaveChannelsXML();
                VideoChannelsPlugin.Instance.UpdateChannels();
            }
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
                VideoChannelsPlugin.Instance._channels[selection.Text].DeleteOnSave = true;
                selection.Remove();
            }
        }

        private void OnAddVideoChannel(string Name, string Slug, string Provider)
        {
            this._video_channels_list_updated = true;
            this.AddSubscriptionToListview(Name, Slug, Provider);
            Channel c = ChannelFactory.CreateChannel(Name, Slug, Provider);
            c.CommitOnSave = true;
            VideoChannelsPlugin.Instance._channels.Add(Name, c);
        }
    }
}
