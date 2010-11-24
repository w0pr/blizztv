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
 */

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
            numericUpDownNumberOfDaysToShowEventsOnMainWindow.Value = (decimal)(EventsPlugin.Instance.Settings as Settings).NumberOfDaysToShowEventsOnMainWindow;
        }

        public void SaveSettings()
        {
            (EventsPlugin.Instance.Settings as Settings).AllowEventNotifications = checkBoxAllowEventNotifications.Checked;
            (EventsPlugin.Instance.Settings as Settings).AllowNotificationOfInprogressEvents = checkBoxAllowNotificationOfEventsInProgress.Checked;
            (EventsPlugin.Instance.Settings as Settings).MinutesToNotifyBeforeEvent = (int)numericUpDownMinutesBefore.Value;
            (EventsPlugin.Instance.Settings as Settings).NumberOfDaysToShowEventsOnMainWindow = (int)numericUpDownNumberOfDaysToShowEventsOnMainWindow.Value;

            EventsPlugin.Instance.SaveSettings();
        }
    }
}
