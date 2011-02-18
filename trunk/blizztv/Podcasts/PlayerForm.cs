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
using BlizzTV.Modules.Players;

namespace BlizzTV.Podcasts
{
    public partial class PlayerForm : BasePlayerForm
    {
        private Episode _episode;

        public PlayerForm(Episode episode)
        {
            InitializeComponent();            

            this._episode = episode;

            this.LoadingCircle.Visible = false;
            this.AudioPlayer.enableContextMenu = false;
            this.AudioPlayer.DoubleClick += PlayerDoubleClick; // setup mouse handlers.
            this.AudioPlayer.MouseDown += PlayerMouseDown;
            this.AudioPlayer.MouseUp += PlayerMouseUp;
            this.AudioPlayer.MouseMove += PlayerMouseMove;

            this.SwitchNormalMode();
        }

        private void PlayerForm_Load(object sender, EventArgs e)
        {
            this.Text = string.Format("{0} - [{1}]", this._episode.Title, this._episode.PodcastName);
            this.AudioPlayer.URL = this._episode.Enclosure;
        }

        private void SwitchNormalMode()
        {
            this.AudioPlayer.uiMode = "full";
            this.AudioPlayer.Dock = DockStyle.Fill;
        }

        private void SwitchCompactMode()
        {
            this.AudioPlayer.uiMode = "none";
            this.AudioPlayer.Dock = DockStyle.None;
        }

        void AudioPlayer_DoubleClick(object sender, MouseEventArgs e)
        {

        }

        private void AudioPlayer_OpenStateChange(object sender, AxWMPLib._WMPOCXEvents_OpenStateChangeEvent e)
        {
            MediaState state = (MediaState) e.newState;
        }

        public enum MediaState
        {
            Undefined = 0,
            PlaylistChanging = 1,
            PlaylistLocating = 2,
            PlaylistConnecting = 3,
            PlaylistLoading = 4,
            PlaylistOpening = 5,
            PlaylistOpenNoMedia = 6,
            PlaylistChanged = 7,
            MediaChanging = 8,
            MediaLocating = 9,
            MediaConnecting = 10,
            MediaLoading = 11,
            MediaOpening = 12,
            MediaOpen = 13,
            BeginCodecAcquisition = 14,
            EndCodecAcquisition = 15,
            BeginLicenseAcquisition = 16,
            EndLicenseAcquisition = 17,
            BeginIndividualization = 18,
            EndIndividualization = 19,
            MediaWaiting = 20,
            OpeningUnknownUrl = 21 
        }
    }
}
