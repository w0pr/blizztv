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

namespace LibStreams
{
    public sealed class Providers : IDisposable
    {
        private static readonly Providers _instance = new Providers();
        public static Providers Instance { get { return _instance; } }

        public Dictionary<string, Provider> List = new Dictionary<string, Provider>();
        private bool disposed = false;

        private Providers()
        {
            XDocument xdoc = XDocument.Load("StreamProviders.xml");

            var entries = from provider in xdoc.Descendants("Provider") 
                          select new {
                                Name = provider.Element("Name").Value,
                                Movie = provider.Element("Movie").Value, 
                                FlashVars=provider.Element("FlashVars").Value
                            };

            foreach (var entry in entries)
            {
                this.List.Add(entry.Name, new Provider(entry.Name, entry.Movie,entry.FlashVars));
            }
        }

        ~Providers() { Dispose(false); }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        private void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing) // managed resources
                {
                    this.List.Clear();
                    this.List = null;
                }
                disposed = true;
            }
        }
    }

    public class Provider
    {
        private string _name;
        private string _movie;
        private string _flash_vars;

        public string Name { get { return this._name; } internal set { this._name = value; } }
        public string Movie { get { return this._movie; } internal set { this._movie = value; } }
        public string FlashVars { get { return this._flash_vars; } internal set { this._flash_vars = value; } }

        public Provider(string Name, string Movie, string FlashVars)
        {
            this._name = Name;
            this._movie = Movie;
            this._flash_vars = FlashVars;
        }
    }
}
