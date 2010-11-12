namespace LibEvents
{
    partial class frmCalendar
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.Calendar.CalendarHighlightRange calendarHighlightRange19 = new System.Windows.Forms.Calendar.CalendarHighlightRange();
            System.Windows.Forms.Calendar.CalendarHighlightRange calendarHighlightRange20 = new System.Windows.Forms.Calendar.CalendarHighlightRange();
            System.Windows.Forms.Calendar.CalendarHighlightRange calendarHighlightRange21 = new System.Windows.Forms.Calendar.CalendarHighlightRange();
            System.Windows.Forms.Calendar.CalendarHighlightRange calendarHighlightRange22 = new System.Windows.Forms.Calendar.CalendarHighlightRange();
            System.Windows.Forms.Calendar.CalendarHighlightRange calendarHighlightRange23 = new System.Windows.Forms.Calendar.CalendarHighlightRange();
            System.Windows.Forms.Calendar.CalendarHighlightRange calendarHighlightRange24 = new System.Windows.Forms.Calendar.CalendarHighlightRange();
            this.MonthView = new System.Windows.Forms.Calendar.MonthView();
            this.Calendar = new System.Windows.Forms.Calendar.Calendar();
            this.StatusStrip = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.StatusStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // MonthView
            // 
            this.MonthView.FirstDayOfWeek = System.DayOfWeek.Monday;
            this.MonthView.ItemPadding = new System.Windows.Forms.Padding(1, 2, 1, 2);
            this.MonthView.Location = new System.Drawing.Point(-2, 4);
            this.MonthView.MaxSelectionCount = 30;
            this.MonthView.Name = "MonthView";
            this.MonthView.Size = new System.Drawing.Size(190, 449);
            this.MonthView.TabIndex = 0;
            this.MonthView.SelectionChanged += new System.EventHandler<System.Windows.Forms.Calendar.DateRangeChangedEventArgs>(this.MonthView_SelectionChanged);
            // 
            // Calendar
            // 
            this.Calendar.AllowItemDelete = false;
            this.Calendar.AllowItemEdit = false;
            this.Calendar.AllowNew = false;
            this.Calendar.DefaultStartTime = System.TimeSpan.Parse("08:30:00");
            calendarHighlightRange19.DayOfWeek = System.DayOfWeek.Monday;
            calendarHighlightRange19.EndTime = System.TimeSpan.Parse("18:00:00");
            calendarHighlightRange19.StartTime = System.TimeSpan.Parse("08:00:00");
            calendarHighlightRange20.DayOfWeek = System.DayOfWeek.Tuesday;
            calendarHighlightRange20.EndTime = System.TimeSpan.Parse("18:00:00");
            calendarHighlightRange20.StartTime = System.TimeSpan.Parse("08:00:00");
            calendarHighlightRange21.DayOfWeek = System.DayOfWeek.Wednesday;
            calendarHighlightRange21.EndTime = System.TimeSpan.Parse("18:00:00");
            calendarHighlightRange21.StartTime = System.TimeSpan.Parse("08:00:00");
            calendarHighlightRange22.DayOfWeek = System.DayOfWeek.Thursday;
            calendarHighlightRange22.EndTime = System.TimeSpan.Parse("18:00:00");
            calendarHighlightRange22.StartTime = System.TimeSpan.Parse("08:00:00");
            calendarHighlightRange23.DayOfWeek = System.DayOfWeek.Friday;
            calendarHighlightRange23.EndTime = System.TimeSpan.Parse("18:00:00");
            calendarHighlightRange23.StartTime = System.TimeSpan.Parse("08:00:00");
            calendarHighlightRange24.DayOfWeek = System.DayOfWeek.Saturday;
            calendarHighlightRange24.EndTime = System.TimeSpan.Parse("18:00:00");
            calendarHighlightRange24.StartTime = System.TimeSpan.Parse("08:00:00");
            this.Calendar.HighlightRanges = new System.Windows.Forms.Calendar.CalendarHighlightRange[] {
        calendarHighlightRange19,
        calendarHighlightRange20,
        calendarHighlightRange21,
        calendarHighlightRange22,
        calendarHighlightRange23,
        calendarHighlightRange24};
            this.Calendar.Location = new System.Drawing.Point(194, 4);
            this.Calendar.Name = "Calendar";
            this.Calendar.Size = new System.Drawing.Size(570, 448);
            this.Calendar.TabIndex = 1;
            this.Calendar.LoadItems += new System.EventHandler<System.Windows.Forms.Calendar.CalendarLoadEventArgs>(this.Calendar_LoadItems);
            this.Calendar.ItemCreating += new System.EventHandler<System.Windows.Forms.Calendar.CalendarItemCancelEventArgs>(this.Calendar_ItemCreating);
            this.Calendar.ItemClick += new System.EventHandler<System.Windows.Forms.Calendar.CalendarItemEventArgs>(this.Calendar_ItemClick);
            // 
            // StatusStrip
            // 
            this.StatusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel1});
            this.StatusStrip.Location = new System.Drawing.Point(0, 455);
            this.StatusStrip.Name = "StatusStrip";
            this.StatusStrip.Size = new System.Drawing.Size(764, 22);
            this.StatusStrip.TabIndex = 2;
            this.StatusStrip.Text = "statusStrip1";
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(0, 17);
            // 
            // frmCalendar
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(764, 477);
            this.Controls.Add(this.StatusStrip);
            this.Controls.Add(this.Calendar);
            this.Controls.Add(this.MonthView);
            this.Name = "frmCalendar";
            this.Text = "Calendar";
            this.Load += new System.EventHandler(this.frmCalendar_Load);
            this.StatusStrip.ResumeLayout(false);
            this.StatusStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Calendar.MonthView MonthView;
        private System.Windows.Forms.Calendar.Calendar Calendar;
        private System.Windows.Forms.StatusStrip StatusStrip;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
    }
}