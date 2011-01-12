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
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading;
using BlizzTV.CommonLib.Logger;

namespace BlizzTV.CommonLib.HttpServer
{
    public sealed class HttpRequest
    {
        public bool Valid { get; private set; }
        public Dictionary<string, string> Headers { get; private set; }
        public HttpMethod HttpMethod { get; private set; }
        public string HttpProtocol { get; private set; }
        public string URL { get; private set; }
        public HttpRequestParameters Parameters { get; private set; }

        private Stream _stream;

        public HttpRequest(Stream stream)
        {
            this.Headers = new Dictionary<string, string>();
            this._stream = stream;
            this.Process();
        }

        private void Process()
        {
            this.Valid = false;

            // parse the http request
            string request = this.ReadLine();
            string[] tokens = request.Split(' ');

            if (tokens.Length != 3)
            {
                Log.Instance.Write(LogMessageTypes.Error, "httpserver: invalid http request.");
                return;
            }

            switch (tokens[0].ToUpper())
            {
                case "GET": this.HttpMethod = CommonLib.HttpServer.HttpMethod.GET; break;
                case "POST": this.HttpMethod = CommonLib.HttpServer.HttpMethod.POST; break;
            }
                        
            this.HttpProtocol = tokens[2];
            this.URL = tokens[1];
            this.Parameters = new HttpRequestParameters(this.URL);

            Log.Instance.Write(LogMessageTypes.Debug, string.Format("[{0}:{1}] URL: {2}", this.HttpProtocol, this.HttpMethod, this.URL));

            // get the headers
            string line;
            while ((line = this.ReadLine()) != null)
            {
                if (line.Equals("")) break;
                string[] keys = line.Split(':');
                this.Headers.Add(keys[0], keys[1]);
            }

            this.Valid = true;
        }

        private string ReadLine()
        {
            string buffer = string.Empty;
            int _char;

            while (true)
            {
                _char = this._stream.ReadByte();
                if (_char == '\n') break;
                if (_char == '\r') continue;
                if (_char == -1) { Thread.Sleep(10); continue; }
                buffer += Convert.ToChar(_char);
            }

            return buffer;
        }
    }

    public enum HttpMethod
    {
        GET,
        POST
    }

    public sealed class HttpRequestParameters
    {
        public string Function { get; private set; }
        public string[] Params { get; private set; }

        public HttpRequestParameters(string url)
        {
            this.Function = "";
            string[] tokens = url.Split('/');

            if (tokens.Length > 1)
            {
                this.Function = tokens[1];
                this.Params = new string[tokens.Length - 2];
                for (int i = 2; i < tokens.Length; i++)
                {
                    this.Params[i - 2] = tokens[i];
                }
            }
        }

    }
}
