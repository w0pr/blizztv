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
using System.Text;
using System.IO;
using System.Runtime.InteropServices;
using Microsoft.Win32.SafeHandles;

namespace BlizzTV.Log
{
    public class DebugConsole
    {
        #region Instance

        private static DebugConsole _instance = new DebugConsole();
        public static DebugConsole Instance { get { return _instance; } }

        #endregion

        private bool _debugConsoleEnabled = false;

        /* Win32 API entries; GetStdHandle() and AllocConsole() allows a windowed application to bind a console window */

        [DllImport("kernel32.dll", EntryPoint = "GetStdHandle", SetLastError = true, CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall)]
        private static extern IntPtr GetStdHandle(int nStdHandle); 

        [DllImport("kernel32.dll", EntryPoint = "AllocConsole", SetLastError = true, CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall)]
        private static extern int AllocConsole();

        // Win32 API constants.
        private const int StdOutputHandle = -11;
        private const int MyCodePage = 437;

        private DebugConsole() { }

        public void EnableDebugConsole() 
        {
            if (this._debugConsoleEnabled) return; 

            this.Init();
            this._debugConsoleEnabled = true;
        }

        public void DisableDebugConsole() 
        {
            if (this._debugConsoleEnabled) this._debugConsoleEnabled = false;
        }

        public void Write(LogMessageTypes type, string str) 
        {
            if (!this._debugConsoleEnabled) return; 

            Console.ForegroundColor = GetMessageColor(type); // the text foreground color.
            Console.WriteLine(string.Format("[{0}][{1}]: {2}", DateTime.Now.ToString("HH:mm:ss"), type.ToString().PadLeft(5), str));
            Console.ResetColor();
        }

        private void Init() // binds a new console window to a windowed application.
        {
            AllocConsole(); // allocate a new console window.
            IntPtr stdHandle = GetStdHandle(StdOutputHandle); // the stdout handle.
            SafeFileHandle safeFileHandle = new SafeFileHandle(stdHandle, true);
            FileStream fileStream = new FileStream(safeFileHandle, FileAccess.Write); 
            Encoding encoding = Encoding.GetEncoding(MyCodePage); 
            StreamWriter standardOutput = new StreamWriter(fileStream, encoding) {AutoFlush = true}; 
            Console.SetOut(standardOutput); // set console's output stream to stdout.
        }

        private ConsoleColor GetMessageColor(LogMessageTypes type) // Allows coloring of messages types.
        {
            switch (type)
            {
                case LogMessageTypes.Debug: return ConsoleColor.Green;
                case LogMessageTypes.Info: return ConsoleColor.Yellow;
                case LogMessageTypes.Error: return ConsoleColor.Red;
                case LogMessageTypes.Fatal: return ConsoleColor.DarkRed;
                default: return ConsoleColor.White;
            }
        }
    }
}
