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
 * $Id: Event.cs 158 2010-11-30 14:06:50Z shalafiraistlin@gmail.com $
 */

using System;
using BlizzTV.Module;
using BlizzTV.Module.Notifications;
using BlizzTV.Module.Storage;

namespace BlizzTV.Modules.Events
{
    public class Event:ListItem
    {
        #region members

        private string _full_title; // the full title of the event.
        private string _description; // the event description.
        private string _event_id; // the event id.
        private bool _is_over = false; // is the event over?
        private bool _notified = false; // was user notified about the event?
        private ZonedDateTime _time; // zoned event time info. 

        public string FullTitle { get { return this._full_title; } }
        public string Description { get { return this._description; } }
        public string EventID { get { return this._event_id; } }
        public bool IsOver { get { return this._is_over; } }
        public bool Notified { get { return this._notified; } }
        public ZonedDateTime Time { get { return this._time; } }

        #endregion

        #region ctor

        public Event(string Title, string FullTitle, string Description, string EventID,bool isOver, ZonedDateTime Time)
            : base(Title)
        {
            this._full_title = FullTitle;
            this._description = Description;
            this._event_id = EventID;
            this._is_over = isOver;
            this._time = Time;
        }

        public override void DoubleClicked(object sender, EventArgs e)
        {
            frmEventViewer f = new frmEventViewer(this);
            f.Show();
        }

        public override void BalloonClicked(object sender, EventArgs e)
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
            if (Settings.Instance.AllowEventNotifications && !this.Notified) // if notifications are enabled & we haven't notified before.
            {
                if ((Settings.Instance.AllowNotificationOfInprogressEvents) && (this.Status == EventStatus.IN_PROGRESS)) // if in-progress event notifications are enabled, check for it the event has started.
                {
                    this._notified = true; // don't notify about it more then once
                    Notifications.Instance.Show(this, string.Format("Event in progress: {0}", this.FullTitle), "Click to see event details.", System.Windows.Forms.ToolTipIcon.Info);
                }
                else if (this.MinutesLeft > 0 && (this.MinutesLeft <= Settings.Instance.MinutesToNotifyBeforeEvent)) // start notifying about the upcoming event.
                {
                    this._notified = true; // don't notify about it more then once
                    Notifications.Instance.Show(this, string.Format("Event starts in {0} minutes: {1}", (this.Time.LocalTime - DateTime.Now).TotalMinutes.ToString("0"), this.FullTitle), "Click to see event details.", System.Windows.Forms.ToolTipIcon.Info);
                }
            }
        }

        private void CheckForAlarms()
        {
            if (this.AlarmExists())
            {
                if (this.Status == EventStatus.UPCOMING)
                {
                    if ((int)this.GetAlarmMinutes() == (int)this.MinutesLeft)
                    {
                        this.ShowForm(new frmAlarm(this));
                    }
                }
                else this.DeleteAlarm();
            }
        }

        public EventStatus Status // returns event status. 
        {
            get
            {
                if (this.IsOver || DateTime.Now > this.Time.LocalTime.AddHours(1)) return EventStatus.OVER; // is it over?
                else if (this.Time.LocalTime <= DateTime.Now && DateTime.Now <= this.Time.LocalTime.AddHours(1)) return EventStatus.IN_PROGRESS; // is it in progress?
                else return EventStatus.UPCOMING; // or is it upcoming?
            }
        }

        public string TimeLeft // returns event status text.
        {            
            get
            {
                string status = "";
                switch (this.Status)
                {
                    case EventStatus.UPCOMING:
                        TimeSpan timeleft = this.Time.LocalTime - DateTime.Now;
                        if (timeleft.Days > 0) status += string.Format("{0} days, ", timeleft.Days);
                        if (timeleft.Hours > 0) status += string.Format("{0} hours, ", timeleft.Hours);
                        if (timeleft.Minutes > 0) status += string.Format("{0} minutes", timeleft.Minutes);
                        break;
                }
                return status;     
            }
        }

        public double MinutesLeft
        {
            get
            {
                if (this.Status == EventStatus.UPCOMING)
                {
                    TimeSpan timeleft = this.Time.LocalTime - DateTime.Now;
                    return timeleft.TotalMinutes;
                }
                else return 0;
            }
        }

        public bool SetupAlarm(byte minutesbefore)
        {
            if (!this.AlarmExists())
            {
                PersistantStorage.Instance.PutByte("alarm", this._event_id, minutesbefore);
                return true;
            }
            else return false;
        }

        public bool AlarmExists()
        {
            if (PersistantStorage.Instance.EntryExists("alarm", this._event_id)) return true;
            else return false;
        }

        public byte GetAlarmMinutes()
        {
            if (this.AlarmExists()) return PersistantStorage.Instance.GetByte("alarm", this._event_id);
            return 0;
        }

        public void DeleteAlarm()
        {
            if (this.AlarmExists()) PersistantStorage.Instance.Delete("alarm", this._event_id);
        }

        #endregion
    }

    public enum EventStatus
    {
        OVER,
        IN_PROGRESS,
        UPCOMING
    }
}
