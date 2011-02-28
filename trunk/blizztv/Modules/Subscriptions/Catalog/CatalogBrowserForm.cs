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
using System.Text.RegularExpressions;

namespace BlizzTV.Modules.Subscriptions.Catalog
{
    public partial class CatalogBrowserForm : Form
    {
        private string _catalogUrl;

        private Regex _protocolRegex = new Regex("blizztv\\://(?<Module>.*?)/(?<SubscriptionName>.*?)/(?<Slug>.*)", RegexOptions.Compiled);

        public CatalogBrowserForm(string catalogUrl)
        {
            InitializeComponent();

            this._catalogUrl = catalogUrl;
        }

        private void frmCatalogBrowser_Load(object sender, EventArgs e)
        {
            this.Browser.Navigate(this._catalogUrl);
        }

        private void Browser_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            this.ProgressBar.Visible = false;
            this.LoadingAnimation.LoadingCircleControl.Active = false;
            this.LoadingAnimation.Visible = false;

            if (Browser.Document != null) this.Text = Browser.Document.Title;
        }

        private void Browser_ProgressChanged(object sender, WebBrowserProgressChangedEventArgs e)
        {
            if (this.ProgressBar.Maximum != e.MaximumProgress) this.ProgressBar.Maximum = (int) e.MaximumProgress;
            if (e.CurrentProgress >= 0 & e.CurrentProgress<= this.ProgressBar.Maximum) this.ProgressBar.Value = (int) e.CurrentProgress;
        }

        private void Browser_Navigating(object sender, WebBrowserNavigatingEventArgs e)
        {
            Match match = this._protocolRegex.Match(e.Url.ToString());

            if (match.Success)
            {
                e.Cancel = true;
                this.Browser_DocumentCompleted(this, null);

                string moduleName = match.Groups[1].Value;
                string subscriptionName = match.Groups[2].Value;
                string slug = match.Groups[3].Value;
            }
            else
            {
                this.ProgressBar.Value = 0;
                this.ProgressBar.Visible = true;
                this.LoadingAnimation.LoadingCircleControl.Active = true;
                this.LoadingAnimation.Visible = true;
            }
        }
    }
}
