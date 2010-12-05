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
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace BlizzTV.CommonLib.Notifications
{
    public class QueuedNotifications
    {
        private static QueuedNotifications _instance = new QueuedNotifications();
        public static QueuedNotifications Instance { get { return _instance; } }

        private List<QueuedNotification> _queuedNotifications = new List<QueuedNotification>();
        public List<QueuedNotification> Queue { get { return this._queuedNotifications; } }

        private QueuedNotifications() { }
    }

    public class QueuedNotification
    {
        public INotificationRequester Item { get; private set; }
        public NotificationEventArgs Args { get; private set; }

        public QueuedNotification(INotificationRequester Item, NotificationEventArgs e)
        {
            this.Item = Item;
            this.Args = e;
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
