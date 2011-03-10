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
using BlizzTV.Utility.Helpers;
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

            // attach our embedded assembly loader.
            AppDomain.CurrentDomain.AssemblyResolve += AssemblyManager.Instance.Resolver;

            Application.EnableVisualStyles(); // as our dependency manager can show download forms, we have to make this calls here
            Application.SetCompatibleTextRenderingDefault(false); // or SetCompatibleTextRenderingDefault will fail and crash the program.

            // code that requires custom-assembly resolving should stay away from Main() -- otherwise JIT will be failing to startup our code.            
            Startup();

            Application.Run(new MainForm());

            SingleInstanceLock.ReleaseMutex(); // release the mutex before the application exits.     
        }

        /// <summary>
        /// As below code needs an assembly-resolve (cause of the UI.Settings.Instance) for the Nini reference, we had to move away the line from Main() function
        /// in order give the JIT chance to parse Main() function and register the custom assembly resolver. Failing to do so would make JIT fail the initilization and run Main().
        /// </summary>
        static void Startup()
        {
            // check command-line-args.
            string[] args = Environment.GetCommandLineArgs();
            if (args.Length > 1 && args[1].ToLower() == "/silent") RuntimeConfiguration.Instance.StartedOnSystemStartup = true;
            else RuntimeConfiguration.Instance.StartedOnSystemStartup = false;

            // start logger & debug-console if enabled.            
            if (UI.Settings.Instance.EnableDebugConsole) DebugConsole.Instance.EnableDebugConsole(); else DebugConsole.Instance.DisableDebugConsole();
            if (UI.Settings.Instance.EnableDebugLogging) LogManager.Instance.EnableLogger(); else LogManager.Instance.DisableLogger();

            // handle any non-processed exceptions.
            AppDomain.CurrentDomain.UnhandledException += UnhandledExceptionHandler;

            if (!DependencyManager.Instance.Satisfied()) { Environment.Exit(-1); } // check if dependencies are satisfied.

            LogManager.Instance.Write(LogMessageTypes.Info, string.Format("BlizzTV v{0} started.", Assembly.GetExecutingAssembly().GetName().Version)); // log the program name & version at the startup.
        }

        static void UnhandledExceptionHandler(object sender, UnhandledExceptionEventArgs e)
        {
            LogManager.Instance.Write(LogMessageTypes.Error, string.Format("{0}: {1}", e.IsTerminating ? "Application terminating because of un-handled exception" : "Caught unhandled exception: ", e.ExceptionObject));
        }
    }
}
