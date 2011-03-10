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
 * $Id$
 */

using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using BlizzTV.EmbeddedModules.Irc.Messages.Outgoing;

namespace BlizzTV.EmbeddedModules.Irc.Connection
{
    public class IrcConnection
    {
        public string Hostname { get; private set; }
        public int Port { get; private set; }

        private readonly Socket _socket;
        private byte[] _inputBuffer;
        private int _bufferPosition = 0;

        public IrcConnection(string hostname, int port)
        {
            this._socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            this._inputBuffer = new byte[0xFFF];

            this.Hostname = hostname;
            this.Port = port;            
        }

        public void Connect()
        {
            if (string.IsNullOrEmpty(this.Hostname)) return;
            this.ConnectAsync(new DnsEndPoint(this.Hostname, this.Port));
        }

        private void ConnectAsync(EndPoint remoteEndPoint)
        {           
            var connectEventArgs = new SocketAsyncEventArgs {RemoteEndPoint = remoteEndPoint};
            connectEventArgs.Completed += ConnectAsynCompleted;
            this._socket.ConnectAsync(connectEventArgs);
        }

        private void ConnectAsynCompleted(object sender, SocketAsyncEventArgs e)
        {
            this.OnConnectionCompleted(e.SocketError == SocketError.Success ? new IrcConnectionCompletedEventArgs(true) : new IrcConnectionCompletedEventArgs(false, new SocketException((int) e.SocketError)));
            this.RecvAsync();
        }

        private void RecvAsync()
        {
            var recvBuffer = new byte[0xFFF];
            var recieveEventArgs = new SocketAsyncEventArgs();
            recieveEventArgs.SetBuffer(recvBuffer, 0, recvBuffer.Length);
            recieveEventArgs.Completed += AsyncRecvCompleted;
            this._socket.ReceiveAsync(recieveEventArgs);
        }

        private void AsyncRecvCompleted(object sender, SocketAsyncEventArgs e)
        {
            if (e.SocketError != SocketError.Success) throw new SocketException((int)e.SocketError);
            if (this._socket == null || this._socket.Connected == false) return;

            Buffer.BlockCopy(e.Buffer, 0, this._inputBuffer, _bufferPosition, e.BytesTransferred);
            this._bufferPosition += e.BytesTransferred;

            string message;

            do
            {
                message = ExtractMessage(ref this._inputBuffer, ref this._bufferPosition);
                if (string.IsNullOrEmpty(message)) continue;
                this.OnMessageRecieved(new IrcMessageEventArgs(message));
            } while (message != string.Empty);

            this.RecvAsync();
        }

        /* http://stackoverflow.com/questions/3554302/irc-using-networkstream-buffer-fills-and-line-gets-chomped */
        private static string ExtractMessage(ref byte[] buffer, ref int lenght)
        {
            var message = string.Empty;
            var stringBuffer = Encoding.UTF8.GetString(buffer, 0, lenght);
            var lineBreakPosition = stringBuffer.IndexOf("\r\n");

            if(lineBreakPosition > -1)
            {
                message = stringBuffer.Substring(0, lineBreakPosition);
                var tempBuffer = new byte[0xFFF];
                lenght = lenght - message.Length - 2;
                if (lenght > 0)
                {
                    Array.Copy(buffer, lineBreakPosition + 2, tempBuffer, 0, lenght);
                    buffer = tempBuffer;
                }
            }
            return message;
        }

        public void Send(IrcOutgoingMessage message)
        {
            var raw = string.Format("{0}\r\n", message.GetRawMessage());
            var sendBuffer = Encoding.UTF8.GetBytes(raw);
            var sendEventArgs = new SocketAsyncEventArgs();
            sendEventArgs.SetBuffer(sendBuffer, 0, sendBuffer.Length);
            this._socket.SendAsync(sendEventArgs);
        }

        #region Events

        public EventHandler<IrcMessageEventArgs> MessageRecieved;

        private void OnMessageRecieved(IrcMessageEventArgs e)
        {
            EventHandler<IrcMessageEventArgs> handler = MessageRecieved;
            if (handler != null) handler(this, e);
        }

        public EventHandler<IrcConnectionCompletedEventArgs> ConnectionCompleted;

        private void OnConnectionCompleted(IrcConnectionCompletedEventArgs e)
        {
            EventHandler<IrcConnectionCompletedEventArgs> handler = ConnectionCompleted;
            if (handler != null) handler(this, e);
        }

        #endregion
    }

    #region Event arguments

    public class IrcConnectionCompletedEventArgs : EventArgs
    {
        public bool Success { get; private set; }
        public Exception Exception { get; private set; }

        public IrcConnectionCompletedEventArgs(bool success, Exception exception = null)
        {
            this.Success = success;
            this.Exception = exception;
        }
    }

    public class IrcMessageEventArgs : EventArgs
    {
        public string Message {get; private set;}
        public IrcMessageEventArgs(string message) { this.Message = message; }
    }

    #endregion 
}
