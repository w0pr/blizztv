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
