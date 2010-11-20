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
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using LibBlizzTV.Utils;

namespace LibStreams.Handlers
{
    public class Own3DTV:Stream
    {
        private Regex _regex = new Regex("liveViewers=(.*)&liveStatus=(.*)&liveVerified=.*", RegexOptions.Compiled);

        public Own3DTV(string Name, string Slug, string Provider) : base(Name, Slug, Provider) { }

        public override void Update()
        {
            Random rnd=new Random();
            this.Link = string.Format("http://www.own3d.tv/live/{0}", this.Slug);

            try
            {
                string api_url = string.Format("http://static.ec.own3d.tv/live_tmp/{0}.txt?{1}", this.Slug, rnd.Next(99999));
                string response = WebReader.Read(api_url);

                Match m = _regex.Match(response);
                if (m.Success)
                {
                    if (m.Groups[2].ToString().ToLower() == "true") this.IsLive = true; else this.IsLive = false;
                    this.ViewerCount = int.Parse(m.Groups[1].ToString());
                }
            }
            catch (Exception e) { throw new Exception("Own3dTV Wrapper Error.", e); } // throw exception to upper layer embedding details in the inner exception.
        }
    }
}
