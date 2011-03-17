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
using BlizzTV.Log;

namespace BlizzTV.InfraStructure.Modules
{
    /* TODO: Needs an overhaul */
    /// <summary>
    /// Module info. wrapper and activator.
    /// </summary>
    public class ModuleController : IDisposable
    {
        private Module _instance; // holds reference to module instance (if exists).
        private readonly Type _entrance; // the modules's entrance point.
        private bool _disposed = false;

        /// <summary>
        /// Is it a valid BlizzTV module?
        /// </summary>
        public bool Valid { get; private set; }

        /// <summary>
        /// Returns instance of the module.
        /// </summary>
        public Module Instance
        {
            get
            {
                if (this._instance != null) return this._instance;

                try
                {
                    if (!this.Valid) throw new NotSupportedException("Can not load an invalid module."); // If the module asked for is not a valid BlizzTV module, fire an exception.
                    this._instance = (Module)Activator.CreateInstance(this._entrance); // Create the module instance using the ctor we stored as entrance point.
                    this._instance.Attributes = this.Attributes;
                    return this._instance;
                }
                catch (Exception e)
                {
                    LogManager.Instance.Write(LogMessageTypes.Error, string.Format("An exception occured while getting instance of the module {0}", e));
                    throw new NotSupportedException("Can not load an invalid module.");
                }
            }
        }

        /// <summary>
        /// The module's attributes.
        /// </summary>
        public ModuleAttributes Attributes { get; private set; } 

        public ModuleController(Type entrance)
        {
            this._entrance = entrance;
            this.ReadModuleInfo(); // read the module's details.
        }

        private void ReadModuleInfo() // reads the modules attributes.
        {
            try
            {
                object[] attr = this._entrance.GetCustomAttributes(typeof(ModuleAttributes), true); // get the attributes of the module.
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
        /// Loads the module wrapped.
        /// </summary>
        /// <returns>The loaded module</returns>
        public Module Load()
        {
            return this.Instance;
        }

        /// <summary>
        /// Kills the wrapped module
        /// </summary>
        public void Kill() 
        {
            this._instance.Dispose();
            this._instance = null;
        }

        #region de-ctor

        // IDisposable pattern: http://msdn.microsoft.com/en-us/library/fs2xkftw%28VS.80%29.aspx

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this); // Take object out the finalization queue to prevent finalization code for it from executing a second time.
        }

        protected virtual void Dispose(bool disposing)
        {
            if (this._disposed) return; // if already disposed, just return

            if (disposing) // only dispose managed resources if we're called from directly or in-directly from user code.
            {
                if(this._instance!=null) this._instance.Dispose();
                if(this.Attributes!=null) this.Attributes.Dispose();
            }

            _disposed = true;
        }

        ~ModuleController() { Dispose(false); } // finalizer called by the runtime. we should only dispose unmanaged objects and should NOT reference managed ones.       

        #endregion
    }
}
