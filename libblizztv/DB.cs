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
using Hamster;

namespace LibBlizzTV
{
    public sealed class DB
    {
        private static DB _instance = new DB();
        public static DB Instance { get { return _instance; } }

        private Database _db = new Database();
        private string _storage_file = "storage";

        private DB() 
        {
            // load the database, if not exists create it
            if(!StorageExists()) this._db.Create(this._storage_file);
            else this._db.Open(this._storage_file);
        }

        public void Insert(string Key, byte Value)
        {
            byte[] _key = ASCIIEncoding.ASCII.GetBytes(Key);
            byte[] _value = { Value };
            this._db.Insert(_key, _value);
        }

        public byte[] Get(string Key)
        {
            try
            {
                byte[] _key = ASCIIEncoding.ASCII.GetBytes(Key);
                byte[] _value = this._db.Find(_key);
                return _value;
            }
            catch (Exception e) { throw; }
        }

        public void Erase(string Key)
        {
            byte[] _key = ASCIIEncoding.ASCII.GetBytes(Key);
            this._db.Erase(_key);
        }

        public void Close()
        {
            this._db.Close();
        }

        public bool StorageExists()
        {
            return File.Exists(this._storage_file);
        }
    }
}
