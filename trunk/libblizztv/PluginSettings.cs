/*    
 * Copyright (C) 2010, BlizzTV Project - http://code.google.com/p/blizztv/
 *  
 * This program is free software: you can redistribute it and/or modify it under the terms of the GNU General 
 * Public License as published by the Free Software Foundation, either version 3 of the License, or (at your 
 * option) any later version.
 * 
 * This program is distributed in the hope that it will be useful, but WITHOUT ANY WARRANTY; without even the 
 * implied warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the GNU General Public License 
 * for more details.
 * 
 * You should have received a copy of the GNU General Public License along with this program.  If not, see 
 * <http://www.gnu.org/licenses/>. 
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Nini.Config;

namespace LibBlizzTV
{
    /// <summary>
    /// Plugin-spefic settings.
    /// </summary>
    public class PluginSettings
    {
        private string _name;
        private IConfig _config;

        /// <summary>
        /// The config.
        /// </summary>
        public IConfig Config { get { return this._config; } }

        /// <summary>
        /// Plugin settings.
        /// </summary>
        /// <param name="Name"></param>
        protected PluginSettings(string Name)
        {
            this._name = string.Format("Plugin-{0}", Name);
            this._config = SettingsParser.Instance.Section(this._name);
            if (this._config == null) this._config = SettingsParser.Instance.AddSection(this._name);
        }

        /// <summary>
        /// Saves the settings.
        /// </summary>
        public void Save()
        {
            SettingsParser.Instance.Save();
        }
    }
}
