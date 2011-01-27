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
 * $Id: frmDownload.cs 272 2010-12-27 11:06:14Z shalafiraistlin@gmail.com $
 */

using System.Windows.Forms;
using BlizzTV.CommonLib.UI;
using BlizzTV.Assets.i18n;

namespace BlizzTV.Downloads
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
            this._download = download;
            this.labelStatistics.Text = i18n.ConnectingDownloadServer;
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

        protected virtual void OnDownloadComplete(bool success) 
        {
            this.progressBar.AsyncInvokeHandler(() =>
                {
                    this.DialogResult = success ? DialogResult.OK : DialogResult.Abort;
                    this.Close();
                });
        }
    }
}
