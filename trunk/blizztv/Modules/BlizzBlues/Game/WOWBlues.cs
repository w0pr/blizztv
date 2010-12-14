using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BlizzTV.Modules.BlizzBlues.Game
{
    public class WOWBlues:BlueParser
    {
        public WOWBlues()
            : base("World of Warcraft")
        {
            this.Sources = new BlueSource[2] { new BlueSource(Region.Us, "http://us.battle.net/wow/en/forum/blizztracker/"), new BlueSource(Region.Eu, "http://eu.battle.net/wow/en/forum/blizztracker/"), };
        }
    }
}
