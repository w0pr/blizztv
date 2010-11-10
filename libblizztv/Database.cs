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
    public sealed class Database : IDisposable
    {
        #region Members

        private string _storage_folder = "state-storage";
        private PersistentDictionary<string, byte> _dictionary;
        private bool disposed = false;

        private static Database _instance = new Database();
        /// <summary>
        /// Database instance.
        /// </summary>
        public static Database Instance { get { return _instance; } }

        #endregion

        #region ctor

        private Database() 
        {
            if (!this.StorageExists()) Directory.CreateDirectory(this._storage_folder);
            this._dictionary=new PersistentDictionary<string,byte>(this._storage_folder);
        }

        #endregion

        #region Database Logic

        /// <summary>
        /// Puts a new byte-value on given key.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <param name="value">The byte-value.</param>
        public void Put(string key, byte value)
        {
            this._dictionary[key] = value;
            this._dictionary.Flush(); // immediatly flush the data to database.
        }

        /// <summary>
        /// Gets the byte-value stored on given key.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <returns></returns>
        public byte Get(string key)
        {
            if (this._dictionary.ContainsKey(key)) return this._dictionary[key];
            else return 0;
        }

        /// <summary>
        /// Returns true if the given key-value pair exists.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <returns></returns>
        public bool KeyExists(string key)
        {
            if (this._dictionary.ContainsKey(key)) return true;
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
        ~Database() { Dispose(false); }

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
