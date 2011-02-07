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
 * 
 * $Id$
 */

using System;
using System.Drawing;
using System.Windows.Forms;

namespace BlizzTV.Events
{
    public partial class frmEventViewer : Form
    {
        private readonly Event _event;
        private readonly Color _eventOverColor = Color.Red;
        private readonly Color _eventInProgressColor = Color.Green;
        private readonly Color _futureEventColor = Color.Black;

        public frmEventViewer(Event _event)
        {           
            InitializeComponent();

            this._event = _event;
            this.Size = new Size(BlizzTV.Events.Settings.Instance.EventViewerWindowWidth, BlizzTV.Events.Settings.Instance.EventViewerWindowHeight); // Load the last known size & location for the window.
        }

        private void frmEventViewer_Load(object sender, EventArgs e)
        {
            this.Text = string.Format("Event: {0}", this._event.FullTitle);
            this.LabelFullTitle.Text = this._event.FullTitle;
            this.LabelLocalTime.Text = this._event.Time.LocalTime.ToString();
            this.RichTextboxDescription.Text = this._event.Description;            

            switch (this._event.Status) // Colorize LabelStatus based on event status
            {
                case EventStatus.Over:
                    this.LabelTimeLeft.ForeColor = _eventOverColor;
                    this.ButtonSetupAlarm.Enabled = false;
                    this.LabelTimeLeft.Text = "Over.";
                    break;
                case EventStatus.InProgress:
                    this.LabelTimeLeft.ForeColor = _eventInProgressColor;
                    this.ButtonSetupAlarm.Enabled = false;
                    this.LabelTimeLeft.Text = "In progress.";
                    break;
                case EventStatus.Upcoming:
                    this.LabelTimeLeft.ForeColor = _futureEventColor;
                    this.ButtonSetupAlarm.Enabled = true;
                    this.LabelAlarm.Visible = true;
                    this.PictureAlarmIcon.Visible = true;
                    this.LabelTimeLeft.Text = string.Format("{0} to go.", this._event.TimeLeft);
                    break;
            }
            
            if (this._event.AlarmExists())
            {
                this.LabelAlarm.Text = string.Format("An alarm is set for event {0} minutes before.", this._event.GetAlarmMinutes());
                this.ButtonSetupAlarm.Enabled = false;
            }
        }

        private void RichTextboxDescription_LinkClicked(object sender, LinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start(e.LinkText, null);            
        }

        private void ButtonClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void ButtonSetupAlarm_Click(object sender, EventArgs e)
        {
            if (this._event.MinutesLeft >= 5)
            {
                frmSetupAlarm f = new frmSetupAlarm(this._event);
                f.ShowDialog();
            }
            else MessageBox.Show("You can not setup an alarm for the event as it's just about to start", "Can not setup alarm", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void frmEventViewer_ResizeEnd(object sender, EventArgs e)
        {
            if (this.Size.Width != BlizzTV.Events.Settings.Instance.EventViewerWindowWidth || this.Size.Height != BlizzTV.Events.Settings.Instance.EventViewerWindowHeight)
            {
                BlizzTV.Events.Settings.Instance.EventViewerWindowWidth = this.Size.Width;
                BlizzTV.Events.Settings.Instance.EventViewerWindowHeight = this.Size.Height;
                BlizzTV.Events.Settings.Instance.Save();
            }
        }
    }
}
