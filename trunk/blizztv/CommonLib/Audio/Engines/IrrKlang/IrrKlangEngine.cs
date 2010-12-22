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
using IrrKlang;

namespace BlizzTV.CommonLib.Audio.Engines.IrrKlang
{
    public class IrrKlangEngine:AudioEngine
    {
        private ISoundEngine _engine;

        public IrrKlangEngine()
        {
            this._engine = new ISoundEngine();
            this._engine.AddFileFactory(new FileFactory());
        }

        public override bool CanPlayStreams { get { return true; } }
        public override bool CanPlayFromMemory { get { return true; } }

        public override void Play(string file)
        {
            ISound sound = this._engine.Play2D(file);
            this.CurrentTrack = new IrrKlangTrack(sound, file);
        }

        public override void PlayInternetStream(string url)
        {
            ISound sound = this._engine.Play2D(url);
            this.CurrentTrack = new IrrKlangTrack(sound, url);
        }

        public override void PlayFromMemory(string name, byte[] data)
        {
            ISoundSource source = this._engine.AddSoundSourceFromMemory(data, name);
            ISound sound = this._engine.Play2D(name);
            this.CurrentTrack = new IrrKlangTrack(sound, name);
        }

        public override void Pause()
        {
            if (this.IsPlaying) this._engine.SetAllSoundsPaused(true);
        }
        
        public override void Resume()
        {
            if (this.CurrentTrack != null && this.CurrentTrack.IsPaused) this._engine.SetAllSoundsPaused(false);
        }

        public override void Stop()
        {
            if (this.IsPlaying)
            {
                (this.CurrentTrack as IrrKlangTrack).Sound.Stop();
                this._engine.StopAllSounds();
                this._engine.RemoveSoundSource((this.CurrentTrack as IrrKlangTrack).Filename);
            }
        }

        public override void StopAll()
        {
            if (this.IsPlaying)
            {
                (this.CurrentTrack as IrrKlangTrack).Sound.Stop();
            }
            this._engine.RemoveAllSoundSources();
        }

        public override void SetPosition(int position)
        {
            if (this.CurrentTrack != null) (this.CurrentTrack as IrrKlangTrack).Sound.PlayPosition = (uint)position;
        }

        public override void SetVolume(double volume)
        {
            if (this.CurrentTrack != null) (this.CurrentTrack as IrrKlangTrack).Sound.Volume = (float)volume;
        }
    }

    public class IrrKlangTrack : IAudioTrack
    {
        public ISound Sound { get; private set; }
        public string Filename { get; private set; }

        public IrrKlangTrack(ISound sound, string filename)
        {
            this.Sound = sound;
            this.Filename = Filename;
        }

        public bool IsPlaying
        {
            get
            {
                if (this.Sound == null) return false;
                else return (!this.Sound.Paused && !this.Sound.Finished);
            }
        }

        public bool IsPaused
        {
            get
            {
                if (this.Sound == null) return false;
                else return this.Sound.Paused;    
            }
        }

        public bool IsFinished
        {
            get
            {
                if (this.Sound == null) return false;
                else return this.Sound.Finished;
            }
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
