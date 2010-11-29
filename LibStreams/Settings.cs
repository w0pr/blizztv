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

namespace LibStreams
{
    public class Settings : PluginSettings
    {
        private static Settings _instance = new Settings();
        public static Settings Instance { get { return _instance; } }
        private Settings() : base("Streams") { }

        public int UpdateEveryXMinutes { get { return this.GetInt("UpdateEveryXMinutes", 60); } set { this.Set("UpdateEveryXMinutes", value); } }
        public bool AutomaticallyOpenChatForAvailableStreams { get { return this.GetBoolean("AutomaticallyOpenChatForAvailableStreams", false); } set { this.Set("AutomaticallyOpenChatForAvailableStreams", value); } }
        public int StreamChatWindowWidth { get { return this.GetInt("StreamChatWindowWidth", 640); } set { this.Set("StreamChatWindowWidth", value); } }
        public int StreamChatWindowHeight { get { return this.GetInt("StreamChatWindowHeight", 385); } set { this.Set("StreamChatWindowHeight", value); } }
    }
}
