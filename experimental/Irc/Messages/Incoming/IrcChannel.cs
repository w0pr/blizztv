using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BlizzTV.EmbeddedModules.Irc.Messages.Incoming
{
    public class IrcChannelMessage : IncomingIrcMessage
    {
        public string ChannelName { get; protected set; }

        public IrcChannelMessage(MessageTypes type, string prefix, string target, string parameters):base (type,prefix,target,parameters) { }
    }


    [IrcMessageAttributes(IrcMessageAttributes.MessageDirection.Incoming, "join")]
    public class IrcJoin : IrcChannelMessage
    {
        public MessageSource Source {get; private set;}

        public IrcJoin(string prefix, string target, string parameters)
            : base(MessageTypes.Join, prefix, target, parameters)
        {
            this.Source = new MessageSource(prefix);
            this.ChannelName = parameters.Substring(1);
        }
    }

    [IrcMessageAttributes(IrcMessageAttributes.MessageDirection.Incoming, "353")]
    public class IrcNamesList : IrcChannelMessage
    {
        public string[] Names { get; private set; }

        public IrcNamesList(string prefix, string target, string parameters)
            : base(MessageTypes.NamesList, prefix, target, parameters)
        {
            char channelType = parameters[0]; // RFC2812: "@" is used for secret channels, "*" for private channels, and "=" for others (public channels).
            parameters = parameters.Substring(1).Trim();

            int namesOffset = parameters.IndexOf(':');
            this.ChannelName = parameters.Substring(0, namesOffset).Trim();
            parameters = parameters.Substring(namesOffset + 1);

            this.Names = parameters.Split(' ');
        }
    }

    [IrcMessageAttributes(IrcMessageAttributes.MessageDirection.Incoming, "366")]
    public class IrcEndofNames : IrcChannelMessage
    {
        public IrcEndofNames(string prefix, string target, string parameters)
            : base(MessageTypes.EndofNames, prefix, target, parameters)
        {
            int descriptionOffset = parameters.IndexOf(':');
            this.ChannelName = parameters.Substring(0, descriptionOffset).Trim();
        }
    }
}
