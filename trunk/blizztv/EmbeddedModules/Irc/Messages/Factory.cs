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
        private static Dictionary<IrcMessageAttributes.MessageTypes, Type> _incomingMessagesTypes = new Dictionary<IrcMessageAttributes.MessageTypes, Type>();
        private static TextInfo _enUSCulture = new CultureInfo("en-US", false).TextInfo;

        static Factory()
        {
            foreach (Type t in Assembly.GetEntryAssembly().GetTypes())
            {
                if (t.IsSubclassOf(typeof(IncomingIrcMessage)))
                {
                    object[] attr = t.GetCustomAttributes(typeof(IrcMessageAttributes), true); // get the attributes of the module.
                    if(attr.Length>0)
                    {
                        _incomingMessagesTypes.Add(((IrcMessageAttributes) attr[0]).Type,t);
                    }
                }
            }            
        }

        public static IncomingIrcMessage Parse(string prefix, string command, string target, string parameters)
        {
            IncomingIrcMessage message = null;

            command = Regex.Replace(command, @"\s", "");
            try
            {
                if (Int32.Parse(command) > 0) command = string.Format("ServerMsg{0}", command);
            }
            catch (FormatException) { }

            foreach (KeyValuePair<IrcMessageAttributes.MessageTypes, Type> pair in _incomingMessagesTypes.Where(pair => _enUSCulture.ToLower(command) == _enUSCulture.ToLower(pair.Key.ToString())))
            {
                message = (IncomingIrcMessage)Activator.CreateInstance(pair.Value, new object[] {prefix, target, parameters});
                message.Type = pair.Key;
                break;
            }

            return message;
        }
    }
}
