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
            checkBoxAllowEventNotifications.Checked = (EventsPlugin.Instance.Settings as Settings).AllowEventNotifications;
            checkBoxAllowNotificationOfEventsInProgress.Checked = (EventsPlugin.Instance.Settings as Settings).AllowNotificationOfInprogressEvents;
            numericUpDownMinutesBefore.Value = (decimal)(EventsPlugin.Instance.Settings as Settings).MinutesToNotifyBeforeEvent;
        }

        public void SaveSettings()
        {
            (EventsPlugin.Instance.Settings as Settings).AllowEventNotifications = checkBoxAllowEventNotifications.Checked;
            (EventsPlugin.Instance.Settings as Settings).AllowNotificationOfInprogressEvents = checkBoxAllowNotificationOfEventsInProgress.Checked;
            (EventsPlugin.Instance.Settings as Settings).MinutesToNotifyBeforeEvent = (int)numericUpDownMinutesBefore.Value;

            EventsPlugin.Instance.SaveSettings();
        }
    }
}
