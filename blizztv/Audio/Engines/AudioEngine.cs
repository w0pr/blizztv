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

namespace BlizzTV.Audio.Engines
{
    public abstract class AudioEngine
    {
        // engine capatabilities.
        public abstract bool CanPlayStreams { get; }
        public abstract bool CanPlayFromMemory { get; }

        // engine state.
        protected bool IsPlaying { get { return this.CurrentTrack != null && this.CurrentTrack.IsPlaying; } }
        protected IAudioTrack CurrentTrack { get; set; }

        // engine API.
        public virtual void Play(string filename) { throw new NotSupportedException("Audio engine does not support Play()."); }
        public virtual void PlayInternetStream(string url) { throw new NotSupportedException("Audio engine does not support PlayInternetStream()."); }
        public virtual void PlayFromMemory(string name, byte[] data) { throw new NotSupportedException("Audio engine does not support PlayFromMemory()."); }
        public virtual void Pause() { throw new NotSupportedException("Audio engine does not support Pause()."); }
        public virtual void Resume() { throw new NotSupportedException("Audio engine does not support Resume()."); }
        public virtual void Stop() { throw new NotSupportedException("Audio engine does not support Stop()."); }
        public virtual void StopAll() { throw new NotSupportedException("Audio engine does not support StopAll()."); }
        public virtual void SetPosition(int position) { throw new NotSupportedException("Audio engine does not support SetPosition()."); }
        public virtual void SetVolume(double volume) { throw new NotSupportedException("Audio engine does not support SetVolume()."); }
    }
}
