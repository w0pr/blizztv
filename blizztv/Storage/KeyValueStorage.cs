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
using System.IO;
using BlizzTV.Log;
using Microsoft.Isam.Esent.Collections.Generic;
using BlizzTV.Utility.Helpers;

namespace BlizzTV.Storage
{
    /// <summary>
    /// Provides a key-value based storage.
    /// </summary>
    public sealed class KeyValueStorage
    {
        #region instance

        private static KeyValueStorage _instance = new KeyValueStorage();
        public static KeyValueStorage Instance { get { return _instance; } }

        #endregion

        private readonly string _storageFolder = ApplicationHelper.GetResourcePath("storage");
        private readonly PersistentDictionary<string, byte> _dictionary;        

        private KeyValueStorage()
        {
            if (!this.StorageExists()) Directory.CreateDirectory(this._storageFolder); // create the storage directory if it doesn't exists yet.

            try
            {
                this._dictionary = new PersistentDictionary<string, byte>(this._storageFolder);
            }            
            catch(Microsoft.Isam.Esent.Interop.EsentErrorException e) // The database sanity may not be all okay, if so try to re-create a new database.
            {
                LogManager.Instance.Write(LogMessageTypes.Error, string.Format("An exception occured while trying to inititialize Esent database. Will be creating a new db. Exception: {0}", e));
                Directory.Delete(this._storageFolder,true);
                Directory.CreateDirectory(this._storageFolder);
                this._dictionary = new PersistentDictionary<string, byte>(this._storageFolder);
            }
        }

        public byte GetByte(string key)
        {
            return this.Exists(key) ? this._dictionary[key] : (byte)0;
        }

        public void SetByte(string key, byte value)
        {
            this._dictionary[key] = value;
            this._dictionary.Flush();
        }

        public bool Exists(string key)
        {
            return this._dictionary.ContainsKey(key);
        }

        public bool Delete(string key)
        {
            return this._dictionary.Remove(key);
        }

        private bool StorageExists()
        {
            return Directory.Exists(_storageFolder);
        }
    }
}
