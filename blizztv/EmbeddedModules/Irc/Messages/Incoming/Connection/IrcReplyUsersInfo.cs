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
 * $Id$
 */

namespace BlizzTV.EmbeddedModules.Irc.Messages.Incoming.Connection
{
    [IrcMessageAttributes(IrcMessageAttributes.MessageTypes.ServerMsg251)]
    public class IrcReplyUsersInfo : IncomingIrcMessage
    {
        public IrcReplyUsersInfo(string prefix, string target, string parameters)
            : base(prefix, target, parameters)
        { }

        public override string ToString()
        {
            return this.Parameters;
        }
    }
}