﻿/*    
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
using System.Collections;
using System.Linq;
using System.Text;

namespace BlizzTV.CommonLib.HttpServer
{
    public sealed class HttpResponse
    {
        private Dictionary<int, string> _responseTexts = new Dictionary<int, string>()
        {
            {200,"OK"},
            {302,"Found"},
            {303,"See Other"},
            {400,"BadRequest"},
            {404,"Not Found"},
            {502,"Server Busy"},
            {500,"Internal Server Error"}
        };

        public string Protocol { get; private set; }
        public string ContentType { get; private set; }
        public bool CloseConnection { get; private set; }
        public ResponseCode Code { get; private set; }        
        public string Content {get;private set;}                
        public string Response {get;private set;}


        public HttpResponse(ResponseCode code, string content)
        {
            this.Protocol = "HTTP/1.1";
            this.ContentType = "text/html";
            this.CloseConnection = true;

            this.Code=code;
            this.Content=content;
            this.ForgeResponse();
        }

        private void ForgeResponse()
        {
            this.Response = string.Format("{0} {1} {2}\r\nDate: {3}\r\nServer: {4}\r\nConnection: {5}\r\nContent-Type: {6}\r\nContent-Lenght: {7}\r\n\r\n{8}",
                Protocol,
                (int)Code,
                this._responseTexts[(int)this.Code],
                DateTime.Now.ToString("R"),
                HttpServer.Instance.Banner,
                this.CloseConnection ? "close":"Keep-Alive" ,
                this.ContentType,
                this.Content.Length,
                this.Content);                               
        }

        public enum ResponseCode
        {
            OK = 200,
            Found = 302,
            SeeOther = 303,
            BadRequest = 400,
            NotFound = 404,
            InternalServerError = 500,
            ServerBusy = 502,
        }
    }
}
