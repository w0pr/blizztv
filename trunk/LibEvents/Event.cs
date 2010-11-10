﻿/*    
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
using System.Text;
using LibBlizzTV;
using LibBlizzTV.Utils;

namespace LibEvents
{
    public class Event:ListItem
    {
        #region members

        private string _full_title; // the full title of the event.
        private string _description; // the event description.
        private string _event_id; // the event id.
        private bool _is_over = false; // is the event over?
        private ZonedDateTime _time; // zoned event time info. 

        public string FullTitle { get { return this._full_title; } }
        public string Description { get { return this._description; } }
        public string EventID { get { return this._event_id; } }
        public bool IsOver { get { return this._is_over; } }
        public ZonedDateTime Time { get { return this._time; } }

        #endregion

        #region ctor

        public Event(string Title, string FullTitle, string Description, string EventID,bool isOver, ZonedDateTime Time)
            : base(Title)
        {
            this._full_title = FullTitle;
            this._description = Description;
            this._event_id = EventID;
            this._is_over = isOver;
            this._time = Time;
        }

        #endregion
    }
}