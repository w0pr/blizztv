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
 * $Id: DownloadManager.cs 252 2010-12-22 09:16:37Z shalafiraistlin@gmail.com $
 */

using System.Collections.Generic;

namespace BlizzTV.Downloads
{
    public sealed class DownloadManager
    {
        #region Instance

        private static DownloadManager _instance = new DownloadManager();
        public static DownloadManager Instance { get { return _instance; } }

        #endregion

        private readonly List<Download> _downloads = new List<Download>(); // the internal download list.

        public Download Add(string uri) // requests a regular download.
        {
            Download download = new Download(uri);
            this._downloads.Add(download);
            return download;
        }
        
        public DownloadStream Stream(string uri) // starts a download-stream.
        {
            return new DownloadStream(uri);
        }
    }
}
