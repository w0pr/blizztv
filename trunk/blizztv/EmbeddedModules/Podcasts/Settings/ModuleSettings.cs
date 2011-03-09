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

namespace BlizzTV.EmbeddedModules.Podcasts.Settings
{
    public class ModuleSettings : ModuleSettingsBase
    {
        #region instance

        private static ModuleSettings _instance = new ModuleSettings();
        public static ModuleSettings Instance { get { return _instance; } }

        #endregion 

        private ModuleSettings() : base("Podcasts") { }

        /// <summary>
        /// Podcast module's update period.
        /// </summary>
        public int UpdatePeriod { get { return this.GetInt("UpdatePeriod", 60); } set { this.Set("UpdatePeriod", value); } }

        /// <summary>
        /// Enables notifications for podcasts module.
        /// </summary>
        public bool NotificationsEnabled { get { return this.GetBoolean("NotificationsEnabled", true); } set { this.Set("NotificationsEnabled", value); } }

        /// <summary>
        /// Sets podcast player windows width.
        /// </summary>
        public int PlayerWidth { get { return this.GetInt("PlayerWidth", 275); } set { this.Set("PlayerWidth", value); } }

        /// <summary>
        /// Sets podcast player windows height.
        /// </summary>
        public int PlayerHeight { get { return this.GetInt("PlayerHeight", 150); } set { this.Set("PlayerHeight", value); } }
    }
}
