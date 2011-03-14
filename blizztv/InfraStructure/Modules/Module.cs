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

namespace BlizzTV.InfraStructure.Modules
{
    /// <summary>
    /// Provides a dynamicly loadable base module.
    /// </summary>
    public class Module : IDisposable
    {
        private bool _disposed = false; // is the module disposed already?

        /// <summary>
        /// The module's attributes.
        /// </summary>
        public ModuleAttributes Attributes { get; set; }

        /// <summary>
        /// Can the module render global menus?
        /// </summary>
        public bool CanRenderMenus { get; protected set; }

        /// <summary>
        /// Can the module render tree nodes?
        /// </summary>
        public bool CanRenderTreeNodes { get; protected set; }

        /// <summary>
        /// Is the module currently refreshing it's data?
        /// </summary>
        public bool RefreshingData { get; protected set; }

        public static TreeView UITreeView = null;

        protected Module()
        {
            this.CanRenderMenus = false;
            this.CanRenderTreeNodes = false;
            this.RefreshingData = false;
        }

        /// <summary>
        /// Allows modules to startup.
        /// </summary>
        public virtual void Startup() { }

        /// <summary>
        /// Returns the module's request menus.
        /// </summary>
        /// <returns>A dictionary of requested menus.</returns>
        public virtual Dictionary<string, ToolStripMenuItem> GetMenus() { return null; }

        /// <summary>
        /// Returns the module's root treenode.
        /// </summary>
        /// <returns><see cref="TreeNode"/></returns>
        public virtual ModuleNode GetModuleNode() { return null; }

        /// <summary>
        /// Returns preferences form for the module. 
        /// Modules that would like to use configuration options should override this method and return it's preferences form.
        /// </summary>
        /// <returns></returns>
        public virtual Form GetPreferencesForm() { return null; }

        /// <summary>
        /// Returns a boolean based on if module is able to parse the supplied link.
        /// </summary>
        /// <param name="link">The resource url.</param>
        /// <returns><see cref="bool"/></returns>
        public virtual bool AddSubscriptionFromUrl(string link) { return false; }

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
                this.Attributes = null;
            }
            _disposed = true;
        }

        #endregion
    }

    #region Event arguments

    /// <summary>
    /// Stores information about module's data update complete event.
    /// </summary>
    public class DataRefreshCompletedEventArgs : EventArgs
    {
        public bool Succes { get; private set; }

        public DataRefreshCompletedEventArgs(bool success)
        {
            this.Succes = success;
        }
    }

    #endregion
}
