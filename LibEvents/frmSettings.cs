using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using LibBlizzTV;

namespace LibEvents
{
    public partial class frmSettings : Form, IPluginSettingsForm
    {
        public frmSettings()
        {
            InitializeComponent();
        }

        private void frmSettings_Load(object sender, EventArgs e)
        {
            checkBoxAllowEventNotifications.Checked = (Plugin.Instance.Settings as Settings).AllowEventNotifications;
            checkBoxAllowNotificationOfEventsInProgress.Checked = (Plugin.Instance.Settings as Settings).AllowNotificationOfInprogressEvents;
            numericUpDownMinutesBefore.Value = (decimal)(Plugin.Instance.Settings as Settings).MinutesToNotifyBeforeEvent;
        }

        public void SaveSettings()
        {
            (Plugin.Instance.Settings as Settings).AllowEventNotifications = checkBoxAllowEventNotifications.Checked;
            (Plugin.Instance.Settings as Settings).AllowNotificationOfInprogressEvents = checkBoxAllowNotificationOfEventsInProgress.Checked;
            (Plugin.Instance.Settings as Settings).MinutesToNotifyBeforeEvent = (int)numericUpDownMinutesBefore.Value;

            Plugin.Instance.SaveSettings();
        }
    }
}
