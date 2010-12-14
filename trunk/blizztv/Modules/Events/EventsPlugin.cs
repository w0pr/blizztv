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
        private List<Event> _events = new List<Event>(); // the items list.
        private static TimeZoneInfo KoreanTimeZone { get { TimeZoneInfo zone = TimeZoneInfo.Local; foreach (TimeZoneInfo z in TimeZoneInfo.GetSystemTimeZones()) { if (z.Id == "Korea Standard Time") zone = z; } return zone; } } // teamliquid calendar flags events in Korean time.
        private readonly ListItem _eventsToday = new ListItem("Today"); // today's events item.
        private readonly ListItem _eventsUpcoming = new ListItem("Upcoming"); // upcoming events item.
        private readonly ListItem _eventsOver = new ListItem("Past"); // past events item.
        private Timer _eventTimer = new Timer(60000); // runs every one minute and check events & alarms.
        private bool _disposed = false;

        public static EventsPlugin Instance;

        public EventsPlugin() : base()
        {
            EventsPlugin.Instance = this;
            this.RootListItem = new ListItem("Events");
            this.RootListItem.Icon = new NamedImage("event_16", Properties.Resources.event_16);
            this._eventsToday.Icon = new NamedImage("event_16", Properties.Resources.event_16);
            this._eventsUpcoming.Icon = new NamedImage("event_16", Properties.Resources.event_16);
            this._eventsOver.Icon = new NamedImage("event_16", Properties.Resources.event_16); 

            this.Menus.Add("calendar", new System.Windows.Forms.ToolStripMenuItem("Calendar", null, new EventHandler(MenuCalendarClicked))); // register calender menu.
            this.RootListItem.ContextMenus.Add("calendar", new System.Windows.Forms.ToolStripMenuItem("Calendar", null, new EventHandler(MenuCalendarClicked))); // calendar menu in context-menus.
        }

        public override void Run()
        {
            this.ParseEvents();

            if (!GlobalSettings.Instance.InSleepMode) this.CheckEvents(); // Go check for events.

            // setup update timer for event checks
            _eventTimer.Elapsed += OnTimerHit;
            _eventTimer.Enabled = true;
        }

        public override System.Windows.Forms.Form GetPreferencesForm()
        {
            return new frmSettings();
        }

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

                    var months = from monthEntry in xdoc.Descendants("month") // get the events.
                                 select new
                                 {
                                     year = monthEntry.Attribute("year"),
                                     month = monthEntry.Attribute("num"),
                                     days = from dayEntry in monthEntry.Descendants("day")
                                            select new
                                            {
                                                day = dayEntry.Attribute("num"),
                                                events = from eventEntry in dayEntry.Descendants("event")
                                                         select new
                                                         {
                                                             hour = eventEntry.Attribute("hour"),
                                                             minute = eventEntry.Attribute("minute"),
                                                             is_over = eventEntry.Attribute("over"),
                                                             title = eventEntry.Element("title"),
                                                             short_title = eventEntry.Element("short-title"),
                                                             description = eventEntry.Element("description"),
                                                             event_id = eventEntry.Element("event-id")
                                                         }
                                            }
                                 };

                    foreach (var monthEntry in months) // create up the event items.
                        foreach (var dayEntry in monthEntry.days)
                            foreach (var eventEntry in dayEntry.events)
                            {
                                Event e = new Event((string)eventEntry.short_title, (string)eventEntry.title, (string)eventEntry.description, (string)eventEntry.event_id, (bool)eventEntry.is_over, new ZonedDateTime(new DateTime((int)monthEntry.year, (int)monthEntry.month, (int)dayEntry.day, (int)eventEntry.hour, (int)eventEntry.minute, 0), KoreanTimeZone)); // TL calendar flags events in Korean time.
                                this._events.Add(e);
                            }
                }

                catch (Exception e)
                {
                    Log.Instance.Write(LogMessageTypes.Error, string.Format("EventsPlugin ParseEvents() Error: \n {0}", e.ToString()));
                }

                this.RootListItem.Childs.Add("events-today", _eventsToday);
                this.RootListItem.Childs.Add("events-upcoming", _eventsUpcoming);
                this.RootListItem.Childs.Add("events-over", _eventsOver);

                foreach (Event e in this._events) // loop through events.
                {
                    DateTime filterStart = DateTime.Now.Date.Subtract(new TimeSpan(Settings.Instance.NumberOfDaysToShowEventsOnMainWindow, 0, 0, 0));
                    DateTime filterEnd = DateTime.Now.Date.AddDays(Settings.Instance.NumberOfDaysToShowEventsOnMainWindow);

                    if ((filterStart <= e.Time.LocalTime) && (e.Time.LocalTime <= filterEnd))
                    {
                        if (e.IsOver) _eventsOver.Childs.Add(e.EventId, e); // if event is over register it in past-events section.
                        else
                        {
                            if (e.Time.LocalTime.Date == DateTime.Now.Date) _eventsToday.Childs.Add(e.EventId, e); // if event takes place today, register it in todays-events section.
                            else _eventsUpcoming.Childs.Add(e.EventId, e); // else register it in upcoming-events section.
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

        #region de-ctor

        protected override void Dispose(bool disposing)
        {
            if (this._disposed) return;
            if (disposing) // managed resources
            {
                this._eventTimer.Enabled = false;
                this._eventTimer.Elapsed -= OnTimerHit;
                this._eventTimer.Dispose();
                this._eventTimer = null;
                foreach (Event e in this._events) { e.Dispose(); }
                this._events.Clear();
                this._events = null;
            }
            base.Dispose(disposing);
        }

        #endregion
    }
}
