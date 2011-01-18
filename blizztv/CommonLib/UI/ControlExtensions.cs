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
 * $Id: InvokeExtensions.cs 221 2010-12-13 13:50:28Z shalafiraistlin@gmail.com $
 */

using System.Windows.Forms;

namespace BlizzTV.CommonLib.UI
{
    public static class ControlExtensions
    {
        public static void DoubleBuffer(this Control control) 
        {
            // http://stackoverflow.com/questions/76993/how-to-double-buffer-net-controls-on-a-form/77233#77233
            // Taxes: Remote Desktop Connection and painting: http://blogs.msdn.com/oldnewthing/archive/2006/01/03/508694.aspx

            if (System.Windows.Forms.SystemInformation.TerminalServerSession) return;
            System.Reflection.PropertyInfo dbProp = typeof(System.Windows.Forms.Control).GetProperty("DoubleBuffered", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
            dbProp.SetValue(control, true, null);
        }

        public static void InvokeHandler(this Control control, MethodInvoker del) // Sync. control-invoke extension.
        {
            if (control.InvokeRequired)
            {
                control.Invoke(del);
                return; 
            }
            del(); // run the actual code.
        }

        public static void AsyncInvokeHandler(this Control control, MethodInvoker del) // Async. control-invoke extension.
        {
            if (control.InvokeRequired)
            {
                control.BeginInvoke(del);
                return; 
            }
            del(); // run the actual code.
        }
    }
}
