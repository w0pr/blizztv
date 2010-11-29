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
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LibBlizzTV;
using LibBlizzTV.Utils;
using LibBlizzTV.Settings;

namespace BlizzTV
{
    public sealed class Settings : LibBlizzTV.Settings.Settings
    {
        private static Settings _instance = new Settings();        
        public static Settings Instance { get { return _instance; } }
        public Plugins Plugins = new Plugins();

        public bool NeedInitialConfig { get { return this.GetBoolean("NeedInitialConfig", true); } set { this.Set("NeedInitialConfig", value); } }
        public bool MinimizeToSystemTray { get { return this.GetBoolean("MinimizeToSystemTray", true); } set { this.Set("MinimizeToSystemTray", value); } }
        public bool AllowAutomaticUpdateChecks { get { return this.GetBoolean("AllowAutomaticUpdateChecks", true); } set { this.Set("AllowAutomaticUpdateChecks", value); } }
        public bool AllowBetaVersionNotifications { get { return this.GetBoolean("AllowBetaVersionNotifications", true); } set { this.Set("AllowBetaVersionNotifications", value); } }
        public bool EnableDebugLogging { get { return this.GetBoolean("EnableDebugLogging", true); } set { this.Set("EnableDebugLogging", value); } }
        public bool EnableDebugConsole { get { return this.GetBoolean("EnableDebugConsole", false); } set { this.Set("EnableDebugConsole", value); } }

        private Settings() : base("UI") { }       
    }

    public class Plugins : LibBlizzTV.Settings.Settings
    {
        public Plugins() : base("Plugins") { }

        public void Disable(string Name) { this.Set(Name, "Off"); }
        public void Enable(string Name) { this.Set(Name, "On"); }
        public bool Enabled(string Name) { return this.GetBoolean(Name, false); }

        public Dictionary<string, bool> List
        {
            get
            {
                Dictionary<string, bool> entries = new Dictionary<string, bool>();
                string[] keys = this.GetEntryKeys();
                foreach (string key in keys) { entries.Add(key, this.GetBoolean(key, false)); }
                return entries;
            }
        }
    }
}
