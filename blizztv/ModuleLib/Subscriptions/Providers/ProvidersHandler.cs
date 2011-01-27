﻿/*    
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

namespace BlizzTV.ModuleLib.Subscriptions.Providers
{
    public class ProvidersHandler
    {
        private readonly Type _type;

        public Dictionary<string, IProvider> Dictionary { get { return ProvidersStorage.Instance.GetProviders(this._type); } }

        public ProvidersHandler(Type type)
        {
            this._type = type;
        }        
    }

    [Serializable]
    [XmlType("Provider")]
    public class IProvider
    {
        [XmlAttribute("Name")]
        public string Name { get; set; }
    }
}