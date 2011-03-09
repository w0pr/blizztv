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
using System.Linq;
using System.Windows.Forms;
using BlizzTV.Assets.i18n;

namespace BlizzTV.EmbeddedModules.Events.UI
{
    public partial class SetupAlarmForm : Form
    {
        private readonly Event _event;
        private static readonly byte[] AlarmPeriods = { 5, 10, 15, 30, 60, 90, 120 };

        public SetupAlarmForm(Event @event)
        {
            InitializeComponent();            
            this._event = @event;
        }

        private void SetupAlarmForm_Load(object sender, EventArgs e)
        {
            this.Text = string.Format(i18n.SetupAlarmTitle, this._event.FullTitle);
            this.LabelEventName.Text = this._event.FullTitle;
            this.LabelEventTime.Text = this._event.Time.LocalTime.ToString();
            this.LabelTimeLeft.Text = this._event.TimeLeft;

            foreach (byte m in AlarmPeriods.Where(m => (double) m < this._event.MinutesLeft))
            {
                this.ComboBoxAlertBefore.Items.Add(m);
            }

            if (this.ComboBoxAlertBefore.Items.Count > 0) this.ComboBoxAlertBefore.SelectedIndex = 0;
        }

        private void ButtonCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void ButtonSetup_Click(object sender, EventArgs e)
        {
            if (this.ComboBoxAlertBefore.SelectedIndex == -1) return;

            if (!this._event.SetupAlarm(byte.Parse(this.ComboBoxAlertBefore.SelectedItem.ToString()))) MessageBox.Show(i18n.ExistingAlarmMessage, i18n.AnAlarmAlreadyExistsForEventTitle, MessageBoxButtons.OK, MessageBoxIcon.Error);
            else this.Close();
        }
    }
}
