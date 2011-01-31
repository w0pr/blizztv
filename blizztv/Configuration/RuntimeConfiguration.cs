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

namespace BlizzTV.Configuration
{
    public sealed class RuntimeConfiguration
    {
        #region Instance

        private static RuntimeConfiguration _instance = new RuntimeConfiguration();
        public static RuntimeConfiguration Instance { get { return _instance; } }

        #endregion

        /// <summary>
        /// States the sleep mode in which plugin's should not automaticly refresh it's data.
        /// </summary>
        public bool InSleepMode = false;

        /// <summary>
        /// Indicates BlizzTV started up by the system itself on boot.
        /// </summary>
        public bool StartedOnSystemStartup = false;

        private RuntimeConfiguration() { }
    }
}
