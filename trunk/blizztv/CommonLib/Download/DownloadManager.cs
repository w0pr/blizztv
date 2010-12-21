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
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BlizzTV.CommonLib.Download
{
    public sealed class DownloadManager
    {
        #region Instance

        private static DownloadManager _instance = new DownloadManager();
        public static DownloadManager Instance { get { return _instance; } }

        #endregion

        public List<Download> Downloads = new List<Download>();

        public Download Add(DownloadType type, string uri)
        {
            Download download = new Download(uri, type);
            this.Downloads.Add(download);
            return download;
        }
    }

    public enum DownloadType
    {
        DownloadToMemory,
        DownloadToFile
    }
}
