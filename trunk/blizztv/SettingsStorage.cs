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
using System.Runtime.Serialization.Formatters.Binary;
using System.Security.Cryptography;
using LibBlizzTV;

namespace BlizzTV
{
    public sealed class SettingsStorage
    {
        private static SettingsStorage _instance = new SettingsStorage();
        public static SettingsStorage Instance { get { return _instance; } }

        private Settings _settings = new Settings();
        public Settings Settings { get { return this._settings; } }

        private string _storage_file = "settings.storage";
        private byte[] _entropy = { 123, 217, 19, 11, 24, 26, 85, 45, 114, 184, 27, 162, 37, 112, 222, 209, 241, 24, 175, 144, 173, 53, 196, 29, 24, 26, 17, 218, 131, 236, 53, 209 };

        private SettingsStorage()
        {
            try { if (this.StorageExists()) this.Load(); } // load the settings
            catch (Exception e) { DebugConsole.WriteLine(DebugConsole.MessageTypes.ERROR, string.Format("Loading settings failed: {0}", e.Message)); }
        }

        private void Load()
        {
            using (FileStream stream = new FileStream(this._storage_file, FileMode.Open))
            {
                byte[] p_data = new byte[stream.Length];
                stream.Read(p_data, 0, (int)stream.Length);
                stream.Close();
                using (MemoryStream unserialized = new MemoryStream(ProtectedData.Unprotect(p_data, this._entropy, DataProtectionScope.CurrentUser))) // decrypt the data using DPAPI.
                {
                    BinaryFormatter b = new BinaryFormatter();
                    this._settings = (Settings)b.Deserialize(unserialized);
                    unserialized.Close();
                }
            }
        }

        public void Save()
        {
            using (FileStream stream = new FileStream(this._storage_file, FileMode.Create))
            {
                using (MemoryStream serialized = new MemoryStream())
                {
                    BinaryFormatter b = new BinaryFormatter();
                    b.Serialize(serialized, this._settings);
                    byte[] p_data = ProtectedData.Protect(serialized.ToArray(), this._entropy, DataProtectionScope.CurrentUser); // okay this is not the world's *most* secure storage but we still add a protection level using DPAPI (http://msdn.microsoft.com/en-us/library/system.security.cryptography.protecteddata.aspx).
                    stream.Write(p_data, 0, p_data.Length);
                    serialized.Close();
                    stream.Close();
                }
            }
        }

        public bool StorageExists()
        {
            return File.Exists(this._storage_file);
        }
    }

    [Serializable]
    public sealed class Settings
    {
        public GlobalSettings GlobalSettings = new GlobalSettings();
        public Dictionary<String, PluginSettings> PluginSettings = new Dictionary<String, PluginSettings>();

        // UI-specific settings
        public bool EnableDebugLogging = false;
        public bool EnableDebugConsole = false;
    }
}
