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
 * $Id: MainForm.cs 499 2011-03-22 14:54:22Z shalafiraistlin@gmail.com $
 */

using System;
using BlizzTV.Media.Player;

namespace BlizzTV.UI.Guide
{
    public partial class VideoGuide : BasePlayer
    {
        private string _movie;

        public VideoGuide(string Movie,string Title)
        {
            InitializeComponent();
            this.Text = Title;
            this._movie = Movie;
        }

        private void VideoGuide_Load(object sender, EventArgs e)
        {
            this.LoadingStarted();
            this.FlashPlayer.LoadMovie(0,_movie); 
        }

        private void FlashPlayer_OnReadyStateChange(object sender, AxShockwaveFlashObjects._IShockwaveFlashEvents_OnReadyStateChangeEvent e)
        {
            // Loading=0, Uninitialized=1, Loaded=2, Interactive=3, Complete=4.
            if (this.FlashPlayer.Visible != false || e.newState != 4) return;

            this.FlashPlayer.Visible = true;
            this.LoadingFinished();
        }
    }
}
