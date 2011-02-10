/*    
 * Copyright (C) 2010-2011, BlizzTV Project - http://code.google.com/p/blizztv/
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

using System;
using System.Collections;
using BlizzTV.Utility.Web;

namespace BlizzTV.Streams.Parsers
{
    /// <summary>
    /// Parser for LiveStream.
    /// </summary>
    public class LiveStream:Stream
    {
        public LiveStream(StreamSubscription subscription) : base(subscription) { }

        public override void Update()
        {
            this.Link = string.Format("http://www.livestream.com/{0}", this.Slug); // the stream link.

            try
            {
                string apiUrl = string.Format("http://x{0}x.api.channel.livestream.com/2.0/info.json", this.Slug); // the api url.
                WebReader.Result result = WebReader.Read(apiUrl); // read the api response
                if (result.State != WebReader.States.Success) return;

                Hashtable data = (Hashtable)Json.JsonDecode(result.Response);
                data = (Hashtable)data["rss"];
                data = (Hashtable)data["channel"];
                this.IsLive = (bool)data["isLive"]; // is the stream live?
                this.ViewerCount = Int32.Parse(data["currentViewerCount"].ToString()); // stream viewers count.
                this.Description = (string)data["description"].ToString(); // stream description.
            }
            catch (Exception e) { throw new Exception("Stream module's livestream parser caught an exception: ", e); } // throw exception to upper layer embedding details in the inner exception.
        }
    }
}
