using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BlizzTV.Modules.BlizzBlues.Game
{
    class SCBlues:BlueParser
    {
        public SCBlues()
            : base("Starcraft")
        {
            this.Sources = new BlueSource[2] { new BlueSource(Region.Us, "http://us.battle.net/sc2/en/forum/blizztracker/"), new BlueSource(Region.Eu, "http://eu.battle.net/sc2/en/forum/blizztracker/") };
            this.Parse();
        }
    }
}
