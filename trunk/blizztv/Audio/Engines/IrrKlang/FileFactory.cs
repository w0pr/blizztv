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

using System.IO;
using BlizzTV.Downloads;
using IrrKlang;

namespace BlizzTV.Audio.Engines.IrrKlang
{
    class FileFactory:IFileFactory
    {
        public Stream openFile(string filename) // our custom-implemented filefactory based on IrrKlang's IFileFactory interface.
        {
            if (!filename.StartsWith("http://")) return File.OpenRead(filename); // just open as normal file.
            return DownloadManager.Instance.Stream(filename); // stream the audio file from internet.
        }
    }
}
