using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace LibVideoChannels
{
    public partial class frmAddChannel : Form
    {
        public frmAddChannel()
        {
            InitializeComponent();
        }

        private void frmAddChannel_Load(object sender, EventArgs e)
        {
            foreach (KeyValuePair<string, Provider> pair in Providers.Instance.List) { comboBoxProviders.Items.Add(pair.Value.Name); }
            comboBoxProviders.SelectedIndex = 0;
        }

        private void buttonOK_Click(object sender, EventArgs e)
        {
            using (Channel c = ChannelFactory.CreateChannel(txtName.Text, txtSlug.Text, comboBoxProviders.SelectedItem.ToString()))
            {
                c.Update();
                if (c.Valid)
                {
                    this.AddVideoChannel(txtName.Text, txtSlug.Text, comboBoxProviders.SelectedItem.ToString());
                    this.Close();
                }
                else MessageBox.Show("There was an error parsing the video channel feed. Please check the channel slug and retry.", "Error parsing video channel", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        public delegate void AddVideoChannelEventHandler(string Name, string Slug, string Provider);
        public event AddVideoChannelEventHandler OnAddVideoChannel;

        private void AddVideoChannel(string Name, string Slug, string Provider)
        {
            if (OnAddVideoChannel != null) OnAddVideoChannel(Name, Slug, Provider);
        }
    }
}
