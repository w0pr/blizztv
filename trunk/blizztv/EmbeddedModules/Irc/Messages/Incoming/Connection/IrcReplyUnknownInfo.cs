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
using System.Text.RegularExpressions;

namespace BlizzTV.EmbeddedModules.Irc.Messages.Incoming.Connection
{
    [IrcMessageAttributes(IrcMessageAttributes.MessageTypes.ServerMsg253)]
    public class IrcReplyUnknownInfo : IncomingIrcMessage
    {
        private static Regex _regex = new Regex(@"(?<unknownconnections>.*?) :.*", RegexOptions.Compiled);

        private readonly int _unknownConnectionsCount;

        public IrcReplyUnknownInfo(string prefix, string target, string parameters)
            : base(prefix, target, parameters)
        {
            var m = _regex.Match(parameters);
            if (!m.Success) return;

            this._unknownConnectionsCount = Int32.Parse(m.Groups["unknownconnections"].Value);
        }

        public override string ToString()
        {
            return string.Format("{0} unknown connection(s)", this._unknownConnectionsCount);
        }
    }
}
