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

using IrrKlang;

namespace BlizzTV.Audio.Engines.IrrKlang
{
    public class IrrKlangEngine:AudioEngine
    {
        private readonly ISoundEngine _engine; 

        public IrrKlangEngine()
        {
            this._engine = new ISoundEngine(); // startup IrrKlang's internal sound engine.
            this._engine.AddFileFactory(new FileFactory()); // setup our own file factory so can add the support for http streaming.
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
            ISound sound = this._engine.Play2D(url); // just call the Play2D function the URL supplied as our custom-implemented FileFactory will be catching the http stream request.
            this.CurrentTrack = new IrrKlangTrack(sound, url);
        }

        public override void PlayFromMemory(string name, byte[] data)
        {
            ISoundSource source = this._engine.AddSoundSourceFromMemory(data, name); // create the named sound source.
            ISound sound = this._engine.Play2D(name); // start playing the track with using the name of sound source.
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
            if (!this.IsPlaying) return;
            ((IrrKlangTrack) this.CurrentTrack).Sound.Stop();
            this._engine.StopAllSounds();
            this._engine.RemoveSoundSource(((IrrKlangTrack)this.CurrentTrack).Filename); // remove the current tracks sound source.
        }

        public override void StopAll()
        {
            if (this.IsPlaying) ((IrrKlangTrack) this.CurrentTrack).Sound.Stop();
            this._engine.StopAllSounds();
            this._engine.RemoveAllSoundSources(); // remove all sound sources.
        }

        public override void SetPosition(int position)
        {
            if (this.CurrentTrack != null) ((IrrKlangTrack) this.CurrentTrack).Sound.PlayPosition = (uint)position;
        }

        public override void SetVolume(double volume)
        {
            if (this.CurrentTrack != null) ((IrrKlangTrack) this.CurrentTrack).Sound.Volume = (float)volume;
        }
    }
}
