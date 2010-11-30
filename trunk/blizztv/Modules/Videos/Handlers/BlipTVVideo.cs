﻿/*    
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
 * $Id: BlipTVVideo.cs 158 2010-11-30 14:06:50Z shalafiraistlin@gmail.com $
 */

namespace BlizzTV.Modules.Videos.Handlers
{
    public class BlipTVVideo:Video
    {
        public BlipTVVideo(string Title, string Guid, string Link, string Provider)
            : base(Title, Guid, Link, Provider) { }
    }
}