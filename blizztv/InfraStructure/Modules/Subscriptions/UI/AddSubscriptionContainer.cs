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

namespace BlizzTV.InfraStructure.Modules.Subscriptions.UI
{
    public partial class AddSubscriptionContainer : Form
    {
        public AddSubscriptionContainer()
        {
            InitializeComponent();
        }

        public void TryAdd()
        {
            var thread = new System.Threading.Thread(this.ParseSubscription) { IsBackground = true };
            thread.Start();
        }

        protected virtual void ParseSubscription()
        {
            throw new NotSupportedException();
        }

        public EventHandler<SubscriptionParsedEventArgs> SubscriptionParsed;

        public void OnSubscriptionParsed(SubscriptionParsedEventArgs args)
        {
            EventHandler<SubscriptionParsedEventArgs> handler = SubscriptionParsed;
            if (handler != null) handler(this, args);
        }
    }

    public class SubscriptionParsedEventArgs : EventArgs
    {
        public bool Success { get; private set; }
        public Subscription Subscription { get; private set; }

        public SubscriptionParsedEventArgs(bool success, Subscription subscription=null)
        {
            this.Success = success;
            this.Subscription = subscription;
        }
    }
}
