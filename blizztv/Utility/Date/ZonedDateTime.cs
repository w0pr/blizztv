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

namespace BlizzTV.Utility.Date
{
    /// <summary>
    /// Stores DateTime with zone information and allows to get store DateTime in original, UTC and local-time zone.
    /// </summary>
    public class ZonedDateTime 
    {
        private readonly DateTime _original;
        private readonly DateTime _utc;
        private readonly DateTime _local;
        private readonly TimeZoneInfo _timeZoneInfo;

        /// <summary>
        /// The original time on provided zone.
        /// </summary>
        public TimeZoneInfo TimeZoneInfo { get { return this._timeZoneInfo; } }

        /// <summary>
        /// The time in UTC.
        /// </summary>
        public DateTime OriginalTime { get { return this._original; } }

        /// <summary>
        /// The time in computer's current zone.
        /// </summary>
        public DateTime UniversalTime { get { return this._utc; } }

        /// <summary>
        /// The original zone.
        /// </summary>
        public DateTime LocalTime { get { return this._local; } }

        public ZonedDateTime(DateTime dateTime, TimeZoneInfo timeZoneInfo)
        {
            this._original = dateTime;
            this._utc = TimeZoneInfo.ConvertTimeToUtc(dateTime, timeZoneInfo);
            this._local = TimeZoneInfo.ConvertTime(this._utc, TimeZoneInfo.Local);
            this._timeZoneInfo = timeZoneInfo;
        }
    }
}
