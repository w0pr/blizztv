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
    [AttributeUsage(AttributeTargets.Class)]
    public class ModuleAttributes : Attribute // Defines module attributes.
    {
        private readonly string _iconName;
        private bool _disposed = false;

        public string Name { get; private set; } // The module name.
        public string Description { get; private set; } // The module description.
        public Bitmap Icon { get; private set; } // The module icon.

        public ModuleAttributes(string name, string description, string iconName = null)
        {
            Icon = null;
            this.Name = name;
            this.Description = description;
            this._iconName = iconName;
        }

        internal void ResolveResources() // resolves resources for the plugin and sets the icon if appliable.
        {
            if (this._iconName == null) return; // if we've a supplied icon-file name.

            using (var stream = (Bitmap)Resources.ResourceManager.GetObject(this._iconName)) // get the resource stream.
            {
                this.Icon = stream != null ? new Bitmap(stream) : Resources.blizztv_16; // if asked icon does not exists use the default icon.                                   
            }
        }

        public override string ToString() { return this.Name; }

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
