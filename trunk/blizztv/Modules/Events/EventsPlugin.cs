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
using System.Xml.Linq;
using System.Timers;
using BlizzTV.CommonLib.Web;
using BlizzTV.CommonLib.Settings;
using BlizzTV.CommonLib.Logger;
using BlizzTV.CommonLib.Utils;
using BlizzTV.ModuleLib;
using BlizzTV.CommonLib.Workload;

namespace BlizzTV.Modules.Events
{
    [ModuleAttributes("Events", "Events aggregator plugin.", "event_16")]
    public class EventsPlugin : Module
    {
        #region members

        private List<Event> _events = new List<Event>(); // the items list.
        private TimeZoneInfo KOREAN_TIME_ZONE { get { TimeZoneInfo zone = TimeZoneInfo.Local; foreach (TimeZoneInfo z in TimeZoneInfo.GetSystemTimeZones()) { if (z.Id == "Korea Standard Time") zone = z; } return zone; } } // teamliquid calendar flags events in Korean time.
        private ListItem _events_today = new ListItem("Today"); // today's events item.
        private ListItem _events_upcoming = new ListItem("Upcoming"); // upcoming events item.
        private ListItem _events_over = new ListItem("Past"); // past events item.
        private Timer _event_timer = new Timer(60000); // runs every one minute and check events & alarms.
        private bool disposed = false;

        public static Module Instance;

        #endregion        

        #region ctor

        public EventsPlugin() : base()
        {
            EventsPlugin.Instance = this;
            this.RootListItem = new ListItem("Events");

            this.Menus.Add("calendar", new System.Windows.Forms.ToolStripMenuItem("Calendar", null, new EventHandler(MenuCalendarClicked))); // register calender menu.
            this.RootListItem.ContextMenus.Add("calendar", new System.Windows.Forms.ToolStripMenuItem("Calendar", null, new EventHandler(MenuCalendarClicked))); // calendar menu in context-menus.
        }

        #endregion

        #region API handlers

        public override void Run()
        {
            this.ParseEvents();

            // Go check for events.
            if (!GlobalSettings.Instance.InSleepMode) this.CheckEvents();

            // setup update timer for event checks
            _event_timer.Elapsed += new ElapsedEventHandler(OnTimerHit);
            _event_timer.Enabled = true;
        }

        public override System.Windows.Forms.Form GetPreferencesForm()
        {
            return new frmSettings();
        }

        #endregion

        #region internal logic

        private void ParseEvents()
        {
            if (!this.Updating)
            {
                this.Updating = true;
                this.NotifyUpdateStarted();

                Workload.Instance.Add(this, 1);
                this.RootListItem.SetTitle("Updating events..");

                try
                {
                    string xml = WebReader.Read("http://www.teamliquid.net/calendar/xml/calendar.xml"); // read teamliquid calendar xml.
                    XDocument xdoc = XDocument.Parse(xml); // parse the xml.

                    var months = from month_entry in xdoc.Descendants("month") // get the events.
                                 select new
                                 {
                                     year = month_entry.Attribute("year"),
                                     month = month_entry.Attribute("num"),
                                     days = from day_entry in month_entry.Descendants("day")
                                            select new
                                            {
                                                day = day_entry.Attribute("num"),
                                                events = from event_entry in day_entry.Descendants("event")
                                                         select new
                                                         {
                                                             hour = event_entry.Attribute("hour"),
                                                             minute = event_entry.Attribute("minute"),
                                                             is_over = event_entry.Attribute("over"),
                                                             title = event_entry.Element("title"),
                                                             short_title = event_entry.Element("short-title"),
                                                             description = event_entry.Element("description"),
                                                             event_id = event_entry.Element("event-id")
                                                         }
                                            }
                                 };

                    foreach (var month_entry in months) // create up the event items.
                        foreach (var day_entry in month_entry.days)
                            foreach (var event_entry in day_entry.events)
                            {
                                Event e = new Event((string)event_entry.short_title, (string)event_entry.title, (string)event_entry.description, (string)event_entry.event_id, (bool)event_entry.is_over, new ZonedDateTime(new DateTime((int)month_entry.year, (int)month_entry.month, (int)day_entry.day, (int)event_entry.hour, (int)event_entry.minute, 0), KOREAN_TIME_ZONE)); // TL calendar flags events in Korean time.
                                this._events.Add(e);
                            }
                }
                catch (Exception e)
                {
                    Log.Instance.Write(LogMessageTypes.ERROR, string.Format("EventsPlugin ParseEvents() Error: \n {0}", e.ToString()));
                }

                this.RootListItem.Childs.Add("events-today", _events_today);
                this.RootListItem.Childs.Add("events-upcoming", _events_upcoming);
                this.RootListItem.Childs.Add("events-over", _events_over);

                foreach (Event e in this._events) // loop through events.
                {
                    DateTime _filter_start = DateTime.Now.Date.Subtract(new TimeSpan(Settings.Instance.NumberOfDaysToShowEventsOnMainWindow, 0, 0, 0));
                    DateTime _filter_end = DateTime.Now.Date.AddDays(Settings.Instance.NumberOfDaysToShowEventsOnMainWindow);

                    if ((_filter_start <= e.Time.LocalTime) && (e.Time.LocalTime <= _filter_end))
                    {
                        if (e.IsOver) _events_over.Childs.Add(e.EventID, e); // if event is over register it in past-events section.
                        else
                        {
                            if (e.Time.LocalTime.Date == DateTime.Now.Date) _events_today.Childs.Add(e.EventID, e); // if event takes place today, register it in todays-events section.
                            else _events_upcoming.Childs.Add(e.EventID, e); // else register it in upcoming-events section.
                        }
                    }
                }

                this.RootListItem.SetTitle("Events");  // add unread feeds count to root item's title.                               
                Workload.Instance.Step(this);

                this.NotifyUpdateComplete(new PluginUpdateCompleteEventArgs(true));
                this.Updating = false;
            }
        }

        private void OnTimerHit(object source, ElapsedEventArgs e)
        {
            if (!GlobalSettings.Instance.InSleepMode) this.CheckEvents();
        }

        private void CheckEvents()
        {
            foreach (Event _event in this._events) // loop through all events.
            {
                if (!_event.IsOver) // if it's not already over.
                {
                    if (_event.Time.LocalTime.Date == DateTime.Now.Date) // if the event is scheduled for today.
                    {
                        _event.Check(); // check the event
                    }
                }
            }
        }

        private void MenuCalendarClicked(object sender, EventArgs e) // calendars menu handler
        {
            frmCalendar c = new frmCalendar(this._events);
            c.Show();
        }

        #endregion

        #region de-ctor

        protected override void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing) // managed resources
                {
                    this._event_timer.Enabled = false;
                    this._event_timer.Elapsed -= OnTimerHit;
                    this._event_timer.Dispose();
                    this._event_timer = null;
                    foreach (Event e in this._events) { e.Dispose(); }
                    this._events.Clear();
                    this._events = null;
                }
                base.Dispose(disposing);
            }
        }

        #endregion
    }
}
