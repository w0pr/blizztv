﻿/*    
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
 */

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace LibVideoChannels
{
    public partial class Player : Form
    {
        private Video _video;

        public Player(Video Video)
        {
            InitializeComponent();
            this._video = Video;
            this.Width = VideoChannelsPlugin.GlobalSettings.VideoPlayerWidth;
            this.Height = VideoChannelsPlugin.GlobalSettings.VideoPlayerHeight;
            this._video.Process();
        }

        private void Player_Load(object sender, EventArgs e)
        {
            this.Text = string.Format("Video Channel: {0}@{1}", this._video.Title, this._video.Provider);
            this.Stage.FlashVars = this._video.FlashVars;
            this.Stage.LoadMovie(0, string.Format("{0}?{1}", this._video.Movie, this._video.FlashVars));
        }
    }
}