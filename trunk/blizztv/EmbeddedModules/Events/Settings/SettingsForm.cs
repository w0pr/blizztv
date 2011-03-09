/*    
 * Copyright (C) 2010-2011, BlizzTV Project - http://code.google.com/p/blizztv/
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
 * $Id$
 */

using System;
using System.Windows.Forms;
using BlizzTV.InfraStructure.Modules.Settings;

namespace BlizzTV.EmbeddedModules.Events.Settings
{
    public partial class SettingsForm : Form, IModuleSettingsForm
    {
        public SettingsForm()
        {
            InitializeComponent();
        }

        private void SettingsForm_Load(object sender, EventArgs e)
        {
            checkBoxEnableNotifications.Checked = ModuleSettings.Instance.EventNotificationsEnabled;
            checkBoxEnableInProgressEventNotifications.Checked = ModuleSettings.Instance.InProgressEventNotificationsEnabled;
            numericUpDownMinutesToNotifyBeforeEvent.Value = (decimal)ModuleSettings.Instance.MinutesToNotifyBeforeEvent;
            numericUpDownNumberOfDaysToShowEventsOnMainWindow.Value = (decimal)ModuleSettings.Instance.NumberOfDaysToShowEventsOnMainWindow;
        }

        public void SaveSettings()
        {
            ModuleSettings.Instance.EventNotificationsEnabled = checkBoxEnableNotifications.Checked;
            ModuleSettings.Instance.InProgressEventNotificationsEnabled = checkBoxEnableInProgressEventNotifications.Checked;
            ModuleSettings.Instance.MinutesToNotifyBeforeEvent = (int)numericUpDownMinutesToNotifyBeforeEvent.Value;
            ModuleSettings.Instance.NumberOfDaysToShowEventsOnMainWindow = (int)numericUpDownNumberOfDaysToShowEventsOnMainWindow.Value;
            ModuleSettings.Instance.Save();
        }

        private void checkBoxEnableNotifications_CheckedChanged(object sender, EventArgs e)
        {
            checkBoxEnableInProgressEventNotifications.Enabled = checkBoxEnableNotifications.Checked;
            numericUpDownMinutesToNotifyBeforeEvent.Enabled = checkBoxEnableNotifications.Checked;
        }
    }
}
