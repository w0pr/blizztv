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
 * 
 * $Id$
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LibVideos.Handlers;

namespace LibVideos
{
    public static class ChannelFactory
    {
        public static Channel CreateChannel(string Name, string Slug, string Provider)
        {
            Channel _channel = null;
            switch (Provider.ToLower())
            {
                case "youtube":
                    _channel = new Youtube(Name, Slug, Provider);
                    break;
                case "bliptv":
                    _channel = new BlipTV(Name, Slug, Provider);
                    break;
                default:
                    break;
            }

            if (_channel != null) return _channel;
            else throw new NotImplementedException(string.Format("Video channel provider not implemented: '{0}'", Provider)); // throw an exception if video channel was not associated with a valid provider.
        }
    }
}