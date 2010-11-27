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

namespace LibBlizzTV
{
    /// <summary>
    /// Storage for settings.
    /// </summary>
    public sealed class SettingsStorage : IDisposable
    {
        #region Members 

        private static SettingsStorage _instance = new SettingsStorage();

        /// <summary>
        /// Returns instance of the settings storage.
        /// </summary>
        public static SettingsStorage Instance { get { return _instance; } }

        private OldSettings _settings = new OldSettings(); // the settings

        /// <summary>
        /// The settings stored.
        /// </summary>
        public OldSettings Settings { get { return this._settings; } }
        
        private string _storage_file = "settings"; // the settings file
        private bool disposed = false;

        #endregion 

        #region ctor

        private SettingsStorage()
        {
            #pragma warning disable 0618 // ignore obsolete warning.
            AppDomain.CurrentDomain.AppendPrivatePath("plugins"); // let the app.domain be aware of our custom plugin assemblies folder.           
            #pragma warning restore 0618 // restore obsolete warnings.

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
                    byte[] data = new byte[stream.Length];
                    stream.Read(data, 0, (int)stream.Length);
                    stream.Close();
                    using (MemoryStream mstream = new MemoryStream(data))
                    {
                        BinaryFormatter b = new BinaryFormatter();
                        this._settings = (OldSettings)b.Deserialize(mstream);
                        mstream.Close();
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

        /// <summary>
        /// Saves the settings.
        /// </summary>
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
                        stream.Write(serialized.ToArray(), 0, serialized.ToArray().Length);
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

        /// <summary>
        /// Does settings storage exists?
        /// </summary>
        /// <returns>Returns true if settings storage exists</returns>
        public bool StorageExists() // does the storage file exists?
        {
            return File.Exists(this._storage_file);
        }

        #endregion

        #region de-ctor

        /// <summary>
        /// de-ctor.
        /// </summary>
        ~SettingsStorage() { Dispose(false); }

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
                    this._settings = null;
                }
                disposed = true;
            }
        }

        #endregion
    }

    /// <summary>
    /// Holds settings.
    /// </summary>
    [Serializable]
    public sealed class OldSettings
    {
        #region Members

        /// <summary>
        /// Holds a list of plugin settings.
        /// </summary>
        public Dictionary<String, PluginSettings> PluginSettings = new Dictionary<String, PluginSettings>(); // Plugin-specific settings

        private bool disposed = false;

        #endregion

        #region de-ctor

        /// <summary>
        /// de-ctor.
        /// </summary>
        ~OldSettings() { Dispose(false); }

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
                    this.PluginSettings.Clear();
                    this.PluginSettings = null;
                }
                disposed = true;
            }
        }

        #endregion
    }
}
