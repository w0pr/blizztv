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

namespace LibBlizzTV
{
    public class PluginInfo
    {
        private bool _valid = false; // is the module a valid LibBlizzTV module?
        private string _assembly_file; // the assemblie's file name on the disk.        
        private Assembly _assembly; // the module assembly itself.
        private Type _plugin_entrance; // the module's entrance point (actual module's ctor).
        private PluginAttribute _attributes;

        public string AssemblyName { get { return this._assembly_file; } }
        public string AssemblyVersion { get { return this._assembly.GetName().Version.ToString(); } }
        public bool Valid { get { return this._valid; } }
        public PluginAttribute Attributes { get { return _attributes; } }


        public PluginInfo(string AssemblyFile)
        {
            this._assembly_file = AssemblyFile;
            if (Path.GetFileName(Assembly.GetExecutingAssembly().Location) == this._assembly_file) this._valid = false; // libblizztv.dll itself is not a valid module ;)
            else this.ReadPluginInfo();
        }

        private void ReadPluginInfo()
        {
            try
            {
                this._assembly = Assembly.LoadFile(string.Format("{0}\\{1}", Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), this._assembly_file)); // Load the asked modules assembly.

                foreach (Type t in this._assembly.GetTypes()) // Loop through each available types to see if it actually implements a LibBlizzTV Module.
                {
                    if (t.IsSubclassOf(typeof(Plugin)))
                    {
                        this._plugin_entrance = t; // this is our entry point (the module's ctor).                        
                        object[] _attr = t.GetCustomAttributes(typeof(PluginAttribute), true); // get the attributes for the plugin

                        if (_attr.Length > 0)
                        {
                            (_attr[0] as PluginAttribute).ResolveResources(this._assembly);
                            this._attributes = (PluginAttribute)_attr[0];                            
                            this._valid = true; // yes we're valid ;)
                        }
                        else throw new LoadPluginInfoException(this._assembly_file, "Plugin does not define the required attributes."); // all plugins should define the required atributes
                    }
                }
            }
            catch (Exception e)
            {

            }
        }

        public Plugin CreateInstance()
        {
            if (!this._valid) throw new NotSupportedException(); // If the plugin asked is not a valid one, ignore it
            Plugin p = (Plugin)Activator.CreateInstance(this._plugin_entrance); // Create the module instance using the ctor we stored as entrance point.
            p.PluginInfo = this; // attach the plugin-info to plugin itself.
            return p;
        }

        public override string ToString()
        {
            return string.Format("{0} - v{1}", this.AssemblyName, this.AssemblyVersion);
        }
    }

    public class LoadPluginInfoException : Exception
    {
        public LoadPluginInfoException(string PluginFile, string Message)
            : base(string.Format("{0} - {1}", PluginFile, Message))
        { }
    }
}
