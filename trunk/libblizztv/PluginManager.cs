/*    
 * Copyright (C) 2010, BlizzTV Project - http://code.google.com/p/blizztv/
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
 */

using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Reflection;

namespace LibBlizzTV
{
    public sealed class PluginManager
    {
        private static readonly PluginManager _instance = new PluginManager();
        public static PluginManager Instance { get { return _instance; } }
        public string AssemblyName { get { return Assembly.GetExecutingAssembly().GetName().Name; } }
        public string AssemblyVersion { get { return Assembly.GetExecutingAssembly().GetName().Version.ToString(); } }

        public Dictionary<string, PluginInfo> Plugins = new Dictionary<string, PluginInfo>(); 


        private PluginManager()
        {
            DebugConsole.init();

            DebugConsole.WriteLine(DebugConsole.MessageTypes.BANNER, @"__________.__   .__                 _______________   ____");
            DebugConsole.WriteLine(DebugConsole.MessageTypes.BANNER, @"\______   \  |  |__|________________\__    ___/\   \ /   /");
            DebugConsole.WriteLine(DebugConsole.MessageTypes.BANNER, @" |    |  _/  |  |  |\___   /\___   /  |    |    \   Y   /  ");
            DebugConsole.WriteLine(DebugConsole.MessageTypes.BANNER, @" |    |   \  |__|  | /    /  /    /   |    |     \     /   ");
            DebugConsole.WriteLine(DebugConsole.MessageTypes.BANNER, @" |______  /____/|__|/_____ \/_____ \  |____|      \___/    ");
            DebugConsole.WriteLine(DebugConsole.MessageTypes.BANNER, @"        \/                \/      \/                       ");

            DebugConsole.WriteLine(DebugConsole.MessageTypes.INFO, string.Format("{0} - v{1} initialized..", this.GetType().Module.Name, this.GetType().Assembly.GetName().Version.ToString()));

            this.ScanPlugins();
            
            foreach (KeyValuePair<string,PluginInfo> pi in this.Plugins)
            {
                DebugConsole.WriteLine(DebugConsole.MessageTypes.INFO, string.Format("Available Plugin: {0}",pi.ToString()));
            }                
        }

        private void ScanPlugins()
        {
            DirectoryInfo SelfDir = new DirectoryInfo(".");
            FileInfo[] _dll_files = SelfDir.GetFiles("*.dll");
            foreach (FileInfo _dll in _dll_files)
            {
                PluginInfo pi = new PluginInfo(_dll.Name);
                if (pi.Valid) Plugins.Add(pi.AssemblyName, pi);
            }
        }
    }
}
