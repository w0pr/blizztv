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
    public sealed class Database
    {
        private static Database _instance = new Database();
        public static Database Instance { get { return _instance; } }

        private string _storage_folder = "state-storage";
        private PersistentDictionary<string,byte> _dictionary;

        private Database() 
        {
            if (!this.StorageExists()) Directory.CreateDirectory(this._storage_folder);
            this._dictionary=new PersistentDictionary<string,byte>(this._storage_folder);
        }

        private bool StorageExists()
        {
            if (!Directory.Exists(this._storage_folder)) return false;
            else return true;
        }

        public void Put(string key, byte value)
        {
            this._dictionary[key] = value;
        }

        public byte Get(string key)
        {
            if (this._dictionary.ContainsKey(key)) return this._dictionary[key];
            else return 0;
        }

        public bool KeyExists(string key)
        {
            if (this._dictionary.ContainsKey(key)) return true;
            else return false;
        }
    }
}
