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
    public class Settings:ModuleSettings
    {
        #region instance

        private static Settings _instance = new Settings();
        public static Settings Instance { get { return _instance; } }

        #endregion

        private Settings() : base("Events") { }

        public bool EventNotificationsEnabled { get { return this.GetBoolean("EventNotificationsEnabled", true); } set { this.Set("EventNotificationsEnabled", value); } }

        public bool InProgressEventNotificationsEnabled { get { return this.GetBoolean("InProgressEventNotificationsEnabled", true); } set { this.Set("InProgressEventNotificationsEnabled", value); } }

        public int MinutesToNotifyBeforeEvent { get { return this.GetInt("MinutesToNotifyBeforeEvent", 15); } set { this.Set("MinutesToNotifyBeforeEvent", value); } }

        public int NumberOfDaysToShowEventsOnMainWindow { get { return this.GetInt("NumberOfDaysToShowEventsOnMainWindow", 7); } set { this.Set("NumberOfDaysToShowEventsOnMainWindow", value); } }

        public int EventViewerWindowWidth { get { return this.GetInt("EventViewerWindowWidth", 467); } set { this.Set("EventViewerWindowWidth", value); } }

        public int EventViewerWindowHeight { get { return this.GetInt("EventViewerWindowHeight", 264); } set { this.Set("EventViewerWindowHeight", value); } }

        public int CalendarWindowWidth { get { return this.GetInt("CalendarWindowWidth", 780); } set { this.Set("CalendarWindowWidth", value); } }

        public int CalendarWindowHeight { get { return this.GetInt("CalendarWindowHeight", 515); } set { this.Set("CalendarWindowHeight", value); } }
    }
}
