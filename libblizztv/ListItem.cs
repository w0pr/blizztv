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
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LibBlizzTV
{
    public class ListItem
    {
        public string _title;
        public string _key;
        protected ItemState _state = ItemState.READ;

        public string Title { get { return this._title; } set { this._title = value; } }
        public string Key { get { return this._key; } }
        public ItemState State { get { return this._state; } protected set { this._state = value; } }

        public ListItem() { this.generate_random_key(); }
        public ListItem(string Title) { this._title = Title; this.generate_random_key(); }

        public virtual void DoubleClick(object sender, EventArgs e) {}
        public virtual void UpdateState() { }

        private void generate_random_key() { this._key = System.Convert.ToBase64String(Guid.NewGuid().ToByteArray()); }
    }

    public enum ItemState
    {
        UNREAD,
        READ,
        MARKED
    }
}
