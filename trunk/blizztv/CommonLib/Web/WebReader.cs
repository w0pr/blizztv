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
using BlizzTV.Log;

namespace BlizzTV.CommonLib.Web
{
    public static class WebReader
    {
        public static Result Read(string url, int timeout = 30 * 1000)
        {
            Result result=new Result();

            try
            {
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
                request.Timeout = timeout;
                using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
                {
                    using (StreamReader reader = new StreamReader(response.GetResponseStream()))
                    {
                        result.Response = reader.ReadToEnd();
                        result.Status= Status.Success;
                    }
                }
            }
            catch (WebException e)
            {
                if (e.Status == WebExceptionStatus.Timeout) result.Status = Status.Timeout; else result.Status = Status.Failed;                    
                LogManager.Instance.Write(LogMessageTypes.Error, string.Format("WebReader:Read() Exception: {0}", e));
            }
            return result;
        }

        public class Result
        {
            public string Response { get; internal set; }
            public Status Status { get; internal set; }

            public Result()
            {
                this.Response = "";
            }
        }

        public enum Status
        {
            Unknown,
            Success,
            Failed,
            Timeout
        }
    }
}
