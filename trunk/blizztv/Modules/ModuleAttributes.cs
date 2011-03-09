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
using System.Drawing;

namespace BlizzTV.Modules
{
    /// <summary>
    /// Defines module attributes.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class)]
    public class ModuleAttributes : Attribute 
    {
        private readonly string _iconName;
        private bool _disposed = false;

        /// <summary>
        /// The module name.
        /// </summary>
        public string Name { get; private set; } 
        
        /// <summary>
        /// The module description.
        /// </summary>
        public string Description { get; private set; } 

        /// <summary>
        /// The module icon.
        /// </summary>
        public Bitmap Icon { get; private set; } // The module icon.


        /// <summary>
        /// The module functionality
        /// </summary>
        public ModuleFunctionality Functionality { get; private set; }

        /// <summary>
        /// Module attributes constructor.
        /// </summary>
        /// <param name="name">The module name.</param>
        /// <param name="description">The module description.</param>       
        /// <param name="iconName">Module icon's name.</param>
        /// <param name="functionality">The module functionality.</param>
        public ModuleAttributes(string name, string description, string iconName = null, ModuleFunctionality functionality = ModuleFunctionality.RendersTreeItems)
        {
            Icon = null;
            this.Name = name;
            this.Description = description;
            this.Functionality = functionality;
            this._iconName = iconName;
        }

        /// <summary>
        /// Resolves resources for the module and sets the icon if appliable.
        /// </summary>
        public void ResolveResources() 
        {
            if (this._iconName == null) return; // if no icon name is supplied, just ignore than.

            using (var stream = (Bitmap)Assets.Images.Icons.Png._16.ResourceManager.GetObject(this._iconName)) // get the icon stream from resource manager.
            {
                this.Icon = stream != null ? new Bitmap(stream) : Assets.Images.Icons.Png._16.blizztv; // if asked icon does not exist, then use the default icon.                                   
            }
        }

        [Flags]
        /// <summary>
        /// States module functionality.
        /// </summary>
        public enum ModuleFunctionality
        {
            /// <summary>
            /// Module renders items for main-window treeview.
            /// </summary>
            RendersTreeItems,
            /// <summary>
            /// Module renders main-window menus.
            /// </summary>
            RendersMenus
        }

        #region de-ctor

        ~ModuleAttributes() { Dispose(false); }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        private void Dispose(bool disposing)
        {
            if (this._disposed) return;
            if (disposing) // managed resources
            {
                this.Icon.Dispose();
                this.Icon = null;
            }
            _disposed = true;
        }

        #endregion
    }
}
