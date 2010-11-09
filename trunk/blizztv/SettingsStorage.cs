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
using LibBlizzTV.Utils;

namespace BlizzTV
{
    public sealed class SettingsStorage : IDisposable
    {
        #region Members 

        private static SettingsStorage _instance = new SettingsStorage();
        public static SettingsStorage Instance { get { return _instance; } }

        private Settings _settings = new Settings(); // the settings
        public Settings Settings { get { return this._settings; } }
        
        private string _storage_file = "settings.storage"; // the settings file
        private byte[] _entropy = { 123, 217, 19, 11, 24, 26, 85, 45, 114, 184, 27, 162, 37, 112, 222, 209, 241, 24, 175, 144, 173, 53, 196, 29, 24, 26, 17, 218, 131, 236, 53, 209 }; // the entropy that will be used by the DPAPI.
        private bool disposed = false;

        #endregion 

        #region ctor

        private SettingsStorage()
        {
            if (this.StorageExists()) this.Load(); // load the saved-settings
            else System.Windows.Forms.MessageBox.Show("Can not read the settings file. BlizzTV will be start with default settings and you have to reconfigure from the Preferences menu.", "Can not read settings file!", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
        }

        #endregion

        #region Logic

        private void Load() // loads the settings
        {
            try
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
            catch (Exception e) 
            {
                System.Windows.Forms.MessageBox.Show("It seems your settings file is corrupted. BlizzTV will be start with default settings and you have to reconfigure from the Preferences menu.", "Error reading settings!", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                File.Delete(this._storage_file); // Delete the corrupted settings file so that even if the user does not re-configure the settings, he should not see this error again.
                Log.Instance.EnableLogger(); // As the settings can not be loaded, enable the logger manually and write the exception details.
                Log.Instance.Write(LogMessageTypes.ERROR, string.Format("Settings file corrupted. \n\nError Details:{0}",e.ToString()));
            }
        }

        public void Save() // saves the settings and protects them using DPAPI
        {
            try
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
            catch (Exception e)
            {
                System.Windows.Forms.MessageBox.Show("Can't save the settings file. Check your permissions.", "Error saving settings!", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                Log.Instance.Write(LogMessageTypes.ERROR, string.Format("Can't save the settings file. \n\nError Details:{0}", e.ToString()));
            }
        }

        public bool StorageExists() // does the storage file exists?
        {
            return File.Exists(this._storage_file);
        }

        #endregion

        #region de-ctor

        ~SettingsStorage() { Dispose(false); }

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
                    this._settings = null;
                }
                disposed = true;
            }
        }

        #endregion
    }

    [Serializable]
    public sealed class Settings
    {
        #region Members

        public GlobalSettings GlobalSettings = new GlobalSettings(); // Global settings that can be used by both plugins and the UI itself.
        public Dictionary<String, PluginSettings> PluginSettings = new Dictionary<String, PluginSettings>(); // Plugin-specific settings

        // UI-specific settings
        public bool EnableDebugLogging = false;
        public bool EnableDebugConsole = false;

        private bool disposed = false;

        #endregion

        #region de-ctor

        ~Settings() { Dispose(false); }

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
                    this.GlobalSettings = null;
                    this.PluginSettings.Clear();
                    this.PluginSettings = null;
                }
                disposed = true;
            }
        }

        #endregion
    }
}
