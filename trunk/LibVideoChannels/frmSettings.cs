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
        public frmSettings()
        {
            InitializeComponent();
        }

        private void frmSettings_Load(object sender, EventArgs e)
        {
            numericUpDownNumberOfVideosToQueryChannelFor.Value = (decimal)(VideoChannelsPlugin.Instance.Settings as Settings).NumberOfVideosToQueryChannelFor;
            numericUpDownUpdateFeedsEveryXMinutes.Value = (decimal)(VideoChannelsPlugin.Instance.Settings as Settings).UpdateEveryXMinutes;
        }
       
        public void SaveSettings()
        {
            (VideoChannelsPlugin.Instance.Settings as Settings).NumberOfVideosToQueryChannelFor = (int)numericUpDownNumberOfVideosToQueryChannelFor.Value;
            (VideoChannelsPlugin.Instance.Settings as Settings).UpdateEveryXMinutes = (int)numericUpDownUpdateFeedsEveryXMinutes.Value;
            VideoChannelsPlugin.Instance.SaveSettings();
        }
    }
}
