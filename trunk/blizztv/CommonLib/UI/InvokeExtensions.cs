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

using System.Windows.Forms;

namespace BlizzTV.CommonLib.UI
{
    public static class InvokeExtensions
    {
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
