﻿/*    
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
 * $Id: PluginManager.cs 158 2010-11-30 14:06:50Z shalafiraistlin@gmail.com $
 */

using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using BlizzTV.Module.Utils;

namespace BlizzTV.Module
{
    /// <summary>
    /// The plugin manager responsible of organizing the plugins and stuff.
    /// </summary>
    public sealed class PluginManager : IDisposable
    {
        #region members

        private static readonly PluginManager _instance = new PluginManager();
        private bool disposed = false;

        /// <summary>
        /// The plugin manager instance.
        /// </summary>
        public static PluginManager Instance { get { return _instance; } }

        // TODO: shoud be a readonly collection.
        /// <summary>
        /// The available valid plugin's list.
        /// </summary>
        public Dictionary<string, PluginInfo> AvailablePlugins = new Dictionary<string, PluginInfo>();

        private Dictionary<string, Plugin> _instantiated_plugins = new Dictionary<string, Plugin>();

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
        public Dictionary<string, Plugin> InstantiatedPlugins { get { return this._instantiated_plugins; } }

        #endregion

        #region internal logic 

        private PluginManager()
        {
            Log.Instance.Write(LogMessageTypes.INFO, string.Format("Plugin manager - ({0}) initialized..", this.GetType().Module.Name)); // log the plugin-manager startup message.
            this.ScanModules();
            
            foreach (KeyValuePair<string,PluginInfo> pi in this.AvailablePlugins) // print all avaiable plugin's list to log.
            {
                Log.Instance.Write(LogMessageTypes.INFO, string.Format("Found Plugin: {0}", pi.Value.Attributes.Name.ToString()));
            }                
        }

        private void ScanModules()
        {
            foreach (Type t in Assembly.GetEntryAssembly().GetTypes())
            {
                if(t.IsSubclassOf(typeof(Plugin)))
                {
                    PluginInfo pi=new PluginInfo(t);
                    if (pi.Valid) AvailablePlugins.Add(pi.Attributes.Name, pi);
                }
            }
        }

        /// <summary>
        /// Instantiates the asked plugin.
        /// </summary>
        /// <param name="key">The plugin to instantiate.</param>
        /// <returns>Plugin instance.</returns>
        public Plugin Instantiate(string key)
        {
            PluginInfo pi = this.AvailablePlugins[key];
            if (this._instantiated_plugins.ContainsKey(key)) return pi.Instance;
            else
            {
                Plugin p= pi.CreateInstance();
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
            PluginInfo pi = this.AvailablePlugins[key];
            this.InstantiatedPlugins.Remove(key);
            pi.Kill();
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
                    foreach (KeyValuePair<string, PluginInfo> pair in this.AvailablePlugins) { pair.Value.Dispose(); }
                    this.AvailablePlugins.Clear();
                    this.AvailablePlugins = null;
                }
                disposed = true;
            }
        }

        #endregion
    }
}