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

using BlizzTV.Modules.Settings;

namespace BlizzTV.Events
{
    /// <summary>
    /// Module Events settings.
    /// </summary>
    public class Settings:ModuleSettings
    {
        #region instance

        private static Settings _instance = new Settings();
        public static Settings Instance { get { return _instance; } }

        #endregion

        private Settings() : base("Events") { }
         
        /// <summary>
        /// Enables notifications for events.
        /// </summary>
        public bool EventNotificationsEnabled { get { return this.GetBoolean("EventNotificationsEnabled", true); } set { this.Set("EventNotificationsEnabled", value); } }

        /// <summary>
        /// Enables notifications for events that are in-progress.
        /// </summary>
        public bool InProgressEventNotificationsEnabled { get { return this.GetBoolean("InProgressEventNotificationsEnabled", true); } set { this.Set("InProgressEventNotificationsEnabled", value); } }

        /// <summary>
        /// Minutes to notify about before an event.
        /// </summary>
        public int MinutesToNotifyBeforeEvent { get { return this.GetInt("MinutesToNotifyBeforeEvent", 15); } set { this.Set("MinutesToNotifyBeforeEvent", value); } }

        /// <summary>
        /// Number of days to show on main window.
        /// </summary>
        public int NumberOfDaysToShowEventsOnMainWindow { get { return this.GetInt("NumberOfDaysToShowEventsOnMainWindow", 7); } set { this.Set("NumberOfDaysToShowEventsOnMainWindow", value); } }

        /// <summary>
        /// Event viewer window's width.
        /// </summary>
        public int EventViewerWindowWidth { get { return this.GetInt("EventViewerWindowWidth", 467); } set { this.Set("EventViewerWindowWidth", value); } }

        /// <summary>
        /// Event viewer window's height.
        /// </summary>
        public int EventViewerWindowHeight { get { return this.GetInt("EventViewerWindowHeight", 264); } set { this.Set("EventViewerWindowHeight", value); } }

        /// <summary>
        /// Calendar window's width.
        /// </summary>
        public int CalendarWindowWidth { get { return this.GetInt("CalendarWindowWidth", 780); } set { this.Set("CalendarWindowWidth", value); } }

        /// <summary>
        /// Calendar window's height.
        /// </summary>
        public int CalendarWindowHeight { get { return this.GetInt("CalendarWindowHeight", 515); } set { this.Set("CalendarWindowHeight", value); } }
    }
}
