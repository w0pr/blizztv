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
using System.IO;
using System.Reflection;
using LibBlizzTV.Utils;

namespace LibBlizzTV
{
    /// <summary>
    /// The plugin info and activator.
    /// </summary>
    public class PluginInfo : IDisposable
    {
        #region members

        private bool _valid = false; // is it a valid BlizzTV plugin?
        private string _assembly_file; // the plugins file name on the disk.        
        private Assembly _assembly; // the plugin assembly itself.
        private Type _plugin_entrance; // the plugin's entrance point (actual module's ctor).
        private PluginAttributes _attributes; // the plugin's attributes
        private Plugin _instance = null; // contains the plugin instance if initiated.
        private bool disposed = false;

        /// <summary>
        /// The plugin's assembly name.
        /// </summary>
        public string AssemblyName { get { return this._assembly_file; } }

        /// <summary>
        /// The plugin's assembly version.
        /// </summary>
        public string AssemblyVersion { get { return this._assembly.GetName().Version.ToString(); } }

        /// <summary>
        /// Is it a valid BlizzTV plugin?
        /// </summary>
        public bool Valid { get { return this._valid; } }

        /// <summary>
        /// The plugin's attributes
        /// </summary>
        public PluginAttributes Attributes { get { return _attributes; } }

        /// <summary>
        /// Returns the plugin instance.
        /// <remarks>If the plugin is not initiated before it will be so.</remarks>
        /// </summary>
        public Plugin Instance { get { return this._instance; } }

        #endregion

        #region ctor

        /// <summary>
        /// Constructs a new PluginInfo based on given assembly filename.
        /// </summary>
        /// <param name="AssemblyFile">The assemblies filename.</param>
        public PluginInfo(string AssemblyFile)
        {
            this._assembly_file = AssemblyFile; // store the assemblies filename.
            if (Path.GetFileName(Assembly.GetExecutingAssembly().Location) == this._assembly_file) this._valid = false; // libblizztv.dll itself is not a valid plugin ;)
            else this.ReadPluginInfo(); // read the assemblies details.
        }

        #endregion

        #region PluginInfo API

        /// <summary>
        /// Creates a instance
        /// </summary>
        /// <returns>Returns the instance of the plugin asked for.</returns>
        public Plugin CreateInstance()
        {
            if (this._instance == null) // just allow one instance.
            {
                try
                {
                    if (!this._valid) throw new NotSupportedException(); // If the plugin asked for is not a valid BlizzTV pluin, fire an exception.
                    this._instance = (Plugin)Activator.CreateInstance(this._plugin_entrance); // Create the plugin instance using the ctor we stored as entrance point.
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

        /// <summary>
        /// Returns the plugin's assembly name and version.
        /// </summary>
        /// <returns>Plugin's assembly name and version.</returns>
        public override string ToString()
        {
            return string.Format("{0} - v{1}", this.AssemblyName, this.AssemblyVersion);
        }

        #endregion

        #region internal logic

        private void ReadPluginInfo() // reads a supplied assemblies details
        {
            try
            {
                this._assembly = Assembly.LoadFile(string.Format("{0}\\plugins\\{1}", Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), this._assembly_file)); // load the asked plugins assembly.

                foreach (Type t in this._assembly.GetTypes()) // Loop through each available types to see if it actually implements a LibBlizzTV plugin.
                {
                    if (t.IsSubclassOf(typeof(Plugin))) // if type extends the Plugin class
                    {
                        this._plugin_entrance = t; // this is our entry point (the module's ctor).                        
                        object[] _attr = t.GetCustomAttributes(typeof(PluginAttributes), true); // get the attributes for the plugin

                        if (_attr.Length > 0) // if plugin defines attributes, check them
                        {
                            (_attr[0] as PluginAttributes).ResolveResources(this._assembly); // resolve the attribute resources
                            this._attributes = (PluginAttributes)_attr[0]; // store the attributes
                            this._valid = true; // yes we're valid ;)
                        }
                        else throw new LoadPluginInfoException(this._assembly_file, "Plugin does not define the required attributes."); // all plugins should define the required atributes
                    }
                }
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
        ~PluginInfo() { Dispose(false); }

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
                    this._assembly = null;
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
