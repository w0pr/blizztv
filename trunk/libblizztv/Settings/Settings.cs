using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Nini.Config;

namespace LibBlizzTV.Settings
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
            this._section = Manager.Instance.Section(SectionName);
            if (this._section == null) this._section = Manager.Instance.AddSection(SectionName);
        }

        /// <summary>
        /// 
        /// </summary>
        public void Save()
        {
            Manager.Instance.Save();     
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
