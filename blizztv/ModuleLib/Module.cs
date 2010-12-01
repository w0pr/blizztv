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
 * $Id: Plugin.cs 158 2010-11-30 14:06:50Z shalafiraistlin@gmail.com $
 */

using System;
using System.Collections.Generic;
using System.Reflection;
using BlizzTV.CommonLib.Logger;

namespace BlizzTV.ModuleLib
{
    /// <summary>
    /// A dynamicly loadable plugin for BlizzTV application
    /// </summary>
    public class Module : IDisposable
    {
        #region members

        private Assembly _assembly; // the assembly 
        private ModuleAttributes _attributes;
        private ListItem _root_list_item;
        private bool disposed = false;

        /// <summary>
        /// The plugins attributes.
        /// </summary>
        public ModuleAttributes Attributes { get { return this._attributes; } internal set { this._attributes = value; } }

        /// <summary>
        /// Plugin sub-menus.
        /// </summary>
        public Dictionary<string,System.Windows.Forms.ToolStripMenuItem> Menus = new Dictionary<string,System.Windows.Forms.ToolStripMenuItem>();

        /// <summary>
        /// 
        /// </summary>
        public ListItem RootListItem { get { return this._root_list_item; } protected set { this._root_list_item = value; } }

        #endregion

        #region ctor

        /// <summary>
        /// ctor
        /// </summary>
        public Module()
        {
            this._assembly = Assembly.GetCallingAssembly(); // As this will be called by actual modules ctor, get calling assemby (the actual module's assembly).
        }

        #endregion

        #region plugin API & events.

        /// <summary>
        /// Notifies the plugin to start running.
        /// </summary>
        /// <remarks>All plugin's should override this method.</remarks>
        public virtual void Run() { throw new NotImplementedException(); }

        /// <summary>
        /// Plugins shoud override this method and return the preferences form.
        /// </summary>
        /// <returns>The preferences form.</returns>
        public virtual System.Windows.Forms.Form GetPreferencesForm()
        {
            return null;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        public delegate void PluginUpdateStartedEventHandler(object sender);

        /// <summary>
        /// 
        /// </summary>
        public event PluginUpdateStartedEventHandler OnPluginUpdateStarted;

        /// <summary>
        /// 
        /// </summary>
        protected void NotifyUpdateStarted()
        {
            Log.Instance.Write(LogMessageTypes.DEBUG, string.Format("Plugin update started: '{0}'.", this.Attributes.Name));
            if (OnPluginUpdateStarted != null) OnPluginUpdateStarted(this);
        }


        /// <summary>
        /// PluginLoadComplete event handler delegate.
        /// </summary>
        /// <param name="sender">The sender object.</param>
        /// <param name="e"><see cref="PluginUpdateCompleteEventArgs"/></param>
        public delegate void PluginUpdateCompleteEventHandler(object sender,PluginUpdateCompleteEventArgs e);

        /// <summary>
        /// PluginLoadComplete event handler.
        /// </summary>
        public event PluginUpdateCompleteEventHandler OnPluginUpdateComplete;

        /// <summary>
        /// Notifies about the plugin load process supplying a success code.
        /// </summary>
        /// <param name="e"><see cref="PluginUpdateCompleteEventArgs"/></param>
        /// <remarks>Plugins can use this method to notify observers about it's loading results.</remarks>
        protected void NotifyUpdateComplete(PluginUpdateCompleteEventArgs e)
        {
            if (e.Success) Log.Instance.Write(LogMessageTypes.DEBUG, string.Format("Plugin update completed with success: '{0}'.", this.Attributes.Name));
            else Log.Instance.Write(LogMessageTypes.ERROR, string.Format("Plugin update failed: '{0}'.", this.Attributes.Name));
            if (OnPluginUpdateComplete != null) OnPluginUpdateComplete(this,e); // notify observers.
        }      

        /// <summary>
        /// Delegate for event handler that notifies about the plugins current workload.
        /// </summary>
        /// <param name="sender">The sender object.</param>
        /// <param name="units">The units of current workload.</param>
        public delegate void WorkloadAddEventHandler(object sender, int units);

        /// <summary>
        /// Event handler that notifies about the plugins current workload.
        /// </summary>
        public event WorkloadAddEventHandler OnWorkloadAdd;

        /// <summary>
        /// Notifies about current workload.
        /// </summary>
        /// <param name="units">The units of current workload.</param>
        protected void AddWorkload(int units)
        {
            if (OnWorkloadAdd != null) OnWorkloadAdd(this, units);
        }

        /// <summary>
        /// Delegate for event handler that notifies about the consuming a workload step.
        /// </summary>
        /// <param name="sender">The sender object.</param>
        public delegate void WorkloadStepEventHandler(object sender);

        /// <summary>
        /// Event handler that notifies about the consuming a workload step.
        /// </summary>
        public event WorkloadStepEventHandler OnWorkloadStep;

        /// <summary>
        /// Consumes a workload unit.
        /// </summary>
        protected void StepWorkload()
        {
            if (OnWorkloadStep != null) OnWorkloadStep(this);
        }

        #endregion

        #region internal-logic

        #endregion

        #region de-ctor

        /// <summary>
        /// de-ctor.
        /// </summary>
        ~Module() { Dispose(false); }

        /// <summary>
        /// Disposes the object.
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="disposing"></param>
        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing) // managed resources
                {
                    this._assembly = null;
                    this._attributes = null;
                    this.RootListItem.Childs.Clear();
                    this.RootListItem = null;
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
    public class PluginUpdateCompleteEventArgs : EventArgs
    {
        private bool _success;

        /// <summary>
        /// Returns true if plugin load was succesfull.
        /// </summary>
        public bool Success { get { return this._success; } }

        /// <summary>
        /// Constructs a new PluginUpdateCompleteEventArgs.
        /// </summary>
        /// <param name="Success">Did plugin loaded with success?</param>
        public PluginUpdateCompleteEventArgs(bool Success)
        {
            this._success = Success;
        }
    }

    #endregion
}
