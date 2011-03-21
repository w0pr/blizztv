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
using System.Collections.Generic;
using System.Windows.Forms;
using BlizzTV.Assets.i18n;
using BlizzTV.InfraStructure.Modules.Subscriptions.Providers;
using BlizzTV.InfraStructure.Modules.Subscriptions.UI;
using BlizzTV.Utility.Extensions;

namespace BlizzTV.EmbeddedModules.Videos.Settings
{
    public partial class AddChannel : AddSubscriptionContainer
    {
        public AddChannel()
        {
            InitializeComponent();
        }

        private void AddChannel_Load(object sender, EventArgs e)
        {
            toolTip.SetToolTip(this.txtName, "Input the name of video channel.");
            toolTip.SetToolTip(this.comboBoxProviders, "Select the video channel provider.");

            foreach (KeyValuePair<string, Provider> pair in Providers.Instance.Dictionary) { comboBoxProviders.Items.Add(pair.Value.Name); }
            comboBoxProviders.SelectedIndex = 0;
        }

        private void comboBoxProviders_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.AsyncInvokeHandler(() =>
            {
                var provider = (VideoProvider)Providers.Instance.Dictionary[comboBoxProviders.SelectedItem.ToString().ToLower()];
                this.toolTip.SetToolTip(this.txtSlug, string.Format("Input the slug of the video channel. Hint: {0}", provider.URLHint));
            });
        }

        protected override void ParseSubscription()
        {
            if (txtName.Text.Trim() == "" || txtSlug.Text.Trim() == "")
            {
                MessageBox.Show(i18n.FillVideoChannelNameAndUrlFieldsMessage, i18n.AllFieldsRequiredTitle, MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.OnSubscriptionParsed(new SubscriptionParsedEventArgs(false));
                return;
            }

            VideoProvider provider = null;
            this.InvokeHandler(() => { provider = (VideoProvider)Providers.Instance.Dictionary[comboBoxProviders.SelectedItem.ToString().ToLower()]; });
            string channelKey = string.Format("{0}@{1}", txtSlug.Text, provider.Name.ToLower());

            if (Subscriptions.Instance.Dictionary.ContainsKey(channelKey))
            {
                MessageBox.Show(string.Format(i18n.VideoChannelSubscriptionsAlreadyExists, Subscriptions.Instance.Dictionary[channelKey].Name), i18n.SubscriptionExists, MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.OnSubscriptionParsed(new SubscriptionParsedEventArgs(false));
                return;
            }

            try
            {
                var subscription = new VideoSubscription { Name = txtName.Text, Slug = txtSlug.Text, Provider = provider.Name };

                using (var channel = ChannelFactory.CreateChannel(subscription))
                {
                    if (!channel.IsValid())
                    {
                        MessageBox.Show(i18n.ErrorParsingVideoChannelMessage, i18n.ErrorParsingVideoChannelTitle, MessageBoxButtons.OK, MessageBoxIcon.Error);
                        this.OnSubscriptionParsed(new SubscriptionParsedEventArgs(false));
                        return;
                    }
                }

                this.OnSubscriptionParsed(new SubscriptionParsedEventArgs(true, subscription));
            }
            catch (Exception)
            {
                MessageBox.Show(i18n.ErrorParsingVideoChannelMessage, i18n.ErrorParsingVideoChannelTitle, MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.OnSubscriptionParsed(new SubscriptionParsedEventArgs(false));
            }
        }
    }
}
