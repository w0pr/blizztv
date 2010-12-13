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
using System.Text.RegularExpressions;
using BlizzTV.CommonLib.Logger;

namespace BlizzTV.Updates
{
    public class Update
    {
        private readonly bool _valid = true;
        private readonly string _id;
        private readonly string _date;
        private readonly string _link;
        private readonly string _filename;
        private readonly string _details;
        private readonly Version _version;
        private readonly UpdateTypes _updateType = UpdateTypes.Invalid;
        private static readonly Regex RegexUpdateType = new Regex(@"\[r\:(.*)-v\:(.*)\]", RegexOptions.Compiled);

        public bool Valid { get { return this._valid; } }
        public string Id { get { return this._id; } }
        public string Date { get { return this._date; } }
        public string Link { get { return this._link; } }
        public string Filename { get { return this._filename; } }
        public string Details { get { return this._details; } }
        public Version Version { get { return this._version; } }
        public UpdateTypes UpdateType { get { return this._updateType; } }

        public Update(string id, string date, string link, string filename,string details)
        {
            try
            {
                this._id = id;
                this._date = date;
                this._link = link;
                this._filename = filename;
                this._details = details;

                Match m = RegexUpdateType.Match(this._details);

                if (m.Success)
                {
                    string release = m.Groups[1].Value.ToLower();
                    this._version = Version.Parse(m.Groups[2].Value);

                    switch (release)
                    {
                        case "beta": this._updateType = UpdateTypes.Beta; break;
                        case "stable": this._updateType = UpdateTypes.Stable; break;
                        default: this._updateType = UpdateTypes.Invalid; break;
                    }
                }
            }
            catch (Exception e)
            {
                this._valid = false;
                Log.Instance.Write(LogMessageTypes.Error,string.Format("Error parsing update data! Exception details: {0}",e));
            }
        }
    }

    public enum UpdateTypes
    {
        Invalid,
        Stable,
        Beta
    }
}
