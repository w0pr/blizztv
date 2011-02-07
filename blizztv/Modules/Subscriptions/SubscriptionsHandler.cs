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
 * $Id$
 */

using System;
using System.Collections.Generic;

namespace BlizzTV.Modules.Subscriptions
{
    public class SubscriptionsHandler
    {
        private readonly Type _type;

        public IEnumerable<Subscription> List { get { return SubscriptionsStorage.Instance.GetSubscriptions(this._type); } }

        protected SubscriptionsHandler(Type type)
        {
            this._type = type;
        }

        /// <summary>
        /// Adds a subscription.
        /// </summary>
        /// <param name="subscription">A <see cref="Subscription"/>.</param>
        protected void Add(Subscription subscription)
        {
            SubscriptionsStorage.Instance.Subscriptions.Add(subscription);
            SubscriptionsStorage.Instance.Save();            
        }

        /// <summary>
        /// Removes a subscription.
        /// </summary>
        /// <param name="subscription">A <see cref="Subscription"/>.</param>
        public void Remove(Subscription subscription)
        {
            SubscriptionsStorage.Instance.Subscriptions.Remove(subscription);
            SubscriptionsStorage.Instance.Save();
        }

        /// <summary>
        /// Renames a subscription.
        /// </summary>
        /// <param name="subscription">A <see cref="Subscription"/>.</param>
        /// <param name="name">The new name.</param>
        public void Rename(Subscription subscription, string name)
        {
            subscription.Name = name;
            SubscriptionsStorage.Instance.Save();
        }
    }
}
