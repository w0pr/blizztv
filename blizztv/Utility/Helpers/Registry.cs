/*    
 * Copyright (C) 2010-2011, BlizzTV Project - http://code.google.com/p/blizztv/
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

using Microsoft.Win32;
using System.Security;

namespace BlizzTV.Utility.Helpers
{
    /// <summary>
    /// Helper class that provides registery access.
    /// </summary>
    public static class Registry
    {
        /// <summary>
        /// Returns asked registry value.
        /// </summary>
        /// <param name="rootKey">The root key.</param>
        /// <param name="key">The key.</param>
        /// <param name="valueName">The value name.</param>
        /// <returns>The registry value.</returns>
        public static object GetValue(RootKey rootKey, string key, string valueName)
        {            
            RegistryKey askedKey = GetKey(rootKey, key);
            if (askedKey == null) return null;

            object value = askedKey.GetValue(valueName, null);
            askedKey.Close(); 
            askedKey.Flush();
            return value;
        }

        /// <summary>
        /// Sets a registry value.
        /// </summary>
        /// <param name="rootKey">The root key.</param>
        /// <param name="key">The key.</param>
        /// <param name="valueName">The value name.</param>
        /// <param name="value">The registry value.</param>
        public static void SetValue(RootKey rootKey, string key, string valueName, object value)
        {
            RegistryKey askedKey = GetKey(rootKey, key);
            if (askedKey == null) return;

            askedKey.SetValue(valueName, value);
            askedKey.Close(); 
            askedKey.Flush(); 
        }

        /// <summary>
        /// Deletes a registry value.
        /// </summary>
        /// <param name="rootKey">The root key.</param>
        /// <param name="key">The key.</param>
        /// <param name="valueName">The value name.</param>
        public static void DeleteValue(RootKey rootKey, string key, string valueName)
        {
            RegistryKey askedKey = GetKey(rootKey, key);
            if (askedKey == null) return;

            askedKey.DeleteValue(valueName);
            askedKey.Close(); 
            askedKey.Flush(); 
        }

        /// <summary>
        /// Checks for a value's existance.
        /// </summary>
        /// <param name="rootKey">The root key.</param>
        /// <param name="key">The key.</param>
        /// <param name="valueName">The value name</param>
        /// <returns>Returns a bool based on asked value's existance.</returns>
        public static bool ValueExists(RootKey rootKey, string key, string valueName)
        {
            RegistryKey askedKey = GetKey(rootKey, key);
            if (askedKey == null) return false;

            object value = askedKey.GetValue(valueName, null);
            askedKey.Close(); 
            askedKey.Flush();

            return value != null;
        }

        private static RegistryKey GetKey(RootKey rootKey, string key) /* Returns an asked key */
        {
            try
            {
                RegistryKey requestedKey = null;
                RegistryKey registryRoot = null;

                switch (rootKey)
                {
                    case RootKey.HKEY_CLASSES_ROOT: registryRoot = Microsoft.Win32.Registry.ClassesRoot; break;
                    case RootKey.HKEY_CURRENT_USER: registryRoot = Microsoft.Win32.Registry.CurrentUser; break;
                    case RootKey.HKEY_LOCAL_MACHINE: registryRoot = Microsoft.Win32.Registry.LocalMachine; break;
                }

                if (registryRoot != null) requestedKey = registryRoot.OpenSubKey(key, true); // if asked subkey does not exists, the call will just return a null value.
                return requestedKey;
            }
            catch (SecurityException) // Catch any security exceptions which means user may not have appropriate credentals to access the key.
            {
                return null;
            }
        }
    }

    public enum RootKey
    {
        HKEY_CLASSES_ROOT,
        HKEY_CURRENT_USER,
        HKEY_LOCAL_MACHINE
    }
}
