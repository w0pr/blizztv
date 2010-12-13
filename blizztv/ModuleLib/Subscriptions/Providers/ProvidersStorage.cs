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
using System.Xml.Serialization;
using System.Reflection;
using BlizzTV.CommonLib.Logger;

namespace BlizzTV.ModuleLib.Subscriptions.Providers
{
    public sealed class ProvidersStorage
    {
        #region instance

        private static ProvidersStorage _instance = new ProvidersStorage();
        public static ProvidersStorage Instance { get { return _instance; } }

        #endregion

        private Type[] _knownTypes = new[] { typeof(IProvider) };
        
        private List<IProvider> _providers = new List<IProvider>();        
        public List<IProvider> Providers { get { return this._providers; } }

        private ProvidersStorage()
        {
            Log.Instance.Write(LogMessageTypes.Info, "Loading providers database..");
            this.RegisterKnownTypes();
            this.Load();
        }

        private void RegisterKnownTypes()
        {
            foreach (Type t in Assembly.GetEntryAssembly().GetTypes())
            {
                if (t.IsSubclassOf(typeof(IProvider)))
                {
                    Array.Resize(ref this._knownTypes, this._knownTypes.Length + 1);
                    this._knownTypes[this._knownTypes.Length - 1] = t;
                }
            }
        }

        private void Load()
        {
            try
            {
                using (MemoryStream memStream = new MemoryStream(Encoding.UTF8.GetBytes(Properties.Resources.Providers)))
                {
                    XmlSerializer xs = new XmlSerializer(typeof(List<IProvider>), new XmlAttributeOverrides(), this._knownTypes, new XmlRootAttribute("Providers"), "");
                    this._providers = (List<IProvider>)xs.Deserialize(memStream);
                }

            }
            catch (Exception e) 
            { 
                Log.Instance.Write(LogMessageTypes.Error, string.Format("An error occured while loading providers database: {0}", e.ToString()));
                System.Windows.Forms.MessageBox.Show("Providers database is broken. Please re-install BlizzTV to fix.", "Providers Database Broken", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
            }
        }

        public Dictionary<string,IProvider> GetProviders(Type type)
        {
            return this._providers.Where(provider => provider.GetType() == type).ToDictionary(provider => provider.Name);
        }
    }
}
