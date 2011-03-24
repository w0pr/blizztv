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

namespace BlizzTV.InfraStructure.Modules.Subscriptions.Catalog
{
    public class CatalogSettings : BlizzTV.Settings.Settings
    {
        #region singleton instance

        private static CatalogSettings _instance = new CatalogSettings();
        public static CatalogSettings Instance { get { return _instance; } }

        #endregion  

        // catalog browser window width
        public int BrowserWidth { get { return this.GetInt("BrowserWidth", 640); } set { this.Set("BrowserWidth", value); } }

        // catalog browser window height
        public int BrowserHeight { get { return this.GetInt("BrowserHeight", 385); } set { this.Set("BrowserHeight", value); } }

        private CatalogSettings() : base("Catalog") { }   
    }
}
