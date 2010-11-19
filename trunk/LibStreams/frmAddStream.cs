using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace LibStreams
{
    public partial class frmAddStream : Form
    {
        public frmAddStream()
        {
            InitializeComponent();
        }

        private void frmAddStream_Load(object sender, EventArgs e)
        {
            foreach (KeyValuePair<string, Provider> pair in Providers.Instance.List) { comboBoxProviders.Items.Add(pair.Value.Name); }
            comboBoxProviders.SelectedIndex = 0;
        }

        private void buttonOK_Click(object sender, EventArgs e)
        {
            this.AddStream(txtName.Text, txtSlug.Text, comboBoxProviders.SelectedItem.ToString());
            this.Close();
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        public delegate void AddStreamEventHandler(string Name, string Slug, string Provider);
        public event AddStreamEventHandler OnAddStream;

        private void AddStream(string Name, string Slug, string Provider)
        {
            if (OnAddStream != null) OnAddStream(Name, Slug, Provider);
        }
    }
}
