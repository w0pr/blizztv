using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LibBlizzTV
{
    /// <summary>
    /// Implements a plugin settings form.
    /// </summary>
    public interface IPluginSettingsForm
    {
        /// <summary>
        /// Notifies the form to save settings.
        /// </summary>
        void SaveSettings();
    }
}
