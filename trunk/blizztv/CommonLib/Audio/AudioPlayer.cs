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
using System.Threading;
using BlizzTV.CommonLib.Audio.Engines;
using BlizzTV.CommonLib.Audio.Engines.IrrKlang;
using BlizzTV.CommonLib.Logger;

namespace BlizzTV.CommonLib.Audio
{
    public sealed class AudioPlayer
    {
        #region Instance

        private static AudioPlayer _instance = new AudioPlayer();
        public static AudioPlayer Instance { get { return _instance; } }

        #endregion

        private AudioEngine _engine;
        public AudioEngineStatus EngineStatus = AudioEngineStatus.Unknown;

        private AudioPlayer() 
        {
            try
            {
                this._engine = new IrrKlangEngine();
                this.EngineStatus = AudioEngineStatus.Ready;
            }
            catch (Exception e)
            {
                if (e.GetType() == typeof(System.IO.FileNotFoundException)) this.EngineStatus = AudioEngineStatus.MissingDependency;
                else this.EngineStatus = AudioEngineStatus.NoAvailableSoundDevice;
                Log.Instance.Write(LogMessageTypes.Error, string.Format("Audio engine initialization failed because of exception: {0}", e));
            }
        }

        public void Play(string filename)
        {
            if (this.EngineStatus != AudioEngineStatus.Ready) return;
            new Thread(() => { this._engine.Play(filename); }) { IsBackground = true }.Start();          
        }

        public void PlayInternetStream(string url)
        {
            if (this.EngineStatus != AudioEngineStatus.Ready) return;
            if (!this._engine.CanPlayStreams) throw new NotSupportedException();
            new Thread(() => { this._engine.PlayInternetStream(url); }) { IsBackground = true }.Start();
        }

        public void PlayFromMemory(string name, byte[] data)
        {
            if (this.EngineStatus != AudioEngineStatus.Ready) return;
            if (!this._engine.CanPlayFromMemory) throw new NotFiniteNumberException();
            new Thread(() => { this._engine.PlayFromMemory(name, data); }) { IsBackground = true }.Start();
        }

        public enum AudioEngineStatus
        {
            Unknown,
            Ready,
            MissingDependency,
            NoAvailableSoundDevice
        }
    }
}
