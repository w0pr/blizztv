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

using BlizzTV.CommonLib.Utils;

namespace BlizzTV.Modules.BlizzBlues.Game
{
    class SCBlues:BlueParser
    {
        public SCBlues()
            : base("Starcraft")
        {
            this.Sources = new BlueSource[2] { new BlueSource(Region.Us, "http://us.battle.net/sc2/en/forum/blizztracker/"), new BlueSource(Region.Eu, "http://eu.battle.net/sc2/en/forum/blizztracker/") };
            this.Icon = new NamedImage("sc2", Assets.Images.Icons.Png._16.sc2);
        }
    }
}
