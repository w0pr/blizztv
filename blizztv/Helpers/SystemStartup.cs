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

namespace BlizzTV.Helpers
{
    public static class SystemStartup
    {
        private const string _startupKey = "SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run";

        public static bool Enabled
        {
            get
            {
                string expectedValue = string.Format("\"{0}\" /silent", Assembly.GetExecutingAssembly().Location);
                if (!Registry.ValueExists(RootKey.HKEY_LOCAL_MACHINE, _startupKey, "BlizzTV") || (string)Registry.GetValue(RootKey.HKEY_LOCAL_MACHINE, _startupKey, "BlizzTV") != expectedValue) return false;
                return true;
            }
            set
            {
                if (value == true) // if the system startup value does not exist or is path is incorrect, reset the registry value.
                {
                    string pathValue = string.Format("\"{0}\" /silent", Assembly.GetExecutingAssembly().Location);
                    if (!Registry.ValueExists(RootKey.HKEY_LOCAL_MACHINE, _startupKey, "BlizzTV") || (string)Registry.GetValue(RootKey.HKEY_LOCAL_MACHINE, _startupKey, "BlizzTV") != pathValue) Registry.SetValue(RootKey.HKEY_LOCAL_MACHINE, _startupKey, "BlizzTV", pathValue);
                }
                else // if the system startup value exists, delete it.
                {
                    if (Registry.ValueExists(RootKey.HKEY_LOCAL_MACHINE, _startupKey, "BlizzTV")) Registry.DeleteValue(RootKey.HKEY_LOCAL_MACHINE, _startupKey, "BlizzTV"); 
                }
            }
        }

        public static bool IsSupported // If we lack administrative privileges, we'll not be able to add a startup-entry in SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run.
        {            
            get { return OperatingSystem.Instance.GotAdminPrivileges; }
        }
    }
}
