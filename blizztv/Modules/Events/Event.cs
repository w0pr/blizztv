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
using BlizzTV.CommonLib.Utils;
using BlizzTV.ModuleLib;
using BlizzTV.CommonLib.Storage;
using BlizzTV.CommonLib.Notifications;

namespace BlizzTV.Modules.Events
{
    public class Event:ListItem
    {
        public string FullTitle { get; private set; }
        public string Description { get; private set; }
        public string EventId { get; private set; }
        public bool IsOver { get; private set; }
        public bool Notified { get; private set; }
        public ZonedDateTime Time { get; private set; }

        public EventStatus Status // returns event status. 
        {
            get
            {
                if (this.IsOver || DateTime.Now > this.Time.LocalTime.AddHours(1)) return EventStatus.Over; // is it over?
                if (this.Time.LocalTime <= DateTime.Now && DateTime.Now <= this.Time.LocalTime.AddHours(1)) return EventStatus.InProgress; // is it in progress?
                return EventStatus.Upcoming; // or is it upcoming?
            }
        }

        public double MinutesLeft
        {
            get
            {
                if (this.Status == EventStatus.Upcoming)
                {
                    TimeSpan timeleft = this.Time.LocalTime - DateTime.Now;
                    return timeleft.TotalMinutes;
                }
                return 0;
            }
        }

        public string TimeLeft // returns event status text.
        {
            get
            {
                string status = "";
                switch (this.Status)
                {
                    case EventStatus.Upcoming:
                        TimeSpan timeleft = this.Time.LocalTime - DateTime.Now;
                        if (timeleft.Days > 0) status += string.Format("{0} days, ", timeleft.Days);
                        if (timeleft.Hours > 0) status += string.Format("{0} hours, ", timeleft.Hours);
                        if (timeleft.Minutes > 0) status += string.Format("{0} minutes", timeleft.Minutes);
                        break;
                }
                return status;
            }
        }

        public Event(string title, string fullTitle, string description, string eventId,bool isOver, ZonedDateTime time)
            : base(title)
        {
            Notified = false;
            this.FullTitle = fullTitle;
            this.Description = description;
            this.EventId = eventId;
            this.IsOver = isOver;
            this.Time = time;

            this.Icon = new NamedImage("event", Assets.Images.Icons.Png._16._event);
        }

        public override void Open(object sender, EventArgs e)
        {
            this.ShowEvent();
        }

        public override void NotificationClicked()
        {
            this.ShowEvent();
        }

        private void ShowEvent()
        {
            frmEventViewer f = new frmEventViewer(this);
            f.Show();
        }

        public void Check() // Check the event for notifications and alarms.
        {
            this.CheckForNotifications();
            this.CheckForAlarms();
        }

        private void CheckForNotifications()
        {
            if (Settings.Instance.EventNotificationsEnabled && !this.Notified) // if notifications are enabled & we haven't notified before.
            {
                if ((Settings.Instance.InProgressEventNotificationsEnabled) && (this.Status == EventStatus.InProgress)) // if in-progress event notifications are enabled, check for it the event has started.
                {
                    this.Notified = true; // don't notify about it more then once
                    NotificationManager.Instance.Show(this, new NotificationEventArgs(this.FullTitle, "Event is in progress, click to see event details.", System.Windows.Forms.ToolTipIcon.Info));
                }
                else if (this.MinutesLeft > 0 && (this.MinutesLeft <= Settings.Instance.MinutesToNotifyBeforeEvent)) // start notifying about the upcoming event.
                {
                    this.Notified = true; // don't notify about it more then once
                    NotificationManager.Instance.Show(this, new NotificationEventArgs(this.FullTitle, string.Format("Event starts in {0} minutes, click to see event details.", this.MinutesLeft.ToString("0")), System.Windows.Forms.ToolTipIcon.Info));
                }
            }
        }

        private void CheckForAlarms()
        {
            if (!this.AlarmExists()) return;

            if (this.Status == EventStatus.Upcoming)
            {
                if ((int)this.GetAlarmMinutes() == (int)this.MinutesLeft)
                {
                    this.ShowForm(new frmAlarm(this));
                }
            }
            else this.DeleteAlarm();
        }

        public bool SetupAlarm(byte minutesbefore)
        {
            if (!this.AlarmExists())
            {
                KeyValueStorage.Instance.SetByte(string.Format("alarm.{0}", this.EventId), minutesbefore);
                return true;
            }
            return false;
        }

        public bool AlarmExists()
        {
            return KeyValueStorage.Instance.Exists(string.Format("alarm.{0}", this.EventId));
        }

        public byte GetAlarmMinutes()
        {
            return this.AlarmExists() ? KeyValueStorage.Instance.GetByte(string.Format("alarm.{0}", this.EventId)) : (byte)0;
        }

        public void DeleteAlarm()
        {
            if (this.AlarmExists()) KeyValueStorage.Instance.Delete(string.Format("alarm.{0}", this.EventId));
        }
    }

    public enum EventStatus
    {
        Over,
        InProgress,
        Upcoming
    }
}
