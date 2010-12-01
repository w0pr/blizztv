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
 * $Id: frmSettings.cs 158 2010-11-30 14:06:50Z shalafiraistlin@gmail.com $
 */

using System;
using System.Windows.Forms;
using BlizzTV.ModuleLib;
using BlizzTV.ModuleLib.Settings;

namespace BlizzTV.Modules.Events
{
    public partial class frmSettings : Form, IModuleSettingsForm
    {
        public frmSettings()
        {
            InitializeComponent();
        }

        private void frmSettings_Load(object sender, EventArgs e)
        {
            checkBoxAllowEventNotifications.Checked = Settings.Instance.AllowEventNotifications;
            checkBoxAllowNotificationOfEventsInProgress.Checked = Settings.Instance.AllowNotificationOfInprogressEvents;
            numericUpDownMinutesBefore.Value = (decimal)Settings.Instance.MinutesToNotifyBeforeEvent;
            numericUpDownNumberOfDaysToShowEventsOnMainWindow.Value = (decimal)Settings.Instance.NumberOfDaysToShowEventsOnMainWindow;
        }

        public void SaveSettings()
        {
            Settings.Instance.AllowEventNotifications = checkBoxAllowEventNotifications.Checked;
            Settings.Instance.AllowNotificationOfInprogressEvents = checkBoxAllowNotificationOfEventsInProgress.Checked;
            Settings.Instance.MinutesToNotifyBeforeEvent = (int)numericUpDownMinutesBefore.Value;
            Settings.Instance.NumberOfDaysToShowEventsOnMainWindow = (int)numericUpDownNumberOfDaysToShowEventsOnMainWindow.Value;
            Settings.Instance.Save();
        }
    }
}
