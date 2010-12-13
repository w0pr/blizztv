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
using BlizzTV.ModuleLib.Subscriptions;

namespace BlizzTV.Modules.Videos
{
    public sealed class Subscriptions : SubscriptionsHandler
    {
        private static Subscriptions _instance = new Subscriptions();
        public static Subscriptions Instance { get { return _instance; } }

        private Subscriptions() : base(typeof(VideoSubscription)) { }

        public bool Add(VideoSubscription subscription)
        {
            if (!this.Dictionary.ContainsKey(subscription.Slug))
            {
                base.Add(subscription);
                return true;
            }
            return false;
        }

        public Dictionary<string, VideoSubscription> Dictionary
        {
            get
            {
                Dictionary<string, VideoSubscription> dictionary = new Dictionary<string, VideoSubscription>();
                foreach (ISubscription subscription in this.List)
                {
                    dictionary.Add((subscription as VideoSubscription).Slug, (subscription as VideoSubscription));
                }
                return dictionary;
            }
        }
    }

    [Serializable]
    [XmlType("Video")]
    public class VideoSubscription : ISubscription
    {
        [XmlAttribute("Slug")]
        public string Slug { get; set; }

        [XmlAttribute("Provider")]
        public string Provider { get; set; }

        public VideoSubscription() { }
    }
}
