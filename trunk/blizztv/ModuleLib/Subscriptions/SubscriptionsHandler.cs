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
using System.Xml.Serialization;

namespace BlizzTV.ModuleLib.Subscriptions
{
    public class SubscriptionsHandler
    {
        private readonly Type _type;

        public List<ISubscription> List { get { return SubscriptionsStorage.Instance.GetSubscriptions(this._type); } }

        public SubscriptionsHandler(Type type)
        {
            this._type = type;
        }

        public void Add(ISubscription subscription)
        {
            SubscriptionsStorage.Instance.Subscriptions.Add(subscription);
            SubscriptionsStorage.Instance.Save();            
        }

        public void Remove(ISubscription subscription)
        {
            SubscriptionsStorage.Instance.Subscriptions.Remove(subscription);
            SubscriptionsStorage.Instance.Save();
        }        
    }

    [Serializable]
    [XmlType("Subscription")]
    public class ISubscription
    {
        [XmlAttribute("Name")]
        public string Name { get; set; }
    }
}
