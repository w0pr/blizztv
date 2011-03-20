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

namespace BlizzTV.EmbeddedModules.Streams.Parsers
{
    /// <summary>
    /// Ustream parser.
    /// </summary>
    public class UStream : Stream
    {
        private UInt32 _streamId;

        public UStream(StreamSubscription subscription) : base(subscription) { }

        public override bool Parse()
        {
            this.Link = string.Format("http://www.ustream.com/channel/{0}", this.Slug); // the stream link.

            try
            {
                var apiUrl = string.Format("http://api.ustream.tv/json/channel/{0}/listAllChannels?key={1}", this.Slug, "0FB451E8E15DCEBECE707004AA0166E8"); // the api url
                WebReader.Result result = WebReader.Read(apiUrl); // read the api response.
                if (result.State != WebReader.States.Success) return false;

                var data = (Hashtable)Json.JsonDecode(result.Response); // start parsing json.
                var resultsObject = (ArrayList)data["results"]; // the results object.
                if (resultsObject.Count > 0)
                {
                    var table = (Hashtable)resultsObject[0];
                    if ((string)table["status"].ToString() == "live") // if the stream is live.
                    {
                        this.IsLive = true;
                        this._streamId = UInt32.Parse(table["id"].ToString()); // the stream id.
                        this.ViewerCount = Int32.Parse(table["viewersNow"].ToString()); // viewers count.
                        this.Description = (string)table["title"].ToString(); // stream description.
                    }
                }

                this.Process();
            }
            catch (Exception) { return false; } // throw exception to upper layer embedding details in the inner exception.

            return true;
        }

        public override void Process() // for ustream we also need to replace stream_id variable in movie and flash vars templates.
        {
            base.Process(); // base processor should also work (to let it replace the slug variable).
            this.Movie = this.Movie.Replace("%stream_id%", this._streamId.ToString()); // replace stream_id in movie template.
            if (this.ChatAvailable) this.ChatMovie = this.ChatMovie.Replace("%stream_id%", this._streamId.ToString());
        }
    }
}
