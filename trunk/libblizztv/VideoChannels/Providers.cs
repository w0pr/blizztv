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
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

namespace LibBlizzTV.VideoChannels
{
    public sealed class Providers
    {
        private static readonly Providers _instance = new Providers();
        public static Providers Instance { get { return _instance; } }

        public Dictionary<string, Provider> List = new Dictionary<string, Provider>();

        private Providers()
        {
            XDocument xdoc = XDocument.Load("VideoProviders.xml");

            var entries = from provider in xdoc.Descendants("Provider")
                          select new
                          {
                              Name = provider.Element("Name").Value,
                              VideoTemplate = provider.Element("VideoTemplate").Value,
                          };

            foreach (var entry in entries)
            {
                this.List.Add(entry.Name, new Provider(entry.Name, entry.VideoTemplate));
            }
        }
    }
}
