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
 * $Id: LiveStream.cs 158 2010-11-30 14:06:50Z shalafiraistlin@gmail.com $
 */

using System;
using System.Collections;
using BlizzTV.ModuleLib.Utils;

namespace BlizzTV.Modules.Streams.Handlers
{
    public class LiveStream:Stream // livestream wrapper
    {
        #region ctor

        public LiveStream(string Name, string Slug, string Provider) : base(Name, Slug, Provider) { }

        #endregion 

        #region internal logic 

        public override void Update()
        {
            this.Link = string.Format("http://www.livestream.com/{0}", this.Slug); // the stream link.

            try
            {
                string api_url = string.Format("http://x{0}x.api.channel.livestream.com/2.0/info.json", this.Slug); // the api url.
                string response = WebReader.Read(api_url); // read the api response
                if (response != null) // start parsing json.
                {
                    Hashtable data = (Hashtable)JSON.JsonDecode(response);
                    data = (Hashtable)data["rss"]; 
                    data = (Hashtable)data["channel"];
                    this.IsLive = (bool)data["isLive"]; // is the stream live?
                    this.ViewerCount = Int32.Parse(data["currentViewerCount"].ToString()); // stream viewers count.
                    this.Description = (string)data["description"].ToString(); // stream description.
                }
            }
            catch (Exception e) { throw new Exception("LiveStream Wrapper Error.", e); } // throw exception to upper layer embedding details in the inner exception.
        }

        #endregion
    }
}
