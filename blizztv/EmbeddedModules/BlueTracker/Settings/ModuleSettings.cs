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

using BlizzTV.InfraStructure.Modules.Settings;

namespace BlizzTV.EmbeddedModules.BlueTracker.Settings
{
    /// <summary>
    /// Settings for BlizzBlues module.
    /// </summary>
    public class ModuleSettings : ModuleSettingsBase
    {
        #region instance

        private static readonly ModuleSettings _instance = new ModuleSettings();
        public static ModuleSettings Instance { get { return _instance; } }

        #endregion

        private ModuleSettings() : base("BlizzBlues") { }
        
        /// <summary>
        /// Enables tracking of World of Warcraft blues.
        /// </summary>
        public bool TrackWorldofWarcraft { get { return this.GetBoolean("TrackWorldofWarcraft", true); } set { this.Set("TrackWorldofWarcraft", value); } }

        /// <summary>
        /// Enables tracking of Starcraft blues.
        /// </summary>
        public bool TrackStarcraft { get { return this.GetBoolean("TrackStarcraft", true); } set { this.Set("TrackStarcraft", value); } }

        /// <summary>
        /// Data update period in seconds.
        /// </summary>
        public int UpdatePeriod { get { return this.GetInt("UpdatePeriod", 60); } set { this.Set("UpdatePeriod", value); } }

        /// <summary>
        /// Enables notifications for the module.
        /// </summary>
        public bool NotificationsEnabled { get { return this.GetBoolean("NotificationsEnabled", true); } set { this.Set("NotificationsEnabled", value); } }
    }
}
