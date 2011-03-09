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
using System.Drawing;
using System.Windows.Forms;
using BlizzTV.Assets.i18n;
using BlizzTV.EmbeddedModules.Events.Settings;

namespace BlizzTV.EmbeddedModules.Events.UI
{
    public partial class EventViewerForm : Form
    {
        private readonly Event _event;
        private readonly Color _eventOverColor = Color.Red;
        private readonly Color _eventInProgressColor = Color.Green;
        private readonly Color _futureEventColor = Color.Black;

        public EventViewerForm(Event @event)
        {           
            InitializeComponent();

            this._event = @event;
            this.Size = new Size(ModuleSettings.Instance.EventViewerWindowWidth, ModuleSettings.Instance.EventViewerWindowHeight); // set the size of the window based on last known values.
        }

        private void EventViewerForm_Load(object sender, EventArgs e)
        {
            this.Text = string.Format("Event: {0}", this._event.FullTitle);
            this.LabelFullTitle.Text = this._event.FullTitle;
            this.LabelLocalTime.Text = this._event.Time.LocalTime.ToString();
            this.RichTextboxDescription.Text = this._event.Description;            

            switch (this._event.Status)
            {
                case EventStatus.Over:
                    this.LabelTimeLeft.ForeColor = _eventOverColor;
                    this.ButtonSetupAlarm.Enabled = false;
                    this.LabelTimeLeft.Text = i18n.EventOverMessage;
                    break;

                case EventStatus.InProgress:
                    this.LabelTimeLeft.ForeColor = _eventInProgressColor;
                    this.ButtonSetupAlarm.Enabled = false;
                    this.LabelTimeLeft.Text = i18n.EventInProgressMessage;
                    break;

                case EventStatus.Upcoming:
                    this.LabelTimeLeft.ForeColor = _futureEventColor;
                    this.ButtonSetupAlarm.Enabled = true;
                    this.LabelAlarm.Visible = true;
                    this.PictureAlarmIcon.Visible = true;
                    this.LabelTimeLeft.Text = string.Format(i18n.EventUpcomingMessage, this._event.TimeLeft);
                    break;
            }
            
            if (this._event.AlarmExists())
            {
                this.LabelAlarm.Text = string.Format(i18n.ExistingAlarmMessage, this._event.GetAlarmMinutes());
                this.ButtonSetupAlarm.Enabled = false;
            }
        }

        private void ButtonClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void ButtonSetupAlarm_Click(object sender, EventArgs e)
        {
            if (this._event.MinutesLeft < 5) MessageBox.Show(i18n.CanNotSetupEventAlarmMessage, i18n.CanNotSetupEventAlarmTitle, MessageBoxButtons.OK, MessageBoxIcon.Information); // don't allow settings alarms for events that are just about to start.

            SetupAlarmForm f = new SetupAlarmForm(this._event);
            f.ShowDialog();
        }

        private void RichTextboxDescription_LinkClicked(object sender, LinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start(e.LinkText, null);
        }

        private void frmEventViewer_ResizeEnd(object sender, EventArgs e)
        {
            if (this.Size.Width != ModuleSettings.Instance.EventViewerWindowWidth || this.Size.Height != ModuleSettings.Instance.EventViewerWindowHeight)
            {
                ModuleSettings.Instance.EventViewerWindowWidth = this.Size.Width;
                ModuleSettings.Instance.EventViewerWindowHeight = this.Size.Height;
                ModuleSettings.Instance.Save();
            }
        }
    }
}
