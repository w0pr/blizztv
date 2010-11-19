using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LibBlizzTV;

namespace LibVideos
{
    [Serializable]
    public class Settings:PluginSettings
    {
        public int NumberOfVideosToQueryChannelFor = 10;
        public int UpdateEveryXMinutes = 10;
    }
}
