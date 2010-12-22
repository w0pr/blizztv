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

namespace BlizzTV.CommonLib.Audio
{
    public sealed class AudioPlayer
    {
        #region Instance

        private static AudioPlayer _instance = new AudioPlayer();
        public static AudioPlayer Instance { get { return _instance; } }

        #endregion

        private AudioEngine _engine;

        private AudioPlayer() 
        {
            this._engine = new IrrKlangEngine();
        }

        public void Play(string filename)
        {
            new Thread(() => { this._engine.Play(filename); }) { IsBackground = true }.Start();          
        }

        public void PlayInternetStream(string url)
        {
            if (!this._engine.CanPlayStreams) throw new NotSupportedException();
            new Thread(() => { this._engine.PlayInternetStream(url); }) { IsBackground = true }.Start();
        }

        public void PlayFromMemory(string name, byte[] data)
        {
            if (!this._engine.CanPlayFromMemory) throw new NotFiniteNumberException();

            new Thread(() => { this._engine.PlayFromMemory(name, data); }) { IsBackground = true }.Start();
        }
    }
}
