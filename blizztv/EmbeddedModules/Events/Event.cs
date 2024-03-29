﻿/*    
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
using System.Text.RegularExpressions;
using BlizzTV.EmbeddedModules.Events.Settings;
using BlizzTV.EmbeddedModules.Events.UI;
using BlizzTV.InfraStructure.Modules;
using BlizzTV.Notifications;
using BlizzTV.Storage;
using BlizzTV.Utility.Date;
using BlizzTV.Utility.Extensions;
using BlizzTV.Utility.Imaging;

namespace BlizzTV.EmbeddedModules.Events
{
    /// <summary>
    /// Holds an event.
    /// </summary>
    public class Event : ModuleNode
    {
        private bool _disposed = false;

        private readonly Regex[] _teamliquidDescriptionFilters = new Regex[] // filters for teamliquid calendar's [tlpd] and similar tags.
        {
            new Regex(@"\[/?tlpd.*?\]", RegexOptions.Compiled),
            new Regex(@"\[/?b.*?\]", RegexOptions.Compiled),
            new Regex(@"\[/?i.*?\]", RegexOptions.Compiled)
        };      

        public string FullTitle { get; private set; } /* the full event title */
        public string Description { get; private set; } /* description */
        public string EventId { get; private set; } /* unique event id */
        public bool IsOver { get; private set; } /* is the event over? */
        public bool Notified { get; private set; } /* was user notified about event? */
        public ZonedDateTime Time { get; private set; } /* event date & time */

        /// <summary>
        /// Returns event status.
        /// </summary>
        public EventStatus Status
        {
            get
            {
                if (this.IsOver || DateTime.Now > this.Time.LocalTime.AddHours(1)) return EventStatus.Over; // is it over?
                if (this.Time.LocalTime <= DateTime.Now && DateTime.Now <= this.Time.LocalTime.AddHours(1)) return EventStatus.InProgress; // is it in progress?
                return EventStatus.Upcoming; // or is it upcoming?
            }
        }

        /// <summary>
        /// Returns minutes left to start of the event.
        /// </summary>
        public double MinutesLeft
        {
            get
            {
                if (this.Status != EventStatus.Upcoming) return 0;

                TimeSpan timeleft = this.Time.LocalTime - DateTime.Now;
                return timeleft.TotalMinutes;
            }
        }

        /// <summary>
        /// Returns time left to start of the events as a formatted string.
        /// </summary>
        public string TimeLeft
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
            this.Notified = false;
            this.FullTitle = fullTitle;
            this.Description = description;
            foreach(Regex regex in this._teamliquidDescriptionFilters) { this.Description = regex.Replace(this.Description, ""); }            
            this.EventId = eventId;
            this.IsOver = isOver;
            this.Time = time;

            this.Icon = new NodeIcon("event", Assets.Images.Icons.Png._16._event);
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
            var f = new EventViewerForm(this);
            f.Show();
        }

        public void Check() // Check the event for notifications and alarms.
        {
            this.CheckForNotifications();
            this.CheckForAlarms();
        }

        private void CheckForNotifications()
        {
            if (ModuleSettings.Instance.EventNotificationsEnabled && !this.Notified) // if notifications are enabled & we haven't notified before.
            {
                if ((ModuleSettings.Instance.InProgressEventNotificationsEnabled) && (this.Status == EventStatus.InProgress)) // if in-progress event notifications are enabled, check for it the event has started.
                {
                    this.Notified = true; // don't notify about it more then once
                    NotificationManager.Instance.Show(this, new NotificationEventArgs(this.FullTitle, "Event is in progress, click to see event details.", System.Windows.Forms.ToolTipIcon.Info));
                }
                else if (this.MinutesLeft > 0 && (this.MinutesLeft <= ModuleSettings.Instance.MinutesToNotifyBeforeEvent)) // start notifying about the upcoming event.
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
                    Module.UITreeView.AsyncInvokeHandler(() =>
                    {
                        var form = new AlarmForm(this);
                        form.Show();
                    });
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
