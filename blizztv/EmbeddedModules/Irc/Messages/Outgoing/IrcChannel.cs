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

namespace BlizzTV.EmbeddedModules.Irc.Messages.Outgoing
{
    [IrcMessageAttributes(IrcMessageAttributes.MessageDirection.Outgoing, "join")]
    public class IrcChannelJoin : OutgoingIrcMessage
    {
        private readonly string _channel;

        public IrcChannelJoin(string channel)
            : base(MessageTypes.Nick)
        {
            this._channel = channel;
        }

        public override string GetRawMessage()
        {
            return string.Format("Join {0}", this._channel);
        }
    }
}
