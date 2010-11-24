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
using System.Xml.Linq;
using System.Text;
using System.Timers;
using LibBlizzTV;
using LibBlizzTV.Utils;

namespace LibEvents
{
    [PluginAttributes("Events", "Events aggregator plugin.", "event_16.png")]
    public class EventsPlugin : Plugin
    {
        #region members

        private List<Event> _events = new List<Event>(); // the items list.
        private TimeZoneInfo KOREAN_TIME_ZONE { get { TimeZoneInfo zone = TimeZoneInfo.Local; foreach (TimeZoneInfo z in TimeZoneInfo.GetSystemTimeZones()) { if (z.Id == "Korea Standard Time") zone = z; } return zone; } } // teamliquid calendar flags events in Korean time.
        private ListItem _root_item = new ListItem("Events");  // root item on treeview.
        private ListItem _events_today_item = new ListItem("Today"); // today's events item.
        private ListItem _events_upcoming_item = new ListItem("Upcoming"); // upcoming events item.
        private ListItem _events_past_item = new ListItem("Past"); // past events item.
        private Timer _event_timer = new Timer(60000); // runs every one minute and check events & alarms.
        private bool disposed = false;

        public static Plugin Instance;

        #endregion        

        #region ctor

        public EventsPlugin(PluginSettings ps)
            : base(ps)
        {
            EventsPlugin.Instance = this;
            this.Menus.Add("calendar", new System.Windows.Forms.ToolStripMenuItem("Calendar", null, new EventHandler(MenuCalendarClicked))); // register calender menu.
            this._root_item.ContextMenus.Add("calendar", new System.Windows.Forms.ToolStripMenuItem("Calendar", null, new EventHandler(MenuCalendarClicked))); // calendar menu in context-menus.
        }

        #endregion

        #region API handlers

        public override void Run()
        {            
            this.RegisterListItem(this._root_item); // register root item.                

            bool success = this.ParseEvents(); // parse events.
            PluginLoadComplete(new PluginLoadCompleteEventArgs(success));
            PluginDataUpdateComplete(new PluginDataUpdateCompleteEventArgs(success));                

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

        private bool ParseEvents()
        {
            bool success = true;

            this.AddWorkload(1);
            this._root_item.SetTitle("Updating events..");

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
                success = false;
                Log.Instance.Write(LogMessageTypes.ERROR, string.Format("EventsPlugin ParseEvents() Error: \n {0}", e.ToString()));                
            }

            if (success) // if parsing of calendar xml all okay.
            {
                List<ListItem> events_today = new List<ListItem>();
                List<ListItem> events_upcoming = new List<ListItem>();
                List<ListItem> events_past = new List<ListItem>();

                this.RegisterListItem(_events_today_item, _root_item); // register today's events item.            
                this.RegisterListItem(_events_upcoming_item, _root_item); // register upcoming events item.          
                this.RegisterListItem(_events_past_item, _root_item); // register past events item.       

                foreach (Event e in this._events) // loop through events.
                {
                    DateTime _filter_start = DateTime.Now.Date.Subtract(new TimeSpan((Settings as Settings).NumberOfDaysToShowEventsOnMainWindow, 0, 0, 0));
                    DateTime _filter_end = DateTime.Now.Date.AddDays((Settings as Settings).NumberOfDaysToShowEventsOnMainWindow);

                    if ((_filter_start <= e.Time.LocalTime) && (e.Time.LocalTime <= _filter_end))
                    {
                        if (e.IsOver) events_past.Add(e); // if event is over register it in past-events section.
                        else
                        {
                            if (e.Time.LocalTime.Date == DateTime.Now.Date) events_today.Add(e); // if event takes place today, register it in todays-events section.
                            else events_upcoming.Add(e); // else register it in upcoming-events section.
                        }
                    }
                }

                if (events_today.Count > 0) this.RegisterListItems(events_today, this._events_today_item);
                if (events_upcoming.Count > 0) this.RegisterListItems(events_upcoming, this._events_upcoming_item);
                if (events_past.Count > 0) this.RegisterListItems(events_past, this._events_past_item);                
            }
            else
            {
                ListItem error = new ListItem("Error parsing TeamLiquid calendar feed.");
                error.SetState(ItemState.ERROR);
                this.RegisterListItem(error, this._root_item);
            }

            this._root_item.SetTitle("Events");  // add unread feeds count to root item's title.                               
            this.StepWorkload();

            return success;
        }

        private void OnTimerHit(object source, ElapsedEventArgs e)
        {
            this.CheckEvents();
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
