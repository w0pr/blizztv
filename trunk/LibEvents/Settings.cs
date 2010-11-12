using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LibBlizzTV;

namespace LibEvents
{
    [Serializable]
    public class Settings:PluginSettings
    {
        public bool AllowEventNotifications = true;
        public bool AllowNotificationOfInprogressEvents = true;
        public int MinutesToNotifyBeforeEvent = 15;
    }
}
