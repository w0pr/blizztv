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

using Nini.Config;

namespace BlizzTV.Settings
{
    public class Settings
    {
        private readonly IConfig _section;

        public Settings(string sectionName)
        {
            this._section = SettingsManager.Instance.Section(sectionName);
            if (this._section == null) this._section = SettingsManager.Instance.AddSection(sectionName); // if the section does not exist already, create it.
        }

        public void Save()
        {
            SettingsManager.Instance.Save();     
        }

        protected bool GetBoolean(string key, bool defaultValue) { return this._section.GetBoolean(key, defaultValue); }
        protected double GetDouble(string key, double defaultValue) { return this._section.GetDouble(key, defaultValue); }
        protected float GetFloat(string key, float defaultValue) { return this._section.GetFloat(key, defaultValue); }
        protected int GetInt(string key, int defaultValue) { return this._section.GetInt(key, defaultValue); }        
        protected long GetLong(string key, long defaultValue) { return this._section.GetLong(key, defaultValue); }
        protected string GetString(string key, string defaultValue) { return this._section.Get(key, defaultValue); }
        protected string[] GetEntryKeys() { return this._section.GetKeys(); }
        protected void Set(string key, object value) { this._section.Set(key, value); }
    }
}
