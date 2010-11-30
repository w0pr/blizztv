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

using System.Text.RegularExpressions;

namespace LibVideos.Handlers
{
    public class YoutubeVideo:Video
    {
        private static Regex _regex = new Regex(@"http://www\.youtube\.com/watch\?v\=(.*)\&", RegexOptions.Compiled); // compiled regex for reading video id's.

        public YoutubeVideo(string Title, string Guid, string Link, string Provider)
            : base(Title, Guid, Link, Provider)
        {
            Match m = _regex.Match(this.Link);
            if (m.Success) this.VideoID = m.Groups[1].Value;
        }
    }
}
