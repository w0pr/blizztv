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
 * $Id: Settings.cs 158 2010-11-30 14:06:50Z shalafiraistlin@gmail.com $
 */

using Nini.Config;

namespace BlizzTV.CommonLib.Settings
{
    /// <summary>
    /// Settings
    /// </summary>
    public class Settings
    {
        private IConfig _section;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="SectionName"></param>
        public Settings(string SectionName)
        {
            this._section = SettingsManager.Instance.Section(SectionName);
            if (this._section == null) this._section = SettingsManager.Instance.AddSection(SectionName);
        }

        /// <summary>
        /// 
        /// </summary>
        public void Save()
        {
            SettingsManager.Instance.Save();     
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <param name="default_value"></param>
        /// <returns></returns>
        protected bool GetBoolean(string key, bool default_value) { return this._section.GetBoolean(key, default_value); }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <param name="default_value"></param>
        /// <returns></returns>
        protected double GetDouble(string key, double default_value) { return this._section.GetDouble(key, default_value); }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <param name="default_value"></param>
        /// <returns></returns>
        protected float GetFloat(string key, float default_value) { return this._section.GetFloat(key, default_value); }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <param name="default_value"></param>
        /// <returns></returns>
        protected int GetInt(string key, int default_value) { return this._section.GetInt(key, default_value); }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        /// 
        protected string[] GetEntryKeys() { return this._section.GetKeys(); }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <param name="default_value"></param>
        /// <returns></returns>
        protected long GetLong(string key, long default_value) { return this._section.GetLong(key, default_value); }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <param name="default_value"></param>
        /// <returns></returns>
        protected string GetString(string key, string default_value) { return this._section.Get(key, default_value); }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        protected void Set(string key, object value) { this._section.Set(key, value); }
    }
}
