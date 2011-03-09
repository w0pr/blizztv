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
using System.Drawing;
using System.Windows.Forms;
using System.Windows.Forms.Calendar;
using BlizzTV.EmbeddedModules.Events.Settings;

namespace BlizzTV.EmbeddedModules.Events.UI
{
    public partial class CalendarForm : Form
    {
        private readonly List<Event> _events = new List<Event>(); // the events list.

        public CalendarForm(List<Event> events)
        {
            InitializeComponent();

            this._events = events;
            this.Size = new Size(ModuleSettings.Instance.CalendarWindowWidth, ModuleSettings.Instance.CalendarWindowHeight); // set the size of the window based on last known values.
        }

        private void CalendarForm_Load(object sender, EventArgs e)
        {
            /* setup monthview control */
            this.MonthView.MonthTitleTextColor = Color.Navy;
            this.MonthView.MonthTitleColor = CalendarColorTable.FromHex("#C2DAFC");
            this.MonthView.ArrowsColor = CalendarColorTable.FromHex("#77A1D3");
            this.MonthView.DaySelectedBackgroundColor = CalendarColorTable.FromHex("#F4CC52");
            this.MonthView.DaySelectedTextColor = MonthView.ForeColor;
        }

        private void Calendar_LoadItems(object sender, CalendarLoadEventArgs e) // loads events to calendar-view.
        {
            foreach (Event @event in this._events) // loop through events.
            {
                CalendarItem c = new CalendarItem(this.Calendar, @event.Time.LocalTime, @event.Time.LocalTime.AddHours(1), @event.FullTitle); // create a calendar item.
                
                if (Calendar.ViewIntersects(c)) // if the event intersects view calendars current view range (start day-end day).
                {
                    c.Tag = @event; // store event on the tag.
                    c.ToolTipText = @event.FullTitle; // item tooltip.
                    Calendar.Items.Add(c); // add it to calendar.
                }

                // we don't need to add this event to calendar as the calendar view does not interesect event date & time. 
                // (The calendar will explicitly re-ask us the events list when it's view range changes).
                else c = null; 
            }
        }

        private void MonthView_SelectionChanged(object sender, DateRangeChangedEventArgs e) // months view range change.
        {
            this.Calendar.SetViewRange(MonthView.SelectionStart, MonthView.SelectionEnd); // update calendar's view range based on the change.
        }

        private void Calendar_ItemCreating(object sender, CalendarItemCancelEventArgs e) 
        {
            e.Cancel = true; // cancel new item creation by user on calendar.
        }

        private void Calendar_ItemDoubleClick(object sender, CalendarItemEventArgs e)
        {
            ((Event)e.Item.Tag).Open(sender, e);
        }

        private void CalendarForm_ResizeEnd(object sender, EventArgs e)
        {
            if (this.Size.Width != ModuleSettings.Instance.CalendarWindowWidth || this.Size.Height != ModuleSettings.Instance.CalendarWindowHeight)
            {
                ModuleSettings.Instance.CalendarWindowWidth = this.Size.Width;
                ModuleSettings.Instance.CalendarWindowHeight = this.Size.Height;
                ModuleSettings.Instance.Save();
            }
        }
    }
}
