using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BlizzTV.EmbeddedModules.Irc.Messages;
using BlizzTV.EmbeddedModules.Irc.Messages.Incoming;
using BlizzTV.InfraStructure.Modules;

namespace BlizzTV.EmbeddedModules.Irc
{
    public class IrcChannel : ListItem, IMessageConsumer
    {
        public string Name { get; private set; }

        public IrcChannel(IrcChannelJoin message)
            : base(message.ChannelName)
        {
            this.Name = message.ChannelName;
        }

        public void ProcessMessage(IncomingIrcMessage message)
        {

        }
    }
}
