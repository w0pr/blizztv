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

            this._event = Event;
            this.Text = string.Format("Setup alarm for event: {0}", this._event.FullTitle);
            this.LabelEventName.Text = this._event.FullTitle;
            this.LabelEventTime.Text = this._event.Time.LocalTime.ToString();
            this.LabelTimeLeft.Text = this._event.StatusText;
            this.ComboBoxAlertBefore.SelectedIndex = 2;
        }

        private void ButtonCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
