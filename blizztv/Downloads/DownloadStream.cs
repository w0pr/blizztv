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
using System.Threading;
using BlizzTV.Log;

namespace BlizzTV.Downloads
{
    public class DownloadStream : Stream /* overrides the stream */
    {
        public string Uri { get; private set; } /* the uri of the original resource */

        private readonly MemoryStream _memoryStream=new MemoryStream(); /* memory stream that downloaded data is written */
        private WebResponse _response; /* the web response */
        private Stream _httpStream; /* the http-stream that data is read */
        private bool _finishedReading = false; /* did we reach the end-of-stream? */
        private long _readPosition = 0; /* current position */

        public DownloadStream(string uri)
        {
            this.Uri = uri;

            /* start the download of stream within a new thread */
            Thread thread = new Thread(() =>
                {
                    try
                    {
                        LogManager.Instance.Write(LogMessageTypes.Info, string.Format("Download of stream started: {0}.", this.Uri));
                        WebRequest request = WebRequest.Create(this.Uri); /* create a request to the uri */

                        using (this._response = request.GetResponse()) /* read the response */
                        {
                            if (this._response == null)
                            {
                                LogManager.Instance.Write(LogMessageTypes.Error, string.Format("Stream-downloader can't read the response: {0}", this.Uri));
                                this._finishedReading = true;
                                return;
                            }

                            using (this._httpStream = this._response.GetResponseStream()) /* get the response stream */
                            {
                                if (this._httpStream == null)
                                {
                                    LogManager.Instance.Write(LogMessageTypes.Error, string.Format("Stream-downloader can't read the response stream: {0}", this.Uri));
                                    this._finishedReading = true;
                                    return;
                                }

                                const int bufferSize = 4096; /* set chunks to 4096 bytes */
                                byte[] buffer = new byte[bufferSize];
                                int bytesRead;

                                while ((bytesRead = this._httpStream.Read(buffer, 0, bufferSize)) > 0) /* start reading the stream */
                                {
                                    lock (this._memoryStream) /* lock the memory-stream while writing so make sure that it can't collide with a read operation */
                                    {
                                        this.Seek(0, SeekOrigin.End); /* seek to the end as a read operation may have changed the current position */
                                        this._memoryStream.Write(buffer, 0, bytesRead); /* write the read-buffer */
                                    }
                                }

                                LogManager.Instance.Write(LogMessageTypes.Info, string.Format("Download of stream {0} completed.", this.Uri));
                            }
                        }
                    }

                    catch (Exception e)
                    {
                        LogManager.Instance.Write(LogMessageTypes.Info, string.Format("Download of stream {0} failed with exception: {1}", this.Uri, e));
                    }

                    finally
                    {
                        this._finishedReading = true; /* we're done with reading the http-stream */

                        /* free-up resources that we no longer need */
                        if (this._httpStream != null)
                        {
                            this._httpStream.Close();
                            this._httpStream = null;
                        }

                        if (this._response != null)
                        {
                            this._response.Close();
                            this._response = null;
                        }
                    }

                }) { IsBackground = true };
            thread.Start();
        }

        public override int Read(byte[] buffer, int offset, int count) /* override the stream read method */
        {
            int bytesRead;
            while (!this._finishedReading && this._memoryStream.Length < count) Thread.Sleep(10); /* if we've not yet read the remote http-stream, sleep the calling thread */

            lock (this._memoryStream) /* lock the read operation so that it can't collide with a write operation */
            {
                this._memoryStream.Seek(this._readPosition, SeekOrigin.Begin); /* seek to the current read-position */
                bytesRead = this._memoryStream.Read(buffer, offset, count); /* read from the stream */
                this._readPosition += bytesRead; /* push the current read position */
            }

            if (bytesRead == 0 && this._finishedReading) this._memoryStream.Close(); /* close the memory stream if read position is at the end-of-file when we're already done with reading the remote http-stream */
            return bytesRead;
        }

        /* override the other common stream methods */
        public override bool CanRead { get { return this._memoryStream.CanRead; } }
        public override bool CanSeek { get { return this._memoryStream.CanSeek; } }
        public override bool CanWrite { get { return false; } } /* no you can't write man */
        public override long Position  { get { return this._memoryStream.Position; } set { this._memoryStream.Position = value; } }
        public override long Seek(long offset, SeekOrigin origin) { return this._memoryStream.Seek(offset, origin); } 
        public override long Length { get { return this._memoryStream.Length; } }        
        public override void Write(byte[] buffer, int offset, int count) { return; } /* don't allow writing to the stream at all */
        public override void Flush() { return; } /* we don't need a flush routine as we don't allow writing */
        public override void SetLength(long value) { return; } /* we don't need a SetLenght routine as we don't allow writing */
    }
}
