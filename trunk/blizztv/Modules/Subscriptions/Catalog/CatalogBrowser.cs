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
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace BlizzTV.Modules.Subscriptions.Catalog
{
    public partial class CatalogBrowser : Form
    {
        private string _catalogUrl;
        private Regex _protocolRegex = new Regex("blizztv\\://(?<Module>.*?)/(?<SubscriptionName>.*?)/(?<Slug>.*)", RegexOptions.Compiled);

        public CatalogBrowser(string catalogUrl)
        {
            InitializeComponent();
            this._catalogUrl = catalogUrl;
        }

        private void Catalog_Load(object sender, EventArgs e)
        {
            this.browser.Navigate(this._catalogUrl);
        }

        private void WebBrowser_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            this.loadingAnimation.LoadingCircleControl.Active = false;
            this.loadingAnimation.Visible = false;

            if (browser.Document != null) this.Text = browser.Document.Title;
        }

        private void WebBrowser_Navigating(object sender, WebBrowserNavigatingEventArgs e)
        {
            Match match = this._protocolRegex.Match(e.Url.ToString());

            if (match.Success)
            {
                e.Cancel = true;
                this.WebBrowser_DocumentCompleted(this, null);

                string moduleName = match.Groups[1].Value;
                string subscriptionName = match.Groups[2].Value;
                string slug = match.Groups[3].Value;
            }
            else
            {
                this.loadingAnimation.LoadingCircleControl.Active = true;
                this.loadingAnimation.Visible = true;
            }
        }        
    }
}
