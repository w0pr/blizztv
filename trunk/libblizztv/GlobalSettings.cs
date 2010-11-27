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

namespace LibBlizzTV
{
    /// <summary>
    /// Global settings that is used by both the BlizzTV and it's plugins.
    /// </summary>
    public sealed class GlobalSettings
    {
        private static GlobalSettings _instance = new GlobalSettings();

        /// <summary>
        /// Returns instance of Global.Settings.
        /// </summary>
        public static GlobalSettings Instance { get { return _instance; } }

        private GlobalSettings()
        {
            if (Properties.Settings.Default.NeedsUpgrade)
            {
                Properties.Settings.Default.Upgrade();
                Properties.Settings.Default.NeedsUpgrade = false;
                Properties.Settings.Default.Save();
            }
        }

        /// <summary>
        /// The default content viewing-method.
        /// </summary>
        public ContentViewerModes ContentViewerMode = Properties.Settings.Default.ContentViewerMode;

        /// <summary>
        /// The default video player width.
        /// </summary>
        public int VideoPlayerWidth = Properties.Settings.Default.VideoPlayerWidth;

        /// <summary>
        /// The default video player height.
        /// </summary>
        public int VideoPlayerHeight = Properties.Settings.Default.VideoPlayerHeight;

        /// <summary>
        /// States if video's should be played automatically.
        /// </summary>
        public bool VideoAutoPlay = Properties.Settings.Default.AutoplayVideos;

        /// <summary>
        /// Always on top setting for player windows.
        /// </summary>
        public bool PlayerWindowsAlwaysOnTop = Properties.Settings.Default.PlayerWindowsAlwaysOnTop;

        /// <summary>
        /// States the sleep mode in which plugin's should not automaticly refresh it's data.
        /// </summary>
        public bool InSleepMode = false;

        /// <summary>
        /// Saves the global settings.
        /// </summary>
        public void Save()
        {
            Properties.Settings.Default.Save();
        }
    }

    /// <summary>
    /// Available content-viewing methods.
    /// </summary>
    [Serializable]
    public enum ContentViewerModes
    {
        /// <summary>
        /// Render content with internal viewers.
        /// </summary>
        InternalViewers,
        /// <summary>
        /// Render content with computer's default web-browser.
        /// </summary>
        DefaultWebBrowser
    }
}
