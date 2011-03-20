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
using BlizzTV.Utility.Extensions;

namespace BlizzTV.InfraStructure.Modules.Subscriptions.UI
{
    public partial class AddSubscriptionHost : Form
    {
        public Subscription Subscription { get; private set; }

        private readonly AddSubscriptionContainer _hostedForm;

        public AddSubscriptionHost(AddSubscriptionContainer hostableForm)
        {
            InitializeComponent();

            this._hostedForm = hostableForm;
            this._hostedForm.SubscriptionParsed += ResourceParsed;
        }

        private void AddSubscriptionForm_Load(object sender, EventArgs e)
        {
            this._hostedForm.TopLevel = false;
            this._hostedForm.Dock = DockStyle.Fill;
            this._hostedForm.FormBorderStyle = FormBorderStyle.None;
            this.panelHost.Controls.Add(this._hostedForm);
            this._hostedForm.Show();
        }

        private void buttonAdd_Click(object sender, EventArgs e)
        {
            this.buttonAdd.Enabled = false;
            this.loadingAnimation.Active = true;
            this.loadingAnimation.Visible = true;
            this.labelStatus.Visible = true;
            this._hostedForm.TryAdd();
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }

        private void ResourceParsed(object sender, SubscriptionParsedEventArgs args)
        {
            this.AsyncInvokeHandler(() =>
            {
                this.buttonAdd.Enabled = true;
                this.loadingAnimation.Active = false;
                this.loadingAnimation.Visible = false;
                this.labelStatus.Visible = false;

                if (!args.Success) return;

                this.Subscription = args.Subscription;
                this.DialogResult = DialogResult.OK;
                this.Close();
            });
        }
    }
}
