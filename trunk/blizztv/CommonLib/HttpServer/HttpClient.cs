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
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.IO;
using BlizzTV.CommonLib.Logger;

namespace BlizzTV.CommonLib.HttpServer
{
    public sealed class HttpClient
    {
        private TcpClient _client;
        private Stream _inputStream;
        private StreamWriter _outputStream;

        public HttpClient(TcpClient client)
        {
            this._client = client;
            this._inputStream = new BufferedStream(this._client.GetStream());
            this._outputStream = new StreamWriter(this._client.GetStream());
                
            Thread clientThread = new Thread(() => { this.Process(); }) { IsBackground = true };
            clientThread.Start();
        }

        private void Process()
        {
            while (this._client.Connected)
            {
                HttpRequest request = new HttpRequest(this._inputStream);
                if (request.Valid)
                {
                    HttpResponse response = HttpRequestProxy.Instance.Route(request);
                    if (response != null)
                    {
                        this._outputStream.Write(response.Response);
                        this._outputStream.Flush();
                        if (response.CloseConnection) this._client.Close();
                    }
                }
            }
        }
    }
}
