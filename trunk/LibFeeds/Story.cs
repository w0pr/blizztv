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
using LibBlizzTV;

namespace LibFeeds
{
    public class Story : ListItem
    {
        public string GUID;
        public string Link;
        public string Description;

        public Story(string Title, string GUID, string Link, string Description)
            : base(Title)
        {
            this.GUID = GUID;
            this.Link = Link;
            this.Description = Description;

            //   if (Plugin.Storage.Exists(this.Title)) this.SetState((ItemState)Plugin.Storage.Get(this.Title)); // get the item state if available
            //    else this.SetState(ItemState.UNREAD);
        }

        public override void DoubleClick(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start(this.Link, null);
            this.SetState(ItemState.READ);

            //Plugin.Storage.Put(this._title, (byte)this._state);
        }
    }
}
