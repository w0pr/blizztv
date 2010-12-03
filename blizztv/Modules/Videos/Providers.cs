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
using System.Linq;
using System.Xml.Linq;
using System.Xml.Serialization;
using BlizzTV.CommonLib.Logger;
using BlizzTV.ModuleLib.Subscriptions.Providers;

namespace BlizzTV.Modules.Videos
{
    [Serializable]
    [XmlType("Video")]
    public class Provider : IProvider
    {
        [XmlAttribute("Movie")]
        public string Movie { get; set; }

        [XmlAttribute("FlashVars")]
        public string FlashVars { get; set; }

        public Provider() { }
    }

    public sealed class Providers : ProvidersHandler
    {
        private static Providers _instance = new Providers();
        public static Providers Instance { get { return _instance; } }

        private Providers() : base(typeof(Provider)) { }
    }
}
