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
using LibBlizzTV;
using LibBlizzTV.Utils;

namespace LibEvents
{
    [Plugin("Events", "Events plugin based on TeamLiquid events calendar.", "event_16.png")]
    public class EventsPlugin : Plugin
    {
        private List<Event> _events = new List<Event>();
        private TimeZoneInfo KOREAN_TIME_ZONE { get { TimeZoneInfo zone = TimeZoneInfo.Local; foreach (TimeZoneInfo z in TimeZoneInfo.GetSystemTimeZones()) { if (z.Id == "Korea Standard Time") zone = z; } return zone; } }
        private bool disposed = false;

        public EventsPlugin() { }

        public override void Load(PluginSettings ps)
        {
            EventsPlugin.PluginSettings = ps;

            ListItem root = new ListItem("Events");  
            this.RegisterListItem(root);

            ListItem events_today = new ListItem("Today");
            this.RegisterListItem(events_today, root);

            ListItem events_upcoming = new ListItem("Upcoming");
            this.RegisterListItem(events_upcoming, root);

            ListItem events_past = new ListItem("Past");
            this.RegisterListItem(events_past, root);

            this.RegisterPluginMenuItem(this, new MenuItemEventArgs("Calendar", new EventHandler(MenuCalendarClicked),this.PluginInfo.Attributes.Icon));


            string xml = WebReader.Read("http://www.teamliquid.net/calendar/xml/calendar.xml");
            XDocument xdoc = XDocument.Parse(xml);

            var months = from month_entry in xdoc.Descendants("month")
                          select new
                          {
                              year = month_entry.Attribute("year"),
                              month = month_entry.Attribute("num"),
                              days = from day_entry in month_entry.Descendants("day")
                                     select new
                                     {
                                         day=day_entry.Attribute("num"),
                                         events=from event_entry in day_entry.Descendants("event")
                                                select new 
                                                {
                                                    hour=event_entry.Attribute("hour"),
                                                    minute=event_entry.Attribute("minute"),
                                                    is_over=event_entry.Attribute("over"),
                                                    title=event_entry.Element("title"),
                                                    short_title=event_entry.Element("short-title"),
                                                    description=event_entry.Element("description"),
                                                    event_id=event_entry.Element("event-id")
                                                }
                                     }
                          };

            foreach (var month_entry in months)
                foreach (var day_entry in month_entry.days)
                    foreach (var event_entry in day_entry.events)
                    {
                        Event e = new Event((string)event_entry.short_title, (string)event_entry.title, (string)event_entry.description, (string)event_entry.event_id,(bool)event_entry.is_over, new ZonedDateTime(new DateTime((int)month_entry.year, (int)month_entry.month, (int)day_entry.day, (int)event_entry.hour, (int)event_entry.minute, 0),KOREAN_TIME_ZONE));
                        this._events.Add(e);
                    }


            foreach (Event e in this._events)
            {
                if (e.IsOver) RegisterListItem(e, events_past);
                else
                {
                    if (e.Time.LocalTime.Date == DateTime.Now.Date) RegisterListItem(e, events_today);
                    else RegisterListItem(e, events_upcoming);
                }
            }

            PluginLoadComplete(new PluginLoadCompleteEventArgs(true));            
        }

        private void MenuCalendarClicked(object sender, EventArgs e)
        {
            frmCalendar c = new frmCalendar(this._events);
            c.Show();
        }

        ~EventsPlugin() { Dispose(false); }

        private void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing) // managed resources
                {
                    EventsPlugin.PluginSettings = null;
                    foreach (Event e in this._events) { e.Dispose(); }
                    this._events.Clear();
                    this._events = null;
                }
                disposed = true;
            }
        }
    }
}
