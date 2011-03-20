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
using BlizzTV.Assets.i18n;
using BlizzTV.InfraStructure.Modules.Subscriptions.UI;

namespace BlizzTV.EmbeddedModules.Feeds.Settings
{
    public partial class AddFeed : AddSubscriptionContainer
    {
        public AddFeed()
        {
            InitializeComponent();
        }

        private void AddFeed_Load(object sender, EventArgs e)
        {
            toolTip.SetToolTip(this.txtName, "Input the name of feed.");
            toolTip.SetToolTip(this.txtURL, "Input the URL for the feed source - for example; http://www.teamliquid.net/rss/news.xml.");
        }

        protected override void ParseSubscription()
        {
            if (txtName.Text.Trim() == "" || txtURL.Text.Trim() == "")
            {
                MessageBox.Show(i18n.FillFeedNameAndUrlFieldsMessage, i18n.AllFieldsRequiredTitle, MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.OnSubscriptionParsed(new SubscriptionParsedEventArgs(false));
                return;
            }

            if (Subscriptions.Instance.Dictionary.ContainsKey(txtURL.Text))
            {
                MessageBox.Show(string.Format(i18n.FeedSubscriptionAlreadyExists, Subscriptions.Instance.Dictionary[txtURL.Text].Name), i18n.SubscriptionExists, MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.OnSubscriptionParsed(new SubscriptionParsedEventArgs(false));
                return;
            }

            try
            {
                var subscription = new FeedSubscription {Name = txtName.Text, Url = txtURL.Text};

                using (var feed = new Feed(subscription))
                {
                    if (!feed.IsValid())
                    {
                        MessageBox.Show(i18n.ErrorParsingFeedMessage, i18n.ErrorParsingFeedTitle, MessageBoxButtons.OK, MessageBoxIcon.Error);
                        this.OnSubscriptionParsed(new SubscriptionParsedEventArgs(false));
                        return;
                    }
                }

                this.OnSubscriptionParsed(new SubscriptionParsedEventArgs(true, subscription));
            }
            catch(Exception)
            {
                MessageBox.Show(i18n.ErrorParsingFeedMessage, i18n.ErrorParsingFeedTitle, MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.OnSubscriptionParsed(new SubscriptionParsedEventArgs(false));
            }
        }
    }
}
