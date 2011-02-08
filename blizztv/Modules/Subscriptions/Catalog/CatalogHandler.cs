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
using System.Text;
using System.Xml.Serialization;
using System.IO;
using BlizzTV.Utility.Web;

namespace BlizzTV.Modules.Subscriptions.Catalog
{
    /// <summary>
    /// Provides catalog support for services.
    /// </summary>
    public class CatalogHandler
    {
        private readonly string _catalogUrl; // the catalog url.
        private readonly Type _entryType; // the catalog entry type.
        private List<CatalogEntry> _entries = new List<CatalogEntry>(); // the entries of the catalog.

        protected CatalogHandler(Type entryType, string catalogUrl)
        {
            this._entryType = entryType;
            this._catalogUrl = catalogUrl;
            this.Load();
        }

        private void Load() // loads the catalog from web.
        {
            WebReader.Result result = WebReader.Read(this._catalogUrl);
            if (result.State != WebReader.States.Success) return;

            using (MemoryStream stream = new MemoryStream(Encoding.ASCII.GetBytes(result.Response)))
            {
                XmlSerializer xs = new XmlSerializer(typeof(List<CatalogEntry>), new XmlAttributeOverrides(), new Type[] { this._entryType }, new XmlRootAttribute("Catalog"), "");
                this._entries = (List<CatalogEntry>)xs.Deserialize(stream);
            }
        }

        public bool ShowDialog() // shows a catalog browser window.
        {
            frmCatalog f = new frmCatalog(this._entries);
            f.ShowDialog();
            return f.AddedNewSubscriptions;
        }
    }

    /// <summary>
    /// A catalog entry.
    /// </summary>
    [Serializable]
    [XmlType("Entry")]
    public class CatalogEntry
    {
        /// <summary>
        /// The category of the entry.
        /// </summary>
        [XmlAttribute("Category")]
        public string Category { get; set; }

        /// <summary>
        /// Entry name.
        /// </summary>
        [XmlAttribute("Name")]
        public string Name { get; set; }

        /// <summary>
        /// The descriptin of the entry.
        /// </summary>
        [XmlAttribute("Description")]
        public string Description { get; set; }

        public virtual void AddAsSubscription() { }
    }
}
