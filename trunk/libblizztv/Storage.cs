using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LibBlizzTV
{
    public sealed class Storage
    {
        private string _plugin_identifier;

        public Storage(string Idetifier)
        {
            this._plugin_identifier = Idetifier;
        }

        public void Put(string Key, byte Value)
        {
            Key = string.Format("{0}.{1}", this._plugin_identifier, Key);
            if (Exists(Key)) DB.Instance.Erase(Key);
            DB.Instance.Insert(Key, Value);
        }

        public byte Get(string Key)
        {
            try
            {
                Key = string.Format("{0}.{1}", this._plugin_identifier, Key);
                byte[] value = DB.Instance.Get(Key);
                return value[0];
            }
            catch (Exception e) { throw; }
        }

        public bool Exists(string Key)
        {
            try
            {
                byte[] value = DB.Instance.Get(Key);
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }
    }
}
