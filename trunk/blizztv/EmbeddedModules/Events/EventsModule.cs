/*    
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
using BlizzTV.EmbeddedModules.Events.Settings;
using BlizzTV.EmbeddedModules.Events.UI;
using BlizzTV.InfraStructure.Modules;
using BlizzTV.InfraStructure.Modules.Settings;
using BlizzTV.Log;
using BlizzTV.Utility.Date;
using BlizzTV.Utility.Extensions;
using BlizzTV.Utility.Imaging;
using BlizzTV.Utility.Web;

namespace BlizzTV.EmbeddedModules.Events
{
    [ModuleAttributes("Events", "E-Sports events tracker.", "_event")]
    public class EventsModule : Module
    {
        private bool _disposed = false;
        private readonly ModuleNode _moduleNode = new ModuleNode("Events");
        private List<Event> _events = new List<Event>(); // list of events.
        private System.Timers.Timer _eventTimer = new System.Timers.Timer(60000); // runs every one minute and check events & alarms.
        private static TimeZoneInfo KoreanTimeZone { get { TimeZoneInfo zone = TimeZoneInfo.Local; foreach (TimeZoneInfo z in TimeZoneInfo.GetSystemTimeZones()) { if (z.Id == "Korea Standard Time") zone = z; } return zone; } } // teamliquid calendar flags events in Korean time.
        private readonly ModuleNode _eventsToday = new ModuleNode("Today"); // today's events item.
        private readonly ModuleNode _eventsUpcoming = new ModuleNode("Upcoming"); // upcoming events item.
        private readonly ModuleNode _eventsOver = new ModuleNode("Past"); // past events item.        
        
        public EventsModule() : base()
        {
            this.CanRenderMenus = true;
            this.CanRenderTreeNodes = true;

            var eventIcon = new NamedImage("event", Assets.Images.Icons.Png._16._event);
            this._eventsToday.Icon = eventIcon;
            this._eventsUpcoming.Icon = eventIcon;
            this._eventsOver.Icon = eventIcon;

            this._moduleNode.Nodes.Add(_eventsToday);
            this._moduleNode.Nodes.Add( _eventsUpcoming);
            this._moduleNode.Nodes.Add(_eventsOver);

            State state = this._moduleNode.State; /* temp */
            State state2 = this._eventsToday.State; /* temp */
            State state3 = this._eventsUpcoming.State; /* temp */
            State state4 = this._eventsOver.State; /* temp */

            this._moduleNode.Menu.Add("calendar", new ToolStripMenuItem("Calendar", Assets.Images.Icons.Png._16.calendar, new EventHandler(MenuCalendarClicked))); // calendar menu in context-menus.
            this._moduleNode.Menu.Add("settings", new ToolStripMenuItem("Settings", Assets.Images.Icons.Png._16.settings, new EventHandler(MenuSettingsClicked)));
        }
        
        /// <summary>
        /// Returns global menu items.
        /// </summary>
        /// <returns></returns>
        public override Dictionary<string, ToolStripMenuItem> GetMenus()
        {
            return new Dictionary<string, ToolStripMenuItem> { { "calendar", new ToolStripMenuItem("Calendar", Assets.Images.Icons.Png._16.calendar, new EventHandler(MenuCalendarClicked)) } };
        }

        public override ModuleNode GetModuleNode()
        {
            return this._moduleNode;
        }

        public override void Startup()
        {
            this.ParseEvents();

            if (!RuntimeConfiguration.Instance.InSleepMode) this.CheckEvents(); // Go check for events.

            _eventTimer.Elapsed += OnTimerHit; // setup timer for event status checks.
            _eventTimer.Enabled = true;
        }

        private void ParseEvents()
        {
            if (this.RefreshingData) return; 
            this.RefreshingData = true;

            Module.UITreeView.AsyncInvokeHandler(() => { this._moduleNode.Text = @"Updating events.."; });
            Workload.WorkloadManager.Instance.Add(1);

            try
            {
                WebReader.Result result = WebReader.Read("http://www.teamliquid.net/calendar/xml/calendar.xml"); // read teamliquid calendar xml.
                if (result.State != WebReader.States.Success)
                {
                    Module.UITreeView.AsyncInvokeHandler(() =>
                    {
                        this._moduleNode.State = State.Error;
                        this._moduleNode.Icon = new NamedImage("error", Assets.Images.Icons.Png._16.error);
                    });
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

                foreach (Event @event in from monthEntry in months
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
                    this._events.Add(@event);
                }
            }

            catch (Exception e)
            {
                LogManager.Instance.Write(LogMessageTypes.Error, string.Format("Events module caught an exception while parsing events: {0}", e));
            }

            Module.UITreeView.AsyncInvokeHandler(() =>
            {
                Module.UITreeView.BeginUpdate();
                foreach (Event @event in from e in this._events
                                let filterStart = DateTime.Now.Date.Subtract(new TimeSpan(ModuleSettings.Instance.NumberOfDaysToShowEventsOnMainWindow, 0, 0, 0))
                                let filterEnd = DateTime.Now.Date.AddDays(ModuleSettings.Instance.NumberOfDaysToShowEventsOnMainWindow)
                                where (filterStart <= e.Time.LocalTime) && (e.Time.LocalTime <= filterEnd)
                                select e)
                {
                    if (@event.IsOver) _eventsOver.Nodes.Add(@event); // if event is over register it in past-events section.
                    else
                    {
                        if (@event.Time.LocalTime.Date == DateTime.Now.Date) _eventsToday.Nodes.Add(@event); // if event takes place today, register it in todays-events section.
                        else _eventsUpcoming.Nodes.Add(@event); // else register it in upcoming-events section.
                    }
                }
                Module.UITreeView.EndUpdate();
                this._moduleNode.Text = @"Events";
            });

            Workload.WorkloadManager.Instance.Step();
            this.RefreshingData = false;
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
            var c = new CalendarForm(this._events);
            c.Show();
        }

        private void MenuSettingsClicked(object sender, EventArgs e)
        {
            var f = new ModuleSettingsHostForm(this.Attributes, this.GetPreferencesForm());
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
