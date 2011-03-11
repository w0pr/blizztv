using System;
using System.Collections.Generic;
using BlizzTV.EmbeddedModules.IRC;
using BlizzTV.EmbeddedModules.Irc.Messages;
using BlizzTV.EmbeddedModules.Irc.Messages.Incoming;
using BlizzTV.EmbeddedModules.Irc.UI;
using BlizzTV.InfraStructure.Modules;
using BlizzTV.Utility.Extensions;

namespace BlizzTV.EmbeddedModules.Irc
{
    public class IrcChannel : ListItem, IMessageConsumer
    {
        public readonly List<string> Users = new List<string>();
        public string Name { get; private set; }
        public readonly List<IrcMessage> MessageBackLog = new List<IrcMessage>();

        /// <summary>
        /// Event handler that notifies observers about event log being updated.
        /// </summary>
        public EventHandler<EventArgs> BackLogUpdated;

        /// <summary>
        /// Let's notification of backlog updates.
        /// </summary>
        private void OnBackLogUpdated()
        {
            EventHandler<EventArgs> handler = BackLogUpdated;
            if (handler != null) handler(this, EventArgs.Empty);
        }

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
                    foreach(string name in ((IrcNamesList) message).Names) { this.Users.Add(name); }
                    break;
                case IrcMessage.MessageTypes.EndofNames:
                    this.OpenChannelWindow();
                    break;
                case IrcMessage.MessageTypes.PrivMsg:
                    this.AddToBackLog(message);
                    break;
            }
        }

        private void AddToBackLog(IncomingIrcMessage message)
        {
            this.MessageBackLog.Add(message);
            this.OnBackLogUpdated();
        }

        private void OpenChannelWindow()
        {
            Module.UIThreadControl.AsyncInvokeHandler(() =>
            {
                var form = new ChannelForm(this);
                form.Show();
            });
        }
    }
}
