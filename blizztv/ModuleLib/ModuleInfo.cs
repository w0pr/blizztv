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
 * $Id: PluginInfo.cs 154 2010-11-29 14:46:54Z shalafiraistlin@gmail.com $
 */

using System;
using System.IO;
using System.Reflection;
using BlizzTV.ModuleLib.Utils;

namespace BlizzTV.ModuleLib
{
    /// <summary>
    /// The plugin info and activator.
    /// </summary>
    public class ModuleInfo : IDisposable
    {
        #region members

        private bool _valid = false; // is it a valid BlizzTV plugin?
        private Type _plugin_entrance; // the plugin's entrance point (actual module's ctor).
        private ModuleAttributes _attributes; // the plugin's attributes
        private Module _instance = null; // contains the plugin instance if initiated.
        private bool disposed = false;

        /// <summary>
        /// Is it a valid BlizzTV plugin?
        /// </summary>
        public bool Valid { get { return this._valid; } }

        /// <summary>
        /// The plugin's attributes
        /// </summary>
        public ModuleAttributes Attributes { get { return _attributes; } }

        /// <summary>
        /// Returns the plugin instance.
        /// <remarks>If the plugin is not initiated before it will be so.</remarks>
        /// </summary>
        public Module Instance { get { return this._instance; } }

        #endregion

        #region ctor

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Entrance"></param>
        public ModuleInfo(Type Entrance)
        {
            this._plugin_entrance = Entrance;
            this.ReadPluginInfo(); // read the assemblies details.
        }

        #endregion

        #region PluginInfo API

        /// <summary>
        /// Creates a instance
        /// </summary>
        /// <returns>Returns the instance of the plugin asked for.</returns>
        public Module CreateInstance()
        {
            if (this._instance == null) // just allow one instance.
            {
                try
                {
                    if (!this._valid) throw new NotSupportedException(); // If the plugin asked for is not a valid BlizzTV pluin, fire an exception.
                    this._instance = (Module)Activator.CreateInstance(this._plugin_entrance); // Create the plugin instance using the ctor we stored as entrance point.
                    this._instance.Attributes = this._attributes;
                }
                catch (Exception e)
                {
                    Log.Instance.Write(LogMessageTypes.ERROR, string.Format("PluginInfo:CreateInstance() exception: {0}", e.ToString()));
                }
            }
            return this._instance;
        }

        /// <summary>
        /// Kills the plugin instance.
        /// </summary>
        public void Kill()
        {
            this._instance.Dispose();
            this._instance = null;
        }

        #endregion

        #region internal logic

        private void ReadPluginInfo() // reads a supplied assemblies details
        {
            try
            {
                object[] _attr = this._plugin_entrance.GetCustomAttributes(typeof(ModuleAttributes), true); // get the attributes for the plugin
                
                if (_attr.Length > 0) // if plugin defines attributes, check them
                {
                    (_attr[0] as ModuleAttributes).ResolveResources(); // resolve the attribute resources
                    this._attributes = (ModuleAttributes)_attr[0]; // store the attributes
                    this._valid = true; // yes we're valid ;)
                }
                else throw new LoadPluginInfoException("todo", "Plugin does not define the required attributes."); // all plugins should define the required atributes                
            }
            catch (Exception e) 
            {
                Log.Instance.Write(LogMessageTypes.ERROR,string.Format("ReadPluginInfo() exception: {0}",e.ToString()));
            }
        }

        #endregion

        #region de-ctor

        /// <summary>
        /// de-ctor.
        /// </summary>
        ~ModuleInfo() { Dispose(false); }

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
                    //this._assembly = null;
                    this._plugin_entrance = null;
                    this._attributes = null;
                }
                disposed = true;
            }
        }

        #endregion
    }

    #region exceptions 

    /// <summary>
    /// Load PlugiInfo Exception
    /// </summary>
    public class LoadPluginInfoException : Exception
    {
        /// <summary>
        /// Contains information about a plugin load exception.
        /// </summary>
        /// <param name="PluginFile">The plugin assembly.</param>
        /// <param name="Message">The exception message.</param>
        public LoadPluginInfoException(string PluginFile, string Message)
            : base(string.Format("{0} - {1}", PluginFile, Message))
        { }
    }

    #endregion
}
