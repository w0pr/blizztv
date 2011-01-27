using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using BlizzTV.CommonLib.UI;
using BlizzTV.Downloads;

namespace BlizzTV.CommonLib.Updates
{
    public partial class frmUpdater : frmDownload
    {
        private Update _update;

        public frmUpdater(Update update)
        {
            InitializeComponent();
            this._update = update;
        }

        private void frmUpdater_Load(object sender, EventArgs e)
        {
            this.Text = string.Format("Updating to BlizzTV {0}", this._update.Version.ToString());
            this.StartDownload(new Download(this._update.DownloadLink,this._update.FileName));
        }
        
        protected override void OnDownloadComplete(bool success)
        {
            this.AsyncInvokeHandler(() =>
            {
                if (success)
                {
                    this.labelStatus.Text = "Please wait while update is being installed..";
                    this._update.Install();
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Failed downloading the update. Please try again later.", "Update failed!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    this.Close();
                }
            });
        }
    }
}
