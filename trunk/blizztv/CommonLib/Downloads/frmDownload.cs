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
using BlizzTV.CommonLib.UI;

namespace BlizzTV.CommonLib.Downloads
{
    public partial class frmDownload : Form
    {
        private Download _download;

        public frmDownload()
        {
            InitializeComponent();
        }

        public frmDownload(string title)
        {
            InitializeComponent();
            if (title != string.Empty) this.Text = title;
        }

        public void StartDownload(Download download)
        {
            this.labelStatistics.Text = "Connecting to download server..";
            this._download = download;
            this._download.Progress += OnDownloadProgress;
            this._download.Complete += OnDownloadComplete;
            this._download.Start();
        }

        private void OnDownloadProgress(int progress)
        {
            this.progressBar.AsyncInvokeHandler(() =>
            {
                this.progressBar.Value = progress;
                this.labelStatistics.Text = string.Format("{0} of {1} with {2}. (%{3})", this._download.Downloaded, this._download.Size, this._download.Speed, this._download.DownlodadedPercent);
            });            
        }

        protected virtual void OnDownloadComplete(object sender, EventArgs e) 
        {
            this.progressBar.AsyncInvokeHandler(() =>
            {
                if (this._download.Success) this.DialogResult = System.Windows.Forms.DialogResult.OK;
                else this.DialogResult = System.Windows.Forms.DialogResult.Abort;
                this.Close();
            });
        }
    }
}
