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
using BlizzTV.ModuleLib;

namespace BlizzTV.CommonLib.Notifications
{
    public partial class frmArchivedNotifications : Form
    {
        private readonly Form _parent;

        public frmArchivedNotifications(Form parent)
        {
            InitializeComponent();
            this._parent = parent;
            this.SnapToParent();
        }

        private void SnapToParent()
        {
            this.Left = this._parent.Left + this._parent.Width + 2;
            this.Top = this._parent.Top;
            if (this.Height != this._parent.Height) this.Height = this._parent.Height;
        }
        
        private void frmQueuedNotifications_Load(object sender, EventArgs e)
        {
            foreach(ArchivedNotification notification in ArchivedNotifications.Instance.Queue)
            {
                ArchivedNotificationListItemWrapper itemWrapper = new ArchivedNotificationListItemWrapper(notification);
                this.listViewNotifications.Items.Add(itemWrapper);
                this.SetIcon(itemWrapper);
            }
        }

        private void SetIcon(ArchivedNotificationListItemWrapper listItem)
        {
            if (listItem.Notification.Item.GetType().IsSubclassOf(typeof(ListItem)))
            {
                ListItem item = (ListItem)listItem.Notification.Item;
                if (item.Icon != null)
                {
                    if (!this.ImageList.Images.ContainsKey(item.GetType().ToString())) this.ImageList.Images.Add(item.GetType().ToString(), item.Icon);
                    listItem.ImageKey = item.GetType().ToString();
                }
            }
        }

        private void listViewNotifications_DoubleClick(object sender, EventArgs e)
        {
            if (listViewNotifications.SelectedItems.Count > 0)
            {
                ArchivedNotificationListItemWrapper selection = (ArchivedNotificationListItemWrapper)listViewNotifications.SelectedItems[0];
                selection.Notification.Item.NotificationClicked();
            }
        }

        private void ButtonClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmQueuedNotifications_FormClosing(object sender, FormClosingEventArgs e)
        {
            NotificationManager.Instance.ClearQueuedNotifications();
        }

        private class ArchivedNotificationListItemWrapper : ListViewItem
        {
            public ArchivedNotification Notification { get; private set; }

            public ArchivedNotificationListItemWrapper(ArchivedNotification notification)
            {
                this.Notification = notification;          
                this.SubItems.Add(notification.Args.Title);
            }
        }
    }
}
