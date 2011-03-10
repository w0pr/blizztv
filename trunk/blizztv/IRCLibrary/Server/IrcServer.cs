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

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BlizzTV.IRCLibrary.Connection;
using BlizzTV.Log;

namespace BlizzTV.IRCLibrary.Server
{
    public class IrcServer
    {
        public string Name { get; private set; }
        public string Hostname { get; private set; }
        public int Port { get; private set; }
        public bool Connected { get; private set; }

        private IrcConnection _connection = null;

        public IrcServer(string name, string hostname, int port = 6667)
        {
            this.Name = name;
            this.Hostname = hostname;
            this.Port = port;
            this.Connected = false;
        }

        public void Connect()
        {
            if (this.Connected) return;
            
            if (this._connection == null)
            {
                this._connection = new IrcConnection(this.Hostname, this.Port);
                this._connection.MessageRecieved += MessageRecieved;
                this._connection.ConnectionCompleted += ConnectionCompleted;
            }

            LogManager.Instance.Write(LogMessageTypes.Info, string.Format("IrcClient connecting to {0}:{1}.", this.Hostname, this.Port));
            this._connection.Connect();
        }

        private void ConnectionCompleted(object sender, IrcConnectionCompletedEventArgs e)
        {
            LogManager.Instance.Write(LogMessageTypes.Info, string.Format("IrcClient {0} to {1}:{2}.", e.Success ? "connected" : "failed to connect", this.Hostname, this.Port));
            this.Send("NICK blizztvdev");
        }

        private void MessageRecieved(object sender, IrcMessageEventArgs e)
        {
            Log.LogManager.Instance.Write(LogMessageTypes.Info, string.Format("IRCMessage: {0}", e.Message));
        }

        public void Send(string message)
        {
            this._connection.Send(message);
        }
    }
}
