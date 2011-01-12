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
using System.Reflection;
using BlizzTV.CommonLib.Logger;

namespace BlizzTV.CommonLib.HttpServer
{
    public sealed class HttpServer
    {
        #region Instance

        private static HttpServer _instance = new HttpServer();
        public static HttpServer Instance { get { return _instance; } }

        #endregion

        public string Banner = string.Format("BlizzTV-httpserv {0}", Assembly.GetExecutingAssembly().GetName().Version.ToString());
        public int Port = 62543;

        private TcpListener _listener;
        private bool _isActive = false;
        
        private HttpServer()
        {
            Thread serverThread = new Thread(() => { this.Listen(); }) { IsBackground = true };
            serverThread.Start();
        }

        private void Listen()
        {
            this._isActive = true;
            this._listener = new TcpListener(IPAddress.Loopback, this.Port);
            this._listener.Start();

            Log.Instance.Write(LogMessageTypes.Debug, string.Format("Embedded httpserver started.. [{0}:{1}]", IPAddress.Loopback, this.Port));

            while (this._isActive)
            {
                HttpClient httpClient = new HttpClient(this._listener.AcceptTcpClient());
            }
        }
    }
}
