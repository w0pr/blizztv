﻿/*    
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

using BlizzTV.Utility.Imaging;

namespace BlizzTV.EmbeddedModules.BlueTracker.Parsers
{
    /// <summary>
    /// Parses official Starcraft forums.
    /// </summary>
    class Starcraft:BlueParser
    {
        public Starcraft()
            : base(BlueType.Starcraft)
        {
            this.Sources = new BlueSource[2]
            {
                new BlueSource(Region.Us, "http://us.battle.net/sc2/en/forum/blizztracker/"), 
                new BlueSource(Region.Eu, "http://eu.battle.net/sc2/en/forum/blizztracker/")
            };

            this.Icon = new NodeIcon("sc2", Assets.Images.Icons.Png._16.sc2);
        }
    }
}
