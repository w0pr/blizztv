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
        private bool _disposed = false; // is the module disposed already?

        /// <summary>
        /// Is the module currently refreshing it's data?
        /// </summary>
        public bool RefreshingData { get; protected set; }

        /// <summary>
        /// The module's attributes.
        /// </summary>
        public ModuleAttributes Attributes { get; set; }

        protected Module()
        {
            RefreshingData = false;
        }

        /// <summary>
        /// Notifies the module to refresh it's data.
        /// </summary>
        public virtual void Refresh() { throw new NotImplementedException(); }

        /// <summary>
        /// Returns the module's request menus.
        /// </summary>
        /// <returns>A dictionary of requested menus.</returns>
        public virtual Dictionary<string, ToolStripMenuItem> GetMenus() { return null; }

        /// <summary>
        /// Returns the module's root treeview item.
        /// </summary>
        /// <returns><see cref="ListItem"/></returns>
        public virtual ListItem GetRootItem() { return null; }                    

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

        #region Events

        /// <summary>
        /// Notifies observers about module is refreshing it's data.
        /// </summary>
        public event EventHandler<EventArgs> DataRefreshStarting;

        /// <summary>
        /// Let's modules notify observers about starting of data refresh.
        /// </summary>
        /// <param name="e"><see cref="EventArgs"/></param>
        protected void OnDataRefreshStarting(EventArgs e)
        {
            LogManager.Instance.Write(LogMessageTypes.Debug, string.Format("[{0}] Data refresh started.", this.Attributes.Name));
            EventHandler<EventArgs> handler = DataRefreshStarting;
            if (handler != null) handler(this, e);
        }

        /// <summary>
        /// Notifies observers about module completed it's data refresh.
        /// </summary>
        public event EventHandler<DataRefreshCompletedEventArgs> DataRefreshCompleted;

        /// <summary>
        /// Let's modules notify observers about completion of it's data refresh.
        /// </summary>
        /// <param name="e"><see cref="EventArgs"/></param>
        protected void OnDataRefreshCompleted(DataRefreshCompletedEventArgs e)
        {
            LogManager.Instance.Write(LogMessageTypes.Debug, string.Format("[{0}] Data refresh {1}.", this.Attributes.Name, e.Succes ? "completed with success" : "failed"));
            EventHandler<DataRefreshCompletedEventArgs> handler = DataRefreshCompleted;
            if (handler != null) handler(this, e);
        }

        #endregion

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
