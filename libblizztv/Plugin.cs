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
    /// <summary>
    /// A dynamicly loadable plugin for BlizzTV application
    /// </summary>
    public class Plugin : IDisposable
    {
        #region members

        private Assembly _assembly; // the assembly 
        private PluginInfo _plugin_info; // the plugin info
        private static Storage _storage; // the key-value storage
        private bool disposed = false;

        /// <summary>
        /// Plugin specific settings.
        /// </summary>
        public static PluginSettings PluginSettings;

        /// <summary>
        /// Global settings that are also usable by plugins.
        /// </summary>
        public static GlobalSettings GlobalSettings;

        /// <summary>
        /// The plugin info.
        /// </summary>
        public PluginInfo PluginInfo { get { return this._plugin_info; } internal set { this._plugin_info = value; } }

        /// <summary>
        /// The key-value storage for plugin's use.
        /// </summary>
        public static Storage Storage { get { return Plugin._storage; } }

        /// <summary>
        /// Plugin sub-menus.
        /// </summary>
        public Dictionary<string,System.Windows.Forms.ToolStripMenuItem> Menus = new Dictionary<string,System.Windows.Forms.ToolStripMenuItem>();


        #endregion

        #region ctor

        /// <summary>
        /// ctor
        /// </summary>
        public Plugin()
        {            
            this._assembly = Assembly.GetCallingAssembly(); // As this will be called by actual modules ctor, get calling assemby (the actual module's assembly).
            Plugin._storage = new Storage(this._assembly.GetName().Name); // startup the storage for the plugin. (plugin's name should be supplied as they're used in key-name's)
        }

        #endregion

        #region The plugin API & events.

        /// <summary>
        /// Loads the plugin with supplied plugin settings.
        /// </summary>
        /// <param name="ps">The plugin's specific settings.</param>
        /// <remarks>All plugin's should override this method.</remarks>
        public virtual void Load(PluginSettings ps) { throw new NotImplementedException(); }

        /// <summary>
        /// Applies global settings that's usable by the plugins.
        /// </summary>
        /// <param name="gs">The global settings</param>
        public void ApplyGlobalSettings(GlobalSettings gs)
        {
            GlobalSettings = gs;
        }

        /// <summary>
        /// PluginLoadComplete event handler delegate.
        /// </summary>
        /// <param name="sender">The sender object.</param>
        /// <param name="e"><see cref="PluginLoadCompleteEventArgs"/></param>
        public delegate void PluginLoadCompleteEventHandler(object sender,PluginLoadCompleteEventArgs e);
        /// <summary>
        /// PluginLoadComplete event handler.
        /// </summary>
        public event PluginLoadCompleteEventHandler OnPluginLoadComplete;
        /// <summary>
        /// Notifies about the plugin load process supplying a success code.
        /// </summary>
        /// <param name="e"><see cref="PluginLoadCompleteEventArgs"/></param>
        /// <remarks>Plugins can use this method to notify BlizzTV application about it's loading results.</remarks>
        protected void PluginLoadComplete(PluginLoadCompleteEventArgs e)
        {
            if (e.Success) Log.Instance.Write(LogMessageTypes.DEBUG, string.Format("Plugin loading completed: {0}", this.PluginInfo.AssemblyName.ToString()));
            else Log.Instance.Write(LogMessageTypes.ERROR, string.Format("Plugin loading failed: {0}", this.PluginInfo.AssemblyName.ToString()));
            if (OnPluginLoadComplete != null) OnPluginLoadComplete(this,e); // notify observers.
        }

        /// <summary>
        /// RegsiterListItem event handler delegate.
        /// </summary>
        /// <param name="sender">The sender object.</param>
        /// <param name="i">The item to register.</param>
        /// <param name="g">If appliable, the parent item to be placed under.</param>
        public delegate void RegisterListItemEventHandler(object sender, ListItem i, ListItem g);
        /// <summary>
        /// RegisterListItem event handler.
        /// </summary>
        public event RegisterListItemEventHandler OnRegisterListItem;
        /// <summary>
        /// Registers a list item to be render in main form's treeview.
        /// </summary>
        /// <param name="Item">The item to register.</param>
        /// <param name="Parent">If appliable, the parent item to be placed under.</param>
        protected void RegisterListItem(ListItem Item, ListItem Parent=null)
        {
            if (OnRegisterListItem != null) OnRegisterListItem(this, Item, Parent); // notify observers.
        }

        #endregion

        #region de-ctor

        /// <summary>
        /// de-ctor.
        /// </summary>
        ~Plugin() { Dispose(false); }

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
                    this._plugin_info = null;
                    PluginSettings = null;
                    GlobalSettings = null;
                    _storage = null;
                }
                disposed = true;
            }
        }

        #endregion
    }

    #region event handler arguments 

    /// <summary>
    /// Notifies information about plugin's loading results.
    /// </summary>
    public class PluginLoadCompleteEventArgs : EventArgs
    {
        private bool _success;
        /// <summary>
        /// Returns true if plugin load was succesfull.
        /// </summary>
        public bool Success { get { return this._success; } }

        /// <summary>
        /// Constructs a new PluginLoadCompleteEventArgs.
        /// </summary>
        /// <param name="Success">Did plugin loaded with success?</param>
        public PluginLoadCompleteEventArgs(bool Success)
        {
            this._success = Success;
        }
    }

    #endregion

    #region plugin attributes 

    /// <summary>
    /// Plugin attributes.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class)]
    public class PluginAttribute : Attribute
    {
        #region members

        private string _name;
        private string _description;
        private string _icon_name;
        private Bitmap _icon = null;
        private bool disposed = false;

        /// <summary>
        /// The plugin name.
        /// </summary>
        public string Name { get { return this._name; } }

        /// <summary>
        /// The plugin description.
        /// </summary>
        public string Description { get { return this._description; } }

        /// <summary>
        /// The plugin icon.
        /// </summary>        
        public Bitmap Icon { get { return this._icon; } }

        #endregion

        #region ctor

        /// <summary>
        /// The plugin's attributes.
        /// </summary>
        /// <param name="Name">The plugin name.</param>
        /// <param name="Description">The plugin description.</param>
        /// <param name="IconName">Pass null as value or do not supply a value if you don't want to specify an icon for your plugin.</param>
        /// <remarks>Pass null as value or do not supply a value if you don't want to specify an icon for your plugin.</remarks>
        public PluginAttribute(string Name, string Description, string IconName = null)
        {
            this._name = Name;
            this._description = Description;
            this._icon_name = IconName;
        }

        #endregion

        #region internal logic

        internal void ResolveResources(Assembly _assembly) // resolves resources for the plugin and sets the icon if appliable.
        {
            if (_assembly != null && this._icon_name != null) // if we've a supplied icon-file name.
            {
                using (var stream = _assembly.GetManifestResourceStream(string.Format("{0}.Resources.{1}", _assembly.GetName().Name, this._icon_name))) // get the resources stream
                {
                    if (stream != null) this._icon = new Bitmap(stream); // if the asked icon exists in resources set is as the plugin icon.
                    else this._icon = Resources.blizztv_16; // if it does not exists use the default icon.
                }
            }
        }

        /// <summary>
        /// Returns the plugin's name.
        /// </summary>
        /// <returns>The plugin's name.</returns>
        public override string ToString()
        {
            return this._name;
        }

        #endregion

        #region de-ctor

        /// <summary>
        /// de-ctor.
        /// </summary>
        ~PluginAttribute() { Dispose(false); }

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
                    this._icon.Dispose();
                    this._icon = null;
                }
                disposed = true;
            }
        }

        #endregion
    }

    #endregion
}
