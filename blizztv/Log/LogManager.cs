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
using System.IO;

namespace BlizzTV.Log
{
    public sealed class LogManager : IDisposable // Enables logging of messages to a log-file.
    {
        #region Instance

        private static LogManager _instance = new LogManager();
        public static LogManager Instance { get { return _instance; } }

        #endregion

        private bool _loggerEnabled = false; 
        private const string LogFile = "debug.log"; 
        private FileStream _fileStream;
        private StreamWriter _logStream;        
        private bool _disposed = false;

        private LogManager() { }

        public void EnableLogger() 
        {
            if (this._loggerEnabled) return;

            try
            {
                this._fileStream = new FileStream(LogFile, FileMode.Append, FileAccess.Write);
                this._logStream = new StreamWriter(this._fileStream);
                this._loggerEnabled = true;
            }
            catch(Exception e)
            {
                this._loggerEnabled = false;
                this._fileStream = null;
                this._logStream = null;
                DebugConsole.Instance.Write(LogMessageTypes.Error,string.Format("LogManager caught an exception while opening the log file: {0} - {1}", LogFile, e));
            }
        }

        public void DisableLogger() 
        {
            if (!this._loggerEnabled) return;

            this._loggerEnabled = false;
            this._logStream.Dispose();
            this._logStream = null;
            this._fileStream.Dispose();           
            this._fileStream = null;
        }

        public void Write(LogMessageTypes type, string str) 
        {
            DebugConsole.Instance.Write(type, str); // also forward the log request to debug-console.            
            if (!this._loggerEnabled) return;

            this._logStream.WriteLine(string.Format("[{0}][{1}]: {2}", DateTime.Now.ToString("HH:mm:ss"), type.ToString().PadLeft(5), str));
            this._logStream.AutoFlush = true; 
        }

        #region de-ctor

        ~LogManager() { Dispose(false); }

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
                this._logStream.Dispose();                
                this._fileStream.Dispose();                
            }

            this._logStream = null;
            this._fileStream = null;
            _disposed = true;
        }

        #endregion
    }

    public enum LogMessageTypes
    {
        Trace,
        Debug,
        Info,
        Warn,
        Error,
        Fatal,
    }
}
