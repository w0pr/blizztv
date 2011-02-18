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
using System.Timers;
using Timer = System.Timers.Timer;
using System.Windows.Forms;
using BlizzTV.Modules.Players;
using BlizzTV.Utility.Extensions;

namespace BlizzTV.Podcasts
{
    public partial class PlayerForm : BasePlayerForm
    {
        private Episode _episode;
        private WindowMode _windowMode = WindowMode.Normal;        
        private int _lastNormalModeHeight = 0;
        private const int CompactModeHeight=20;

        private const int SliderSpeed = 125;
        private string _sliderText;
        private SliderDiretion _sliderDirection = SliderDiretion.Left;
        private int _sliderPosition = 0;
        private Timer _sliderTimer;

        public PlayerForm(Episode episode)
        {
            InitializeComponent();            

            this._episode = episode;

            this.LoadingCircle.Visible = false;

            this.DoubleClick += PlayerDoubleClick;
            this.DoubleClick += ModeDoubleClickHandler;
            this.MouseDown += PlayerMouseDown;
            this.MouseUp += PlayerMouseUp;
            this.MouseMove += PlayerMouseMove;

            this.AudioPlayer.enableContextMenu = false;
            this.AudioPlayer.DoubleClick += PlayerDoubleClick;
            this.AudioPlayer.DoubleClick += ModeDoubleClickHandler;

            this.LabelSlider.DoubleClick += PlayerDoubleClick;
            this.LabelSlider.DoubleClick += ModeDoubleClickHandler;
            this.LabelSlider.MouseDown += PlayerMouseDown;
            this.LabelSlider.MouseUp += PlayerMouseUp;
            this.LabelSlider.MouseMove += PlayerMouseMove;
            this.LabelSlider.DoubleBuffer();

            this.SwitchNormalMode();
        }

        private void PlayerForm_Load(object sender, EventArgs e)
        {
            this.Text = string.Format("{0} - [{1}]", this._episode.Title, this._episode.PodcastName);
            this._sliderText = string.Format("     {0} - [{1}]     ", this._episode.Title, this._episode.PodcastName);            
            this.AudioPlayer.URL = this._episode.Enclosure;
        }

        private void SwitchNormalMode()
        {
            this._windowMode = WindowMode.Normal;
            if(this._lastNormalModeHeight!=0) this.Height = this._lastNormalModeHeight;
            this.AudioPlayer.uiMode = "full";
            this.AudioPlayer.Dock = DockStyle.Fill;
            this.LabelSlider.Visible = false;
            this.pictureSlider.Visible = false;
            this.DisposeSliderTimer();
        }

        private void SwitchCompactMode()
        {
            this._windowMode = WindowMode.Compact;
            this._lastNormalModeHeight = this.Height;
            this.Height = CompactModeHeight;
            
            
            this.AudioPlayer.uiMode = "none";            
            this.AudioPlayer.Dock = DockStyle.None;
            this.AudioPlayer.Left = 170;
            this.AudioPlayer.Width = 50;

            this.LabelSlider.Left = 20;
            this.LabelSlider.Visible = true;
            this.SetUpSliderTimer();

            this.pictureSlider.Visible = true;
        }
        
        private void SetUpSliderTimer()
        {
            this._sliderPosition = 0;
            this._sliderDirection = SliderDiretion.Left;

            this._sliderTimer = new Timer(SliderSpeed);
            this._sliderTimer.Elapsed += OnSliderTimerHit;
            this._sliderTimer.Enabled = true;
        }

        private void DisposeSliderTimer()
        {
            if (this._sliderTimer == null) return;

            this._sliderTimer.Enabled = false;
            this._sliderTimer.Elapsed -= OnSliderTimerHit;
            this._sliderTimer = null;
        }

        private void OnSliderTimerHit(object source, ElapsedEventArgs e)
        {
            this.LabelSlider.AsyncInvokeHandler(() =>
            {
                this.LabelSlider.Text = this._sliderText.Substring(this._sliderPosition);

                switch (this._sliderDirection)
                {
                    case SliderDiretion.Left:
                        this._sliderPosition++;
                        break;
                    case SliderDiretion.Right:
                        this._sliderPosition--;
                        break;
                }

                if (this._sliderPosition >= this._sliderText.Length || this._sliderPosition <= 0)
                {
                    switch (this._sliderDirection)
                    {
                        case SliderDiretion.Left:
                            this._sliderDirection = SliderDiretion.Right;
                            this._sliderPosition = this._sliderText.Length;
                            break;
                        case SliderDiretion.Right:
                            this._sliderDirection = SliderDiretion.Left;
                            this._sliderPosition = 0;
                            break;
                    }
                }
            });
        }

        private void ModeDoubleClickHandler(object sender, MouseEventArgs e)
        {
            switch (this._windowMode)
            {
                case WindowMode.Normal:
                    this.SwitchCompactMode();
                    break;
                case WindowMode.Compact:
                    this.SwitchNormalMode();
                    break;
            }
        }

        private void ModeDoubleClickHandler(object sender,EventArgs e)
        {
            switch (this._windowMode)
            {
                case WindowMode.Normal:
                    this.SwitchCompactMode();
                    break;
                case WindowMode.Compact:
                    this.SwitchNormalMode();
                    break;
            }   
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

        public enum SliderDiretion
        {
            Left,
            Right
        }

        public enum WindowMode
        {
            Normal,
            Compact
        }
    }
}
