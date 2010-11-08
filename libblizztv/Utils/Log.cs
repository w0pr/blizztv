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
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace LibBlizzTV.Utils
{
    public enum LogMessageTypes
    {
        DEBUG,
        INFO,
        ERROR,
        FATAL,
    }

    public sealed class Log : IDisposable
    {
        private static Log _instance = new Log();
        public static Log Instance { get { return _instance; } }

        private bool _logger_enabled = false;
        private FileStream _file_stream;
        private StreamWriter _log_stream;
        private string _log_file = "debug.log";
        private bool disposed = false;

        private Log() { }

        public void EnableLogger()
        {
            if (!this._logger_enabled)
            {
                this._file_stream = new FileStream(this._log_file, FileMode.Append, FileAccess.Write);
                this._log_stream = new StreamWriter(this._file_stream);
                this._logger_enabled = true;
            }
        }

        public void DisableLogger()
        {
            if (this._logger_enabled)
            {
                this._logger_enabled = false;
                this._file_stream.Close();
            }
        }

        public void Write(LogMessageTypes _type, string _str)
        {
            DebugConsole.Instance.Write(_type, _str); // also pass message to debug console
            if (this._logger_enabled)
            {
                this._log_stream.WriteLine(string.Format("[{0} {1}] {2}", _type.ToString(), DateTime.Now.ToString("HH:mm:ss"), _str));
                this._log_stream.AutoFlush = true;
            }
        }

        ~Log() { Dispose(false); }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        private void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                this._file_stream.Close();
                this._file_stream.Dispose();
                this._file_stream = null;
                disposed = true;
            }
        }
    }
}
