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
using System.Windows.Forms;
using System.Drawing;
using BlizzTV.Log;
using BlizzTV.Media.Player;
using BlizzTV.Settings;

namespace BlizzTV.EmbeddedModules.Videos.UI
{
    public partial class PlayerForm : BasePlayer // The video player.
    {
        private readonly Video _video; // The video. 

        public PlayerForm(Video video)
        {
            InitializeComponent();

            this.FlashPlayer.DoubleClick += SwitchBorderlessMode; // setup mouse handlers.
            this.FlashPlayer.MouseDown += FormDragStart;
            this.FlashPlayer.MouseUp += FormDragEnd;
            this.FlashPlayer.MouseMove += FormDrag;

            this.SwitchTopMostMode(GlobalSettings.Instance.PlayerWindowsAlwaysOnTop); // set the form's top-most mode.            
            this._video = video; // set the video.
            this.Size = new Size(EmbeddedModules.Videos.Settings.ModuleSettings.Instance.PlayerWidth, EmbeddedModules.Videos.Settings.ModuleSettings.Instance.PlayerHeight); // set the window size based on last saved values.
            this._video.Process(); // process the video so that it's template variables are replaced.
        }

        private void PlayerForm_Load(object sender, EventArgs e)
        {
            try
            {
                this.LoadingStarted();
                this.Text = string.Format("[{0}] {1}", this._video.ChannelName, this._video.Title); // set the window title.
                this.FlashPlayer.LoadMovie(0,this._video.Movie); // load the movie.
            }
            catch (Exception exc)
            {
                LogManager.Instance.Write(LogMessageTypes.Error, string.Format("VideoChannelsPlugin Player Error: \n {0}", exc.ToString()));
                MessageBox.Show(string.Format("An error occured in video player. \n\n[Error Details: {0}]", exc.Message), "Video Channels Plugin Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void FlashPlayer_OnReadyStateChange(object sender, AxShockwaveFlashObjects._IShockwaveFlashEvents_OnReadyStateChangeEvent e)
        {
            // Loading=0, Uninitialized=1, Loaded=2, Interactive=3, Complete=4.
            if (this.FlashPlayer.Visible != false || e.newState != 4) return;

            this.FlashPlayer.Visible = true;
            this.LoadingFinished();
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

        private void PlayerForm_ResizeEnd(object sender, EventArgs e)
        {
            if (this.Size.Width == EmbeddedModules.Videos.Settings.ModuleSettings.Instance.PlayerWidth && this.Size.Height == EmbeddedModules.Videos.Settings.ModuleSettings.Instance.PlayerHeight) return;

            EmbeddedModules.Videos.Settings.ModuleSettings.Instance.PlayerWidth = this.Size.Width;
            EmbeddedModules.Videos.Settings.ModuleSettings.Instance.PlayerHeight = this.Size.Height;
            EmbeddedModules.Videos.Settings.ModuleSettings.Instance.Save();
        }
    }
}
