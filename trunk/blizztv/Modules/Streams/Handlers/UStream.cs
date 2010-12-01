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

using System;
using System.Collections;
using BlizzTV.CommonLib.Web;

namespace BlizzTV.Modules.Streams.Handlers
{
    public class UStream:Stream // ustream wrapper
    {
        #region members

        private UInt32 _stream_id;

        #endregion 

        #region ctor

        public UStream(string Name, string Slug, string Provider) : base(Name, Slug, Provider) { }

        #endregion

        #region internal logic

        public override void Update()
        {
            this.Link = string.Format("http://www.ustream.com/channel/{0}", this.Slug); // the stream link.

            try
            {
                string api_url = string.Format("http://api.ustream.tv/json/channel/{0}/listAllChannels?key={1}", this.Slug, "F7DE9C9A56F4ABB48D170A9881E5AF66"); // the api url
                string response = WebReader.Read(api_url); // read the api response.

                Hashtable data = (Hashtable)JSON.JsonDecode(response); // start parsing json.
                ArrayList results = (ArrayList)data["results"]; // the results object.
                if (results.Count > 0)
                {
                    Hashtable table = (Hashtable)results[0];
                    if ((string)table["status"].ToString() == "live") // if the stream is live.
                    {
                        this.IsLive = true;
                        this._stream_id = UInt32.Parse(table["id"].ToString()); // the stream id.
                        this.ViewerCount = Int32.Parse(table["viewersNow"].ToString()); // viewers count.
                        this.Description = (string)table["title"].ToString(); // stream description.
                    }
                }
            }
            catch (Exception e) { throw new Exception("Ustream Wrapper Error.", e); } // throw exception to upper layer embedding details in the inner exception.
        }

        public override void Process() // for ustream we also need to replace stream_id variable in movie and flash vars templates.
        {
            base.Process(); // base processor should also work (to let it replace the slug variable).
            this.Movie = this.Movie.Replace("%stream_id%", this._stream_id.ToString()); // replace stream_id in movie template.
            this.FlashVars = this.FlashVars.Replace("%stream_id%", this._stream_id.ToString()); // replace stream_id in flashvars template.
            if (this.ChatAvailable) this.ChatMovie = this.ChatMovie.Replace("%stream_id%", this._stream_id.ToString());
        }

        #endregion
    }
}
