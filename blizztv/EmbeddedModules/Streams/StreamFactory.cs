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
using BlizzTV.EmbeddedModules.Streams.Parsers;

namespace BlizzTV.EmbeddedModules.Streams
{
    /// <summary>
    /// Stream factory.
    /// </summary>
    public static class StreamFactory // streams factory
    {
        public static Stream CreateStream(StreamSubscription subscription) // creates a stream object based on it's provider
        {
            Stream stream=null;
            switch (subscription.Provider.ToLower()) // create the appr. stream object based on it's provider.
            {
                case "livestream": stream = new LiveStream(subscription); break;
                case "justintv": stream = new JustinTv(subscription); break;
                case "ustream": stream = new UStream(subscription); break;
                case "own3dtv": stream = new Own3Dtv(subscription); break;
                default: break;
            }

            if(stream==null) throw new NotImplementedException(string.Format("Stream provider not implemented: '{0}'", subscription.Provider)); // throw an exception if stream was not associated with a valid provider.
            return stream; // if we found a valid stream provider.
        }
    }
}
