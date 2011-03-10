using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BlizzTV.EmbeddedModules.Irc.Messages
{
    public class MessageSource
    {
        public string Name { get; private set; }
        public string User { get; private set; }
        public string Host { get; private set; }

        public MessageSource(string data)
        {
            this.Name = data;
            this.User = string.Empty;
            this.Host = string.Empty;

            if (data.IndexOf('!') == -1) return;
            int userOffset = this.Name.IndexOf('!');
            this.User = this.Name.Substring(userOffset + 1);
            this.Name = this.Name.Substring(0, userOffset);

            if (this.User.IndexOf('@') == -1) return;
            int hostOffset = this.User.IndexOf('@');
            this.Host = this.User.Substring(hostOffset + 1);
            this.User = this.User.Substring(0, hostOffset);
        }
    }
}
