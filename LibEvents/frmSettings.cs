﻿/*    
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
 * $Id$
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