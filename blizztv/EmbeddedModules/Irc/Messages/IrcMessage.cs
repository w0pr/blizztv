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
using System.Drawing;
using System.Linq;
using System.Text;

namespace BlizzTV.EmbeddedModules.Irc.Messages
{
    public class IrcMessage
    {
        public string Prefix { get; private set; }
        public string Target { get; private set; }
        public string Parameters { get; private set; }
        public MessageTypes Type { get; private set; }

        public IrcMessage(MessageTypes type) : this(type, string.Empty, string.Empty, string.Empty)
        {            
        }

        public IrcMessage(MessageTypes type, string prefix, string target, string parameters)
        {
            this.Type = type;
            this.Prefix = prefix;
            this.Target = target;
            this.Parameters = parameters;
        }

        public enum MessageTypes
        {
            Nick,
            User,
            Notice,
            Ping,
            Pong,
            ConnectionInfo,
            ServerInfo,
            Motd,
            Join,
            NamesList,
            EndofNames,
            PrivMsg
        }

        public virtual Color ForeColor() { return Color.Black; }
    }

    [AttributeUsage(AttributeTargets.Class)]
    public class IrcMessageAttributes : Attribute
    {
        public string Command { get; private set; }
        public MessageDirection Direction { get; private set; }

        public IrcMessageAttributes(MessageDirection direction, string commandId)
        {
            this.Direction = direction;
            this.Command = commandId;
        }

        public enum MessageDirection
        {
            Incoming,
            Outgoing
        }
    }
}
