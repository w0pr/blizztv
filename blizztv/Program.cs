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
using System.Reflection;
using System.Threading;
using BlizzTV.Configuration;
using BlizzTV.Dependency;
using BlizzTV.Log;
using BlizzTV.UI;
using BlizzTV.Win32API;

namespace BlizzTV
{
    static class Program
    {
        private static readonly Mutex SingleInstanceLock = new Mutex(true, Assembly.GetExecutingAssembly().GetName().Name); // mutex to enforce-single-instance of the application.
        // more on single instance mutexes: http://www.ai.uga.edu/~mc/SingleInstance.html & http://sanity-free.org/143/csharp_dotnet_single_instance_application.html

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
       {
            // check mutex-lock - don't allow more than once instance.
            if (!SingleInstanceLock.WaitOne(0, true))
            {
                WindowMessaging.PostMessage((IntPtr)WindowMessaging.HWND_BROADCAST, WindowMessaging.WM_BLIZZTV_SETFRONTMOST, IntPtr.Zero, IntPtr.Zero); // message the actual instance to get on front-most.
                return;
            }

            // check command-line-args.
            string[] args = Environment.GetCommandLineArgs();
            if (args.Length > 1 && args[1].ToLower() == "/silent") RuntimeConfiguration.Instance.StartedOnSystemStartup = true;
            else RuntimeConfiguration.Instance.StartedOnSystemStartup = false;
            
            // start logger & debug-console if enabled.            
            if (UI.Settings.Instance.EnableDebugConsole) DebugConsole.Instance.EnableDebugConsole(); else DebugConsole.Instance.DisableDebugConsole();
            if (UI.Settings.Instance.EnableDebugLogging) LogManager.Instance.EnableLogger(); else LogManager.Instance.DisableLogger();           
            
            // check if dependencies are satisfied.
            if (!DependencyManager.Instance.Satisfied()) { Application.ExitThread(); return; }

            LogManager.Instance.Write(LogMessageTypes.Info, string.Format("BlizzTV v{0} started.", Assembly.GetExecutingAssembly().GetName().Version)); // log the program name & version at the startup.
            
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new frmMain());

            SingleInstanceLock.ReleaseMutex(); // release the mutex before the application exits.     
        }
    }
}
