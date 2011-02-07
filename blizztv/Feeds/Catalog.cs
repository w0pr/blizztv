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
using System.Xml.Serialization;
using BlizzTV.Modules.Subscriptions;
using BlizzTV.Modules.Subscriptions.Catalog;

namespace BlizzTV.Feeds
{
    public class Catalog : CatalogHandler
    {
        #region instance

        private static Catalog _instance = new Catalog();
        public static Catalog Instance { get { return _instance; } }

        #endregion

        private Catalog() : base(typeof(FeedEntry), "http://blizztv.googlecode.com/svn/catalog/feeds/catalog.xml") { }
    }

    [Serializable]
    [XmlType("Feed")]
    public class FeedEntry : CatalogEntry
    {
        [XmlAttribute("Url")]
        public string Url { get; set; }

        public override void AddAsSubscription()
        {
            FeedSubscription subscription = new FeedSubscription();
            subscription.Name = this.Name;
            subscription.Url = this.Url;

            Subscriptions.Instance.Add(subscription);
        }
    }
}
