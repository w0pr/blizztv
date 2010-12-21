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

namespace BlizzTV.CommonLib.Download
{
    public class WebStream 
    {
        internal int _bufferSize = 4096;
        internal byte[] _buffer;
        internal WebRequest _request;
        internal WebResponse _response;
        internal Stream _storageStream;

        public static ManualResetEvent allDone = new ManualResetEvent(false);

        public WebStream(DownloadType type,string uri, string targetFile=null)
        {
            Log.Instance.Write(LogMessageTypes.Info, string.Format("DownloadStream initialized: {0} - {1}", type.ToString(), uri));

            this._buffer = new byte[this._bufferSize];

            switch (type)
            {
                case  DownloadType.DownloadToFile:
                    this._storageStream = new FileStream(targetFile, FileMode.Create);
                    break;
                case DownloadType.DownloadToMemory:
                    this._storageStream = new MemoryStream();
                    break;
            }

            Log.Instance.Write(LogMessageTypes.Info, "BeginGetResponse()");

            this._request = WebRequest.Create(uri);           
            IAsyncResult result = (IAsyncResult)this._request.BeginGetResponse(new AsyncCallback(AsyncResponseCallback), this);
            allDone.WaitOne();
        }

        private static void AsyncResponseCallback(IAsyncResult asyncResult)
        {
            Log.Instance.Write(LogMessageTypes.Info, "ResponseCallback()");

            WebStream stream = (WebStream)asyncResult.AsyncState;
            stream._response = stream._request.EndGetResponse(asyncResult);
            IAsyncResult asyncRead = stream._response.GetResponseStream().BeginRead(stream._buffer, 0, stream._bufferSize, new AsyncCallback(AsynReadCallback), stream);
        }

        private static void AsynReadCallback(IAsyncResult asyncResult)
        {
            WebStream stream = (WebStream)asyncResult.AsyncState;
            int bytesRead = stream._response.GetResponseStream().EndRead(asyncResult);

            if (bytesRead > 0)
            {
                Log.Instance.Write(LogMessageTypes.Info, string.Format("ReadCallback {0} bytes", bytesRead));
                stream._storageStream.Write(stream._buffer, 0, bytesRead);
                IAsyncResult asyncRead = stream._response.GetResponseStream().BeginRead(stream._buffer, 0,stream._bufferSize, new AsyncCallback(AsynReadCallback), stream);
            }
            else
            {
                Log.Instance.Write(LogMessageTypes.Info, "Stream read completed..");
                stream._response.GetResponseStream().Close();
                stream._storageStream.Close();
                allDone.Set();
            }
        }
    }
}
