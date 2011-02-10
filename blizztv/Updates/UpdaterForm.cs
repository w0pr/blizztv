/*    
 * Copyright (C) 2010-2011, BlizzTV Project - http://code.google.com/p/blizztv/
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
using System.Windows.Forms;
using BlizzTV.Downloads;
using BlizzTV.Assets.i18n;
using BlizzTV.Utility.Extensions;

namespace BlizzTV.Updates
{
    public partial class UpdaterForm : DownloadForm
    {
        private readonly Update _update;

        public UpdaterForm(Update update)
        {
            if (update == null) return; 
            this._update = update;

            InitializeComponent();
        }

        private void UpdaterForm_Load(object sender, EventArgs e)
        {
            this.Text = string.Format(i18n.DownloadingUpdateTitle, this._update.Version);
            this.StartDownload(new Download(this._update.DownloadLink,this._update.FileName)); // start downloading the update.
        }
        
        protected override void OnDownloadComplete(bool success)
        {
            this.AsyncInvokeHandler(() =>
            {
                if (success)
                {
                    this.labelStatus.Text = i18n.UpdateIsBeingInstalledMessage;
                    this._update.Install(); // start installing the update.
                    this.Close();
                }
                else
                {
                    MessageBox.Show(i18n.FailedDownloadingTheUpdateMessage, i18n.FailedDownloadingTheUpdateTitle, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    this.Close();
                }
            });
        }
    }
}
