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
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LibBlizzTV;
using LibBlizzTV.Settings;

namespace LibEvents
{
    public class Settings:PluginSettings
    {
        private static Settings _instance = new Settings();
        public static Settings Instance { get { return _instance; } }
        private Settings() : base("Events") { }

        public bool AllowEventNotifications { get { return this.GetBoolean("AllowEventNotifications", true); } set { this.Set("AllowEventNotifications", value); } }
        public bool AllowNotificationOfInprogressEvents { get { return this.GetBoolean("AllowNotificationOfInprogressEvents", true); } set { this.Set("AllowNotificationOfInprogressEvents", value); } }
        public int MinutesToNotifyBeforeEvent { get { return this.GetInt("MinutesToNotifyBeforeEvent", 15); } set { this.Set("MinutesToNotifyBeforeEvent", value); } }
        public int NumberOfDaysToShowEventsOnMainWindow { get { return this.GetInt("NumberOfDaysToShowEventsOnMainWindow", 7); } set { this.Set("NumberOfDaysToShowEventsOnMainWindow", value); } }
    }
}
