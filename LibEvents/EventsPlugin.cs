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
        #region members

        private List<Event> _events = new List<Event>(); // the items list.
        private TimeZoneInfo KOREAN_TIME_ZONE { get { TimeZoneInfo zone = TimeZoneInfo.Local; foreach (TimeZoneInfo z in TimeZoneInfo.GetSystemTimeZones()) { if (z.Id == "Korea Standard Time") zone = z; } return zone; } } // teamliquid calendar flags events in Korean time.
        private ListItem _root_item = new ListItem("Events");  // root item on treeview.
        private ListItem _events_today_item = new ListItem("Today"); // today's events item.
        private ListItem _events_upcoming_item = new ListItem("Upcoming"); // upcoming events item.
        private ListItem _events_past_item = new ListItem("Past"); // past events item.
        private bool disposed = false;

        #endregion        

        #region ctor

        public EventsPlugin() { }

        #endregion

        #region API handlers

        public override void Load(PluginSettings ps)
        {
            EventsPlugin.PluginSettings = ps;

            this.RegisterListItem(this._root_item); // register root item.           
            this.RegisterListItem(_events_today_item, _root_item); // register today's events item.            
            this.RegisterListItem(_events_upcoming_item, _root_item); // register upcoming events item.          
            this.RegisterListItem(_events_past_item, _root_item); // register past events item.
            this.RegisterPluginMenuItem(this, new NewMenuItemEventArgs("Calendar", new EventHandler(MenuCalendarClicked), this.PluginInfo.Attributes.Icon)); // register calendar menu.            

            PluginLoadComplete(new PluginLoadCompleteEventArgs(ParseEvents())); // parse events.            
        }

        #endregion

        #region internal logic

        private bool ParseEvents()
        {
            bool success = true;

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
                System.Windows.Forms.MessageBox.Show(string.Format("An error occured while parsing TeamLiquid calendar. Please try again. \n\n[Error Details: {0}]", e.Message), "Events Plugin Error", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
            }

            if (success) // if parsing of calendar xml all okay.
            {
                foreach (Event e in this._events) // loop through events.
                {
                    if (e.IsOver) RegisterListItem(e, _events_past_item); // if event is over register it in past-events section.
                    else
                    {
                        if (e.Time.LocalTime.Date == DateTime.Now.Date) RegisterListItem(e, _events_today_item); // if event takes place today, register it in todays-events section.
                        else RegisterListItem(e, _events_upcoming_item); // else register it in upcoming-events section.
                    }
                }
            }

            return success;
        }

        private void MenuCalendarClicked(object sender, EventArgs e) // calendars menu handler
        {
            frmCalendar c = new frmCalendar(this._events);
            c.Show();
        }

        #endregion

        #region de-ctor

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

        #endregion
    }
}
