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
 */

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using LibBlizzTV;
using LibBlizzTV.Utils;

namespace LibVideoChannels
{
    public partial class Player : Form // The video player.
    {
        private Video _video; // The video. 

        public Player(Video Video)
        {
            InitializeComponent();

            this._video = Video; // set the video.
            this.Width = Plugin.Instance.GlobalSettings.VideoPlayerWidth; // get the default player width. 
            this.Height = Plugin.Instance.GlobalSettings.VideoPlayerHeight; // get the default player height.
            this._video.Process(); // process the video so that it's template variables are replaced.
        }

        private void Player_Load(object sender, EventArgs e)
        {
            try
            {
                this.Text = string.Format("Video Channel: {0}@{1}", this._video.Title, this._video.Provider); // set the window title.
                this.Stage.FlashVars = this._video.FlashVars; // set the flashvars.
                this.Stage.LoadMovie(0, string.Format("{0}?{1}", this._video.Movie, this._video.FlashVars)); // load the movie.
            }
            catch (Exception exc)
            {
                Log.Instance.Write(LogMessageTypes.ERROR, string.Format("VideoChannelsPlugin Player Error: \n {0}", exc.ToString()));
                System.Windows.Forms.MessageBox.Show(string.Format("An error occured in video player. \n\n[Error Details: {0}]", exc.Message), "Video Channels Plugin Error", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
            }
        }
    }
}
