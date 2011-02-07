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
using BlizzTV.Log;

namespace BlizzTV.Modules
{
    /* TODO: Needs an overhaul */
    /// <summary>
    /// Module info. wrapper and activator.
    /// </summary>
    public class ModuleInfo : IDisposable
    {
        private Type _pluginEntrance; // the modules's entrance point (module's ctor).
        private bool _disposed = false;

        /// <summary>
        /// Is it a valid BlizzTV module?
        /// </summary>
        public bool Valid { get; private set; } 

        /// <summary>
        /// The module's attributes.
        /// </summary>
        public ModuleAttributes Attributes { get; private set; } 

        /// <summary>
        /// Returns the instance of the module.
        /// </summary>
        /* TODO: should not be public and functionality should be provided by CreateInstance() function */
        public Module ModuleInstance { get; private set; } // returns the module instance -- if not initiated before it will be so.

        public ModuleInfo(Type entrance)
        {
            this._pluginEntrance = entrance;
            this.ReadModuleInfo(); // read the module's details.
        }

        private void ReadModuleInfo() // reads the modules attributes.
        {
            try
            {
                object[] attr = this._pluginEntrance.GetCustomAttributes(typeof(ModuleAttributes), true); // get the attributes of the module.
                if (attr.Length > 0) // if it has required attributes defined
                {
                    ((ModuleAttributes)attr[0]).ResolveResources(); // resolve the attribute resources like icons and so.
                    this.Attributes = (ModuleAttributes)attr[0]; // store the attributes.
                    this.Valid = true; // yes we're valid ;)
                }
                else
                {
                    LogManager.Instance.Write(LogMessageTypes.Error, "Can't read the module info as required attributes are not defined.");
                    this.Valid = false;
                }
            }
            catch (Exception e)
            {
                LogManager.Instance.Write(LogMessageTypes.Error, string.Format("An exception occured while reading module info: {0}", e));
                this.Valid = false;
            }
        }

        /// <summary>
        /// Returns instance of the module.
        /// </summary>
        /// <returns></returns>
        /* TODO: should be named Instance() */
        public Module CreateInstance()
        {
            if (this.ModuleInstance == null) // if module is not instanted yet
            {
                try
                {
                    if (!this.Valid) throw new NotSupportedException(); // If the module asked for is not a valid BlizzTV module, fire an exception.
                    this.ModuleInstance = (Module)Activator.CreateInstance(this._pluginEntrance); // Create the module instance using the ctor we stored as entrance point.
                    this.ModuleInstance.Attributes = this.Attributes;
                }
                catch (Exception e)
                {
                    LogManager.Instance.Write(LogMessageTypes.Error, string.Format("An exception occured while getting instance of the module {0}", e));
                }
            }
            return this.ModuleInstance;
        }

        public void Kill() // Kills the module's instance.
        {
            this.ModuleInstance.Dispose();
            this.ModuleInstance = null;
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
}
