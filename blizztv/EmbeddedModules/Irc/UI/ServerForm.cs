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
using System.Windows.Forms;
using BlizzTV.EmbeddedModules.Irc.Connection;
using BlizzTV.EmbeddedModules.Irc.Messages.Incoming;

namespace BlizzTV.EmbeddedModules.Irc.UI
{
    public partial class ServerForm : Form
    {
        private IrcServer _ircServer;

        public ServerForm(IrcServer ircServer)
        {
            InitializeComponent();

            this._ircServer = ircServer;
        }

        private void ServerForm_Load(object sender, EventArgs e)
        {            
            
            this.LoadBackLog();
        }

        private void LoadBackLog()
        {
            foreach(IncomingIrcMessage message in this._ircServer.MessageBackLog)
            {
                this.textBox.SelectionColor = message.ForeColor();
                this.textBox.SelectedText += string.Format("{0}{1}", message, Environment.NewLine);
            }
        }
    }
}
