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
    public partial class frmSetupAlarm : Form
    {
        private Event _event;

        public frmSetupAlarm(Event Event)
        {
            InitializeComponent();

            byte[] minutes={5,10,15,30,60,90};

            this._event = Event;
            this.Text = string.Format("Setup alarm for event: {0}", this._event.FullTitle);
            this.LabelEventName.Text = this._event.FullTitle;
            this.LabelEventTime.Text = this._event.Time.LocalTime.ToString();
            this.LabelTimeLeft.Text = this._event.StatusText;

            foreach (byte m in minutes)
            {
                if (m < (byte)this._event.MinutesLeft) this.ComboBoxAlertBefore.Items.Add(m);
            }

            if (this.ComboBoxAlertBefore.Items.Count > 0) this.ComboBoxAlertBefore.SelectedIndex = 0;
        }

        private void ButtonCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void ButtonSetup_Click(object sender, EventArgs e)
        {
            if (this.ComboBoxAlertBefore.SelectedIndex != -1)
            {
                if (!this._event.SetupAlarm(byte.Parse(this.ComboBoxAlertBefore.SelectedItem.ToString()))) MessageBox.Show("An alarm already exists for the event!", "Alarm Exists!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                else this.Close();
            }
        }
    }
}
