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
using System.Threading;
using System.Net;
using BlizzTV.CommonLib.Logger;

namespace BlizzTV.CommonLib.Audio
{
    public class AudioStream:Stream
    {
        private MemoryStream _memoryStream=new MemoryStream();
        private Stream _httpStream;
        private bool _httpReadFinished = false;

        public AudioStream(string uri)
        {            
            Thread thread = new Thread(() =>
                {
                    Log.Instance.Write(LogMessageTypes.Fatal, "Starting download thread, waiting for http response..");
                    HttpWebRequest request = (HttpWebRequest)WebRequest.Create(uri);
                    HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                    this._httpStream = response.GetResponseStream();
                    Log.Instance.Write(LogMessageTypes.Fatal, "Got response..");

                    byte[] buffer = new byte[1024];
                    int readBytes = 0;
                    while ((readBytes = this._httpStream.Read(buffer, 0, buffer.Length)) > 0)
                    {
                        this._memoryStream.Write(buffer, 0, readBytes);
                        Log.Instance.Write(LogMessageTypes.Fatal, string.Format("[RECV] - {0} bytes",readBytes.ToString()));
                    }

                    this._httpReadFinished = true;

                }) { IsBackground = true };
            thread.Start();
        }

        public override int Read(byte[] buffer, int offset, int count)
        {
            while (!this._httpReadFinished && this._memoryStream.Length < 1024) Thread.Sleep(100);
            int bytesRead = this._memoryStream.Read(buffer, offset, count);
            if (bytesRead == 0 & this._httpReadFinished) this.Close();
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
        public override bool CanRead { get { return true; } }
        public override bool CanSeek { get { return true; } }
        public override bool CanWrite { get { return false; } }        
    }
}
