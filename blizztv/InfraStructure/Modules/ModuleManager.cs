/*    
 * Copyright (C) 2010-2011, BlizzTV Project - http://code.google.com/p/blizztv/
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

namespace BlizzTV.InfraStructure.Modules
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

        public readonly Dictionary<string, ModuleController> AvailableModules = new Dictionary<string, ModuleController>(); // The available valid module's list. TODO: shoud be a readonly collection.
        public readonly Dictionary<string, Module> LoadedModules = new Dictionary<string, Module>(); // The instantiated modules list.

        private bool _disposed = false;        

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
                    if (!t.IsSubclassOf(typeof (Module))) continue; // check if type is a subclass 'Module'.

                    var moduleInfo = new ModuleController(t);
                    if (moduleInfo.Valid) AvailableModules.Add(moduleInfo.Attributes.Name, moduleInfo);
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

        public Module Load(string key) // Instantiates the asked module.
        {
            if (!this.AvailableModules.ContainsKey(key)) throw new NotSupportedException("No module exists with given name");

            ModuleController moduleInfo = this.AvailableModules[key];
            if (!this.LoadedModules.ContainsKey(key)) this.LoadedModules.Add(key, moduleInfo.Instance); // if module is not loaded yet, load it first.
            return moduleInfo.Instance;
        }

        public void Kill(string key) // Kills the asked module instance.
        {
            if (!this.AvailableModules.ContainsKey(key)) throw new NotSupportedException("No module exists with given name");

            ModuleController moduleInfo = this.AvailableModules[key];
            this.LoadedModules.Remove(key);
            moduleInfo.Kill();
        }

        #region de-ctor

        // IDisposable pattern: http://msdn.microsoft.com/en-us/library/fs2xkftw%28VS.80%29.aspx

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this); // Take object out the finalization queue to prevent finalization code for it from executing a second time.
        }

        private void Dispose(bool disposing)
        {
            if (this._disposed) return; // if already disposed, just return

            if (disposing) // only dispose managed resources if we're called from directly or in-directly from user code.
            {
                foreach (KeyValuePair<string, ModuleController> pair in this.AvailableModules) { pair.Value.Kill(); }
                this.AvailableModules.Clear();
                this.LoadedModules.Clear();
            }

            _disposed = true;
        }

        ~ModuleManager() { Dispose(false); } // finalizer called by the runtime. we should only dispose unmanaged objects and should NOT reference managed ones.       

        #endregion
    }
}
