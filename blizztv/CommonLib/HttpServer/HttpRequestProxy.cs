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
using System.Linq;
using System.Text;
using System.Reflection;
using BlizzTV.CommonLib.Logger;

namespace BlizzTV.CommonLib.HttpServer
{
    internal sealed class HttpRequestProxy
    {
        #region Instance

        private static HttpRequestProxy _instance = new HttpRequestProxy();
        public static HttpRequestProxy Instance { get { return _instance; } }

        #endregion

        private Dictionary<string, HttpRequestHandler> _handlers = new Dictionary<string, HttpRequestHandler>();

        private HttpRequestProxy() 
        {
            this.RegisterHandlers();
        }

        public HttpResponse Route(HttpRequest request)
        {
            if (request.Parameters.Function == string.Empty) return new HttpResponse(HttpResponse.ResponseCode.OK, string.Format("<html><head><title>{0}</title></head><body><h1>Ah, potential customer!</h1><hr><b>{0}</b></body></html>", HttpServer.Instance.Banner));

            foreach (KeyValuePair<string, HttpRequestHandler> pair in this._handlers)
            {
                if (pair.Key == request.Parameters.Function) return pair.Value.Handle(request);
            }

            return new HttpResponse(HttpResponse.ResponseCode.OK, string.Format("<html><head><title>{0}</title></head><body><h1>Out of the way you nobgoblin! (404)</h1><hr><b>{0}</b></body></html>", HttpServer.Instance.Banner));
        }

        private void RegisterHandlers()
        {
            foreach (Type t in Assembly.GetEntryAssembly().GetTypes())
            {
                if (t.IsSubclassOf(typeof(HttpRequestHandler)))
                {
                    try
                    {
                        object[] attributes = t.GetCustomAttributes(typeof(HttpRequestHandlerAttributes), true);
                        if (attributes.Length > 0)
                        {
                            var handler = (HttpRequestHandler)Activator.CreateInstance(t);
                            this._handlers.Add((attributes[0] as HttpRequestHandlerAttributes).Function, handler);
                        }
                    }
                    catch (Exception e)
                    {
                        Log.Instance.Write(LogMessageTypes.Error, string.Format("Exception during activating the IHttpRequestHandler: {0} - {1}", t.ToString(),e));
                    }
                }
            }
        }
    }
}
