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
 * 
 * $Id$
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LibBlizzTV.Utils;
using Nini.Config;

namespace LibBlizzTV.Settings
{
    /// <summary>
    /// The settings parser wrapper.
    /// </summary>
    internal sealed class Manager
    {        
        private static Manager _instance = new Manager();
        private IniConfigSource _parser;
        private bool _file_exists = true;
        private string _config_file = "blizztv.ini";

        /// <summary>
        /// Returns instance of GlobalSettings.
        /// </summary>
        public static Manager Instance { get { return _instance; } }             

        private Manager()
        {
            try
            {
                this._parser = new IniConfigSource(this._config_file);
            }
            catch (Exception e)
            {
                this._file_exists = false;
                this._parser = new IniConfigSource();
                Log.Instance.Write(LogMessageTypes.ERROR, string.Format("SettingsParser load exception: {0}", e.ToString()));
            }
            finally
            {
                this._parser.Alias.AddAlias("On", true);
                this._parser.Alias.AddAlias("Off", false);
            }
        }

        /// <summary>
        /// Returns the askes config section.
        /// </summary>
        /// <param name="Section">The asked section name.</param>
        /// <returns>The asked config section.</returns>
        internal IConfig Section(string Section)
        {
            return this._parser.Configs[Section];
        }

        /// <summary>
        /// Ads a config section.
        /// </summary>
        /// <param name="Section">The config section name.</param>
        /// <returns>Returns added config section.</returns>
        internal IConfig AddSection(string Section)
        {
            return this._parser.AddConfig(Section);
        }

        /// <summary>
        /// Saves the settings.
        /// </summary>
        internal void Save()
        {
            if (this._file_exists) this._parser.Save();
            else
            {
                this._parser.Save(this._config_file);
                this._file_exists = true;
            }
        }
    }
}
