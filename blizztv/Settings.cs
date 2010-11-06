using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LibBlizzTV;

namespace BlizzTV
{
    [Serializable]
    public sealed class Settings
    {
        private static Settings _instance = new Settings();
        public static Settings Instance { get { return _instance; } internal set { _instance = value; } }

        public GlobalSettings GlobalSettings;
        public Dictionary<String, PluginSettings> PluginSettings = new Dictionary<String,PluginSettings>();

        private Settings() { }
    }

    [Serializable]
    public class GlobalSettings
    {

    }
}
