using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LibBlizzTV.Utils;
using Nini.Config;

namespace BlizzTV
{
    public sealed class Settings
    {
        private IConfigSource _source;
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
                this._source = new IniConfigSource("ui.ini");
                this._minimize_to_system_tray = this._source.Configs["UI"].GetBoolean("MinimizeToSystemTray", true);
                this._allow_automatic_update_checks = this._source.Configs["UI"].GetBoolean("AllowAutomaticUpdateChecks", true);
                this._allow_beta_version_notifications = this._source.Configs["UI"].GetBoolean("AllowBetaVersionNotifications", true);
                this._enable_debug_logging = this._source.Configs["UI"].GetBoolean("EnableDebugLogging", true);
                this._enable_debug_console = this._source.Configs["UI"].GetBoolean("EnableDebugConsole", false);
            }
            catch (Exception e)
            {
                Log.Instance.Write(LogMessageTypes.ERROR, string.Format("ApplicationSettings load exception: {0}", e.ToString()));
            }
        }

        public void Save()
        {
            this._source.Configs["UI"].Set("MinimizeToSystemTray", this._minimize_to_system_tray);
            this._source.Configs["UI"].Set("AllowAutomaticUpdateChecks", this._allow_automatic_update_checks);
            this._source.Configs["UI"].Set("AllowBetaVersionNotifications", this._allow_beta_version_notifications);
            this._source.Configs["UI"].Set("EnableDebugLogging", this._enable_debug_logging);
            this._source.Configs["UI"].Set("EnableDebugConsole", this._enable_debug_logging);
            this._source.Save();
        }
    }
}
