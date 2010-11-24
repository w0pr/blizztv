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
using System.IO;
using Microsoft.Isam.Esent.Collections.Generic;

namespace LibBlizzTV
{
    /// <summary>
    /// Wrapper for NO-SQL,embedded and key-value typed ESENT database.
    /// </summary>
    public sealed class KeyValueStorage
    {
        #region members

        private static KeyValueStorage _instance = new KeyValueStorage();        
        private string _storage_folder = "storage";
        private PersistentDictionary <string, byte> _dictionary;
        private bool disposed = false;

        /// <summary>
        /// KeyValueStorage instance.
        /// </summary>
        public static KeyValueStorage Instance { get { return _instance; } }

        #endregion

        #region storage API

        /// <summary>
        /// Creates a new storage for the plugin with the given plugin identifier.
        /// </summary>
        private KeyValueStorage()
        {
            if (!this.StorageExists()) Directory.CreateDirectory(this._storage_folder);
            this._dictionary = new PersistentDictionary<string, byte>(this._storage_folder);
        }

        /// <summary>
        /// Puts a new key-value pair in plugin storage.
        /// </summary>
        /// <param name="type">The pair type. </param>
        /// <param name="category">The category.</param>
        /// <param name="key">The key.</param>        
        /// <param name="value">The byte-value</param>
        public void Put(string type, string category, string key, byte value)
        {
            key = string.Format("{0}.{1}.{2}", type,category, key); // construct the key-name based on caller plugin and the category.
            this._dictionary[key] = value;
            this._dictionary.Flush(); // immediatly flush the data to database.
        }

        /// <summary>
        /// Get's the byte-value for supplied key.
        /// </summary>
        /// <param name="type">The pair type. </param>
        /// <param name="category">The category.</param>
        /// <param name="key">The key.</param>
        /// <returns>Returns the byte-value for the supplied key.</returns>
        public byte Get(string type,string category, string key)
        {
            key = string.Format("{0}.{1}.{2}", type, category, key); // construct the key-name based on caller plugin and the category.
            if (this._dictionary.ContainsKey(key)) return this._dictionary[key];
            else return 0;
        }

        /// <summary>
        /// Returns true if the given key-value pair exists in storage.
        /// </summary>
        /// <param name="type">The pair type. </param>
        /// <param name="category">The category.</param>
        /// <param name="key">The key.</param>
        /// <returns>Returns true if the given key-value pair exists in storage.</returns>
        public bool KeyExists(string type,string category, string key)
        {
            key = string.Format("{0}.{1}.{2}", type, category, key); // construct the key-name based on caller plugin and the category.
            if (this._dictionary.ContainsKey(key)) return true;
            else return false;
        }

        /// <summary>
        /// Returns true if the given key-value pair existed in storage and deleted successfuly.
        /// </summary>
        /// <param name="type">The pair type. </param>
        /// <param name="category">The category.</param>
        /// <param name="key">The key.</param>
        /// <returns>Returns true if the given key-value pair existed in storage and deleted successfuly.</returns>
        public bool Delete(string type, string category, string key)
        {
            if (this.KeyExists(type, category, key))
            {
                key = string.Format("{0}.{1}.{2}", type, category, key); // construct the key-name based on caller plugin and the category.
                this._dictionary.Remove(key);
                this._dictionary.Flush();
                return true;
            }
            else return false;
        }

        private bool StorageExists()
        {
            if (!Directory.Exists(this._storage_folder)) return false;
            else return true;
        }

        #endregion

        #region de-ctor

        /// <summary>
        /// De-constructor.
        /// </summary>
        ~KeyValueStorage() { Dispose(false); }

        /// <summary>
        /// Disposes the object.
        /// </summary>
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
                    this._dictionary.Dispose();
                    this._dictionary = null;
                }
                disposed = true;
            }
        }

        #endregion
    }
}
