/*    
 * Copyright (C) 2010, BlizzTV Project - http://code.google.com/p/blizztv/
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
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using BlizzTV.CommonLib.Downloads;
using BlizzTV.CommonLib.UI;

namespace BlizzTV.CommonLib.Updates
{
    public partial class frmUpdater : Form
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
            this.Download();
        }

        private void Download()
        {
            Download download = new Download(this._update.DownloadLink);
            download.Progress += new Download.DownloadProgressEventHandler(DownloadProgress);
            download.Complete += new EventHandler(OnDownloadComplete);
            download.Start(this._update.FileName);
        }

        void DownloadProgress(int progress)
        {
            this.progressBarUpdater.AsyncInvokeHandler(() => { this.progressBarUpdater.Value = progress; });
        }

        private void OnDownloadComplete(object sender, EventArgs e)
        {
            this.progressBarUpdater.AsyncInvokeHandler(() =>
            {
                if ((sender as Download).Success) 
                {
                    this.LabelStatus.Text = "Please wait while update is being installed..";
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
