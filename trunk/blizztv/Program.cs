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
using BlizzTV.UI;

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
            #region mutex-lock - don't allow more than once instance

            //method: http://www.ai.uga.edu/~mc/SingleInstance.html)

            bool gotMutex = false; 
            Mutex singleInstanceLock = new Mutex(true, Assembly.GetExecutingAssembly().GetName().Name, out gotMutex); // mutex after our app. name.
            
            if (!gotMutex) 
            {
                MessageBox.Show("Another instance of BlizzTV is already running!", "Startup Error!", MessageBoxButtons.OK, MessageBoxIcon.Error); // give a non-friendly error message :/
                return; // exit
            }

            GC.KeepAlive(singleInstanceLock); // okay GC, single_instance_lock is an important variable for us, so never ever throw it to garbage!

            #endregion

            #region if enabled start logger & debug console

            if (Settings.Instance.EnableDebugLogging) Log.Instance.EnableLogger(); else Log.Instance.DisableLogger();
            if (Settings.Instance.EnableDebugConsole) DebugConsole.Instance.EnableDebugConsole(); else DebugConsole.Instance.DisableDebugConsole();

            #endregion

            #region check if our dependencies are satisfied 

            if (!Dependencies.Instance.Satisfied()) { Application.ExitThread(); return; }

            #endregion

            #region actual startup 

            Log.Instance.Write(LogMessageTypes.Info, "BlizzTV Startup.."); 

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new frmMain());

            #endregion             
        }
    }
}
