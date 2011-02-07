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
using System.Windows.Forms;
using BlizzTV.Log;
using BlizzTV.ModuleLib.Players;
using BlizzTV.Settings;

namespace BlizzTV.Videos
{
    public partial class frmPlayer : PlayerWindow // The video player.
    {
        private readonly Video _video; // The video. 

        public frmPlayer(Video video)
        {
            InitializeComponent();

            this.Player.DoubleClick += PlayerDoubleClick; // setup mouse handlers.
            this.Player.MouseDown += PlayerMouseDown;
            this.Player.MouseUp += PlayerMouseUp;
            this.Player.MouseMove += PlayerMouseMove;

            this.SwitchTopMostMode(GlobalSettings.Instance.PlayerWindowsAlwaysOnTop); // set the form's top-most mode.            
            this._video = video; // set the video.
            this.Width = GlobalSettings.Instance.VideoPlayerWidth; // get the default player width. 
            this.Height = GlobalSettings.Instance.VideoPlayerHeight; // get the default player height.
            this._video.Process(); // process the video so that it's template variables are replaced.
        }

        private void Player_Load(object sender, EventArgs e)
        {
            try
            {
                this.Text = string.Format("[{0}] {1}", this._video.ChannelName, this._video.Title); // set the window title.
                this.Player.LoadMovie(0,this._video.Movie); // load the movie.
            }
            catch (Exception exc)
            {
                LogManager.Instance.Write(LogMessageTypes.Error, string.Format("VideoChannelsPlugin Player Error: \n {0}", exc.ToString()));
                MessageBox.Show(string.Format("An error occured in video player. \n\n[Error Details: {0}]", exc.Message), "Video Channels Plugin Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        
        private void MenuAlwaysOnTop_Click(object sender, EventArgs e)
        {
            SwitchTopMostMode(!this.MenuAlwaysOnTop.Checked);
        }

        private void SwitchTopMostMode(bool topMost)
        {
            if (topMost)
            {
                this.TopMost = true;
                this.MenuAlwaysOnTop.Checked = true;
            }
            else
            {
                this.TopMost = false;
                this.MenuAlwaysOnTop.Checked = false;
            }
        }
    }
}
