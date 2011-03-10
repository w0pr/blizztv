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

using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using BlizzTV.EmbeddedModules.Irc.Messages.Incoming;

namespace BlizzTV.EmbeddedModules.Irc.Messages
{
    public static class Factory
    {
        private static readonly Dictionary<string, Type> IncomingMessagesTypes = new Dictionary<string, Type>();
        private static readonly TextInfo EnUsCulture = new CultureInfo("en-US", false).TextInfo;

        static Factory()
        {
            foreach (Type t in Assembly.GetEntryAssembly().GetTypes())
            {
                if (!t.IsSubclassOf(typeof (IncomingIrcMessage))) continue;

                object[] attr = t.GetCustomAttributes(typeof(IrcMessageAttributes), true);
                if (attr.Length <= 0) continue;
                if (((IrcMessageAttributes)attr[0]).Direction == IrcMessageAttributes.MessageDirection.Incoming) IncomingMessagesTypes.Add(((IrcMessageAttributes) attr[0]).Command, t);
            }            
        }

        public static IncomingIrcMessage Parse(string prefix, string command, string target, string parameters)
        {
            IncomingIrcMessage message = null;

            foreach (KeyValuePair<string, Type> pair in IncomingMessagesTypes.Where(pair => EnUsCulture.ToLower(command) == pair.Key))
            {
                message = (IncomingIrcMessage)Activator.CreateInstance(pair.Value, new object[] {prefix, target, parameters});
                break;
            }

            return message;
        }
    }
}
