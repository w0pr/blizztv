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
using BlizzTV.EmbeddedModules.Irc.Messages;
using BlizzTV.EmbeddedModules.Irc.Messages.Incoming;
using BlizzTV.EmbeddedModules.Irc.Messages.Outgoing;
using BlizzTV.EmbeddedModules.Irc.UI;
using BlizzTV.InfraStructure.Modules;
using BlizzTV.Log;

namespace BlizzTV.EmbeddedModules.Irc.Connection
{
    public class IrcServer : ListItem, IMessageConsumer
    {
        public string Hostname { get; private set; }
        public int Port { get; private set; }
        public bool Connected { get; private set; }

        public string ServerName { get; private set; }
        public string ServerVersion { get; private set; }
        public string AvailableUserModes { get; private set; }
        public string AvailableServerModes { get; private set; }
        public string Nickname { get; private set; }
        private Dictionary<string, IrcChannel> _channels = new Dictionary<string, IrcChannel>();

        private IrcConnection _connection = null;
        public readonly List<IrcMessage> MessageBackLog = new List<IrcMessage>();

        public IrcServer(string name, string hostname, int port = 6667):base(name)
        {
            this.Hostname = hostname;
            this.ServerName = this.Hostname;
            this.Port = port;
            this.Connected = false;
            this.Nickname = "blizztv";
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

            if (!e.Success)
            {
                this.Connected = false;
                return;
            }

            this.Connected = true;
            this.Send(new IrcNick(this.Nickname));
            this.Send(new IrcUser("blizztv", "blizztv-user"));
        }

        public void Send(OutgoingIrcMessage message)
        {
            if (!this.Connected) return;
            this._connection.Send(message);
        }

        private void MessageRecieved(object sender, IrcMessageEventArgs e)
        {
            this.ParseMessage(e.Message);
        }

        private void ParseMessage(string line)
        {
                var prefix = string.Empty;
                var command = string.Empty;
                var target = string.Empty;
                var parameters = string.Empty;

                if (line[0] == ':')
                {
                    var prefixEndOffset = line.IndexOf(' ');
                    prefix = line.Substring(1, prefixEndOffset - 1);
                    line = line.Substring(prefixEndOffset + 1);
                }

                var commandEndOffset = line.IndexOf(' ');
                command = line.Substring(0, commandEndOffset);
                line = line.Substring(commandEndOffset + 1);

                if (line.IndexOf(' ') != -1)
                {
                    var targetOffset = line.IndexOf(' ');
                    target = line.Substring(0, targetOffset);
                    line = line.Substring(targetOffset + 1);
                    if (line[0] == ':') line = line.Substring(1);
                }

                parameters = line.Trim();

                var message = Factory.Parse(prefix, command, target, parameters);
                if (message != null) this.RouteMessage(message);
                else LogManager.Instance.Write(LogMessageTypes.Error, string.Format("Unknown message type recieved: {0}:{1}:{2}", prefix, command, parameters));
        }

        private void RouteMessage(IncomingIrcMessage message)
        {
            switch(message.Type)
            {                   
                case IrcMessage.MessageTypes.Notice:
                case IrcMessage.MessageTypes.Ping:
                case IrcMessage.MessageTypes.ConnectionInfo:
                case IrcMessage.MessageTypes.ServerInfo:
                case IrcMessage.MessageTypes.Motd:
                    this.ProcessMessage(message);
                    break;
                case IrcMessage.MessageTypes.Join:
                case IrcMessage.MessageTypes.NamesList:
                case IrcMessage.MessageTypes.EndofNames:
                    this.RouteChannelMessages((IrcChannelMessage)message);
                    break;
            }
        }

        public void RouteChannelMessages(IrcChannelMessage message)
        {
            if(message.Type== IrcMessage.MessageTypes.Join)
            {
                if (((IrcJoin)message).Source.Name == this.Nickname)
                {
                    var channel = new IrcChannel((IrcJoin)message);
                    this._channels.Add(((IrcJoin)message).ChannelName, channel);
                    return;
                }
            }

            foreach (KeyValuePair<string, IrcChannel> pair in this._channels.Where(pair => message.ChannelName == pair.Key))
            {
                pair.Value.ProcessMessage(message);
                break;
            }
        }

        public void ProcessMessage(IncomingIrcMessage message)
        {
            switch (message.Type)
            {
                case IrcMessage.MessageTypes.Ping:
                    this.Send(new IrcPong(((IrcPing)message).Code));
                    break;
                case IrcMessage.MessageTypes.Notice:
                case IrcMessage.MessageTypes.ConnectionInfo:
                case IrcMessage.MessageTypes.Motd:
                    this.MessageBackLog.Add(message);
                    break;
                case IrcMessage.MessageTypes.ServerInfo:
                    this.MessageBackLog.Add(message);
                    this.ServerName = ((IrcReplyServerInfo) message).ServerName;
                    this.ServerVersion = ((IrcReplyServerInfo)message).ServerVersion;
                    this.AvailableUserModes = ((IrcReplyServerInfo)message).UserModes;
                    this.AvailableServerModes = ((IrcReplyServerInfo)message).ChannelModes;
                    break;
            }
        }

        public override void Open(object sender, EventArgs e)
        {
            var serverForm = new ServerForm(this);
            serverForm.Show();
        }
    }
}
