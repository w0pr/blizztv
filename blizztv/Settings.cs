using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LibBlizzTV;
using LibBlizzTV.Utils;
using Nini.Config;

namespace BlizzTV
{
    public sealed class Settings
    {
        private static Settings _instance = new Settings();        
        public static Settings Instance { get { return _instance; } }

        private bool _minimize_to_system_tray = true;
        private bool _allow_automatic_update_checks = true;
        private bool _allow_beta_version_notifications = true;
        private bool _enable_debug_logging = true;
        private bool _enable_debug_console = false;

        public bool MinimizeToSystemTray { get { return this._minimize_to_system_tray; } set { this._minimize_to_system_tray = value; } }
        public bool AllowAutomaticUpdateChecks { get { return this._allow_automatic_update_checks; } set { this._allow_automatic_update_checks = value; } }
        public bool AllowBetaVersionNotifications { get { return this._allow_beta_version_notifications; } set { this._allow_beta_version_notifications = value; } }
        public bool EnableDebugLogging { get { return this._enable_debug_logging; } set { this._enable_debug_logging = value; } }
        public bool EnableDebugConsole { get { return this._enable_debug_console; } set { this._enable_debug_console = value; } }

        private Settings()
        {
            try
            {
                this._minimize_to_system_tray = SettingsParser.Instance.Section("UI").GetBoolean("MinimizeToSystemTray", true);
                this._allow_automatic_update_checks = SettingsParser.Instance.Section("UI").GetBoolean("AllowAutomaticUpdateChecks", true);
                this._allow_beta_version_notifications = SettingsParser.Instance.Section("UI").GetBoolean("AllowBetaVersionNotifications", true);
                this._enable_debug_logging = SettingsParser.Instance.Section("UI").GetBoolean("EnableDebugLogging", true);
                this._enable_debug_console = SettingsParser.Instance.Section("UI").GetBoolean("EnableDebugConsole", false);
            }
            catch (Exception e)
            {
                Log.Instance.Write(LogMessageTypes.ERROR, string.Format("ApplicationSettings load exception: {0}", e.ToString()));
            }
        }

        public void Save()
        {
            IConfig config = SettingsParser.Instance.Section("UI");
            if (config == null) config = SettingsParser.Instance.AddSection("UI");
            config.Set("MinimizeToSystemTray", this._minimize_to_system_tray);
            config.Set("AllowAutomaticUpdateChecks", this._allow_automatic_update_checks);
            config.Set("AllowBetaVersionNotifications", this._allow_beta_version_notifications);
            config.Set("EnableDebugLogging", this._enable_debug_logging);
            config.Set("EnableDebugConsole", this._enable_debug_logging);
            SettingsParser.Instance.Save();
        }

        public void EnablePlugin(string Name)
        {
            IConfig config = SettingsParser.Instance.Section("Plugins");
            if (config == null) config = SettingsParser.Instance.Section("Plugins");
            config.Set(Name, "On");
            SettingsParser.Instance.Save();
        }

        public void DisablePlugin(string Name)
        {
            IConfig config = SettingsParser.Instance.Section("Plugins");
            if (config == null) config = SettingsParser.Instance.AddSection("Plugins");
            config.Set(Name, "Off");
            SettingsParser.Instance.Save();
        }

        public bool PluginEnabled(string Name)
        {
            IConfig config = SettingsParser.Instance.Section("Plugins");
            if (config == null) config = SettingsParser.Instance.AddSection("Plugins");
            return config.GetBoolean(Name);
        }

        public Dictionary<string, bool> GetPluginEntries()
        {
            IConfig config = SettingsParser.Instance.Section("Plugins");
            if (config == null) config = SettingsParser.Instance.AddSection("Plugins");

            Dictionary<string, bool> entries = new Dictionary<string, bool>();
            string[] keys = config.GetKeys();
            foreach (string key in keys) { entries.Add(key, config.GetBoolean(key, false)); }
            return entries;
        }
    }
}
