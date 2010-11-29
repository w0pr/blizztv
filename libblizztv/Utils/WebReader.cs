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
using System.Net;
using System.IO;

namespace LibBlizzTV.Utils
{
    /// <summary>
    /// Reads data from web and returns.
    /// </summary>
    public static class WebReader
    {
        /// <summary>
        /// Returns content's of a given http url.
        /// </summary>
        /// <param name="url">The web-page's address.</param>
        /// <returns>Returns contents of the given wep-page.</returns>
        public static string Read(string url)
        {
            string buffer;
            using (WebClient client = new WebClient())
            {
                try
                {
                    client.Headers.Add(HttpRequestHeader.UserAgent, "Mozilla/4.0 (compatible; MSIE 7.0; Windows NT 5.1; .NET CLR 2.0.50727)");
                    using (StreamReader reader = new StreamReader(client.OpenRead(url)))
                    {
                        buffer = reader.ReadToEnd();
                    }
                }
                catch (Exception e)
                {
                    Log.Instance.Write(LogMessageTypes.ERROR,string.Format("WebReader:Read() Exception: {0}",e.ToString()));
                    return null;
                }
            }
            return buffer;
        }
    }
}
