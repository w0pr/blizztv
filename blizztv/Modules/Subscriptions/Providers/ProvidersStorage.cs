/*    
 * Copyright (C) 2010-2011, BlizzTV Project - http://code.google.com/p/blizztv/
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
using System.Windows.Forms;
using BlizzTV.Log;
using BlizzTV.Assets.i18n;

namespace BlizzTV.Modules.Subscriptions.Providers
{
    /// <summary>
    /// Provides a XML based storage for subscription-providers.
    /// </summary>
    public sealed class ProvidersStorage
    {
        #region instance

        private static ProvidersStorage _instance = new ProvidersStorage();
        public static ProvidersStorage Instance { get { return _instance; } }

        #endregion

        private Type[] _knownTypes = new[] { typeof(Provider) }; // known types that implements Provider.
        private List<Provider> _providers = new List<Provider>(); // the internal list of providers.

        private ProvidersStorage()
        {
            LogManager.Instance.Write(LogMessageTypes.Info, "Loading providers database..");
            this.RegisterKnownTypes();
            this.Load();
        }

        private void RegisterKnownTypes() // loads & register known types that implements Providers.
        {
            foreach (Type t in Assembly.GetEntryAssembly().GetTypes().Where(t => t.IsSubclassOf(typeof (Provider)))) 
            {
                Array.Resize(ref this._knownTypes, this._knownTypes.Length + 1); 
                this._knownTypes[this._knownTypes.Length - 1] = t;
            }
        }

        private void Load() // loads the providers from xml storage.
        {
            try
            {
                using (MemoryStream memStream = new MemoryStream(Encoding.UTF8.GetBytes(Assets.XML.Subscriptions.Providers)))
                {
                    XmlSerializer xs = new XmlSerializer(typeof(List<Provider>), new XmlAttributeOverrides(), this._knownTypes, new XmlRootAttribute("Providers"), "");                    
                    this._providers = (List<Provider>)xs.Deserialize(memStream);
                }

            }
            catch (Exception e) 
            { 
                LogManager.Instance.Write(LogMessageTypes.Error, string.Format("An exception occured while loading providers database: {0}", e));
                MessageBox.Show(i18n.ErrorLoadingProvidersDatabaseMessage, i18n.ErrorLoadingProvidersDatabaseTitle, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Returns a dictionary of providers based on supplied provider type.
        /// </summary>
        /// <param name="type">The provider type.</param>
        /// <returns>Dictionary of providers based on provided provider-type.</returns>
        public Dictionary<string,Provider> GetProviders(Type type)
        {
            return this._providers.Where(provider => provider.GetType() == type).ToDictionary(provider => provider.Name);
        }
    }
}
