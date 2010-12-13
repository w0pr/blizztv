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

using System.Collections.Generic;
using System.Linq;

namespace BlizzTV.UI
{
    public sealed class Settings : CommonLib.Settings.Settings // The UI settings.
    {
        #region singleton instance

        private static Settings _instance = new Settings();
        public static Settings Instance { get { return _instance; } }

        #endregion  

        // do we need an initial configuration?
        public bool NeedInitialConfig { get { return this.GetBoolean("NeedInitialConfig", true); } set { this.Set("NeedInitialConfig", value); } }

        // should the main window minimize to system tray instead of closing when close button is clicked?
        public bool MinimizeToSystemTray { get { return this.GetBoolean("MinimizeToSystemTray", true); } set { this.Set("MinimizeToSystemTray", value); } }

        // allow automatic update checks?
        public bool AllowAutomaticUpdateChecks { get { return this.GetBoolean("AllowAutomaticUpdateChecks", true); } set { this.Set("AllowAutomaticUpdateChecks", value); } }

        // allow beta version notifications?
        public bool AllowBetaVersionNotifications { get { return this.GetBoolean("AllowBetaVersionNotifications", true); } set { this.Set("AllowBetaVersionNotifications", value); } }

        // enable debug logger?
        public bool EnableDebugLogging { get { return this.GetBoolean("EnableDebugLogging", true); } set { this.Set("EnableDebugLogging", value); } }

        // enable debug console?
        public bool EnableDebugConsole { get { return this.GetBoolean("EnableDebugConsole", false); } set { this.Set("EnableDebugConsole", value); } }

        // the plugin settings wrapper.
        public Plugins Modules = new Plugins(); 

        private Settings() : base("UI") { }              
    }

    public class Plugins : CommonLib.Settings.Settings // The plugin settings wrapper.
    {
        // the plugin settings list. TODO: convert to readonly-dictionary.
        public Dictionary<string, bool> List 
        {
            get
            {
                string[] keys = this.GetEntryKeys();
                return keys.ToDictionary(key => key, key => this.GetBoolean(key, false));
            }
        }
        
        // Is plugin enabled?
        public bool Enabled(string name) { return this.GetBoolean(name, false); }

        // Enables a plugin.
        public void Enable(string name) { this.Set(name, "On"); }

        // Disables a plugin.
        public void Disable(string name) { this.Set(name, "Off"); } 
        
        public Plugins() : base("Plugins") { }
    }
}
