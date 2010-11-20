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
            if (txtName.Text.Trim() != "" && txtSlug.Text.Trim() != "")
            {
                if (!StreamsPlugin.Instance._streams.ContainsKey(txtName.Text))
                {
                    this.AddStream(txtName.Text, txtSlug.Text, comboBoxProviders.SelectedItem.ToString());
                    this.Close();
                }
                else MessageBox.Show(string.Format("A stream already exists with name '{0}', please choose another name and retry.", txtName.Text), "Key exists", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else MessageBox.Show("Please fill the stream name and slug fields!", "All fields required", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
