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
using Microsoft.Win32;
using System.Security;
using BlizzTV.CommonLib.Logger;

namespace BlizzTV.CommonLib.Helpers
{
    public enum RootKey
    {
        HKEY_CLASSES_ROOT,
        HKEY_CURRENT_USER,
        HKEY_LOCAL_MACHINE
    }

    public static class Registry
    {
        public static RegistryKey GetKey(RootKey rootKey, string key)
        {
            RegistryKey requested = null;
            RegistryKey root = null;

            try
            {
                switch (rootKey)
                {
                    case RootKey.HKEY_CLASSES_ROOT: root = Microsoft.Win32.Registry.ClassesRoot; break;
                    case RootKey.HKEY_CURRENT_USER: root = Microsoft.Win32.Registry.CurrentUser; break;
                    case RootKey.HKEY_LOCAL_MACHINE: root = Microsoft.Win32.Registry.LocalMachine; break;
                }
                requested = root.OpenSubKey(key, true);
            }
            catch (SecurityException e)
            {
                Log.Instance.Write(LogMessageTypes.Error, string.Format("Registry.GetKey({0},{1}) failed: {2}", rootKey, key, e));
            }

            return requested;
        }

        public static object GetValue(RootKey rootKey, string key, string valueName)
        {
            if (!KeyExists(rootKey, key)) return null;            
            
            RegistryKey askedKey = GetKey(rootKey, key);
            if (askedKey == null) return null;

            object value = askedKey.GetValue(valueName, null);
            askedKey.Close();
            askedKey.Flush();
            return value;
        }

        public static bool SetValue(RootKey rootKey, string key, string valueName, object value)
        {
            RegistryKey askedKey = GetKey(rootKey, key);
            if (askedKey == null) return false;

            askedKey.SetValue(valueName, value);
            askedKey.Close();
            askedKey.Flush();
            return true;
        }

        public static void DeleteValue(RootKey rootKey, string key, string valueName)
        {
            if (!ValueExists(rootKey, key, valueName)) return;

            RegistryKey askedKey = GetKey(rootKey, key);
            if (askedKey == null) return;

            askedKey.DeleteValue(valueName);
            askedKey.Close();
            askedKey.Flush();
        }

        public static bool KeyExists(RootKey rootKey, string key)
        {
            RegistryKey askedKey = GetKey(rootKey, key);
            if (askedKey == null) return false;

            askedKey.Close();
            askedKey.Flush();
            return true;
        }

        public static bool ValueExists(RootKey rootKey, string key, string valueName)
        {
            if (!KeyExists(rootKey, key)) return false;

            RegistryKey askedKey = GetKey(rootKey, key);
            if (askedKey == null) return false;

            object value = askedKey.GetValue(valueName, null);
            askedKey.Close();
            askedKey.Flush();
            if (value != null) return true; else return false;
        }        
    }
}
