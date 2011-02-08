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
using System.Collections.Generic;
using System.Windows.Forms;
using BlizzTV.Log;

namespace BlizzTV.Modules
{
    /// <summary>
    /// Provides a dynamicly loadable base module.
    /// </summary>
    public class Module : IDisposable 
    {
        private ModuleAttributes _attributes;
        private bool _disposed = false;

        /// <summary>
        /// The root item for the module.
        /// </summary>
        public ListItem RootListItem { get; protected set; }

        /// <summary>
        /// Is the module currently updating it's data?
        /// </summary>
        public bool Updating { get; protected set; }

        /// <summary>
        /// The module's attributes.
        /// </summary>
        public ModuleAttributes Attributes { get { return this._attributes; } internal set { this._attributes = value; } } // The module attributes.

        /// <summary>
        /// Module's bound menus on form-menu.
        /// </summary>
        public Dictionary<string,ToolStripMenuItem> Menus = new Dictionary<string,ToolStripMenuItem>(); // The module sub-menus.

        protected Module()
        {
            Updating = false;
        }

        public virtual void Run() { throw new NotImplementedException(); } // Notifies the module to start running -- all of them should override this method.

        public virtual Form GetPreferencesForm() { return null; } // Modules shoud override this method and return the preferences form.

        public virtual bool TryDragDrop(string link) { return false; } // Modules can override this to supply drag & drop support.

        public delegate void PluginUpdateStartedEventHandler(object sender);
        public event PluginUpdateStartedEventHandler OnPluginUpdateStarted;

        /// <summary>
        /// Notifies update module started it's data update.
        /// </summary>
        protected void NotifyUpdateStarted()
        {
            LogManager.Instance.Write(LogMessageTypes.Debug, string.Format("Plugin update started: '{0}'.", this.Attributes.Name));
            if (OnPluginUpdateStarted != null) OnPluginUpdateStarted(this);
        }

        public delegate void PluginUpdateCompleteEventHandler(object sender,PluginUpdateCompleteEventArgs e);
        public event PluginUpdateCompleteEventHandler OnPluginUpdateComplete;

        /// <summary>
        /// Notifies about module data update completed.
        /// </summary>
        /// <param name="e"><see cref="PluginUpdateCompleteEventArgs"/></param>
        protected void NotifyUpdateComplete(PluginUpdateCompleteEventArgs e)
        {
            if (e.Success) LogManager.Instance.Write(LogMessageTypes.Debug, string.Format("Plugin update completed with success: '{0}'.", this.Attributes.Name));
            else LogManager.Instance.Write(LogMessageTypes.Error, string.Format("Plugin update failed: '{0}'.", this.Attributes.Name));
            if (OnPluginUpdateComplete != null) OnPluginUpdateComplete(this,e); 
        }

        #region de-ctor

        ~Module() { Dispose(false); }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (this._disposed) return;
            if (disposing) // managed resources
            {
                this._attributes = null;
                this.RootListItem.Childs.Clear();
                this.RootListItem = null;
            }
            _disposed = true;
        }

        #endregion
    }

    // TODO: Check if success code is supplied all-valid by modules.
    /// <summary>
    /// Containts information about module data-update results.
    /// </summary>
    public class PluginUpdateCompleteEventArgs : EventArgs
    {
        public bool Success { get; private set; }

        public PluginUpdateCompleteEventArgs(bool success)
        {
            this.Success = success;
        }
    }
}
