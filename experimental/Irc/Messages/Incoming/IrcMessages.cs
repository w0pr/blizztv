using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BlizzTV.EmbeddedModules.Irc.Messages.Incoming
{
    [IrcMessageAttributes(IrcMessageAttributes.MessageDirection.Incoming, "privmsg")]
    public class IrcPrivMsg : IncomingIrcMessage
    {
        public MessageSource Source { get; private set; }
        public string Target { get; private set; }
        public string Message { get; private set; }

        public IrcPrivMsg(string prefix, string target, string parameters)
            : base(MessageTypes.PrivMsg, prefix, target, parameters)
        {
            this.Source = new MessageSource(prefix);
            this.Target = target;
            this.Message = parameters;
        }

        public override string ToString()
        {
            return string.Format("<{0}> {1}", this.Source.Name, this.Message);
        }
    }
}
