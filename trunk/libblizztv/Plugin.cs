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
using System.Drawing;
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

        public virtual void Load() { throw new NotImplementedException(); }

        public delegate void PluginLoadCompleteEventHandler(object sender,PluginLoadCompleteEventArgs e);
        public event PluginLoadCompleteEventHandler OnPluginLoadComplete;

        /* plugin load  complete */
        protected void PluginLoadComplete(PluginLoadCompleteEventArgs e)
        {
            DebugConsole.WriteLine(DebugConsole.MessageTypes.DEBUG, string.Format("Plugin Load Completed: {0}", this.PluginInfo.ToString()));
            if (OnPluginLoadComplete != null) OnPluginLoadComplete(this,e);
        }

        /* register list group */
        public delegate void RegisterListGroupEventHandler(object sender, ListGroup g);
        public event RegisterListGroupEventHandler OnRegisterListGroup;

        protected void RegisterListGroup(ListGroup g)
        {
            DebugConsole.WriteLine(DebugConsole.MessageTypes.DEBUG,string.Format("Plugin {0} registered list group: {1}",this._plugin_info.AssemblyName.ToString(),g.Name));
            if(OnRegisterListGroup!=null) OnRegisterListGroup(this,g);
        }

        /* add list item */
        public delegate void RegisterListItemEventHandler(object sender, ListItem i,ListGroup g);
        public event RegisterListItemEventHandler OnRegisterListItem;

        protected void RegisterListItem(ListItem Item,ListGroup Group)
        {
            if (OnRegisterListItem != null) OnRegisterListItem(this, Item, Group);
        }
    }

    public class ListGroup
    {
        private string _name;
        private string _key;

        public string Name { get { return this._name; } }
        public string Key { get { return this._key; } }

        public ListGroup(string Name, string Key)
        {
            this._name = Name;
            this._key = Key;
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
                var stream = _assembly.GetManifestResourceStream(string.Format("{0}.Resources.{1}", _assembly.GetName().Name, this._icon_name));
                if (stream != null) this._icon = new Bitmap(stream);
                else this._icon = Resources.blizztv_16; // if an icon is not specified use the default one.
            }
        }

        public override string ToString()
        {
            return this._name;
        }
    }
}
