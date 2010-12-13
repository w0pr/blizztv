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
 * $Id$
 */

using System;
using System.Drawing;
using BlizzTV.Properties;

namespace BlizzTV.ModuleLib
{
    /// <summary>
    /// Defines plugin attributes.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class)]
    public class ModuleAttributes : Attribute
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
        /// <param name="ModuleName">The plugin name.</param>
        /// <param name="Description">The plugin description.</param>
        /// <param name="IconName">Pass null as value or do not supply a value if you don't want to specify an icon for your plugin.</param>
        /// <remarks>Pass null as value or do not supply a value if you don't want to specify an icon for your plugin.</remarks>
        public ModuleAttributes(string Name, string Description, string IconName = null)
        {
            this._name = Name;
            this._description = Description;
            this._icon_name = IconName;
        }

        #endregion

        #region internal logic

        internal void ResolveResources() // resolves resources for the plugin and sets the icon if appliable.
        {
            if (this._icon_name != null) // if we've a supplied icon-file name.
            {
                using (var stream = (Bitmap)Resources.ResourceManager.GetObject(this._icon_name)) // get the resources stream
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
        ~ModuleAttributes() { Dispose(false); }

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
}
