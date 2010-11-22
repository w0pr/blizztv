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
    [Serializable]
    public class GlobalSettings
    {
        /// <summary>
        /// The default content viewing-method.
        /// </summary>
        public ContentViewMethods ContentViewer = ContentViewMethods.INTERNAL_VIEWERS;

        /// <summary>
        /// The default video player width.
        /// </summary>
        public int VideoPlayerWidth = 640;

        /// <summary>
        /// The default video player height.
        /// </summary>
        public int VideoPlayerHeight = 385;

        /// <summary>
        /// States if video's should be played automatically.
        /// </summary>
        public bool VideoAutoPlay = true;

        /// <summary>
        /// 
        /// </summary>
        public bool PlayerWindowsAlwaysOnTop = true;
    }

    /// <summary>
    /// Available content-viewing methods.
    /// </summary>
    public enum ContentViewMethods
    {
        /// <summary>
        /// Render content with internal viewers.
        /// </summary>
        INTERNAL_VIEWERS,
        /// <summary>
        /// Render content with computer's default web-browser.
        /// </summary>
        DEFAULT_WEB_BROWSER
    }
}
