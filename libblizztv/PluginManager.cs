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
    public sealed class PluginManager : IDisposable
    {        
        public string AssemblyName { get { return Assembly.GetExecutingAssembly().GetName().Name; } }
        public string AssemblyVersion { get { return Assembly.GetExecutingAssembly().GetName().Version.ToString(); } }
        private bool disposed = false;

        private static readonly PluginManager _instance = new PluginManager();
        public static PluginManager Instance { get { return _instance; } }

        public Dictionary<string, PluginInfo> Plugins = new Dictionary<string, PluginInfo>(); 

        private PluginManager()
        {
            Log.Instance.Write(LogMessageTypes.INFO, string.Format("Plugin manager - ({0}) initialized..", this.GetType().Module.Name));
            this.ScanPlugins();
            
            foreach (KeyValuePair<string,PluginInfo> pi in this.Plugins)
            {
                Log.Instance.Write(LogMessageTypes.INFO, string.Format("Found Plugin: {0}", pi.Value.AssemblyName.ToString()));
            }                
        }

        private void ScanPlugins()
        {
            DirectoryInfo SelfDir = new DirectoryInfo(".");
            FileInfo[] _dll_files = SelfDir.GetFiles("*.dll");
            foreach (FileInfo _dll in _dll_files)
            {
                PluginInfo pi = new PluginInfo(_dll.Name);
                if (pi.Valid) Plugins.Add(pi.AssemblyName, pi);
            }
        }

        ~PluginManager() { Dispose(false); }

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
    }
}
