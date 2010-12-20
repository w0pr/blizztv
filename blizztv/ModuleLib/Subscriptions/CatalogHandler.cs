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
using System.Xml.Serialization;
using System.Reflection;
using System.IO;
using BlizzTV.CommonLib.Web;

namespace BlizzTV.ModuleLib.Subscriptions
{
    public class CatalogHandler
    {
        private string _catalogUrl;
        private Type _entryType;
        private List<CatalogEntry> _entries = new List<CatalogEntry>();
        protected List<CatalogEntry> Entries { get { return this._entries; } }

        public CatalogHandler(Type entryType, string catalogUrl)
        {
            this._entryType = entryType;
            this._catalogUrl = catalogUrl;
            this.Load();
        }

        private void Load()
        {
            string data = WebReader.Read(this._catalogUrl);
            if (data != null)
            {
                using (MemoryStream stream = new MemoryStream(Encoding.ASCII.GetBytes(data)))
                {
                    XmlSerializer xs = new XmlSerializer(typeof(List<CatalogEntry>), new XmlAttributeOverrides(),new Type[] { this._entryType}, new XmlRootAttribute("Catalog"), "");
                    this._entries = (List<CatalogEntry>)xs.Deserialize(stream);
                }
            }
        }

        public bool ShowDialog()
        {
            frmCatalog f = new frmCatalog(this._entries);
            f.ShowDialog();
            return f.AddedNewSubscriptions;
        }
    }

    [Serializable]
    [XmlType("Entry")]
    public class CatalogEntry
    {
        [XmlAttribute("Category")]
        public string Category { get; set; }

        [XmlAttribute("Name")]
        public string Name { get; set; }

        [XmlAttribute("Description")]
        public string Description { get; set; }

        public virtual void AddAsSubscription() { }
    }
}
