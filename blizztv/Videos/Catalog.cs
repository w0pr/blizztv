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

namespace BlizzTV.Videos
{
    public class Catalog : CatalogHandler
    {
        #region instance

        private static Catalog _instance = new Catalog();
        public static Catalog Instance { get { return _instance; } }

        #endregion

        private Catalog() : base(typeof(VideoEntry), "http://blizztv.googlecode.com/svn/catalog/videos/catalog.xml") { }
    }

    [Serializable]
    [XmlType("Video")]
    public class VideoEntry : CatalogEntry
    {
        [XmlAttribute("Slug")]
        public string Slug { get; set; }

        [XmlAttribute("Provider")]
        public string Provider { get; set; }

        public override void AddAsSubscription()
        {
            VideoSubscription subscription = new VideoSubscription();
            subscription.Name = this.Name;
            subscription.Provider = this.Provider;
            subscription.Slug = this.Slug;

            Subscriptions.Instance.Add(subscription);
        }
    }
}
