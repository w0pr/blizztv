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
    public partial class frmAlarm : Form
    {
        private Event _event;

        public frmAlarm(Event Event)
        {
            InitializeComponent();

            this._event = Event;
        }

        private void frmAlarm_Load(object sender, EventArgs e)
        {
            this.LabelEvent.Text = string.Format("Event {0}", this._event.FullTitle);
            this.LabelStatus.Text = string.Format("is about to start in {0} minutes.", this._event.MinutesLeft.ToString());
            this._event.DeleteAlarm();
        }

        private void ButtonView_Click(object sender, EventArgs e)
        {
            frmEventViewer f = new frmEventViewer(this._event);
            f.Show();
        }

        private void ButtonOkay_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
