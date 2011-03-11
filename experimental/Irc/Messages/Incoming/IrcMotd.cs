﻿/*    
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
 * $Id: IrcMotd.cs 459 2011-03-10 18:29:04Z shalafiraistlin@gmail.com $
 */

namespace BlizzTV.EmbeddedModules.Irc.Messages.Incoming
{
    [IrcMessageAttributes(IrcMessageAttributes.MessageDirection.Incoming, "372")]
    public class IrcMotd : IncomingIrcMessage
    {
        public IrcMotd(string prefix, string target, string parameters)
            : base(MessageTypes.Motd, prefix, target, parameters)
        { }
    }

    [IrcMessageAttributes(IrcMessageAttributes.MessageDirection.Incoming, "375")]
    public class IrcMotdStart : IncomingIrcMessage
    {
        public IrcMotdStart(string prefix, string target, string parameters)
            : base(MessageTypes.Motd, prefix, target, parameters)
        { }

        public override string ToString()
        {
            return string.Format("{0}\n-", this.Parameters);
        }
    }

    [IrcMessageAttributes(IrcMessageAttributes.MessageDirection.Incoming, "376")]
    public class IrcMotdEnd : IncomingIrcMessage
    {
        public IrcMotdEnd(string prefix, string target, string parameters)
            : base(MessageTypes.Motd, prefix, target, parameters)
        { }

        public override string ToString()
        {
            return string.Format("{0}\n-", this.Parameters);
        }
    }
}
