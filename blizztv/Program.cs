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
using System.Windows.Forms;
using LibBlizzTV.Utils;
using LibBlizzTV;

namespace BlizzTV
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            // don't allow more than one instances. (method explanation: http://www.ai.uga.edu/~mc/SingleInstance.html)
            bool got_mutex = false; // states if got the mutex lock
            System.Threading.Mutex single_instance_lock = new System.Threading.Mutex(true, System.Reflection.Assembly.GetExecutingAssembly().GetName().Name, out got_mutex); // try to create a new mutex named after our app-name.
            if (!got_mutex) // if we can't own the mutex, that means another instance was already owning it!
            {
                MessageBox.Show("Another instance of BlizzTV is already running!", "Startup Error!", MessageBoxButtons.OK, MessageBoxIcon.Error); // give a non-friendly error message :/
                return; // exit
            }

            // Check global settings and start logger and debug console if enabled
            if (SettingsStorage.Instance.Settings.EnableDebugLogging) Log.Instance.EnableLogger(); else Log.Instance.DisableLogger();
            if (SettingsStorage.Instance.Settings.EnableDebugConsole) DebugConsole.Instance.EnableDebugConsole(); else DebugConsole.Instance.DisableDebugConsole();

            Log.Instance.Write(LogMessageTypes.INFO, "Program startup.."); // the startup message so we can see sessions in log easier.

            // Startup the main form
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new frmMain());

            GC.KeepAlive(single_instance_lock); // okay GC, single_instance_lock is an important variable for us, so never ever throw it to garbage!
        }
    }
}
