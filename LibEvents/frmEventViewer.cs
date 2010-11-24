﻿/*    
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
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace LibEvents
{
    public partial class frmEventViewer : Form
    {
        private Event _event;
        private Color EventOverColor = Color.Red;
        private Color EventInProgressColor = Color.Green;
        private Color FutureEventColor = Color.Black;

        public frmEventViewer(Event Event)
        {
            this._event = Event;
            InitializeComponent();
        }

        private void frmEventViewer_Load(object sender, EventArgs e)
        {
            this.Text = string.Format("Event: {0}", this._event.FullTitle);
            this.LabelFullTitle.Text = this._event.FullTitle;
            this.LabelLocalTime.Text = string.Format("({0})", this._event.Time.LocalTime.ToString());
            this.RichTextboxDescription.Text = this._event.Description;

            switch (this._event.Status) // Colorize LabelStatus based on event status
            {
                case EventStatus.OVER:
                    this.LabelStatus.ForeColor = EventOverColor;
                    this.ButtonSetupAlarm.Enabled = false;
                    this.LabelStatus.Text = "Over.";
                    break;
                case EventStatus.IN_PROGRESS:
                    this.LabelStatus.ForeColor = EventInProgressColor;
                    this.ButtonSetupAlarm.Enabled = false;
                    this.LabelStatus.Text = "In progress.";
                    break;
                case EventStatus.UPCOMING:
                    this.LabelStatus.ForeColor = FutureEventColor;
                    this.ButtonSetupAlarm.Enabled = true;
                    this.LabelAlarm.Visible = true;
                    this.LabelStatus.Text = string.Format("{0} to go.", this._event.TimeLeft);
                    break;
            }

            if (this._event.AlarmExists())
            {
                this.LabelAlarm.Text = string.Format("An alarm is set for event {0} minutes before.", this._event.GetAlarmMinutes().ToString());
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
            frmSetupAlarm f = new frmSetupAlarm(this._event);
            f.ShowDialog();
        }
    }
}
