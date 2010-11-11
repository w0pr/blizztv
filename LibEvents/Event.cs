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
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LibBlizzTV;
using LibBlizzTV.Utils;

namespace LibEvents
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

        public void Check() // Check the event and notify user about it if it's in progress or soon to be so.
        {
            if (!this.Notified) // if we haven't notified before
            {
                if (this.Time.LocalTime <= DateTime.Now && DateTime.Now <= this.Time.LocalTime.AddHours(1)) // is event already in progress?
                {
                    this._notified = true; // don't notify about it more then once
                    this.ShowBalloonTip(string.Format("Event in progress: {0}", this.FullTitle), "Click to see event details.", System.Windows.Forms.ToolTipIcon.Info);
                }
                else if ((this.Time.LocalTime - DateTime.Now).TotalMinutes > 0 && (this.Time.LocalTime - DateTime.Now).TotalMinutes <= 5) // start notifying about the upcoming event.
                {
                    this._notified = true; // don't notify about it more then once
                    this.ShowBalloonTip(string.Format("Event starts in {0} minutes: {1}",(this.Time.LocalTime - DateTime.Now).TotalMinutes.ToString("0"), this.FullTitle), "Click to see event details.", System.Windows.Forms.ToolTipIcon.Info);
                }
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

        public string StatusText // returns event status text.
        {            
            get
            {
                string status = "";
                switch (this.Status)
                {
                    case EventStatus.OVER:
                        status = "Over";
                        break;
                    case EventStatus.IN_PROGRESS:
                        status = "In progress";
                        break;
                    case EventStatus.UPCOMING:
                        TimeSpan timeleft = this.Time.LocalTime - DateTime.Now;
                        if (timeleft.Days > 0) status += string.Format("{0} day, ", timeleft.Days);
                        if (timeleft.Hours > 0) status += string.Format("{0} hour, ", timeleft.Hours);
                        if (timeleft.Minutes > 0) status += string.Format("{0} minutes", timeleft.Minutes);
                        status += " to go.";
                        break;
                }
                return status;     
            }
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
