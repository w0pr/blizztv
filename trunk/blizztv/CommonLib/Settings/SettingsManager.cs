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
using Nini.Config;
using BlizzTV.CommonLib.Logger;

namespace BlizzTV.CommonLib.Settings
{
    internal sealed class SettingsManager
    {
        #region instance

        private static SettingsManager _instance = new SettingsManager();
        public static SettingsManager Instance { get { return _instance; } }

        #endregion

        private readonly IniConfigSource _parser;
        private bool _fileExists = true;
        private const string ConfigFile = "blizztv.ini";
      
        private SettingsManager()
        {
            try
            {
                this._parser = new IniConfigSource(ConfigFile);
            }
            catch (Exception e)
            {
                this._fileExists = false;
                this._parser = new IniConfigSource();
                Log.Instance.Write(LogMessageTypes.Error, string.Format("SettingsParser load exception: {0}", e));
            }
            finally
            {
                this._parser.Alias.AddAlias("On", true);
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
