using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LibBlizzTV;

namespace LibFeeds
{
    [Serializable]
    public class Settings : PluginSettings
    {
        public int UpdateEveryXMinutes = 10;
    }
}
