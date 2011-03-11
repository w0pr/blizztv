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
using BlizzTV.EmbeddedModules.Irc;
using BlizzTV.EmbeddedModules.Irc.Connection;
using BlizzTV.InfraStructure.Modules;
using BlizzTV.Utility.Imaging;

namespace BlizzTV.EmbeddedModules.IRC
{
    [ModuleAttributes("IRC-Client", "IRC client module.", "_event", ModuleAttributes.ModuleFunctionality.RendersMenus | ModuleAttributes.ModuleFunctionality.RendersTreeItems)]
    public class IrcModule : Module
    {        
        private IrcClient ircClient;
        private readonly ListItem _rootItem = new ListItem("IRC") { Icon = new NamedImage("irc", Assets.Images.Icons.Png._16.feed) };

        public IrcModule() { }

        public override void Startup()
        {
            this.ircClient = new IrcClient();
        }

        public override ListItem GetRootItem()
        {
            return this._rootItem;
        }

        public override void Refresh()
        {
            this.OnDataRefreshStarting(EventArgs.Empty); 
          
            foreach(IrcServer server in this.ircClient.ActiveServers)
            {
                this._rootItem.Childs.Add(server.Title, server);
            }
            
            this.OnDataRefreshCompleted(new DataRefreshCompletedEventArgs(true));
        }
    }
}
