using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Microsoft.Isam.Esent.Collections.Generic;

namespace BlizzTV.ModuleLib.Subscriptions
{
    internal class Storage
    {
        private readonly string _storage_folder = "subscriptions";
        private PersistentDictionary<string, string> _dictionary;
        private static Storage _instance=new Storage();
        
        public static Storage Instance { get { return _instance; } }
        public PersistentDictionary<string, string> Dictionary { get { return this._dictionary; } }

        private Storage()
        {
            if (!this.StorageExists()) Directory.CreateDirectory(this._storage_folder);
            this._dictionary = new PersistentDictionary<string, string>(this._storage_folder);
        }

        private bool StorageExists()
        {
            if (!Directory.Exists(this._storage_folder)) return false;
            else return true;
        }
    }
}
