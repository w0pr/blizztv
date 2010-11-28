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
using Nini.Config;
using LibBlizzTV.Utils;

namespace LibBlizzTV
{
    /// <summary>
    /// Global settings that is used by both the BlizzTV and it's plugins.
    /// </summary>
    public sealed class GlobalSettings
    {
        private static GlobalSettings _instance = new GlobalSettings();

        /// <summary>
        /// Returns instance of GlobalSettings.
        /// </summary>
        public static GlobalSettings Instance { get { return _instance; } }

        private int _video_player_width = 640;
        private int _video_player_height = 385;
        private bool _auto_play_videos = true;
        private bool _player_windows_always_on_top = true;
        private bool _use_internal_viewers = true;

        /// <summary>
        /// The default video player width.
        /// </summary>
        public int VideoPlayerWidth { get { return this._video_player_width; } set { this._video_player_width = value; } }

        /// <summary>
        /// The default video player height.
        /// </summary>
        public int VideoPlayerHeight { get { return this._video_player_height; } set { this._video_player_height = value; } }

        /// <summary>
        /// States if video's should be played automatically.
        /// </summary>
        public bool AutoPlayVideos { get { return this._auto_play_videos; } set { this._auto_play_videos = value; } }

        /// <summary>
        /// Always on top setting for player windows.
        /// </summary>
        public bool PlayerWindowsAlwaysOnTop { get { return this._player_windows_always_on_top; } set { this._player_windows_always_on_top = value; } }

        /// <summary>
        /// The default content viewing-method.
        /// </summary>
        public bool UseInternalViewers { get { return this._use_internal_viewers; } set { this._use_internal_viewers = value; } }

        /// <summary>
        /// States the sleep mode in which plugin's should not automaticly refresh it's data.
        /// </summary>
        public bool InSleepMode = false;

        private GlobalSettings()
        {
            try
            {
                this._video_player_width = SettingsParser.Instance.Section("Global").GetInt("VideoPlayerWidth", 640);
                this._video_player_height = SettingsParser.Instance.Section("Global").GetInt("VideoPlayerHeight", 385);
                this._auto_play_videos = SettingsParser.Instance.Section("Global").GetBoolean("AutoPlayVideos", true);
                this._player_windows_always_on_top = SettingsParser.Instance.Section("Global").GetBoolean("PlayerWindowsAlwaysOnTop", true);
                this._use_internal_viewers = SettingsParser.Instance.Section("Global").GetBoolean("UseInternalViewers", true);
            }
            catch (Exception e)
            {
                Log.Instance.Write(LogMessageTypes.ERROR, string.Format("GlobalSettings load exception: {0}", e.ToString()));
            }
        }

        /// <summary>
        /// Saves the global settings.
        /// </summary>
        public void Save()
        {
            IConfig config = SettingsParser.Instance.Section("Global");
            if (config == null) config = SettingsParser.Instance.AddSection("Global");
            config.Set("VideoPlayerWidth", this._video_player_width);
            config.Set("VideoPlayerHeight", this._video_player_height);
            config.Set("AutoPlayVideos", this._auto_play_videos);
            config.Set("PlayerWindowsAlwaysOnTop", this._player_windows_always_on_top);
            config.Set("UseInternalViewers", this._use_internal_viewers);
            SettingsParser.Instance.Save();
        }
    }
}
