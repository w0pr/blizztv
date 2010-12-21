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

namespace BlizzTV.CommonLib.Audio.Engines
{
    public abstract class AudioEngine
    {
        public abstract bool CanPlayStreams { get; }
        public bool IsPlaying { get { if (this.CurrentTrack != null) return this.CurrentTrack.IsPlaying; else return false; } }
        public IAudioTrack CurrentTrack { get; protected set; }

        public abstract void Play(string filename);
        public abstract void PlayStream(string url);
        public abstract void Pause();
        public abstract void Resume();
        public abstract void Stop();
        public abstract void StopAll();
        public abstract void SetPosition(int position);
        public abstract void SetVolume(double volume);
    }
}
