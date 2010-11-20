using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace LibFeeds
{
    public partial class frmAddFeed : Form
    {
        public frmAddFeed()
        {
            InitializeComponent();
        }

        private void frmAddEditFeed_Load(object sender, EventArgs e) { }

        private void buttonOK_Click(object sender, EventArgs e)
        {
            if (txtName.Text.Trim() != "" && txtURL.Text.Trim() != "")
            {
                if (!FeedsPlugin.Instance._feeds.ContainsKey(txtName.Text))
                {
                    using (Feed f = new Feed(txtName.Text, txtURL.Text))
                    {
                        f.Update();
                        if (f.Valid)
                        {
                            this.AddFeed(txtName.Text, txtURL.Text);
                            this.Close();
                        }
                        else MessageBox.Show("There was an error parsing the feed. Please check the feed URL and retry.", "Error parsing feed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else MessageBox.Show(string.Format("A feed already exists with name '{0}', please choose another name and retry.", txtName.Text), "Key exists", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else MessageBox.Show("Please fill the feed name and URL fields!", "All fields required", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        public delegate void AddFeedEventHandler(string Name, string URL);
        public event AddFeedEventHandler OnAddFeed;

        private void AddFeed(string Name, string URL)
        {
            if (OnAddFeed != null) OnAddFeed(Name, URL);
        }
    }
}
