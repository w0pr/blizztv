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
using System.Text;
using System.Xml.Linq;

namespace LibBlizzTV.Streams
{
    public sealed class Streams
    {
        private static readonly Streams _instance = new Streams();
        public static Streams Instance { get { return _instance; } }

        public Dictionary<string, Stream> List = new Dictionary<string, Stream>();

        private Streams()
        {
            XDocument xdoc = XDocument.Load("Streams.xml");

            var entries = from stream in xdoc.Descendants("Stream")
                          select new
                          {
                              Name = stream.Element("Name").Value,
                              ID=stream.Element("ID").Value,
                              Provider = stream.Element("Provider").Value,
                          };

            foreach (var entry in entries)
            {                
                this.List.Add(entry.Name, StreamFactory.CreateStream(entry.ID, entry.Name, entry.Provider));
            }
        }
    }
}
