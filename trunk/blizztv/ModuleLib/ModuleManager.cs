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
using BlizzTV.Log;

namespace BlizzTV.ModuleLib
{
    public sealed class ModuleManager : IDisposable // The module manager responsible of organizing the module and stuff.
    {
        #region instance

        private static readonly ModuleManager _instance = new ModuleManager();
        public static ModuleManager Instance { get { return _instance; } }

        #endregion

        private bool _disposed = false;
        private readonly Dictionary<string, Module> _instantiatedPlugins = new Dictionary<string, Module>();

        public Dictionary<string, ModuleInfo> AvailablePlugins = new Dictionary<string, ModuleInfo>(); // The available valid module's list. TODO: shoud be a readonly collection.
        public Dictionary<string, Module> InstantiatedPlugins { get { return this._instantiatedPlugins; } } // The instantiated modules list.

        private ModuleManager()
        {
            LogManager.Instance.Write(LogMessageTypes.Info, string.Format("Module Manager - ({0}) initialized..", this.GetType().Module.Name)); // log the plugin-manager startup message.
            this.ScanModules();
            
            foreach (KeyValuePair<string,ModuleInfo> pi in this.AvailablePlugins)  { LogManager.Instance.Write(LogMessageTypes.Info, string.Format("Found Plugin: {0}", pi.Value.Attributes.Name)); } // print all avaiable plugin's list to log.
        }

        private void ScanModules()
        {
            try
            {
                foreach (Type t in Assembly.GetEntryAssembly().GetTypes())
                {
                    if (t.IsSubclassOf(typeof(Module)))
                    {
                        ModuleInfo pi = new ModuleInfo(t);
                        if (pi.Valid) AvailablePlugins.Add(pi.Attributes.Name, pi);
                    }
                }
            }
            catch (ReflectionTypeLoadException e)
            {
                foreach (Exception exc in e.LoaderExceptions)
                {
                    LogManager.Instance.Write(LogMessageTypes.Fatal, string.Format("Exception thrown during scanning available modules: {0}", exc.ToString()));
                }
            }
        }

        public Module Instantiate(string key) // Instantiates the asked module.
        {
            ModuleInfo pi = this.AvailablePlugins[key];
            if (this._instantiatedPlugins.ContainsKey(key)) return pi.ModuleInstance;
            else
            {
                Module p = pi.CreateInstance();
                this._instantiatedPlugins.Add(key,p);
                return p;
            }
        }

        public void Kill(string key) // Kills the asked module instance.
        {
            ModuleInfo pi = this.AvailablePlugins[key];
            this.InstantiatedPlugins.Remove(key);
            pi.Kill();
        }

        #region de-ctor

        ~ModuleManager() { Dispose(false); }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        private void Dispose(bool disposing)
        {
            if (this._disposed) return;
            if (disposing) // managed resources
            {
                foreach (KeyValuePair<string, ModuleInfo> pair in this.AvailablePlugins) { pair.Value.Dispose(); }
                this.AvailablePlugins.Clear();
                this.AvailablePlugins = null;
            }
            _disposed = true;
        }

        #endregion
    }
}
