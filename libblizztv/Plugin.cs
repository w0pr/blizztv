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
using System.Reflection;

namespace LibBlizzTV
{
    public class Plugin
    {
        private Assembly _assembly;
        private PluginInfo _plugin_info;

        public string AssemblyVersion { get { return this.GetType().Assembly.GetName().Version.ToString(); } }
        public string FileName { get { return this.GetType().Module.Name; } }
        public PluginInfo PluginInfo { get { return this._plugin_info; } internal set { this._plugin_info = value; } }

        public Plugin()
        {
            this._assembly = Assembly.GetCallingAssembly(); // As this will be called by actual modules ctor, get calling assemby (the actual module's assembly).
        }

        public virtual void Update(){throw new NotImplementedException();}
    }

    [AttributeUsage(AttributeTargets.Class)]
    public class PluginAttribute : Attribute
    {
        private string _name;
        private string _description;

        public string Name { get { return this._name; } }
        public string Description { get { return this._description; } }

        public PluginAttribute(string Name, string Description)
        {
            this._name = Name;
            this._description = Description;
        }

        public override string ToString()
        {
            return this._name;
        }
    }
}
