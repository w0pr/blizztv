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
        private List<Event> _events = new List<Event>();

        public frmCalendar(List<Event> Events)
        {
            InitializeComponent();
            this._events = Events;
        }

        private void frmCalendar_Load(object sender, EventArgs e)
        {
            MonthView.MonthTitleTextColor = Color.Navy;
            MonthView.MonthTitleColor = CalendarColorTable.FromHex("#C2DAFC");
            MonthView.ArrowsColor = CalendarColorTable.FromHex("#77A1D3");
            MonthView.DaySelectedBackgroundColor = CalendarColorTable.FromHex("#F4CC52");
            MonthView.DaySelectedTextColor = MonthView.ForeColor;
        }

        private void Calendar_LoadItems(object sender, System.Windows.Forms.Calendar.CalendarLoadEventArgs e)
        {
            foreach (Event _event in this._events)
            {
                CalendarItem c = new CalendarItem(this.Calendar, _event.Time.LocalTime, _event.Time.LocalTime.AddHours(1), _event.FullTitle);
                if(Calendar.ViewIntersects(c)) Calendar.Items.Add(c);
                c.ToolTipText = _event.FullTitle;
            }
        }

        private void MonthView_SelectionChanged(object sender, System.Windows.Forms.Calendar.DateRangeChangedEventArgs e)
        {
            this.Calendar.SetViewRange(MonthView.SelectionStart, MonthView.SelectionEnd);
        }

        private void Calendar_ItemCreating(object sender, CalendarItemCancelEventArgs e)
        {
            e.Cancel = true;
        }
    }
}
