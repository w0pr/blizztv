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
using System.IO;

namespace BlizzTV.CommonLib.Logger
{
    public sealed class Log : IDisposable // Enables logging of messages to a log-file.
    {
        #region Instance

        private static Log _instance = new Log();
        public static Log Instance { get { return _instance; } }

        #endregion

        private bool _loggerEnabled = false;
        private FileStream _fileStream;
        private StreamWriter _logStream;
        private string _logFile = "debug.log";
        private bool _disposed = false;

        private Log() { }

        public void EnableLogger() // Enables the logger.
        {
            if (this._loggerEnabled) return; // make sure the logger is enabled.
            this._fileStream = new FileStream(this._logFile, FileMode.Append, FileAccess.Write);
            this._logStream = new StreamWriter(this._fileStream);
            this._loggerEnabled = true;
        }

        public void DisableLogger() // Disables the logger.
        {
            if (!this._loggerEnabled) return;
            this._loggerEnabled = false;
            this._fileStream.Close();
        }

        public void Write(LogMessageTypes type, string str) // Writes a supplied log message to the log-file and also forwards it to DebugConsole.
        {
            DebugConsole.Instance.Write(type, str); // also pass message to debug console
            if (!this._loggerEnabled) return;
            this._logStream.WriteLine(string.Format("[{0}][{1}]: {2}", DateTime.Now.ToString("HH:mm:ss"), type.ToString().PadLeft(5), str));
            this._logStream.AutoFlush = true; // Auto-flush and write the data to the file immediatly.
        }

        #region de-ctor

        ~Log() { Dispose(false); }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        private void Dispose(bool disposing)
        {
            if (this._disposed) return;
            if (this._loggerEnabled)
            {
                this._fileStream.Close();
                this._fileStream.Dispose();
            }
            this._fileStream = null;
            _disposed = true;
        }

        #endregion
    }

    public enum LogMessageTypes
    {
        Debug,
        Info,
        Error,
        Fatal,
    }
}
