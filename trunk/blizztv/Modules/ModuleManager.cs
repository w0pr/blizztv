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
using System.Linq;
using System.Reflection;
using BlizzTV.Log;
using Module = BlizzTV.Modules.Module;

namespace BlizzTV.Modules
{
    /* TODO: Needs an overhaul */
    /// <summary>
    /// Module manager interface.
    /// </summary>
    public sealed class ModuleManager : IDisposable // The module manager responsible of organizing the module and stuff.
    {
        #region instance

        private static readonly ModuleManager _instance = new ModuleManager();
        public static ModuleManager Instance { get { return _instance; } }

        #endregion

        private readonly Dictionary<string, Module> _instantiatedModules = new Dictionary<string, Module>(); // contains a list of instantiated modules.
        private bool _disposed = false;        

        public Dictionary<string, ModuleInfo> AvailableModules = new Dictionary<string, ModuleInfo>(); // The available valid module's list. TODO: shoud be a readonly collection.
        public Dictionary<string, Module> InstantiatedModules { get { return this._instantiatedModules; } } // The instantiated modules list.

        private ModuleManager()
        {
            LogManager.Instance.Write(LogMessageTypes.Info, "Module manager initialized.."); 
            this.ScanModules(); // scan for available modules.

            string foundModulesInfo = this.AvailableModules.Aggregate(string.Empty, (current, pair) => current + string.Format("{0}, ", pair.Value.Attributes.Name));
            LogManager.Instance.Write(LogMessageTypes.Info, string.Format("Found modules: {0}", foundModulesInfo));
        }

        private void ScanModules()
        {
            try
            {
                /* loop through all available modules and add valid ones to available list */
                foreach (Type t in Assembly.GetEntryAssembly().GetTypes()) 
                {
                    if (t.IsSubclassOf(typeof(Module))) 
                    {
                        ModuleInfo moduleInfo = new ModuleInfo(t);
                        if (moduleInfo.Valid) AvailableModules.Add(moduleInfo.Attributes.Name, moduleInfo); 
                    }
                }
            }
            catch (ReflectionTypeLoadException e)
            {
                foreach (Exception exc in e.LoaderExceptions)
                {
                    LogManager.Instance.Write(LogMessageTypes.Fatal, string.Format("Exception caught during scanning of available modules: {0}", exc));
                }
            }
        }

        public Module Instantiate(string key) // Instantiates the asked module.
        {
            ModuleInfo moduleInfo = this.AvailableModules[key];
            if (this._instantiatedModules.ContainsKey(key)) return moduleInfo.ModuleInstance; // if the module is already instantiated, just return it.
            else
            {
                Module p = moduleInfo.CreateInstance();
                this._instantiatedModules.Add(key,p);
                return p;
            }
        }

        public void Kill(string key) // Kills the asked module instance.
        {
            ModuleInfo moduleInfo = this.AvailableModules[key];
            this.InstantiatedModules.Remove(key);
            moduleInfo.Kill();
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
                foreach (KeyValuePair<string, ModuleInfo> pair in this.AvailableModules) { pair.Value.Dispose(); }
                this.AvailableModules.Clear();
                this.AvailableModules = null;
            }
            _disposed = true;
        }

        #endregion
    }
}
