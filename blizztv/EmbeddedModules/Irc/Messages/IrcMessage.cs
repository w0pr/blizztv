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
        public string Prefix { get; protected set; }
        public string Target { get; protected set; }
        public string Parameters { get; protected set; }
        public Directions Direction { get; protected set; }
        public IrcMessageAttributes.MessageTypes Type { get; set; }

        public IrcMessage(Directions direction)
        {
            this.Direction = direction;
        }

        public IrcMessage(string prefix, string target, string parameters, Directions direction)
        {
            this.Prefix = prefix;
            this.Target = target;
            this.Parameters = parameters;
            this.Direction = direction;
        }

        public virtual Color ForeColor() { return Color.Black; }

        public enum Directions
        {
            Incoming,
            Outgoing
        }
    }

    [AttributeUsage(AttributeTargets.Class)]
    public class IrcMessageAttributes : Attribute
    {
        public MessageTypes Type { get; private set; }

        public IrcMessageAttributes(MessageTypes type)
        {
            this.Type = type;
        }

        public enum MessageTypes
        {
            Notice,
            Ping,
            ServerMsg001,
            ServerMsg002,
            ServerMsg003,
            ServerMsg004,
            ServerMsg251,
            ServerMsg252,
            ServerMsg253,
            ServerMsg254,
            ServerMsg255,
            ServerMsg372, /* motd */
            ServerMsg375, /* motd start */
            ServerMsg376, /* end of motd */
            
        }
    }
}
