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
using System.Text.RegularExpressions;
using LibBlizzTV.Utils;

namespace BlizzTV.Updates
{
    public class Update
    {
        private bool _valid = true;
        private string _id;
        private string _date;
        private string _link;
        private string _filename;
        private string _details;
        private Version _version;
        private UpdateTypes _update_type = UpdateTypes.INVALID;
        private static Regex _regex_update_type = new Regex(@"\[r\:(.*)-v\:(.*)\]", RegexOptions.Compiled);

        public bool Valid { get { return this._valid; } }
        public string ID { get { return this._id; } }
        public string Date { get { return this._date; } }
        public string Link { get { return this._link; } }
        public string Filename { get { return this._filename; } }
        public string Details { get { return this._details; } }
        public Version Version { get { return this._version; } }
        public UpdateTypes UpdateType { get { return this._update_type; } }

        public Update(string ID, string Date, string Link, string Filename,string Details)
        {
            try
            {

                this._id = ID;
                this._date = Date;
                this._link = Link;
                this._filename = Filename;
                this._details = Details;

                Match m = _regex_update_type.Match(this._details);
                if (m.Success)
                {
                    string release = m.Groups[1].Value.ToLower();
                    this._version = Version.Parse(m.Groups[2].Value);

                    switch (release)
                    {
                        case "beta":
                            this._update_type = UpdateTypes.BETA;
                            break;
                        case "stable":
                            this._update_type = UpdateTypes.STABLE;
                            break;
                        default:
                            this._update_type = UpdateTypes.INVALID;
                            break;
                    }

                }
            }
            catch (Exception e)
            {
                this._valid = false;
                Log.Instance.Write(LogMessageTypes.ERROR,string.Format("Error parsing update data! Exception details: {0}",e.ToString()));
            }
        }
    }

    public enum UpdateTypes
    {
        INVALID,
        STABLE,
        BETA
    }
}
