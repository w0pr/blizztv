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
using System.Drawing;
using System.Text;

namespace LibBlizzTV
{
    public class MenuItemEventArgs:EventArgs
    {
        private string _name;
        private EventHandler _handler;
        private Image _icon;

        public string Name { get { return this._name; } }
        public EventHandler Handler { get { return this._handler; } }
        public Image Icon { get { return this._icon; } }

        public MenuItemEventArgs(string Name, EventHandler Handler, Image Icon = null)
        {
            this._name = Name;
            this._handler = Handler;
            this._icon = Icon;
        }
    }

    public struct ZonedDateTime
    {
        private readonly DateTime _original;
        private readonly DateTime _utc;
        private readonly DateTime _local;
        private readonly TimeZoneInfo _time_zone_info;

        public ZonedDateTime(DateTime DateTime, TimeZoneInfo TimeZone)
        {
            this._original = DateTime;
            this._utc = TimeZoneInfo.ConvertTimeToUtc(DateTime, TimeZone);
            this._local = TimeZoneInfo.ConvertTime(this._utc, TimeZoneInfo.Local);
            this._time_zone_info = TimeZone;
        }

        public TimeZoneInfo TimeZone { get { return this._time_zone_info; } }
        public DateTime OriginalTime { get { return this._original; } }
        public DateTime UniversalTime { get { return this._utc; } }
        public DateTime LocalTime { get { return this._local; } }
    }
}
