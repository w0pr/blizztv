/*    
 * Copyright (C) 2010, Battlenger Project - http://code.google.com/p/battlenger/
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

namespace LibBlizzTV
{
    /// <summary>
    /// Implements support for a debug-console.
    /// </summary>
    public static class DebugConsole
    {
        /// <summary>
        /// The message type.
        /// </summary>
        public enum MessageTypes
        {
            /// <summary>
            /// Info Banner.
            /// </summary>
            BANNER,
            /// <summary>
            /// Debug Message.
            /// </summary>
            DEBUG,
            /// <summary>
            /// Info message.
            /// </summary>
            INFO,
            /// <summary>
            /// Error Message.
            /// </summary>
            ERROR,
            /// <summary>
            /// Fatal Error Message.
            /// </summary>
            FATAL,
            /// <summary>
            /// Connection Message.
            /// </summary>
            CONNECTION
        }

        [DllImport("kernel32.dll", EntryPoint = "GetStdHandle", SetLastError = true, CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall)]
        private static extern IntPtr GetStdHandle(int nStdHandle);
        [DllImport("kernel32.dll", EntryPoint = "AllocConsole", SetLastError = true, CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall)]

        private static extern int AllocConsole();
        private const int STD_OUTPUT_HANDLE = -11;
        private const int MY_CODE_PAGE = 437;

        /// <summary>
        /// 
        /// </summary>
        public static void init()
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

        private static ConsoleColor GetMessageColor(MessageTypes _type)
        {
            switch (_type)
            {
                case MessageTypes.DEBUG:
                    return ConsoleColor.Green;
                case MessageTypes.INFO:
                    return ConsoleColor.Yellow;
                case MessageTypes.ERROR:
                    return ConsoleColor.Red;
                case MessageTypes.FATAL:
                    return ConsoleColor.DarkRed;
                case MessageTypes.CONNECTION:
                    return ConsoleColor.Cyan;
                default:
                    return ConsoleColor.White;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="_type"></param>
        /// <param name="_str"></param>
        public static void WriteLine(MessageTypes _type, string _str)
        {
            Console.ForegroundColor = GetMessageColor(_type);
            if (_type != MessageTypes.BANNER) Console.Write(string.Format("[{0}]: ", _type.ToString()));
            Console.WriteLine(_str);
            Console.ResetColor();
        }
    }
}
