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
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security.Principal;

namespace BlizzTV.CommonLib.Dependencies
{
    public class OperatingSystem
    {
        #region Instance

        private static OperatingSystem _instance = new OperatingSystem();
        public static OperatingSystem Instance { get { return _instance; } }

        #endregion

        public OSType Type { get; private set; }

        public enum OSType
        {
            UNKNOWN,
            XP,
            VISTA,
            WIN7
        }

        private OperatingSystem()
        {
            if (Environment.OSVersion.Platform == PlatformID.Win32NT && Environment.OSVersion.Version.CompareTo(new Version(6, 1)) >= 0) this.Type = OSType.WIN7;
            else if (Environment.OSVersion.Platform == PlatformID.Win32NT && Environment.OSVersion.Version.Major == 6) this.Type = OSType.VISTA;
            else if (Environment.OSVersion.Platform == PlatformID.Win32NT && Environment.OSVersion.Version.Major == 5) this.Type = OSType.XP;
            else this.Type = OSType.UNKNOWN;
        }

        public bool IsAdministrator() // this will most probably not work with Vista with UAC mode enabled - http://stackoverflow.com/questions/1089046/in-net-c-test-if-user-is-an-administrative-user
        {
            bool isAdmin = false;
            try
            {
                WindowsIdentity user = WindowsIdentity.GetCurrent();
                WindowsPrincipal principal = new WindowsPrincipal(user);
                isAdmin = principal.IsInRole(WindowsBuiltInRole.Administrator);
            }
            catch (UnauthorizedAccessException ex) { isAdmin = false; }
            return isAdmin;
        }
    }
}
