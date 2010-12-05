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
using System.Linq;
using System.Text;
using System.Windows.Forms;
using BlizzTV.UILib;

namespace BlizzTV.CommonLib.Notifications
{
    public class NotificationManager
    {
        private frmMain _mainForm;
        private NotifyIcon _trayIcon;
        private ToolStripStatusLabel _notificationIcon;

        private static NotificationManager _instance = new NotificationManager();        
        public static NotificationManager Instance { get { return _instance; } }

        private NotificationManager() { }

        public void AttachControls(frmMain mainForm,NotifyIcon trayIcon,ToolStripStatusLabel notificationIcon)
        {
            this._mainForm = mainForm;
            this._notificationIcon = notificationIcon;
            this._trayIcon = trayIcon;
            this._trayIcon.BalloonTipClicked += NotificationClicked;
        }

        public void Show(INotificationRequester sender, string Title, string Text, ToolTipIcon Icon)
        {
            this._mainForm.AsyncInvokeHandler(() =>
            {
                this._trayIcon.Tag = sender;
                this._trayIcon.ShowBalloonTip(10000, Title, Text, Icon);
            });
        }

        void NotificationClicked(object sender, EventArgs e)
        {
            (this._trayIcon.Tag as INotificationRequester).NotificationClicked();
        }
    }
}