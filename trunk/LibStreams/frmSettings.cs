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
        public frmSettings()
        {
            InitializeComponent();
        }

        private void frmSettings_Load(object sender, EventArgs e)
        {
            numericUpDownUpdateFeedsEveryXMinutes.Value = (decimal)(StreamsPlugin.Instance.Settings as Settings).UpdateEveryXMinutes;
        }

        public void SaveSettings()
        {
            (StreamsPlugin.Instance.Settings as Settings).UpdateEveryXMinutes = (int)numericUpDownUpdateFeedsEveryXMinutes.Value;
            StreamsPlugin.Instance.SaveSettings();
        }
    }
}
