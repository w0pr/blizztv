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

using System.Reflection;
using BlizzTV.Dependency;

namespace BlizzTV.Utility.Helpers
{
    /// <summary>
    /// Helper class that allows adding entries to system-startup registry keys.
    /// </summary>
    public static class SystemStartup
    {
        private const string StartupKey = "SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run"; // the system-startup registry path.
        private static readonly string ExpectedValue = string.Format("\"{0}\" /silent", Assembly.GetExecutingAssembly().Location); // expected value of the key if system-startup option is enabled.

        /// <summary>
        /// Returns whether applications is enabled to run on system-startup.
        /// </summary>
        public static bool Enabled
        {
            get
            {
                return Registry.ValueExists(RootKey.HKEY_LOCAL_MACHINE, StartupKey, "BlizzTV") && (string)Registry.GetValue(RootKey.HKEY_LOCAL_MACHINE, StartupKey, "BlizzTV") == ExpectedValue;
            }
            set
            {
                if (value == true) 
                {
                    if (!Registry.ValueExists(RootKey.HKEY_LOCAL_MACHINE, StartupKey, "BlizzTV") || (string)Registry.GetValue(RootKey.HKEY_LOCAL_MACHINE, StartupKey, "BlizzTV") != ExpectedValue) Registry.SetValue(RootKey.HKEY_LOCAL_MACHINE, StartupKey, "BlizzTV", ExpectedValue); // just set the value only if it does not set before.
                }
                else 
                {
                    if (Registry.ValueExists(RootKey.HKEY_LOCAL_MACHINE, StartupKey, "BlizzTV")) Registry.DeleteValue(RootKey.HKEY_LOCAL_MACHINE, StartupKey, "BlizzTV");  // if the system startup value exists, delete it.
                }
            }
        }

        /// <summary>
        /// Returns whether current user has the sufficient administrative privileges to carry on the operation.
        /// </summary>
        public static bool IsSupported
        {            
            get { return OperatingSystem.Instance.GotAdminPrivileges; }
        }
    }
}
