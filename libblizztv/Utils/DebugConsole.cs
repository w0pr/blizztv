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
    public class DebugConsole : IDisposable
    {
        private static DebugConsole _instance = new DebugConsole();
        public static DebugConsole Instance { get { return _instance; } }

        private bool _debug_console_enabled = false;
        private bool disposed = false;

        [DllImport("kernel32.dll", EntryPoint = "GetStdHandle", SetLastError = true, CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall)]
        private static extern IntPtr GetStdHandle(int nStdHandle);
        [DllImport("kernel32.dll", EntryPoint = "AllocConsole", SetLastError = true, CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall)]

        private static extern int AllocConsole();
        private const int STD_OUTPUT_HANDLE = -11;
        private const int MY_CODE_PAGE = 437;

        private DebugConsole() { }

        public void EnableDebugConsole()
        {
            if (!this._debug_console_enabled)
            {
                this.init();
                this._debug_console_enabled = true;
            }
        }

        public void DisableDebugConsole()
        {
            if (this._debug_console_enabled)
            {
                this._debug_console_enabled = false;
            }
        }

        private  void init()
        {
            AllocConsole();
            IntPtr stdHandle = GetStdHandle(STD_OUTPUT_HANDLE);
            SafeFileHandle safeFileHandle = new SafeFileHandle(stdHandle, true);
            FileStream fileStream = new FileStream(safeFileHandle, FileAccess.Write);
            Encoding encoding = System.Text.Encoding.GetEncoding(MY_CODE_PAGE);
            StreamWriter standardOutput = new StreamWriter(fileStream, encoding);
            standardOutput.AutoFlush = true;
            Console.SetOut(standardOutput);
        }

        private ConsoleColor GetMessageColor(LogMessageTypes _type)
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

        public void Write(LogMessageTypes _type, string _str)
        {
            if (this._debug_console_enabled)
            {
                Console.ForegroundColor = GetMessageColor(_type);
                Console.WriteLine(string.Format("[{0} {1}] {2}", _type.ToString(), DateTime.Now.ToString("HH:mm:ss"), _str));
                Console.ResetColor();
            }
        }

        ~DebugConsole() { Dispose(false); }

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
    }
}
