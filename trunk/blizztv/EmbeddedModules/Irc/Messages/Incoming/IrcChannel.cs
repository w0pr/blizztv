using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BlizzTV.EmbeddedModules.Irc.Messages.Incoming
{
    [IrcMessageAttributes(IrcMessageAttributes.MessageDirection.Incoming, "join")]
    public class IrcChannelJoin : IncomingIrcMessage
    {
        public MessageSource Source {get; private set;}
        public string ChannelName { get; private set; }

        public IrcChannelJoin(string prefix, string target, string parameters)
            : base(MessageTypes.Join, prefix, target, parameters)
        {
            this.Source = new MessageSource(prefix);
            this.ChannelName = parameters.Substring(1);
        }
    }
}
