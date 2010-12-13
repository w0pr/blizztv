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

using BlizzTV.CommonLib.Storage;

namespace BlizzTV.ModuleLib.StatusStorage
{   
    public class StatusStorage
    {
        #region instance

        private static StatusStorage _instance = new StatusStorage();
        public static StatusStorage Instance { get { return _instance; } }

        #endregion

        private StatusStorage() { }

        public byte this[string itemId] { get { return KeyValueStorage.Instance.GetByte(string.Format("state.{0}", itemId)); } set { KeyValueStorage.Instance.SetByte(string.Format("state.{0}", itemId), value); } }

        public bool Exists(string itemId)
        {
            return KeyValueStorage.Instance.Exists(string.Format("state.{0}", itemId));
        }
    }
}
