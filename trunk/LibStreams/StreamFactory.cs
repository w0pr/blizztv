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
using LibStreams.Handlers;

namespace LibStreams
{
    public static class StreamFactory // streams factory
    {
        public static Stream CreateStream(string Name,string Slug,string Provider) // creates a stream object based on it's provider
        {
            Stream _stream=null;
            switch (Provider.ToLower()) // create the appr. stream object based on it's provider.
            {
                case "livestream":                    
                    _stream = new LiveStream(Name,Slug,Provider);
                    break;
                case "justintv":
                    _stream = new JustinTV(Name, Slug, Provider);
                    break;
                case "ustream":
                    _stream = new UStream(Name, Slug, Provider);
                    break;
                case "own3dtv":
                    _stream = new Own3DTV(Name, Slug, Provider);
                    break;
                default:
                    break;
            }

            if (_stream != null) return _stream; // if we found a valid stream provider.
            else throw new NotImplementedException(string.Format("Stream provider not implemented: '{0}'", Provider)); // throw an exception if stream was not associated with a valid provider.
        }
    }
}
