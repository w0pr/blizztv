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

using BlizzTV.ModuleLib.Settings;

namespace BlizzTV.Streams
{
    public class Settings : ModuleSettings
    {
        #region instance

        private static Settings _instance = new Settings();
        public static Settings Instance { get { return _instance; } }

        #endregion

        private Settings() : base("Streams") { }

        public bool NotificationsEnabled { get { return this.GetBoolean("NotificationsEnabled", true); } set { this.Set("NotificationsEnabled", value); } }
        public int UpdatePeriod { get { return this.GetInt("UpdatePeriod", 60); } set { this.Set("UpdatePeriod", value); } }
        public bool AutomaticallyOpenChat { get { return this.GetBoolean("AutomaticallyOpenChat", false); } set { this.Set("AutomaticallyOpenChat", value); } }
        public int ChatWindowWidth { get { return this.GetInt("ChatWindowWidth", 350); } set { this.Set("ChatWindowWidth", value); } }
        public int ChatWindowHeight { get { return this.GetInt("ChatWindowHeight", 385); } set { this.Set("ChatWindowHeight", value); } }
    }
}
