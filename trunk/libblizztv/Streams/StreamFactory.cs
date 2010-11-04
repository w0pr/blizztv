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
using System.Text;

namespace LibBlizzTV.Streams
{
    public sealed class StreamFactory
    {
        private static readonly StreamFactory _instance = new StreamFactory();
        public static StreamFactory Instance { get { return _instance; } }

        public Stream CreateStream(string ID, string Name,string Provider)
        {
            Stream _stream=null;
            switch (Provider)
            {
                case "LiveStream":
                    _stream = new Handlers.LiveStream(ID, Name, Provider);
                    break;
                default:
                    break;
            }

            return _stream;
        }
    }
}
