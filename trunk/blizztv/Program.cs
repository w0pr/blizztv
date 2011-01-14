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
 * 
 * $Id$
 */

using System;
using System.Windows.Forms;
using System.Reflection;
using System.Threading;
using BlizzTV.CommonLib.Logger;
using BlizzTV.CommonLib.Dependencies;
using BlizzTV.CommonLib.Config;
using BlizzTV.CommonLib.Helpers;
using BlizzTV.UI;

namespace BlizzTV
{
    static class Program
    {
        private static Mutex _singleInstanceLock = new Mutex(true, Assembly.GetExecutingAssembly().GetName().Name); // mutex to enforce-single-instance of the application.
        // more on single instance mutexes: http://www.ai.uga.edu/~mc/SingleInstance.html & http://sanity-free.org/143/csharp_dotnet_single_instance_application.html

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            #region mutex-lock - don't allow more than once instance

            if (!_singleInstanceLock.WaitOne(0, true))
            {
                NativeMethods.PostMessage((IntPtr)NativeMethods.HWND_BROADCAST, NativeMethods.WM_BLIZZTV_SETFRONTMOST, IntPtr.Zero, IntPtr.Zero); // message the actual instance to get on front-most.
                return;
            }

            #endregion            

            #region command-line-args

            string[] args = Environment.GetCommandLineArgs();
            if (args.Length > 1 && args[1].ToLower() == "/silent") RuntimeConfiguration.Instance.StartedOnSystemStartup = true;
            else RuntimeConfiguration.Instance.StartedOnSystemStartup = false;

            #endregion 

            #region if enabled start logger & debug console

            if (Settings.Instance.EnableDebugLogging) Log.Instance.EnableLogger(); else Log.Instance.DisableLogger();
            if (Settings.Instance.EnableDebugConsole) DebugConsole.Instance.EnableDebugConsole(); else DebugConsole.Instance.DisableDebugConsole();

            #endregion

            #region check if our dependencies are satisfied 

            if (!Dependencies.Instance.Satisfied()) { Application.ExitThread(); return; }

            #endregion

            #region actual startup

            Log.Instance.Write(LogMessageTypes.Info, string.Format("BlizzTV-{0} started..", Assembly.GetExecutingAssembly().GetName().Version));

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new frmMain());

            _singleInstanceLock.ReleaseMutex(); // release the mutex before the application exits..

            #endregion             
        }
    }
}
