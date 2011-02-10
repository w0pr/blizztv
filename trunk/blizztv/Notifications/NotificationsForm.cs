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
using BlizzTV.Modules;

namespace BlizzTV.Notifications
{
    public partial class NotificationsForm : Form
    {
        private readonly Form _parent; // reference to parent main-window.

        public NotificationsForm(Form parent)
        {
            InitializeComponent();

            this._parent = parent; 
            this.SnapToParent();
            this.OnResizeEnd(EventArgs.Empty); // run the window-resize code at initial load.
        }
        
        private void NotificationsForm_Load(object sender, EventArgs e)
        {
            foreach(ArchivedNotification notification in ArchivedNotifications.Instance.Queue)
            {
                ArchivedNotificationListItemWrapper itemWrapper = new ArchivedNotificationListItemWrapper(notification);
                this.listViewNotifications.Items.Add(itemWrapper);
                this.SetNotificationIcon(itemWrapper); // set the item's icon.
            }
        }

        private void SnapToParent() // snaps the window to main-window.
        {
            this.Left = this._parent.Left + this._parent.Width + 2;
            this.Top = this._parent.Top;
            if (this.Height != this._parent.Height) this.Height = this._parent.Height;
        }

        private void SetNotificationIcon(ArchivedNotificationListItemWrapper listItem)
        {
            if (!listItem.Notification.Item.GetType().IsSubclassOf(typeof (ListItem))) return;

            ListItem item = (ListItem)listItem.Notification.Item; 
            if (item.Icon == null) return; // if item doesn't have an assosiciated icon, just ignore.

            if (!this.ImageList.Images.ContainsKey(item.Icon.Name)) this.ImageList.Images.Add(item.Icon.Name, item.Icon.Image); // add the icon to imagelist if doesnt exists yet.
            listItem.ImageKey = item.Icon.Name;
        }

        private void listViewNotifications_DoubleClick(object sender, EventArgs e)
        {
            if (listViewNotifications.SelectedItems.Count <= 0) return;

            ArchivedNotificationListItemWrapper selection = (ArchivedNotificationListItemWrapper)listViewNotifications.SelectedItems[0];
            selection.Notification.Item.NotificationClicked();
        }

        private void frmArchivedNotifications_ResizeEnd(object sender, EventArgs e) // resizes listview columns based on window width.
        {
            this.listViewNotifications.Columns[1].Width = this.Width - (this.listViewNotifications.Columns[0].Width + 40);
        }

        private void ButtonClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmQueuedNotifications_FormClosing(object sender, FormClosingEventArgs e)
        {
            NotificationManager.Instance.ClearArchivedNotifications(); // clears archived notifications list form-close.
        }

        private class ArchivedNotificationListItemWrapper : ListViewItem // Wrapper list-item for archived notification.
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
