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

namespace BlizzTV.Audio.Engines
{
    /// <summary>
    /// Audio track interface.
    /// </summary>
    public interface IAudioTrack
    {
        /// <summary>
        /// Is the track being played?
        /// </summary>
        bool IsPlaying { get; }

        /// <summary>
        /// Is the track currently paused?
        /// </summary>
        bool IsPaused { get; }

        /// <summary>
        /// Is the track finished playing?
        /// </summary>
        bool IsFinished { get; }

        /// <summary>
        /// Duration of the track.
        /// </summary>
        int Duration { get; }

        /// <summary>
        /// Current position in track.
        /// </summary>
        int Position { get; }
    }
}
