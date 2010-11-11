using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Windows.Forms.Calendar;

namespace LibEvents
{
    public partial class frmCalendar : Form
    {
        private List<Event> _events = new List<Event>(); // the events list.

        public frmCalendar(List<Event> Events)
        {
            InitializeComponent();
            this._events = Events; // set the events list.
        }

        private void frmCalendar_Load(object sender, EventArgs e)
        {
            // setup monthview visuals.
            MonthView.MonthTitleTextColor = Color.Navy;
            MonthView.MonthTitleColor = CalendarColorTable.FromHex("#C2DAFC");
            MonthView.ArrowsColor = CalendarColorTable.FromHex("#77A1D3");
            MonthView.DaySelectedBackgroundColor = CalendarColorTable.FromHex("#F4CC52");
            MonthView.DaySelectedTextColor = MonthView.ForeColor;
        }

        private void Calendar_LoadItems(object sender, System.Windows.Forms.Calendar.CalendarLoadEventArgs e) // load the events to calendar.
        {
            foreach (Event _event in this._events) // loop through events.
            {
                CalendarItem c = new CalendarItem(this.Calendar, _event.Time.LocalTime, _event.Time.LocalTime.AddHours(1), _event.FullTitle); // create a calendar item.
                if (Calendar.ViewIntersects(c)) // if the event intersects view calendars current view range (start day-end day).
                {
                    c.Tag = _event; // store event on the tag.
                    c.ToolTipText = _event.FullTitle; // item tooltip.
                    Calendar.Items.Add(c); // add it to calendar.
                }
                else 
                {
                    // we don't need to add this event to calendar as the calendar view does not interesect event date & time. 
                    // (The calendar will explicitly re-ask us the events list when it's view range changes).
                    c = null;
                }
            }
        }

        private void MonthView_SelectionChanged(object sender, System.Windows.Forms.Calendar.DateRangeChangedEventArgs e) // months view range change.
        {
            this.Calendar.SetViewRange(MonthView.SelectionStart, MonthView.SelectionEnd); // update calendar's view range based on the change.
        }

        private void Calendar_ItemCreating(object sender, CalendarItemCancelEventArgs e) 
        {
            e.Cancel = true; // cancel new item creation on calendar.
        }

        private void Calendar_ItemClick(object sender, CalendarItemEventArgs e)
        {
            (e.Item.Tag as Event).DoubleClicked(sender, e);
        }
    }
}
