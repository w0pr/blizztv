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
 */

using System;
using System.Collections.Generic;
using System.Collections;
using LibBlizzTV.Utils;

namespace LibStreams.Handlers
{
    public class LiveStream:Stream
    {
        public LiveStream(string Title, string Slug, string Provider) : base(Title, Slug, Provider) { }

        public override void Update()
        {
            this.Link = string.Format("http://www.livestream.com/{0}", this.Slug);

            string api_url=string.Format("http://x{0}x.api.channel.livestream.com/2.0/info.json",this.Slug);
            string response = WebReader.Read(api_url);
            if (response != null)
            {
                Hashtable data = (Hashtable)JSON.JsonDecode(response);
                data = (Hashtable)data["rss"];
                data = (Hashtable)data["channel"];
                this.IsLive = (bool)data["isLive"];
                this.ViewerCount = Int32.Parse(data["currentViewerCount"].ToString());
                this.Description = (string)data["description"].ToString();
            }
        }
    }
}