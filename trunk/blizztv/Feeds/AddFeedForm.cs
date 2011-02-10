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

namespace BlizzTV.Feeds
{
    public partial class AddFeedForm : Form
    {
        public readonly FeedSubscription Subscription = new FeedSubscription();

        public AddFeedForm()
        {
            InitializeComponent();
        }

        private void buttonOK_Click(object sender, EventArgs e)
        {
            if (txtName.Text.Trim() == "" || txtURL.Text.Trim() == "")
            {
                MessageBox.Show(i18n.FillTheFeedNameAndUrlFieldsMessage, i18n.AllFieldsRequiredTitle, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (Subscriptions.Instance.Dictionary.ContainsKey(txtURL.Text))
            {
                MessageBox.Show(string.Format(i18n.FeedSubscriptionAlreadyExists, Subscriptions.Instance.Dictionary[txtURL.Text].Name), i18n.SubscriptionExists, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            this.Subscription.Name = txtName.Text;
            this.Subscription.Url = txtURL.Text;

            using (Feed feed = new Feed(this.Subscription))
            {
                if (!feed.IsValid())
                {
                    MessageBox.Show(i18n.ErrorParsingFeedMessage, i18n.ErrorParsingFeedTitle, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }

            this.DialogResult = DialogResult.OK;
            this.Close();  
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}
