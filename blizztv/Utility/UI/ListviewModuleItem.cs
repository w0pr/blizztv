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

using System.Windows.Forms;
using BlizzTV.InfraStructure.Modules;

namespace BlizzTV.Utility.UI
{
    /// <summary>
    /// Provides module-item wrappers for listviews.
    /// </summary>
    public class ListviewModuleItem : ListViewItem
    {
        /// <summary>
        /// The module name.
        /// </summary>
        public string ModuleName { get { return this.SubItems[1].Text; } }

        public ListviewModuleItem(ModuleController p)
        {
            this.ImageKey = p.Attributes.Name;
            this.SubItems.Add(p.Attributes.Name);
            this.SubItems.Add(p.Attributes.Description);
        }
    }
}
