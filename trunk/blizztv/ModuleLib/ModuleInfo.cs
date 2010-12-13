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
using BlizzTV.CommonLib.Logger;

namespace BlizzTV.ModuleLib
{
    public class ModuleInfo : IDisposable // The module info and activator.
    {
        private Type _pluginEntrance; // the modules's entrance point (actual module's ctor).
        private bool _disposed = false;

        public bool Valid { get; private set; } // is it a valid BlizzTV module?
        public ModuleAttributes Attributes { get; private set; } // the modules attributes.
        public Module ModuleInstance { get; private set; } // returns the module instance -- if not initiated before it will be so.

        public ModuleInfo(Type entrance)
        {
            this.ModuleInstance = null;
            this.Valid = false;
            this._pluginEntrance = entrance;
            this.ReadModuleInfo(); // read the assemblies details.
        }

        public Module CreateInstance() // Creates & returns the instance of module.
        {
            if (this.ModuleInstance == null) // just allow one instance.
            {
                try
                {
                    if (!this.Valid) throw new NotSupportedException(); // If the module asked for is not a valid BlizzTV module, fire an exception.
                    this.ModuleInstance = (Module)Activator.CreateInstance(this._pluginEntrance); // Create the module instance using the ctor we stored as entrance point.
                    this.ModuleInstance.Attributes = this.Attributes;
                }
                catch (Exception e)
                {
                    Log.Instance.Write(LogMessageTypes.Error, string.Format("PluginInfo:CreateInstance() exception: {0}", e));
                }
            }
            return this.ModuleInstance;
        }

        public void Kill() // Kills the plugin instance.
        {
            this.ModuleInstance.Dispose();
            this.ModuleInstance = null;
        }

        private void ReadModuleInfo() // reads a modules details.
        {
            try
            {
                object[] attr = this._pluginEntrance.GetCustomAttributes(typeof(ModuleAttributes), true); // get the attributes for the module.
                if (attr.Length > 0) // if it has required attributes defined.
                {
                    ((ModuleAttributes) attr[0]).ResolveResources(); // resolve the attribute resources.
                    this.Attributes = (ModuleAttributes)attr[0]; // store the attributes.
                    this.Valid = true; // yes we're valid ;)
                }
                else throw new LoadModuleInfoException("todo", "Plugin does not define the required attributes."); // all module should define the required atributes.              
            }
            catch (Exception e)  { Log.Instance.Write(LogMessageTypes.Error,string.Format("ReadPluginInfo() exception: {0}",e)); }
        }

        #region de-ctor

        ~ModuleInfo() { Dispose(false); }

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
                this._pluginEntrance = null;
                this.Attributes = null;
            }
            _disposed = true;
        }

        #endregion
    }

    public class LoadModuleInfoException : Exception // Load PlugiInfo Exception
    {
        public LoadModuleInfoException(string pluginFile, string message) // Contains information about a plugin load exception.
            : base(string.Format("{0} - {1}", pluginFile, message))
        { }
    }
}
