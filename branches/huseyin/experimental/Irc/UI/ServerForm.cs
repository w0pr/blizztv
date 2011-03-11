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
 * $Id: ServerForm.cs 462 2011-03-11 10:00:55Z shalafiraistlin@gmail.com $
 */

using System;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using BlizzTV.EmbeddedModules.Irc.Connection;
using BlizzTV.EmbeddedModules.Irc.Messages.Incoming;
using BlizzTV.EmbeddedModules.Irc.Messages.Outgoing;
using BlizzTV.Win32API;

namespace BlizzTV.EmbeddedModules.Irc.UI
{
    public partial class ServerForm : Form
    {        
        private readonly IrcServer _ircServer;

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
                this.serverText.SelectionColor = message.ForeColor();
                this.serverText.SelectedText += string.Format("{0}{1}", message, Environment.NewLine);
            }

            WindowMessaging.SendMessage(this.serverText.Handle, WindowMessaging.WM_VSCROLL, WindowMessaging.SB_BOTTOM, 0);
        }

        private void AppendText(string message)
        {
            this.serverText.SelectionStart = this.serverText.TextLength;
            this.serverText.Text += message;
            WindowMessaging.SendMessage(this.serverText.Handle, WindowMessaging.WM_VSCROLL, WindowMessaging.SB_BOTTOM, 0);
        }

        private void inputBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if(e.KeyChar == Convert.ToChar(Keys.Enter))
            {
                var input = this.inputBox.Text;
                this.inputBox.Text = "";
                if (input[0] != '/') return;

                OutgoingIrcMessage message = null;

                try
                {
                    message = CommandFactory.Get(input.Substring(1));
                    if (message != null) this._ircServer.Send(message);
                    else this.AppendText("> Unknown command.\n");
                }
                catch (Exception exception)
                {
                    this.AppendText("> Invalid command.\n");
                }                                
            }
        }
    }
}
