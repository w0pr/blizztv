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

namespace BlizzTV.CommonLib.Utils
{
    public class ZonedDateTime // Stores DateTime with zone information and allows to get store DateTime in original, UTC and local-time zone.
    {
        private readonly DateTime _original;
        private readonly DateTime _utc;
        private readonly DateTime _local;
        private readonly TimeZoneInfo _timeZoneInfo;

        public ZonedDateTime(DateTime dateTime, TimeZoneInfo timeZoneInfo)
        {
            this._original = dateTime;
            this._utc = TimeZoneInfo.ConvertTimeToUtc(dateTime, timeZoneInfo);
            this._local = TimeZoneInfo.ConvertTime(this._utc, TimeZoneInfo.Local);
            this._timeZoneInfo = timeZoneInfo;
        }

        public TimeZoneInfo TimeZoneInfo { get { return this._timeZoneInfo; } }
        public DateTime OriginalTime { get { return this._original; } }
        public DateTime UniversalTime { get { return this._utc; } }
        public DateTime LocalTime { get { return this._local; } }
    }
}
