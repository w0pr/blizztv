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
 * 
 * $Id$
 */

using System;
using System.Collections.Generic;
using System.Reflection;
using BlizzTV.CommonLib.Logger;

namespace BlizzTV.ModuleLib
{
    /// <summary>
    /// The plugin manager responsible of organizing the plugins and stuff.
    /// </summary>
    public sealed class ModuleManager : IDisposable
    {
        #region members

        private static readonly ModuleManager _instance = new ModuleManager();
        private bool disposed = false;

        /// <summary>
        /// The plugin manager instance.
        /// </summary>
        public static ModuleManager Instance { get { return _instance; } }

        // TODO: shoud be a readonly collection.
        /// <summary>
        /// The available valid plugin's list.
        /// </summary>
        public Dictionary<string, ModuleInfo> AvailablePlugins = new Dictionary<string, ModuleInfo>();

        private Dictionary<string, Module> _instantiated_plugins = new Dictionary<string, Module>();

        /// <summary>
        /// The plugin-manager's so the LibBlizzTV's assembly name.
        /// </summary>
        public string AssemblyName { get { return Assembly.GetExecutingAssembly().GetName().Name; } }

        /// <summary>
        /// The plugin-manager's so the LibBlizzTV's assembly version.
        /// </summary>
        public string AssemblyVersion { get { return Assembly.GetExecutingAssembly().GetName().Version.ToString(); } }


        /// <summary>
        /// The instantiated plugins list.
        /// </summary>
        public Dictionary<string, Module> InstantiatedPlugins { get { return this._instantiated_plugins; } }

        #endregion

        #region internal logic 

        private ModuleManager()
        {
            Log.Instance.Write(LogMessageTypes.INFO, string.Format("Plugin manager - ({0}) initialized..", this.GetType().Module.Name)); // log the plugin-manager startup message.
            this.ScanModules();
            
            foreach (KeyValuePair<string,ModuleInfo> pi in this.AvailablePlugins) // print all avaiable plugin's list to log.
            {
                Log.Instance.Write(LogMessageTypes.INFO, string.Format("Found Plugin: {0}", pi.Value.Attributes.Name.ToString()));
            }                
        }

        private void ScanModules()
        {
            foreach (Type t in Assembly.GetEntryAssembly().GetTypes())
            {
                if(t.IsSubclassOf(typeof(Module)))
                {
                    ModuleInfo pi=new ModuleInfo(t);
                    if (pi.Valid) AvailablePlugins.Add(pi.Attributes.Name, pi);
                }
            }
        }

        /// <summary>
        /// Instantiates the asked plugin.
        /// </summary>
        /// <param name="key">The plugin to instantiate.</param>
        /// <returns>Plugin instance.</returns>
        public Module Instantiate(string key)
        {
            ModuleInfo pi = this.AvailablePlugins[key];
            if (this._instantiated_plugins.ContainsKey(key)) return pi.Instance;
            else
            {
                Module p= pi.CreateInstance();
                this._instantiated_plugins.Add(key,p);
                return p;
            }
        }

        /// <summary>
        /// Kills the asked plugin instance.
        /// </summary>
        /// <param name="key">The plugin to kill.</param>
        public void Kill(string key)
        {
            ModuleInfo pi = this.AvailablePlugins[key];
            this.InstantiatedPlugins.Remove(key);
            pi.Kill();
        }

        #endregion

        #region de-ctor

        /// <summary>
        /// de-ctor
        /// </summary>
        ~ModuleManager() { Dispose(false); }

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
                    foreach (KeyValuePair<string, ModuleInfo> pair in this.AvailablePlugins) { pair.Value.Dispose(); }
                    this.AvailablePlugins.Clear();
                    this.AvailablePlugins = null;
                }
                disposed = true;
            }
        }

        #endregion
    }
}
