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
using System.Text;
using System.IO;
using System.Xml;
using System.Xml.Serialization;
using System.Reflection;

namespace BlizzTV.ModuleLib.Subscriptions.Providers
{
    public sealed class ProvidersStorage
    {
        private static ProvidersStorage _instance = new ProvidersStorage();
        private Type[] _known_types = new Type[] { typeof(IProvider) };
        private List<IProvider> _providers = new List<IProvider>();

        public static ProvidersStorage Instance { get { return _instance; } }
        public List<IProvider> Providers { get { return this._providers; } }

        private ProvidersStorage()
        {
            this.RegisterKnownTypes();
            this.Load();
        }

        private void RegisterKnownTypes()
        {
            foreach (Type t in Assembly.GetEntryAssembly().GetTypes())
            {
                if (t.IsSubclassOf(typeof(IProvider)))
                {
                    Array.Resize(ref this._known_types, this._known_types.Length + 1);
                    this._known_types[this._known_types.Length - 1] = t;
                }
            }
        }

        private void Load()
        {
            try
            {
                using (MemoryStream memStream = new MemoryStream(Encoding.UTF8.GetBytes(Properties.Resources.Providers)))
                {
                    XmlSerializer xs = new XmlSerializer(typeof(List<IProvider>), new XmlAttributeOverrides(), this._known_types, new XmlRootAttribute("Providers"), "");
                    this._providers = (List<IProvider>)xs.Deserialize(memStream);
                }
                
            }
            catch (Exception e) { }
        }

        public Dictionary<string,IProvider> GetProviders(Type type)
        {
            Dictionary<string,IProvider> results = new Dictionary<string,IProvider>();
            foreach (IProvider provider in this._providers)
            {
                if (provider.GetType() == type) results.Add(provider.Name,provider);
            }
            return results;
        }
    }
}
