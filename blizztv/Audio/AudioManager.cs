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
using System.Threading;
using BlizzTV.Audio.Engines;
using BlizzTV.Audio.Engines.IrrKlang;
using BlizzTV.CommonLib.Logger;

namespace BlizzTV.Audio
{
    public sealed class AudioManager
    {
        #region Instance

        private static readonly AudioManager _instance = new AudioManager();
        public static AudioManager Instance { get { return _instance; } }

        #endregion

        private readonly AudioEngine _engine; // the audio engine to be used.
        public readonly AudioEngineStatus EngineStatus = AudioEngineStatus.Unknown; // the engines startup status.

        private AudioManager() 
        {
            try
            {
                this._engine = new IrrKlangEngine(); 
                this.EngineStatus = AudioEngineStatus.Ready;
            }
            catch (Exception e)
            {
                // set the engines status based on the caught exception type. 
                // IrrKlang requires Visual C++ 2010 runtime, if not found it'll be throwing a FileNotFoundException.
                // Other exceptions thrown by the IrrKlang are most probable when no available sound devices exists on system.
                this.EngineStatus = e.GetType() == typeof(System.IO.FileNotFoundException) ? AudioEngineStatus.MissingDependency : AudioEngineStatus.NoAvailableSoundDevice;
                Log.Instance.Write(LogMessageTypes.Error, string.Format("AudioManager is not functional as the underlying engine initialization failed. {0}", e));
            }
        }

        public void Play(string filename)
        {
            if (this.EngineStatus != AudioEngineStatus.Ready) return; // make sure underlying engine is functional.
            new Thread(() => this._engine.Play(filename)) { IsBackground = true }.Start();          
        }

        public void PlayInternetStream(string url)
        {
            if (this.EngineStatus != AudioEngineStatus.Ready) return; // make sure underlying engine is functional.
            if (!this._engine.CanPlayStreams) throw new NotSupportedException(); // make sure it can stream from internet.
            new Thread(() => this._engine.PlayInternetStream(url)) { IsBackground = true }.Start();
        }

        public void PlayFromMemory(string name, byte[] data)
        {
            if (this.EngineStatus != AudioEngineStatus.Ready) return; // make sure underlying engine is functional.
            if (!this._engine.CanPlayFromMemory) throw new NotFiniteNumberException(); // make sure it can read the audio data directly from memory.
            new Thread(() => this._engine.PlayFromMemory(name, data)) { IsBackground = true }.Start();
        }

        public enum AudioEngineStatus
        {
            Unknown, // not initialized yet.
            Ready, // engine is all functional.
            MissingDependency, // engine missing a dependency (like IrrKlang requiring Visual C++ 2010 runtime).
            NoAvailableSoundDevice // no available sound devices exists on the system.
        }
    }
}
