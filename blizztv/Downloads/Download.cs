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
 * $Id: Settings.cs 355 2011-02-07 12:05:26Z shalafiraistlin@gmail.com $
 */

using System;
using System.IO;
using System.Net;
using System.Timers;
using System.Threading;
using BlizzTV.Log;

namespace BlizzTV.Downloads
{    
    public class Download
    {        
        public string Uri { get; private set; } /* the uri of the original resource */
        public string FilePath { get; private set; } /* the file save path */
        public long FileSize { get; private set; } /* total size of the file */
        public long DownloadedBytes { get; private set; } /* current total of downloaded bytes */
        public long BytesPerSec { get; private set; } /* speed in bytes/sec */
        public bool Success { get; private set; } /* was download succesfull? */

        private long _lastDownloadedBytes = 0; /* used by the speed calculation routines */
        private System.Timers.Timer _speedTimer = null; /* timer used for calculating the download speed */
        private FileStream _filestream; /* the filestream to be used for writing the file */
        private WebResponse _response; /* the web-response */
        private Stream _responseStream; /* the response stream */

        public string Size /* returns the file size in B/KB/MB/GB/TB format */
        {
            get
            {
                int i;
                string[] suffixes = { "B", "KB", "MB", "GB", "TB" };
                long bytes = this.FileSize;
                double dblSByte = 0;
                for (i = 0; (int)(bytes / 1024) > 0; i++, bytes /= 1024) dblSByte = bytes / 1024.0;
                return string.Format("{0:0.00} {1}", dblSByte, suffixes[i]);
            }
        }

        public string Downloaded /* returns the downloaded total in B/KB/MB/GB/TB format */
        {
            get
            {
                int i;
                string[] suffixes = { "B", "KB", "MB", "GB", "TB" };
                long bytes = this.DownloadedBytes;
                double dblSByte = 0;
                for (i = 0; (int)(bytes / 1024) > 0; i++, bytes /= 1024) dblSByte = bytes / 1024.0;
                return string.Format("{0:0.00} {1}", dblSByte, suffixes[i]);
            }
        }

        public string Speed /* returns the current speed in B/sec, KB/sec, MB/sec, GB/sec, TB/sec format */
        {
            get
            {
                int i;
                string[] suffixes = { "B/sec", "KB/sec", "MB/sec", "GB/sec", "TB/sec" };
                long bytes = this.BytesPerSec;
                double dblSByte = 0;
                for (i = 0; (int)(bytes / 1024) > 0; i++, bytes /= 1024) dblSByte = bytes / 1024.0;
                return string.Format("{0:0.00} {1}", dblSByte, suffixes[i]);
            }
        }

        public int DownlodadedPercent /* returns downloaded percent */
        {
            get { return (int)((this.DownloadedBytes * 100) / this.FileSize); }
        }

        public delegate void DownloadProgressEventHandler(int progress);
        public event DownloadProgressEventHandler Progress; /* notifies observers about download progress */

        public delegate void DownloadCompleteEventHandler(bool success);
        public event DownloadCompleteEventHandler Complete; /* fires when download is complete */

        public Download(string uri, string filePath="")
        {
            this.Uri = uri;            
            this.FilePath = string.IsNullOrEmpty(filePath) ? Path.GetFileName(uri) : filePath;
            this.FileSize = 0;
            this.DownloadedBytes = 0;
            this.BytesPerSec = 0;
            this.Success = false;
        }

        public void Start() 
        {
            new Thread(this.DownloadFile) { IsBackground = true }.Start(); /* start the download within a new thread */
        }

        private void DownloadFile()
        {
            try
            {
                LogManager.Instance.Write(LogMessageTypes.Info, string.Format("Download started: {0}.", this.Uri));

                using (this._filestream = new FileStream(this.FilePath, FileMode.Create)) /* create the local file */
                {
                    WebRequest request = WebRequest.Create(this.Uri); /* create a request to the uri */

                    using (this._response = request.GetResponse())
                    {
                        if (this._response == null)
                        {
                            LogManager.Instance.Write(LogMessageTypes.Error, string.Format("Downloader can't read the response: {0}", this.Uri));
                            if (this.Complete != null) this.Complete(false);
                            return;
                        }

                        this.FileSize = this._response.ContentLength; /* read the files actual lenght */
                        this._speedTimer = new System.Timers.Timer(1000); /* startup the speed timer and let it hit every 1 secs. */
                        this._speedTimer.Elapsed += OnSpeedTimerHit;
                        this._speedTimer.Enabled = true;

                        using (this._responseStream = this._response.GetResponseStream()) /* get the response stream */
                        {
                            if (this._responseStream == null)
                            {
                                LogManager.Instance.Write(LogMessageTypes.Error, string.Format("Downloader can't read the response stream: {0}", this.Uri));
                                if (this.Complete != null) this.Complete(false);
                                return;
                            }

                            int readBytes;
                            const int bufferSize = 4096; /* set chunks to 4096 bytes */
                            byte[] buffer = new byte[bufferSize];

                            while ((readBytes = this._responseStream.Read(buffer, 0, bufferSize)) > 0) /* start reading the stream */
                            {
                                this._filestream.Write(buffer, 0, readBytes); /* write read bytes to our save-file */
                                int lastPercent = this.DownlodadedPercent;
                                this.DownloadedBytes += readBytes;
                                if (this.Progress != null && this.DownlodadedPercent != lastPercent) this.Progress(this.DownlodadedPercent); /* notify the observers on percent change */
                            }
                        }

                        this.Success = true;
                        LogManager.Instance.Write(LogMessageTypes.Info, string.Format("Download of {0} completed and saved as {1}.", this.Uri, this.FilePath));                        
                    }
                }   
            }

            catch (Exception e)
            {
                this.Success = false;
                LogManager.Instance.Write(LogMessageTypes.Info, string.Format("Download of {0} failed with exception: {1}", this.Uri, e));
            }

            finally
            {
                /* free-up resources that we no longer need */
                if (this._responseStream!=null) 
                {
                    this._responseStream.Close();
                    this._responseStream = null;
                }

                if(this._response!=null)
                {
                    this._response.Close();
                    this._response = null;
                }

                if (this._filestream != null) 
                {
                    this._filestream.Close();
                    this._filestream = null;
                }

                if (this.Complete != null) this.Complete(this.Success);
            }
        }

        void OnSpeedTimerHit(object sender, ElapsedEventArgs e) /* calculates the current download speed */
        {
            this.BytesPerSec = this.DownloadedBytes - this._lastDownloadedBytes; /* delta of downloaded bytes */
            this._lastDownloadedBytes = this.DownloadedBytes;
        }
    }
}
