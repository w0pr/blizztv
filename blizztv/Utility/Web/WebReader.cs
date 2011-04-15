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

using System.Net;
using System.IO;
using BlizzTV.Log;

namespace BlizzTV.Utility.Web
{
    /// <summary>
    /// Provides web reading capabilities.
    /// </summary>
    public static class WebReader
    {
        public static Result Read(string url, int timeout = 30 * 1000)
        {            
            var result=new Result(); // our result object.

            try
            {
                var request = (HttpWebRequest) WebRequest.Create(url);
                request.UserAgent = "Mozilla/5.0 (Windows NT 6.1; rv:2.0) Gecko/20110319 Firefox/4.0";
                request.Timeout = timeout;

                using (var response = (HttpWebResponse)request.GetResponse())
                {
                    using (var reader = new StreamReader(response.GetResponseStream()))
                    {
                        result.Response = reader.ReadToEnd();
                        result.State = States.Success;
                    }
                }
            }
            catch (WebException e)
            {
                result.State = e.Status == WebExceptionStatus.Timeout ? States.Timeout : States.Failed; // check the exception type and set our result state according.
                LogManager.Instance.Write(LogMessageTypes.Error, string.Format("WebReader encountered an exception while reading url {0} - {1}",url, e));
            }

            return result;
        }


        /// <summary>
        /// A result object provided by the web-reader.
        /// </summary>
        public class Result
        {
            /// <summary>
            /// The response read from web.
            /// </summary>
            public string Response { get; internal set; }

            /// <summary>
            /// The result's state.
            /// </summary>
            public States State { get; internal set; }

            public Result()
            {
                this.State = States.Unknown;
                this.Response = "";
            }
        }

        /// <summary>
        /// The operation's result status.
        /// </summary>
        public enum States
        {
            Unknown,
            Success,
            Failed,
            Timeout
        }
    }
}
