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
 * $Id: Common.cs 158 2010-11-30 14:06:50Z shalafiraistlin@gmail.com $
 */

using System;

namespace BlizzTV.Module
{
    #region Common stuff used by BlizzTV and it's plugins
 
    /// <summary>
    /// Stores DateTime with zone information and allows to get store DateTime in original, UTC and local-time zone.
    /// </summary>
    public class ZonedDateTime
    {
        private readonly DateTime _original;
        private readonly DateTime _utc;
        private readonly DateTime _local;
        private readonly TimeZoneInfo _time_zone_info;

        /// <summary>
        /// Constructs a new DateTime with zone information
        /// </summary>
        /// <param name="DateTime">The datetime.</param>
        /// <param name="TimeZoneInfo">The supplied datetime's zone information.</param>
        public ZonedDateTime(DateTime DateTime, TimeZoneInfo TimeZoneInfo)
        {
            this._original = DateTime;
            this._utc = TimeZoneInfo.ConvertTimeToUtc(DateTime, TimeZoneInfo);
            this._local = TimeZoneInfo.ConvertTime(this._utc, TimeZoneInfo.Local);
            this._time_zone_info = TimeZoneInfo;
        }

        /// <summary>
        /// TimeZoneInfo of the DateTime.
        /// </summary>
        public TimeZoneInfo TimeZoneInfo { get { return this._time_zone_info; } }
        /// <summary>
        /// DateTime in original time-zone.
        /// </summary>
        public DateTime OriginalTime { get { return this._original; } }
        /// <summary>
        /// DateTime in UTC.
        /// </summary>
        public DateTime UniversalTime { get { return this._utc; } }
        /// <summary>
        /// DateTime in computer's local time-zone.
        /// </summary>
        public DateTime LocalTime { get { return this._local; } }
    }

    #endregion
}
