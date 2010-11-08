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
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using System.Drawing;
using LibBlizzTV.Utils;

namespace LibBlizzTV
{
    public class Plugin : IDisposable
    {        
        private Assembly _assembly;
        private PluginInfo _plugin_info;
        private static Storage _storage;
        private bool disposed = false;

        public static PluginSettings PluginSettings;
        public static GlobalSettings GlobalSettings;
        
        public PluginInfo PluginInfo { get { return this._plugin_info; } internal set { this._plugin_info = value; } }
        public static Storage Storage { get { return Plugin._storage; } }

        public Plugin()
        {            
            this._assembly = Assembly.GetCallingAssembly(); // As this will be called by actual modules ctor, get calling assemby (the actual module's assembly).
            Plugin._storage = new Storage(this._assembly.GetName().Name);
        }

        public virtual void Load(PluginSettings ps) { throw new NotImplementedException(); }

        public void ApplyGlobalSettings(GlobalSettings gs)
        {
            GlobalSettings = gs;
        }

        /* plugin load complete */
        public delegate void PluginLoadCompleteEventHandler(object sender,PluginLoadCompleteEventArgs e);
        public event PluginLoadCompleteEventHandler OnPluginLoadComplete;
        protected void PluginLoadComplete(PluginLoadCompleteEventArgs e)
        {
            Log.Instance.Write(LogMessageTypes.DEBUG, string.Format("Plugin Load Completed: {0}", this.PluginInfo.AssemblyName.ToString()));
            if (OnPluginLoadComplete != null) OnPluginLoadComplete(this,e);
        }

        /* add list item */
        public delegate void RegisterListItemEventHandler(object sender, ListItem i, ListItem g);
        public event RegisterListItemEventHandler OnRegisterListItem;

        protected void RegisterListItem(ListItem Item, ListItem Parent=null)
        {
            if (OnRegisterListItem != null) OnRegisterListItem(this, Item, Parent);
        }

        /* plugin menu */
        public delegate void RegisterPluginMenuItemEventHandler(object sender,MenuItemEventArgs e);
        public event RegisterPluginMenuItemEventHandler OnRegisterPluginMenuItem;

        protected void RegisterPluginMenuItem(object sender,MenuItemEventArgs e)
        {
            if (OnRegisterPluginMenuItem != null) OnRegisterPluginMenuItem(this, e);
        }

        ~Plugin() { Dispose(false); }

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
                    this._plugin_info = null;
                    PluginSettings = null;
                    GlobalSettings = null;
                    _storage = null;
                }
                disposed = true;
            }
        }
    }

    public class PluginLoadCompleteEventArgs : EventArgs
    {
        private bool _success;
        public bool Success { get { return this._success; } }
        public PluginLoadCompleteEventArgs(bool Success)
        {
            this._success = Success;
        }
    }

    [AttributeUsage(AttributeTargets.Class)]
    public class PluginAttribute : Attribute
    {
        private string _name;
        private string _description;
        private string _icon_name;
        private Bitmap _icon = null;
        private bool disposed = false;

        public string Name { get { return this._name; } }
        public string Description { get { return this._description; } }
        public Bitmap Icon { get { return this._icon; } }

        public PluginAttribute(string Name, string Description, string IconName = null)
        {
            this._name = Name;
            this._description = Description;
            this._icon_name = IconName;
        }

        internal void ResolveResources(Assembly _assembly)
        {
            if (_assembly != null && this._icon_name != null)
            {
                using (var stream = _assembly.GetManifestResourceStream(string.Format("{0}.Resources.{1}", _assembly.GetName().Name, this._icon_name)))
                {
                    if (stream != null) this._icon = new Bitmap(stream);
                    else this._icon = Resources.blizztv_16; // if an icon is not specified use the default one.
                }
            }
        }

        public override string ToString()
        {
            return this._name;
        }

        ~PluginAttribute() { Dispose(false); }

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
                    this._icon.Dispose();
                    this._icon = null;
                }
                disposed = true;
            }
        }
    }
}
