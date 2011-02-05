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

namespace BlizzTV.Settings
{
    /// <summary>
    /// Global settings that is used by both the BlizzTV and it's modules.
    /// </summary>
    public sealed class GlobalSettings : Settings
    {
        #region instance

        private static GlobalSettings _instance = new GlobalSettings();
        public static GlobalSettings Instance { get { return _instance; } }

        #endregion

        // The default video player width.
        public int VideoPlayerWidth { get { return this.GetInt("VideoPlayerWidth", 640); } set { this.Set("VideoPlayerWidth", value); } }

        // The default video player height.
        public int VideoPlayerHeight { get { return this.GetInt("VideoPlayerHeight", 385); } set { this.Set("VideoPlayerHeight", value); } }

        // States if video's should be played automatically.
        public bool AutoPlayVideos { get { return this.GetBoolean("AutoPlayVideos", true); } set { this.Set("AutoPlayVideos", value); } }

        // Always on top setting for player windows.
        public bool PlayerWindowsAlwaysOnTop { get { return this.GetBoolean("PlayerWindowsAlwaysOnTop", true); } set { this.Set("PlayerWindowsAlwaysOnTop", value); } }

        // The default content viewing-method.
        public bool UseInternalViewers { get { return this.GetBoolean("UseInternalViewers", true); } set { this.Set("UseInternalViewers", value); } }

        // Enables notifications.
        public bool NotificationsEnabled { get { return this.GetBoolean("NotificationsEnabled", true); } set { this.Set("NotificationsEnabled", value); } }

        // Enables notification sounds.
        public bool NotificationSoundsEnabled { get { return this.GetBoolean("NotificationSoundsEnabled", true); } set { this.Set("NotificationSoundsEnabled", value); } }

        // Holds the selected notification sound.
        public string NotificationSound { get { return this.GetString("NotificationSound", "DefaultNotification"); } set { this.Set("NotificationSound", value); } }

        // Allow automatic update checks?
        public bool AllowAutomaticUpdateChecks { get { return this.GetBoolean("AllowAutomaticUpdateChecks", true); } set { this.Set("AllowAutomaticUpdateChecks", value); } }

        // Allow beta version notifications?
        public bool AllowBetaVersionNotifications { get { return this.GetBoolean("AllowBetaVersionNotifications", true); } set { this.Set("AllowBetaVersionNotifications", value); } }

        private GlobalSettings() : base("Global") { }
    }
}
