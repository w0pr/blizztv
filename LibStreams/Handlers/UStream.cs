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
    public class UStream:Stream
    {
        private UInt32 _stream_id;

        public UStream(string Title, string Slug, string Provider) : base(Title, Slug, Provider) { }

        public override void Update()
        {
            this.Link = string.Format("http://www.ustream.com/channel/{0}", this.Slug);

            string api_url = string.Format("http://api.ustream.tv/json/channel/{0}/listAllChannels?key={1}", this.Slug,"F7DE9C9A56F4ABB48D170A9881E5AF66");
            string response = WebReader.Read(api_url);

            Hashtable data = (Hashtable)JSON.JsonDecode(response);
            try
            {
                ArrayList results = (ArrayList)data["results"];
                if (results.Count > 0)
                {
                    Hashtable table = (Hashtable)results[0];
                    if ((string)table["status"].ToString() == "live")
                    {
                        this.IsLive = true;
                        this._stream_id = UInt32.Parse(table["id"].ToString());
                        this.ViewerCount = Int32.Parse(table["viewersNow"].ToString());
                        this.Description = (string)table["title"].ToString();
                    }
                }
            }
            catch (Exception e) { }
        }

        public override void Process()
        {
            base.Process();
            this._movie = this._movie.Replace("%stream_id%", this._stream_id.ToString());
            this._flash_vars = this._flash_vars.Replace("%stream_id%", this._stream_id.ToString());
        }
    }
}
