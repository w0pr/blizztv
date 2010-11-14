using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LibBlizzTV;

namespace LibStreams
{
    [Serializable]
    public class Settings : PluginSettings
    {
        public int UpdateEveryXMinutes = 10;
    }
}
