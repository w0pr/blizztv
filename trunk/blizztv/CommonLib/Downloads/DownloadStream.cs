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
    public class DownloadStream:Stream
    {
        private MemoryStream _memoryStream = new MemoryStream();
        private Stream _httpStream;
        private bool _finished = false;
        private long _readPosition = 0;

        public DownloadStream(string uri)
        {
            Thread thread = new Thread(() =>
                {
                    Log.Instance.Write(LogMessageTypes.Info, string.Format("DownloadStream started: {0}.", uri));

                    Log.Instance.Write(LogMessageTypes.Info, "GetResponse()");
                    WebRequest request = WebRequest.Create(uri);
                    WebResponse response = request.GetResponse();
                    this._httpStream = response.GetResponseStream();
                    Log.Instance.Write(LogMessageTypes.Info, "GotResponse()");

                    int bufferSize = 4096;
                    byte[] buffer = new byte[bufferSize];
                    int bytesRead = 0;

                    while ((bytesRead = this._httpStream.Read(buffer, 0, bufferSize)) > 0)
                    {
                        lock (this._memoryStream)
                        {
                            this.Seek(0, SeekOrigin.End);
                            this._memoryStream.Write(buffer, 0, bytesRead);
                            Log.Instance.Write(LogMessageTypes.Info, string.Format("[RECV] - {0} bytes", bytesRead.ToString()));
                        }                        
                    }

                    Log.Instance.Write(LogMessageTypes.Info, "Finished()");
                    this._finished = true;
                    this._httpStream.Close();
                }) { IsBackground = true };
            thread.Start();
        }

        public override int Read(byte[] buffer, int offset, int count)
        {
            while (!this._finished && this._memoryStream.Length < count) Thread.Sleep(10);
            int bytesRead = 0;
            lock (this._memoryStream)
            {
                this._memoryStream.Seek(this._readPosition, SeekOrigin.Begin);
                bytesRead = this._memoryStream.Read(buffer, offset, count);
                this._readPosition += bytesRead;
                Log.Instance.Write(LogMessageTypes.Info, string.Format("[READ] - {0} bytes", bytesRead.ToString()));
            }
            if (bytesRead == 0 && this._finished) this._memoryStream.Close();
            return bytesRead;
        }

        public override void Write(byte[] buffer, int offset, int count)
        {
            return;
        }

        public override long Seek(long offset, SeekOrigin origin)
        {
            return this._memoryStream.Seek(offset, origin);
        }

        public override void Flush() { return; }
        public override void SetLength(long value) { return; }

        public override long Position
        {
            get { return this._memoryStream.Position; }
            set { this._memoryStream.Position = value; }
        }

        public override long Length { get { return this._memoryStream.Length; } }
        public override bool CanRead { get { return this._memoryStream.CanRead; } }
        public override bool CanSeek { get { return this._memoryStream.CanSeek; } }
        public override bool CanWrite { get { return false; } } 
    }
}
