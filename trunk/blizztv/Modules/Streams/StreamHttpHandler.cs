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
using BlizzTV.CommonLib.HttpServer;

namespace BlizzTV.Modules.Streams
{
    [HttpRequestHandlerAttributes("embedstream")]
    public class StreamHttpHandler: HttpRequestHandler 
    {
        public override HttpResponse Handle(HttpRequest httpRequest)
        {
            if (httpRequest.Parameters.Params.Length == 0) return new HttpResponse(HttpResponse.ResponseCode.InternalServerError, "Required parameter missing.");

            string streamName = httpRequest.Parameters.Params[0];
            if (!StreamsPlugin.Instance._streams.ContainsKey(streamName)) return new HttpResponse(HttpResponse.ResponseCode.InternalServerError, string.Format("'{0}' is not a valid stream-subscription.", streamName));

            Stream requestedStream = StreamsPlugin.Instance._streams[streamName];
            requestedStream.Process();
            return new HttpResponse(HttpResponse.ResponseCode.OK, string.Format("<object type='application/x-shockwave-flash' width='624' height='347' data='{0}'><param name='allowFullScreen' value='true'/><param name='allowScriptAccess' value='always'/><param name='allowNetworking' value='all'/><param name='movie' value='{0}'/></object><br/>",requestedStream.Movie));
        }
    }
}
