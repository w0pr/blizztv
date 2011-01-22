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

using IrrKlang;

namespace BlizzTV.Audio.Engines.IrrKlang
{
    public class IrrKlangTrack : IAudioTrack
    {
        public ISound Sound { get; private set; } // the internal sound object of IrrKlang.
        public string Filename { get; private set; } // the filename.

        public IrrKlangTrack(ISound sound, string filename)
        {
            this.Sound = sound;
            this.Filename = filename;
        }

        public bool IsPlaying
        {
            get { return this.Sound != null && (!this.Sound.Paused && !this.Sound.Finished); }
        }

        public bool IsPaused
        {
            get { return this.Sound != null && this.Sound.Paused; }
        }

        public bool IsFinished
        {
            get { return this.Sound != null && this.Sound.Finished; }
        }

        public int Duration
        {
            get { return (int)this.Sound.PlayLength; }
        }

        public int Position
        {
            get { return (int)this.Sound.PlayPosition; }
        }
    }
}
