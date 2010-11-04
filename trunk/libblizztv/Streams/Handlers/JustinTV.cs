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
using System.Collections;

namespace LibBlizzTV.Streams.Handlers
{
    public class JustinTV:Stream
    {
        public JustinTV() { }

        public override void Update()
        {
            string api_url = string.Format("http://api.justin.tv/api/stream/list.json?channel={0}", this.Slug);
            string response = Utils.WebReader.Read(api_url);

            ArrayList data = (ArrayList)Utils.JSON.JsonDecode(response);
            if (data.Count > 0)
            {
                this.IsLive = true;
                Hashtable table = (Hashtable)data[0];
                this.ViewerCount = Int32.Parse(table["stream_count"].ToString());
                this.Description = (string)table["title"].ToString();
            }
        }
    }
}
