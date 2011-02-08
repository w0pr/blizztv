/*    
 * Copyright (C) 2010-2011, BlizzTV Project - http://code.google.com/p/blizztv/
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
using BlizzTV.Log;
using Nini.Config;

namespace BlizzTV.Settings
{
    internal sealed class SettingsManager
    {
        #region instance

        private static SettingsManager _instance = new SettingsManager();
        public static SettingsManager Instance { get { return _instance; } }

        #endregion

        private readonly IniConfigSource _parser; // the ini parser.
        private const string ConfigFile = "blizztv.ini"; // the ini file.
        private bool _fileExists = false; // does the ini file exists?
      
        private SettingsManager()
        {
            try
            {
                this._parser = new IniConfigSource(ConfigFile); // see if the file exists by trying to parse it.
                this._fileExists = true;
            }
            catch (Exception e)
            {
                this._parser = new IniConfigSource(); // initiate a new .ini source.
                this._fileExists = false;
                LogManager.Instance.Write(LogMessageTypes.Error, string.Format("SettingsManager encountered an exception while loading: {0}", e));
            }
            finally
            {
                this._parser.Alias.AddAlias("On", true); // adds aliases so we can use On and Off directives in ini files.
                this._parser.Alias.AddAlias("Off", false);
            }
        }

        internal IConfig Section(string section) // Returns the asked config section.
        {
            return this._parser.Configs[section];
        }

        internal IConfig AddSection(string section) // Adds a config section.
        {
            return this._parser.AddConfig(section);
        }

        internal void Save() //  Saves the settings.
        {
            if (this._fileExists) this._parser.Save();
            else
            {
                this._parser.Save(ConfigFile);
                this._fileExists = true;
            }
        }
    }
}
