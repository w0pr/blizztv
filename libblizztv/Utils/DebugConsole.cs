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
using System.Text;
using System.IO;
using System.Runtime.InteropServices;
using Microsoft.Win32.SafeHandles;

namespace LibBlizzTV.Utils
{
    /// <summary>
    /// Serves a debug-console.
    /// </summary>
    public class DebugConsole : IDisposable
    {
        #region members

        private static DebugConsole _instance = new DebugConsole();
        /// <summary>
        /// DebugConsole instance.
        /// </summary>
        public static DebugConsole Instance { get { return _instance; } }

        private bool _debug_console_enabled = false;
        private bool disposed = false;

        // The GetStdHandle() and AllocConsole() functions to bind a console window to a windowed application.
        [DllImport("kernel32.dll", EntryPoint = "GetStdHandle", SetLastError = true, CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall)]
        private static extern IntPtr GetStdHandle(int nStdHandle);
        [DllImport("kernel32.dll", EntryPoint = "AllocConsole", SetLastError = true, CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall)]
        private static extern int AllocConsole();

        // API constants.
        private const int STD_OUTPUT_HANDLE = -11;
        private const int MY_CODE_PAGE = 437;

        #endregion

        #region ctor

        private DebugConsole() { }

        #endregion

        #region public functions 

        /// <summary>
        /// Enables the debug-console.
        /// </summary>
        public void EnableDebugConsole()
        {
            if (!this._debug_console_enabled) // make sure it's not already inited.
            {
                this.init();
                this._debug_console_enabled = true;
            }
        }

        /// <summary>
        /// Disables the debug-console.
        /// </summary>
        public void DisableDebugConsole()
        {
            if (this._debug_console_enabled)
            {
                this._debug_console_enabled = false;
            }
        }

        /// <summary>
        /// Writes a given message to console with given message-type.
        /// </summary>
        /// <param name="_type">The message type.</param>
        /// <param name="_str">The message.</param>
        public void Write(LogMessageTypes _type, string _str)
        {
            if (this._debug_console_enabled) // make sure that the console is enable
            {
                Console.ForegroundColor = GetMessageColor(_type); // the foreground color.
                Console.WriteLine(string.Format("[{0}] [{1}]: {2}", DateTime.Now.ToString("HH:mm:ss"), _type.ToString().PadLeft(5), _str));
                Console.ResetColor(); // reset color back.
            }
        }

        #endregion

        #region internal logic

        private  void init() // binds a new console window to a windowed application
        {
            AllocConsole(); // allocate a console.
            IntPtr stdHandle = GetStdHandle(STD_OUTPUT_HANDLE); // the console handle.
            SafeFileHandle safeFileHandle = new SafeFileHandle(stdHandle, true);
            FileStream fileStream = new FileStream(safeFileHandle, FileAccess.Write); // filestream.
            Encoding encoding = System.Text.Encoding.GetEncoding(MY_CODE_PAGE); // encoding.
            StreamWriter standardOutput = new StreamWriter(fileStream, encoding); // streamwriter.
            standardOutput.AutoFlush = true; // auto-flush ON.
            Console.SetOut(standardOutput); 
        }

        private ConsoleColor GetMessageColor(LogMessageTypes _type) // Allows coloring of message-type's.
        {
            switch (_type)
            {
                case LogMessageTypes.DEBUG: return ConsoleColor.Green;
                case LogMessageTypes.INFO: return ConsoleColor.Yellow;
                case LogMessageTypes.ERROR: return ConsoleColor.Red;
                case LogMessageTypes.FATAL: return ConsoleColor.DarkRed;
                default: return ConsoleColor.White;
            }
        }

        #endregion

        #region de-ctor

        /// <summary>
        /// De-constructor.
        /// </summary>
        ~DebugConsole() { Dispose(false); }

        /// <summary>
        /// Disposes the object.
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        private void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
            }
        }

    #endregion
    }
}
