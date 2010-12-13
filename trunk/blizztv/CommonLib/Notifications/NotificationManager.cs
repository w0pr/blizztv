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
 * $Id$
 */

using System;
using System.Windows.Forms;
using BlizzTV.CommonLib.UI;
using BlizzTV.UI;
using System.Timers;

namespace BlizzTV.CommonLib.Notifications
{
    public class NotificationManager
    {
        private static NotificationManager _instance = new NotificationManager();
        public static NotificationManager Instance { get { return _instance; } }

        private bool _notificationActive = false;
        private frmMain _mainForm;
        private NotifyIcon _trayIcon;
        private System.Timers.Timer _notificationTimer;
        private ToolStripStatusLabel _notificationIcon;

        private NotificationManager() { }

        public void AttachControls(frmMain mainForm,NotifyIcon trayIcon,ToolStripStatusLabel notificationIcon)
        {
            this._mainForm = mainForm;
            this._trayIcon = trayIcon;
            this._trayIcon.BalloonTipClicked += NotificationClicked;
            this._notificationIcon = notificationIcon;
            this._notificationIcon.Click += NotificationIconClick;
        }

        public void Show(INotificationRequester sender, NotificationEventArgs e)
        {
            this._mainForm.AsyncInvokeHandler(() =>
            {
                if (!this._notificationActive)
                {
                    this._trayIcon.Tag = sender;
                    this._trayIcon.ShowBalloonTip(10000, e.Title,e.Text,e.Icon);
                    this._notificationActive = true;

                    this._notificationTimer = new System.Timers.Timer(10000);
                    this._notificationTimer.Elapsed += NotificationTimer;
                    this._notificationTimer.Enabled = true;
                }
                else
                {
                    ArchivedNotifications.Instance.Queue.Add(new ArchivedNotification(sender, e));
                    if(!this._notificationIcon.Visible) this._notificationIcon.Visible = true;
                }
            });
        }

        private void NotificationTimer(object sender, ElapsedEventArgs e)
        {
            this._notificationActive = false;
        }

        private void NotificationClicked(object sender, EventArgs e)
        {
            this._notificationActive = false;
            (this._trayIcon.Tag as INotificationRequester).NotificationClicked();
        }

        private void NotificationIconClick(object sender, EventArgs e)
        {
            frmArchivedNotifications f = new frmArchivedNotifications(this._mainForm);
            f.ShowDialog();        
        }

        public void ClearQueuedNotifications()
        {
            ArchivedNotifications.Instance.Queue.Clear();
            this._notificationIcon.Visible = false;
        }
    }

        public class NotificationEventArgs : EventArgs
    {
        public string Title { get; private set; }
        public string Text { get; private set; }
        public ToolTipIcon Icon { get; private set; }

        public NotificationEventArgs(string Title, string Text, ToolTipIcon Icon)
        {
            this.Title = Title;
            this.Text = Text;
            this.Icon = Icon;
        }
    }
}
