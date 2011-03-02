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
 * $Id: frmCatalog.cs 360 2011-02-08 21:21:46Z shalafiraistlin@gmail.com $
 */

using System;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace BlizzTV.Modules.Subscriptions.Catalog
{
    public partial class CatalogBrowser : Form
    {
        private ISubscriptionConsumer _consumer;
        private Regex _protocolRegex = new Regex("blizztv\\://(?<Module>.*?)/(?<Name>.*?)/(?<Slug>.*)", RegexOptions.Compiled);
        private Timer _notificationTimer = new Timer();

        public CatalogBrowser(ISubscriptionConsumer consumer)
        {
            InitializeComponent();
            this._consumer = consumer;

            this._notificationTimer.Interval = 2000;
            this._notificationTimer.Tick+=NotificationTimer_Tick;
            this.notificationBar.Hide();
        }

        private void Catalog_Load(object sender, EventArgs e)
        {
            this.browser.Navigate(this._consumer.GetCatalogUrl());
        }

        private void browser_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            this.loadingAnimation.LoadingAnimationControl.Active = false;
            this.loadingAnimation.Visible = false;

            this.buttonBack.Enabled = this.browser.CanGoBack;
            this.buttonForward.Enabled = this.browser.CanGoForward;
        }

        private void browser_Navigating(object sender, WebBrowserNavigatingEventArgs e)
        {
            Match match = this._protocolRegex.Match(e.Url.ToString());

            if (match.Success)
            {
                e.Cancel = true;
                this.browser_DocumentCompleted(this, null);
                this._consumer.ConsumeSubscription(e.Url.ToString());
                this.notificationBar.Text = string.Format("Entry '{0}' has been added to your subscriptions.", match.Groups["Name"].Value);
                this.notificationBar.Show(true);
                this._notificationTimer.Enabled=true;
            }
            else
            {
                this.loadingAnimation.LoadingAnimationControl.Active = true;
                this.loadingAnimation.Visible = true;
            }
        }

        void  NotificationTimer_Tick(object sender, EventArgs e)
        {
 	        this._notificationTimer.Enabled=false;
            this.notificationBar.Hide(true);
        }

        private void buttonBack_Click(object sender, EventArgs e)
        {
            if (this.browser.CanGoBack) this.browser.GoBack();
        }

        private void buttonForward_Click(object sender, EventArgs e)
        {
            if (this.browser.CanGoForward) this.browser.GoForward();
        }
    }
}
