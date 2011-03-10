using System.Collections.Generic;
using BlizzTV.EmbeddedModules.Irc.Messages;
using BlizzTV.EmbeddedModules.Irc.Messages.Incoming;
using BlizzTV.InfraStructure.Modules;

namespace BlizzTV.EmbeddedModules.Irc
{
    public class IrcChannel : ListItem, IMessageConsumer
    {
        private List<string> _users = new List<string>();

        public string Name { get; private set; }        

        public IrcChannel(IrcJoin message)
            : base(message.ChannelName)
        {
            this.Name = message.ChannelName;
        }

        public void ProcessMessage(IncomingIrcMessage message)
        {
            switch (message.Type)
            {
                case IrcMessage.MessageTypes.Join:
                    break;
                case IrcMessage.MessageTypes.NamesList:
                    foreach(string name in ((IrcNamesList) message).Names)
                    {
                        this._users.Add(name);
                    }                    
                    break;
                case IrcMessage.MessageTypes.EndofNames:
                    break;
            }
        }
    }
}
