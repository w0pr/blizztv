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
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using LibBlizzTV.Utils;

namespace LibBlizzTV
{
    /// <summary>
    /// The plugin manager responsible of organizing the plugins and stuff.
    /// </summary>
    public sealed class PluginManager : IDisposable
    {
        #region members

        private static readonly PluginManager _instance = new PluginManager();
        /// <summary>
        /// The plugin manager instance.
        /// </summary>
        public static PluginManager Instance { get { return _instance; } }

        // TODO: shoud be a readonly collection.
        /// <summary>
        /// The available valid plugin's list.
        /// </summary>
        public Dictionary<string, PluginInfo> Plugins = new Dictionary<string, PluginInfo>(); 

        /// <summary>
        /// The plugin-manager's so the LibBlizzTV's assembly name.
        /// </summary>
        public string AssemblyName { get { return Assembly.GetExecutingAssembly().GetName().Name; } }

        /// <summary>
        /// The plugin-manager's so the LibBlizzTV's assembly version.
        /// </summary>
        public string AssemblyVersion { get { return Assembly.GetExecutingAssembly().GetName().Version.ToString(); } }
        private bool disposed = false;

        #endregion

        #region internal logic 

        private PluginManager()
        {
            Log.Instance.Write(LogMessageTypes.INFO, string.Format("Plugin manager - ({0}) initialized..", this.GetType().Module.Name)); // log the plugin-manager startup message.
            this.ScanPlugins(); // scan the available plugins
            
            foreach (KeyValuePair<string,PluginInfo> pi in this.Plugins) // print all avaiable plugin's list to log.
            {
                Log.Instance.Write(LogMessageTypes.INFO, string.Format("Found Plugin: {0}", pi.Value.AssemblyName.ToString()));
            }                
        }

        private void ScanPlugins() // scans the program directory and finds valid BlizzTV plugins
        {
            DirectoryInfo SelfDir = new DirectoryInfo("."); // plugin's are stored in BlizzTV's directory itself.
            FileInfo[] _dll_files = SelfDir.GetFiles("*.dll"); // find all available dll files
            foreach (FileInfo _dll in _dll_files) // loop through all avaible dll files
            {
                PluginInfo pi = new PluginInfo(_dll.Name); // get the assembly info
                if (pi.Valid) Plugins.Add(pi.AssemblyName, pi); // if it's a valid BlizzTV plugin add it to list
            }
        }

        #endregion

        #region de-ctor

        /// <summary>
        /// de-ctor
        /// </summary>
        ~PluginManager() { Dispose(false); }

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
                    foreach (KeyValuePair<string, PluginInfo> pair in this.Plugins) { pair.Value.Dispose(); }
                    this.Plugins.Clear();
                    this.Plugins = null;
                }
                disposed = true;
            }
        }

        #endregion
    }
}
