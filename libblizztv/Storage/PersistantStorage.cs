using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Microsoft.Isam.Esent.Collections.Generic;

namespace LibBlizzTV.Storage
{
    /// <summary>
    /// 
    /// </summary>
    public class PersistantStorage
    {     
        private static PersistantStorage _instance = new PersistantStorage();
        private string _storage_folder = "storage";
        private PersistentDictionary<string, byte> _dictionary;

        
        /// <summary>
        /// 
        /// </summary>
        public static PersistantStorage Instance { get { return _instance; } }

        /// <summary>
        /// 
        /// </summary>
        private PersistantStorage()
        {
            if (!this.StorageExists()) Directory.CreateDirectory(this._storage_folder);
            this._dictionary = new PersistentDictionary<string, byte>(this._storage_folder);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="category"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public byte GetByte(string category, string key)
        {
            key = string.Format("{0}.{1}.{2}", System.Reflection.Assembly.GetCallingAssembly().GetName().Name, category, key);
            if (this._dictionary.ContainsKey(key)) return this._dictionary[key];
            else return 0;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="category"></param>
        /// <param name="key"></param>
        /// <param name="value"></param>
        public void PutByte(string category, string key, byte value)
        {
            key = string.Format("{0}.{1}.{2}", System.Reflection.Assembly.GetCallingAssembly().GetName().Name, category, key);
            this._dictionary[key] = value;
            this._dictionary.Flush(); // immediatly flush the data to database.
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="category"></param>
        /// <param name="key"></param>
        public bool EntryExists(string category, string key)
        {
            key = string.Format("{0}.{1}.{2}", System.Reflection.Assembly.GetCallingAssembly().GetName().Name, category, key);
            if (this._dictionary.ContainsKey(key)) return true;
            else return false;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="category"></param>
        /// <param name="key"></param>
        public void Delete(string category, string key)
        {
            if (this.EntryExists(category,key))
            {
                key = string.Format("{0}.{1}.{2}", System.Reflection.Assembly.GetCallingAssembly().GetName().Name, category, key);
                this._dictionary.Remove(key);
                this._dictionary.Flush();
            }
        }

        private bool StorageExists()
        {
            if (!Directory.Exists(this._storage_folder)) return false;
            else return true;
        }
    }
}
