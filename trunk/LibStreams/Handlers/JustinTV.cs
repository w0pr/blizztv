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
using LibBlizzTV.Utils;

namespace LibStreams.Handlers
{
    public class JustinTV:Stream // justintv wrapper
    {
        #region ctor

        public JustinTV(string Name, string Slug, string Provider) : base(Name, Slug, Provider) { }

        #endregion

        #region internal logic 

        public override void Update()
        {
            this.Link = string.Format("http://www.justin.tv/{0}", this.Slug); // the stream link.

            try
            {
                string api_url = string.Format("http://api.justin.tv/api/stream/list.json?channel={0}", this.Slug); // the api url.
                string response = WebReader.Read(api_url); // read the api response.

                ArrayList data = (ArrayList)JSON.JsonDecode(response); // start parsing the json.
                if (data.Count > 0)
                {
                    this.IsLive = true; // is the stream live?
                    Hashtable table = (Hashtable)data[0];
                    this.ViewerCount = Int32.Parse(table["stream_count"].ToString()); // stream viewers count.
                    this.Description = (string)table["title"].ToString(); // stream description.
                }
            }
            catch (Exception e) { throw new Exception("JustinTV Wrapper Error.", e); } // throw exception to upper layer embedding details in the inner exception.
        }

        #endregion
    }
}
