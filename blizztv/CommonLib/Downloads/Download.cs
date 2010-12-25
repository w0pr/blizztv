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
using System.IO;
using System.Net;
using System.Threading;
using BlizzTV.CommonLib.Logger;

namespace BlizzTV.CommonLib.Downloads
{    
    public class Download
    {
        private FileStream _filestream;

        public string FilePath { get; private set; }
        public string Uri { get; private set; }
        public bool Success { get; private set; }
                              
        public long DownloadedBytes { get { if (this._filestream != null) return this._filestream.Length; else return 0; } }

        public string DownloadedSize
        {
            get
            {
                string[] suffixes = { "B", "KB", "MB", "GB", "TB" };
                int i;
                long totalBytes = this.DownloadedBytes;
                double dblSByte = 0;
                for (i = 0; (int)(totalBytes / 1024) > 0; i++, totalBytes /= 1024) dblSByte = totalBytes / 1024.0;
                return String.Format("{0:0.00} {1}", dblSByte, suffixes[i]);
            }
        }

        public long TotalBytes { get; private set; }

        public string TotalSize
        {
            get
            {
                string[] suffixes = { "B", "KB", "MB", "GB", "TB" };
                int i;
                long totalBytes = this.TotalBytes;
                double dblSByte = 0;
                for (i = 0; (int)(totalBytes / 1024) > 0; i++, totalBytes /= 1024) dblSByte = totalBytes / 1024.0;
                return String.Format("{0:0.00} {1}", dblSByte, suffixes[i]);
            }
        }
        
        public int DownlodadedPercent { get { if (this._filestream != null) return (int)((this._filestream.Length * 100) / this.TotalBytes); else return 0; } }

        public EventHandler Complete;

        public delegate void DownloadProgressEventHandler(int progress);
        public event DownloadProgressEventHandler Progress;

        public Download(string uri, string filename="")
        {
            this.Uri = uri;
            this.FilePath = filename;
        }

        public void Start()
        {
            new Thread(() => { this.DownloadFile(this.FilePath); }) { IsBackground = true }.Start();
        }

        private void DownloadFile(string filename="")
        {
            try
            {
                Log.Instance.Write(LogMessageTypes.Info, string.Format("Download started: {0}.", this.Uri));

                this.FilePath = filename;
                this._filestream = new FileStream(this.FilePath, FileMode.Create);

                WebRequest request = WebRequest.Create(this.Uri);
                WebResponse response = request.GetResponse();
                this.TotalBytes = response.ContentLength;

                int bufferSize = 4096;
                byte[] buffer = new byte[bufferSize];
                int readBytes = 0;

                while ((readBytes = response.GetResponseStream().Read(buffer, 0, bufferSize)) > 0)
                {
                    this._filestream.Write(buffer, 0, readBytes);
                    if (this.Progress != null) this.Progress(this.DownlodadedPercent);
                }

                response.GetResponseStream().Close();                
                this.Success = true;
                Log.Instance.Write(LogMessageTypes.Info, string.Format("Download finished: {0} and saved as {1}.", this.Uri, this.FilePath));
            }
            catch (Exception e)
            {
                this.Success = false;
                Log.Instance.Write(LogMessageTypes.Info, string.Format("Download failed with exception: {0}", e));
            }
            finally
            {
                if (this._filestream != null) this._filestream.Close();
                if (this.Complete != null) this.Complete(this, EventArgs.Empty);
            }
        }
    }
}
