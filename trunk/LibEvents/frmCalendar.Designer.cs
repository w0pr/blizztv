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
            System.Windows.Forms.Calendar.CalendarHighlightRange calendarHighlightRange1 = new System.Windows.Forms.Calendar.CalendarHighlightRange();
            System.Windows.Forms.Calendar.CalendarHighlightRange calendarHighlightRange2 = new System.Windows.Forms.Calendar.CalendarHighlightRange();
            System.Windows.Forms.Calendar.CalendarHighlightRange calendarHighlightRange3 = new System.Windows.Forms.Calendar.CalendarHighlightRange();
            System.Windows.Forms.Calendar.CalendarHighlightRange calendarHighlightRange4 = new System.Windows.Forms.Calendar.CalendarHighlightRange();
            System.Windows.Forms.Calendar.CalendarHighlightRange calendarHighlightRange5 = new System.Windows.Forms.Calendar.CalendarHighlightRange();
            System.Windows.Forms.Calendar.CalendarHighlightRange calendarHighlightRange6 = new System.Windows.Forms.Calendar.CalendarHighlightRange();
            this.MonthView = new System.Windows.Forms.Calendar.MonthView();
            this.Calendar = new System.Windows.Forms.Calendar.Calendar();
            this.StatusStrip = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.StatusStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // MonthView
            // 
            this.MonthView.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)));
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
            this.Calendar.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.Calendar.DefaultStartTime = System.TimeSpan.Parse("08:30:00");
            calendarHighlightRange1.DayOfWeek = System.DayOfWeek.Monday;
            calendarHighlightRange1.EndTime = System.TimeSpan.Parse("18:00:00");
            calendarHighlightRange1.StartTime = System.TimeSpan.Parse("08:00:00");
            calendarHighlightRange2.DayOfWeek = System.DayOfWeek.Tuesday;
            calendarHighlightRange2.EndTime = System.TimeSpan.Parse("18:00:00");
            calendarHighlightRange2.StartTime = System.TimeSpan.Parse("08:00:00");
            calendarHighlightRange3.DayOfWeek = System.DayOfWeek.Wednesday;
            calendarHighlightRange3.EndTime = System.TimeSpan.Parse("18:00:00");
            calendarHighlightRange3.StartTime = System.TimeSpan.Parse("08:00:00");
            calendarHighlightRange4.DayOfWeek = System.DayOfWeek.Thursday;
            calendarHighlightRange4.EndTime = System.TimeSpan.Parse("18:00:00");
            calendarHighlightRange4.StartTime = System.TimeSpan.Parse("08:00:00");
            calendarHighlightRange5.DayOfWeek = System.DayOfWeek.Friday;
            calendarHighlightRange5.EndTime = System.TimeSpan.Parse("18:00:00");
            calendarHighlightRange5.StartTime = System.TimeSpan.Parse("08:00:00");
            calendarHighlightRange6.DayOfWeek = System.DayOfWeek.Saturday;
            calendarHighlightRange6.EndTime = System.TimeSpan.Parse("18:00:00");
            calendarHighlightRange6.StartTime = System.TimeSpan.Parse("08:00:00");
            this.Calendar.HighlightRanges = new System.Windows.Forms.Calendar.CalendarHighlightRange[] {
        calendarHighlightRange1,
        calendarHighlightRange2,
        calendarHighlightRange3,
        calendarHighlightRange4,
        calendarHighlightRange5,
        calendarHighlightRange6};
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