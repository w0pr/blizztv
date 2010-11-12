using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LibBlizzTV;

namespace LibVideoChannels
{
    [Serializable]
    public class Settings:PluginSettings
    {
        public int NumberOfVideosToQueryChannelFor = 10;
    }
}
