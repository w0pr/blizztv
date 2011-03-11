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
 * $Id: IrcUser.cs 459 2011-03-10 18:29:04Z shalafiraistlin@gmail.com $
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BlizzTV.EmbeddedModules.Irc.Messages.Outgoing
{
    [IrcMessageAttributes(IrcMessageAttributes.MessageDirection.Outgoing, "user")]
    public class IrcUser : OutgoingIrcMessage
    {
        private readonly string _user;
        private readonly string _realName;

        public IrcUser(string user, string realName):base(MessageTypes.User)
        {
            this._user = user;
            this._realName = realName;
        }

        public override string GetRawMessage()
        {
            return string.Format("USER {0} 0 * :{1}", this._user,this._realName);
        }
    }
}
