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
 * $Id: Log.cs 158 2010-11-30 14:06:50Z shalafiraistlin@gmail.com $
 */

using System;
using System.IO;

namespace BlizzTV.ModuleLib.Utils
{
    #region log-message-types

    /// <summary>
    /// The log-message type.
    /// </summary>
    public enum LogMessageTypes
    {
        /// <summary>
        /// Debug message.
        /// </summary>
        DEBUG,
        /// <summary>
        /// Informational message.
        /// </summary>
        INFO,
        /// <summary>
        /// Error message.
        /// </summary>
        ERROR,
        /// <summary>
        /// Fatal error messsage.
        /// </summary>
        FATAL,
    }

    #endregion

    /// <summary>
    /// Enables logging of messages to a log file.
    /// </summary>
    public sealed class Log : IDisposable
    {
        #region members

        private static Log _instance = new Log();
        /// <summary>
        /// The log instance.
        /// </summary>
        public static Log Instance { get { return _instance; } }

        private bool _logger_enabled = false;
        private FileStream _file_stream;
        private StreamWriter _log_stream;
        private string _log_file = "debug.log";
        private bool disposed = false;

        #endregion

        #region ctor

        private Log() { }

        #endregion

        #region public functions
        /// <summary>
        /// Enables the logger.
        /// </summary>
        public void EnableLogger()
        {
            if (!this._logger_enabled) // make sure the log is not enabled before
            {
                this._file_stream = new FileStream(this._log_file, FileMode.Append, FileAccess.Write);
                this._log_stream = new StreamWriter(this._file_stream);
                this._logger_enabled = true;
            }
        }

        /// <summary>
        /// Disables the logger
        /// </summary>
        public void DisableLogger()
        {
            if (this._logger_enabled)
            {
                this._logger_enabled = false;
                this._file_stream.Close();
            }
        }

        /// <summary>
        /// Writes supplied message to a log file with given message type. Also forwards the message to DebugConsole.
        /// </summary>
        /// <param name="_type">The message type.</param>
        /// <param name="_str">The message</param>
        public void Write(LogMessageTypes _type, string _str)
        {
            DebugConsole.Instance.Write(_type, _str); // also pass message to debug console
            if (this._logger_enabled)
            {
                this._log_stream.WriteLine(string.Format("[{0}][{1}]: {2}", DateTime.Now.ToString("HH:mm:ss"), _type.ToString().PadLeft(5), _str));
                this._log_stream.AutoFlush = true; // Auto-flush and write the data to the file immediatly.
            }
        }

        #endregion

        #region de-ctor

        /// <summary>
        /// de-constructor
        /// </summary>
        ~Log() { Dispose(false); }

        /// <summary>
        /// Disposes the object.
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        private void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (this._logger_enabled)
                {
                    this._file_stream.Close();
                    this._file_stream.Dispose();
                }
                this._file_stream = null;
                disposed = true;
            }
        }

        #endregion
    }
}
