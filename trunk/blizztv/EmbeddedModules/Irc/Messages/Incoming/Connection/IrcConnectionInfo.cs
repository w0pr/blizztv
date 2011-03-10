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
 * $Id: IrcMotd.cs 457 2011-03-10 14:58:27Z shalafiraistlin@gmail.com $
 */

using System;
using System.Text.RegularExpressions;

namespace BlizzTV.EmbeddedModules.Irc.Messages.Incoming.Connection
{
    [IrcMessageAttributes("001")]
    public class IrcReplyWelcome : IncomingIrcMessage
    {
        public IrcReplyWelcome(string prefix, string target, string parameters)
            : base(MessageTypes.ConnectionInfo, prefix, target, parameters)
        { }

        public override string ToString()
        {
            return string.Format("-\n{0}", this.Parameters);
        }
    }

    [IrcMessageAttributes("002")]
    public class IrcReplyYourHost : IncomingIrcMessage
    {
        public IrcReplyYourHost(string prefix, string target, string parameters)
            : base(MessageTypes.ConnectionInfo, prefix, target, parameters)
        { }

        public override string ToString()
        {
            return this.Parameters;
        }
    }

    [IrcMessageAttributes("003")]
    public class IrcHostCreatedDate : IncomingIrcMessage
    {
        public IrcHostCreatedDate(string prefix, string target, string parameters)
            : base(MessageTypes.ConnectionInfo, prefix, target, parameters)
        { }

        public override string ToString()
        {
            return string.Format("{0}\n-", this.Parameters);
        }
    }

    [IrcMessageAttributes("004")]
    public class IrcReplyServerInfo : IncomingIrcMessage
    {
        private static readonly Regex Regex = new Regex(@"(?<servername>.*?) (?<version>.*?) (?<usermodes>.*?) (?<channelmodes>.*)", RegexOptions.Compiled);

        public string ServerName { get; private set; }
        public string ServerVersion { get; private set; }
        public string UserModes { get; private set; }
        public string ChannelModes { get; private set; }

        public IrcReplyServerInfo(string prefix, string target, string parameters)
            : base(MessageTypes.ServerInfo, prefix, target, parameters)
        {
            var m = Regex.Match(parameters);
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

    [IrcMessageAttributes("251")]
    public class IrcReplyUsersInfo : IncomingIrcMessage
    {
        public IrcReplyUsersInfo(string prefix, string target, string parameters)
            : base(MessageTypes.ConnectionInfo, prefix, target, parameters)
        { }

        public override string ToString()
        {
            return this.Parameters;
        }
    }

    [IrcMessageAttributes("252")]
    public class IrcReplyOpsInfo : IncomingIrcMessage
    {
        private static readonly Regex Regex = new Regex(@"(?<operatorcount>.*?) :.*", RegexOptions.Compiled);

        private readonly int _operatorsOnline;

        public IrcReplyOpsInfo(string prefix, string target, string parameters)
            : base(MessageTypes.ConnectionInfo, prefix, target, parameters)
        {
            var m = Regex.Match(parameters);
            if (!m.Success) return;

            this._operatorsOnline = Int32.Parse(m.Groups["operatorcount"].Value);
        }

        public override string ToString()
        {
            return string.Format("{0} IRC operator(s) online", this._operatorsOnline);
        }
    }

    [IrcMessageAttributes("253")]
    public class IrcReplyUnknownInfo : IncomingIrcMessage
    {
        private static readonly Regex Regex = new Regex(@"(?<unknownconnections>.*?) :.*", RegexOptions.Compiled);

        private readonly int _unknownConnectionsCount;

        public IrcReplyUnknownInfo(string prefix, string target, string parameters)
            : base(MessageTypes.ConnectionInfo, prefix, target, parameters)
        {
            var m = Regex.Match(parameters);
            if (!m.Success) return;

            this._unknownConnectionsCount = Int32.Parse(m.Groups["unknownconnections"].Value);
        }

        public override string ToString()
        {
            return string.Format("{0} unknown connection(s)", this._unknownConnectionsCount);
        }
    }

    [IrcMessageAttributes("254")]
    public class IrcReplyChannelsInfo : IncomingIrcMessage
    {
        private static readonly Regex Regex = new Regex(@"(?<channelscount>.*?) :.*", RegexOptions.Compiled);

        private readonly int _channelsCount;

        public IrcReplyChannelsInfo(string prefix, string target, string parameters)
            : base(MessageTypes.ConnectionInfo, prefix, target, parameters)
        {
            var m = Regex.Match(parameters);
            if (!m.Success) return;

            this._channelsCount = Int32.Parse(m.Groups["channelscount"].Value);
        }

        public override string ToString()
        {
            return string.Format("{0} channels formed", this._channelsCount);
        }
    }

    [IrcMessageAttributes("255")]
    public class IrcSelfUsersInfo : IncomingIrcMessage
    {
        public IrcSelfUsersInfo(string prefix, string target, string parameters)
            : base(MessageTypes.ConnectionInfo, prefix, target, parameters)
        { }

        public override string ToString()
        {
            return string.Format("{0}\n-", this.Parameters);
        }
    }
}
