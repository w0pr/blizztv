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

using System.Text.RegularExpressions;

namespace BlizzTV.EmbeddedModules.Irc.Messages.Incoming.Connection
{
    [IrcMessageAttributes(IrcMessageAttributes.MessageTypes.ServerMsg004)]
    public class IrcReplyServerInfo : IncomingIrcMessage
    {
        private static Regex _regex = new Regex(@"(?<servername>.*?) (?<version>.*?) (?<usermodes>.*?) (?<channelmodes>.*)", RegexOptions.Compiled);

        public string ServerName { get; private set; }
        public string ServerVersion { get; private set; }
        public string UserModes { get; private set; }
        public string ChannelModes { get; private set; }

        public IrcReplyServerInfo(string prefix, string target, string parameters)
            : base(prefix, target, parameters)
        {
            var m = _regex.Match(parameters);
            if (!m.Success) return;

            this.ServerName = m.Groups["servername"].Value;
            this.ServerVersion = m.Groups["version"].Value;
            this.UserModes = m.Groups["usermodes"].Value;
            this.ChannelModes = m.Groups["channelmodes"].Value;
        }

        public override string ToString()
        {
            return string.Format("{0}\n-", this.Parameters);
        }
    }
}
