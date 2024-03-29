﻿/*    
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
using System.Text.RegularExpressions;
using BlizzTV.Log;
using BlizzTV.Utility.Web;

namespace BlizzTV.EmbeddedModules.Streams.Parsers
{
    /// <summary>
    /// Own3D.tv parser
    /// </summary>
    public class Own3Dtv : Stream
    {
        private readonly Regex _regex = new Regex("liveViewers=(.*)&liveStatus=(.*)&liveVerified=.*", RegexOptions.Compiled);

        public Own3Dtv(StreamSubscription subscription) : base(subscription) { }

        public override bool Parse()
        {
            var rnd=new Random();
            this.Link = string.Format("http://www.own3d.tv/live/{0}", this.Slug);

            try
            {
                var apiUrl = string.Format("http://static.ec.own3d.tv/live_tmp/{0}.txt?{1}", this.Slug, rnd.Next(99999));
                WebReader.Result result = WebReader.Read(apiUrl);
                if (result.State != WebReader.States.Success) return false;

                Match m = _regex.Match(result.Response);
                if (m.Success)
                {
                    this.IsLive = m.Groups[2].ToString().ToLower() == "true";
                    this.ViewerCount = int.Parse(m.Groups[1].ToString());
                }

                this.Process();
                return true;
            }
            catch (Exception e)
            {
                LogManager.Instance.Write(LogMessageTypes.Error, string.Format("Stream module - Own3DTV parser caught an exception: {0}", e)); 
                return false;
            }
        }
    }
}
