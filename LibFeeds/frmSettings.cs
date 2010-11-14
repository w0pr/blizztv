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
        public frmSettings()
        {
            InitializeComponent();
        }

        private void frmSettings_Load(object sender, EventArgs e)
        {
            numericUpDownUpdateFeedsEveryXMinutes.Value = (decimal)(FeedsPlugin.Instance.Settings as Settings).UpdateEveryXMinutes;
        }

        public void SaveSettings()
        {
            (FeedsPlugin.Instance.Settings as Settings).UpdateEveryXMinutes = (int)numericUpDownUpdateFeedsEveryXMinutes.Value;
            FeedsPlugin.Instance.SaveSettings();
        }
    }
}
