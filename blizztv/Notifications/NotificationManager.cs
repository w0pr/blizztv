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
using System.Timers;
using BlizzTV.CommonLib.UI;
using BlizzTV.Settings;
using BlizzTV.UI;

namespace BlizzTV.Notifications
{
    public class NotificationManager
    {
        #region instance

        private static NotificationManager _instance = new NotificationManager();
        public static NotificationManager Instance { get { return _instance; } }

        #endregion

        private bool _gotActiveNotification = false; /* does an active notification exists which is currently being showed to user? */
        private const int BalloonDuration = 10000;
        private frmMain _mainForm; 
        private NotifyIcon _trayIcon;
        private ToolStripStatusLabel _archivedNotificationsIcon;
        private System.Timers.Timer _notificationTimer; /* timer for handling notification states */

        private NotificationManager() { }

        public void AttachControls(frmMain mainForm, NotifyIcon trayIcon, ToolStripStatusLabel archivedNotificationsIcon)
        {
            this._mainForm = mainForm;
            this._trayIcon = trayIcon;
            this._trayIcon.BalloonTipClicked += NotificationClicked;
            this._archivedNotificationsIcon = archivedNotificationsIcon;
            this._archivedNotificationsIcon.Click += ArchivedNotificationsIconClick;
        }

        public void Show(INotificationRequester sender, NotificationEventArgs e) // Shows a notification based on NotificationEventArgs supplied by the requester.
        {
            if (!GlobalSettings.Instance.NotificationsEnabled) return;

            this._mainForm.AsyncInvokeHandler(() =>
            {
                if (!this._gotActiveNotification) // if there exists no active notifications.
                {
                    this._trayIcon.Tag = sender; // store the requester in tag, so that if baloon is clicked we can notify the requester back.
                    this._trayIcon.ShowBalloonTip(BalloonDuration, e.Title, e.Text, e.Icon);
                    this._gotActiveNotification = true;

                    if(GlobalSettings.Instance.NotificationSoundsEnabled) NotificationSound.Instance.Play();

                    this._notificationTimer = new System.Timers.Timer(BalloonDuration);
                    this._notificationTimer.Elapsed += BalloonDisappearTimer; // setup a timer so that we can reset oru flags when the balloon disappears.
                    this._notificationTimer.Enabled = true;
                }
                else // if there exists an active notification, add the new notification to archived notifications list.
                {
                    ArchivedNotifications.Instance.Queue.Add(new ArchivedNotification(sender, e));
                    if(!this._archivedNotificationsIcon.Visible) this._archivedNotificationsIcon.Visible = true; // set archived notifications icon on main window visible.
                }
            });
        }

        private void BalloonDisappearTimer(object sender, ElapsedEventArgs e)
        {
            this._gotActiveNotification = false; // reset active notification flag. 
            this._notificationTimer.Dispose();
            this._notificationTimer = null;
        }

        private void NotificationClicked(object sender, EventArgs e)
        {
            this._gotActiveNotification = false;

            if (this._trayIcon.Tag == null) return;
            ((INotificationRequester) this._trayIcon.Tag).NotificationClicked(); // notify the notification requester back about the click event.
        }

        private void ArchivedNotificationsIconClick(object sender, EventArgs e)
        {
            frmArchivedNotifications f = new frmArchivedNotifications(this._mainForm);
            f.ShowDialog();        
        }

        public void ClearArchivedNotifications()
        {
            ArchivedNotifications.Instance.Queue.Clear();
            this._archivedNotificationsIcon.Visible = false;
        }
    }

    /// <summary>
    /// Notification event arguments.
    /// </summary>
    public class NotificationEventArgs : EventArgs 
    {
        /// <summary>
        /// The notification title.
        /// </summary>
        public string Title { get; private set; }

        /// <summary>
        /// The notification text.
        /// </summary> 
        public string Text { get; private set; }

        /// <summary>
        /// The notification icon.
        /// </summary>
        public ToolTipIcon Icon { get; private set; }

        public NotificationEventArgs(string title, string text, ToolTipIcon icon)
        {
            this.Title = title;
            this.Text = text;
            this.Icon = icon;
        }
    }
}
