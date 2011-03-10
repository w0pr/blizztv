using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Text;
using BlizzTV.EmbeddedModules.Irc.Messages;
using BlizzTV.EmbeddedModules.Irc.Messages.Outgoing;

namespace BlizzTV.EmbeddedModules.Irc
{
    public static class CommandFactory
    {
        private static readonly Dictionary<string, Type> OutgoingMessageTypes = new Dictionary<string, Type>();
        private static readonly TextInfo EnUsCulture = new CultureInfo("en-US", false).TextInfo;

        static CommandFactory()
        {
            foreach (Type t in Assembly.GetEntryAssembly().GetTypes())
            {
                if (!t.IsSubclassOf(typeof(OutgoingIrcMessage))) continue;

                object[] attr = t.GetCustomAttributes(typeof(IrcMessageAttributes), true);
                if (attr.Length <= 0) continue;
                if (((IrcMessageAttributes)attr[0]).Direction == IrcMessageAttributes.MessageDirection.Outgoing) OutgoingMessageTypes.Add(((IrcMessageAttributes)attr[0]).Command, t);
            }   
        }

        public static OutgoingIrcMessage Get(string input)
        {
            OutgoingIrcMessage message = null;

            string cmd;
            string parameters = string.Empty;

            if (input.IndexOf(' ') == -1) 
                cmd = input;
            else
            {
                int spaceIndex = input.IndexOf(' ');
                cmd = input.Substring(0, spaceIndex);
                parameters = input.Substring(spaceIndex + 1);
            }                           

            foreach (KeyValuePair<string, Type> pair in OutgoingMessageTypes.Where(pair => EnUsCulture.ToLower(cmd) == pair.Key))
            {
                message = (OutgoingIrcMessage) Activator.CreateInstance(pair.Value, new object[] {parameters});
            }

            return message;
        }
    }
}
