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

using BlizzTV.Storage;

namespace BlizzTV.Modules.StateStorage
{   
    /// <summary>
    /// Provides status storage support for modules.
    /// </summary>
    public class StateStorage
    {
        #region instance

        private static StateStorage _instance = new StateStorage();
        public static StateStorage Instance { get { return _instance; } }

        #endregion

        private StateStorage() { }

        public byte this[string itemId]
        {
            get { return KeyValueStorage.Instance.GetByte(string.Format("state.{0}", itemId)); } 
            set { KeyValueStorage.Instance.SetByte(string.Format("state.{0}", itemId), value); }
        }

        public bool Exists(string itemId)
        {
            return KeyValueStorage.Instance.Exists(string.Format("state.{0}", itemId));
        }
    }
}
