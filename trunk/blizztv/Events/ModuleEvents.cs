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
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using System.Timers;
using System.Windows.Forms;
using BlizzTV.Configuration;
using BlizzTV.Log;
using BlizzTV.Modules;
using BlizzTV.Modules.Settings;
using BlizzTV.Utility.Date;
using BlizzTV.Utility.Imaging;
using BlizzTV.Utility.Web;

namespace BlizzTV.Events
{
    [ModuleAttributes("Events", "E-Sports events tracker.", "_event")]
    public class ModuleEvents : Module
    {
        private List<Event> _events = new List<Event>(); // list of events.
        private System.Timers.Timer _eventTimer = new System.Timers.Timer(60000); // runs every one minute and check events & alarms.
        private static TimeZoneInfo KoreanTimeZone { get { TimeZoneInfo zone = TimeZoneInfo.Local; foreach (TimeZoneInfo z in TimeZoneInfo.GetSystemTimeZones()) { if (z.Id == "Korea Standard Time") zone = z; } return zone; } } // teamliquid calendar flags events in Korean time.

        private readonly ListItem _eventsToday = new ListItem("Today"); // today's events item.
        private readonly ListItem _eventsUpcoming = new ListItem("Upcoming"); // upcoming events item.
        private readonly ListItem _eventsOver = new ListItem("Past"); // past events item.        
        private bool _disposed = false;
        
        public ModuleEvents() : base()
        {
            NamedImage eventIcon = new NamedImage("event", Assets.Images.Icons.Png._16._event);
            this.RootListItem = new ListItem("Events")
                                    {
                                        Icon=eventIcon
                                    };

            this._eventsToday.Icon = eventIcon;
            this._eventsUpcoming.Icon = eventIcon;
            this._eventsOver.Icon = eventIcon;

            this.Menus.Add("calendar", new ToolStripMenuItem("Calendar", Assets.Images.Icons.Png._16.calendar, new EventHandler(MenuCalendarClicked))); // register calender menu.
            this.RootListItem.ContextMenus.Add("calendar", new ToolStripMenuItem("Calendar", Assets.Images.Icons.Png._16.calendar, new EventHandler(MenuCalendarClicked))); // calendar menu in context-menus.
            this.RootListItem.ContextMenus.Add("settings", new ToolStripMenuItem("Settings", Assets.Images.Icons.Png._16.settings, new EventHandler(MenuSettingsClicked)));
        }

        public override void Run()
        {
            this.ParseEvents();

            if (!RuntimeConfiguration.Instance.InSleepMode) this.CheckEvents(); // Go check for events.

            _eventTimer.Elapsed += OnTimerHit; // setup timer for event status checks.
            _eventTimer.Enabled = true;
        }

        private void ParseEvents()
        {
            if (this.Updating) return; 

            this.Updating = true;
            this.NotifyUpdateStarted();

            Workload.WorkloadManager.Instance.Add(1);
            this.RootListItem.SetTitle("Updating events..");

            try
            {
                WebReader.Result result = WebReader.Read("http://www.teamliquid.net/calendar/xml/calendar.xml"); // read teamliquid calendar xml.
                if (result.State != WebReader.States.Success)
                {
                    this.RootListItem.State = State.Error;
                    this.RootListItem.Icon = new NamedImage("error", Assets.Images.Icons.Png._16.error);
                    this.RootListItem.SetTitle("Events");
                    Workload.WorkloadManager.Instance.Step();
                    return;
                }

                XDocument xdoc = XDocument.Parse(result.Response); // parse the xml.

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

                foreach (Event e in from monthEntry in months
                                    from dayEntry in monthEntry.days
                                    from eventEntry in dayEntry.events
                                    select new Event(
                                        (string)eventEntry.short_title,
                                        (string)eventEntry.title,
                                        (string)eventEntry.description,
                                        (string)eventEntry.event_id,
                                        (bool)eventEntry.is_over,
                                        new ZonedDateTime(
                                            new DateTime((int)monthEntry.year,
                                                (int)monthEntry.month,
                                                (int)dayEntry.day,
                                                (int)eventEntry.hour,
                                                (int)eventEntry.minute,
                                                0),
                                            KoreanTimeZone)))
                {
                    this._events.Add(e);
                }
            }

            catch (Exception e)
            {
                LogManager.Instance.Write(LogMessageTypes.Error, string.Format("Events module caught an exception while parsing events: {0}", e));
            }

            this.RootListItem.Childs.Add("events-today", _eventsToday);
            this.RootListItem.Childs.Add("events-upcoming", _eventsUpcoming);
            this.RootListItem.Childs.Add("events-over", _eventsOver);

            foreach (Event e in from e in this._events
                                let filterStart = DateTime.Now.Date.Subtract(new TimeSpan(Settings.Instance.NumberOfDaysToShowEventsOnMainWindow, 0, 0, 0))
                                let filterEnd = DateTime.Now.Date.AddDays(Settings.Instance.NumberOfDaysToShowEventsOnMainWindow)
                                where (filterStart <= e.Time.LocalTime) && (e.Time.LocalTime <= filterEnd)
                                select e)
            {
                if (e.IsOver) _eventsOver.Childs.Add(e.EventId, e); // if event is over register it in past-events section.
                else
                {
                    if (e.Time.LocalTime.Date == DateTime.Now.Date) _eventsToday.Childs.Add(e.EventId, e); // if event takes place today, register it in todays-events section.
                    else _eventsUpcoming.Childs.Add(e.EventId, e); // else register it in upcoming-events section.
                }
            }

            this.RootListItem.SetTitle("Events");  
            Workload.WorkloadManager.Instance.Step();

            this.NotifyUpdateComplete(new PluginUpdateCompleteEventArgs(true));
            this.Updating = false;
        }

        public override Form GetPreferencesForm()
        {
            return new SettingsForm();
        }

        private void OnTimerHit(object source, ElapsedEventArgs e)
        {
            if (!RuntimeConfiguration.Instance.InSleepMode) this.CheckEvents();
        }

        private void CheckEvents()
        {
            foreach (Event @event in this._events.Where(@event => !@event.IsOver).Where(@event => @event.Time.LocalTime.Date == DateTime.Now.Date)) // only check events that are scheduled for current day and not already over.
            {
                @event.Check(); 
            }
        }

        private void MenuCalendarClicked(object sender, EventArgs e)
        {
            CalendarForm c = new CalendarForm(this._events);
            c.Show();
        }

        private void MenuSettingsClicked(object sender, EventArgs e)
        {
            ModuleSettingsHost f = new ModuleSettingsHost(this.Attributes, this.GetPreferencesForm());
            f.ShowDialog();
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