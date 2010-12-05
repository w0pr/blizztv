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
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace BlizzTV.CommonLib.Notifications
{
    public partial class frmQueuedNotifications : Form
    {
        public frmQueuedNotifications()
        {
            InitializeComponent();
        }

        private void frmQueuedNotifications_Load(object sender, EventArgs e)
        {
            foreach(QueuedNotification notification in QueuedNotifications.Instance.Queue)
            {
                this.listViewNotifications.Items.Add(new QueuedNotificationListItemWrapper(notification));
            }
        }

        private void frmQueuedNotifications_Resize(object sender, EventArgs e)
        {
            this.listViewNotifications.Columns[0].Width = this.listViewNotifications.Width - 21;
        }

        private void frmQueuedNotifications_FormClosing(object sender, FormClosingEventArgs e)
        {
            NotificationManager.Instance.ClearQueuedNotifications();
        }

        private void ButtonClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void listViewNotifications_DoubleClick(object sender, EventArgs e)
        {
            if(listViewNotifications.SelectedItems.Count>0)
            {
                QueuedNotificationListItemWrapper selection = (QueuedNotificationListItemWrapper)listViewNotifications.SelectedItems[0];
                selection.Notification.Item.NotificationClicked();
            }
        }

        private class QueuedNotificationListItemWrapper : ListViewItem
        {
            public QueuedNotification Notification { get; private set; }

            public QueuedNotificationListItemWrapper(QueuedNotification Notification)
            {
                this.Notification = Notification;
                this.Text = Notification.Args.Title;
            }
        }
    }
}
