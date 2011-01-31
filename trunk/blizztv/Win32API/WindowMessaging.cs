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
using System.Runtime.InteropServices;

namespace BlizzTV.Win32API
{
    /// <summary>
    /// Provides interfaces for Win32 API's window messaging functions.
    /// </summary>
    public static class WindowMessaging
    {
        public static readonly int WM_BLIZZTV_SETFRONTMOST = RegisterWindowMessage("WM_BLIZZTV_SETFRONTMOST"); // our custom defined message.
        public const int HWND_BROADCAST = 0xffff; //  the message is sent to all top-level windows in the system, including disabled or invisible unowned windows, overlapped windows, and pop-up windows; but the message is not sent to child windows.

        [DllImport("user32")]
        // Places (posts) a message in the message queue associated with the thread that created the specified window and returns without waiting for the thread to process the message.
        public static extern bool PostMessage(IntPtr hwnd, int msg, IntPtr wparam, IntPtr lparam);

        [DllImport("user32")]
        // Defines a new window message that is guaranteed to be unique throughout the system. The message value can be used when sending or posting messages.
        private static extern int RegisterWindowMessage(string message);
    }
}
