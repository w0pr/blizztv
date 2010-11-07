﻿/*    
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
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LibBlizzTV
{
    public sealed class Storage
    {
        private string _plugin_identifier;

        public Storage(string Idetifier)
        {
            this._plugin_identifier = Idetifier;
        }

        public void Put(string key, byte value)
        {
            key = string.Format("{0}.{1}", this._plugin_identifier, key);
            Database.Instance.Put(key, value);
        }

        public byte Get(string key)
        {
            key = string.Format("{0}.{1}", this._plugin_identifier, key);
            return Database.Instance.Get(key);
        }

        public bool KeyExists(string key)
        {
            key = string.Format("{0}.{1}", this._plugin_identifier, key);
            return Database.Instance.KeyExists(key);
        }
    }
}
