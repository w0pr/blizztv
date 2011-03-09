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
using System.Drawing;
using System.Timers;
using BlizzTV.Media.Player;
using BlizzTV.Settings;
using System.Windows.Forms;
using BlizzTV.Utility.Extensions;
using Timer = System.Timers.Timer;

namespace BlizzTV.EmbeddedModules.Podcasts.UI
{
    public partial class PlayerForm : BasePlayer
    {
        private readonly Episode _episode;
        private WindowMode _windowMode = WindowMode.Normal;
        private MuteControl _muteControl = MuteControl.Unmuted;
        private int _lastNormalModeHeight = 0;
        private const int CompactModeHeight = 18;

        private bool _needSliding = false;
        private string _sliderText;
        private const int SliderSpeed = 125;
        private SliderDiretion _sliderDirection = SliderDiretion.Left;
        private int _sliderPosition = 0;
        private Timer _sliderTimer;
        private Timer _positionTimer;

        public PlayerForm(Episode episode)
        {
            InitializeComponent();

            this.MouseCoordinateSystem = CoordinateSystem.Relative;
            this.Size = new Size(EmbeddedModules.Podcasts.Settings.ModuleSettings.Instance.PlayerWidth, EmbeddedModules.Podcasts.Settings.ModuleSettings.Instance.PlayerHeight); // set the window size based on last saved values.
            this.SwitchTopMostMode(GlobalSettings.Instance.PlayerWindowsAlwaysOnTop); // set the form's top-most mode.            

            this._episode = episode;

            this.gradientPanel.DoubleClick += SwitchMode;          
            this.gradientPanel.MouseDown += FormDragStart;
            this.gradientPanel.MouseUp += FormDragEnd;
            this.gradientPanel.MouseMove += FormDrag;

            this.MediaPlayer.DoubleClick += SwitchMode;

            this.labelPosition.DoubleClick += SwitchMode;
            this.labelPosition.MouseDown += FormDragStart;
            this.labelPosition.MouseUp += FormDragEnd;
            this.labelPosition.MouseMove += FormDrag;
            this.labelPosition.DoubleBuffer();

            this.LabelSlider.DoubleClick += SwitchMode;   
            this.LabelSlider.MouseDown += FormDragStart;
            this.LabelSlider.MouseUp += FormDragEnd;
            this.LabelSlider.MouseMove += FormDrag;
            this.LabelSlider.DoubleBuffer();

            this.SwitchNormalMode();
        }

        private void PlayerForm_Load(object sender, EventArgs e)
        {
            this.Text = string.Format("{0} - [{1}]", this._episode.Title, this._episode.PodcastName);         
            this.MediaPlayer.URL = this._episode.MediaLocation;
        }

        private void PlayerForm_ResizeEnd(object sender, EventArgs e)
        {
            if (this.Size.Width == EmbeddedModules.Podcasts.Settings.ModuleSettings.Instance.PlayerWidth && this.Size.Height == EmbeddedModules.Podcasts.Settings.ModuleSettings.Instance.PlayerHeight) return;

            EmbeddedModules.Podcasts.Settings.ModuleSettings.Instance.PlayerWidth = this.Size.Width;
            EmbeddedModules.Podcasts.Settings.ModuleSettings.Instance.PlayerHeight = this.Size.Height;
            EmbeddedModules.Podcasts.Settings.ModuleSettings.Instance.Save();

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

        private void MenuAlwaysOnTop_Click(object sender, EventArgs e)
        {
            SwitchTopMostMode(!this.MenuAlwaysOnTop.Checked);
        }

        private void SwitchNormalMode()
        {
            this._windowMode = WindowMode.Normal;
            if(this._lastNormalModeHeight!=0) this.Height = this._lastNormalModeHeight;
            this.gradientPanel.Visible = false;
            this.MediaPlayer.uiMode = "full";
            this.MediaPlayer.Dock = DockStyle.Fill;
            this.LabelSlider.Visible = false;
            this.pictureMuteControl.Visible = false;
            this.DisposeSliderTimer();
            this.DisposePositionTimer();
        }

        private void SwitchCompactMode()
        {
            this._windowMode = WindowMode.Compact;
            this._lastNormalModeHeight = this.Height;
            this.Height = CompactModeHeight;

            this.gradientPanel.Visible = true;

            this.MediaPlayer.uiMode = "none";
            this.MediaPlayer.Dock = DockStyle.None;
            this.MediaPlayer.Left = 20;
            this.MediaPlayer.Top = 1;
            this.MediaPlayer.Width = 50;
            this.MediaPlayer.Height = 16;
            this.MediaPlayer.BringToFront();

            this.labelPosition.Left = 71;
            this.labelPosition.Visible = true;

            this.LabelSlider.Left = 116;
            this.LabelSlider.Width = this.Width - 117;
            this.LabelSlider.Visible = true;
            this.LabelSlider.Height = 16;
            this.LabelSlider.Top = 1;
            this.SetUpSliderTimer();

            this.SetupPositionTimer();

            this.pictureMuteControl.Visible = true;
        }
        
        private void SetUpSliderTimer()
        {
            this.ResetSlider();

            this._sliderTimer = new Timer(SliderSpeed);
            this._sliderTimer.Elapsed += OnSliderTimerHit;
            this._sliderTimer.Enabled = true;
        }

        private void ResetSlider()
        {
            this._sliderPosition = 0;
            this._sliderDirection = SliderDiretion.Left;   
        }
        

        private void SetSliderText(string text)
        {
            this._sliderText = text;

            using(Graphics g=CreateGraphics())
            {
                var labelSize = g.MeasureString(text, LabelSlider.Font);
                this._needSliding = labelSize.Width > LabelSlider.Width;
            }

            this.ResetSlider();
        }

        private void SetupPositionTimer()
        {
            this._positionTimer = new Timer(1000);
            this._positionTimer.Elapsed += OnPositionTimerHit;
            this._positionTimer.Enabled = true;
            this.OnPositionTimerHit(this, null);
        }

        private void DisposeSliderTimer()
        {
            if (this._sliderTimer == null) return;

            this._sliderTimer.Enabled = false;
            this._sliderTimer.Elapsed -= OnSliderTimerHit;
            this._sliderTimer = null;
        }

        private void DisposePositionTimer()
        {
            if (this._positionTimer == null) return;

            this._positionTimer.Enabled = false;
            this._positionTimer.Elapsed -= OnPositionTimerHit;
            this._positionTimer = null;
        }

        private void OnSliderTimerHit(object source, ElapsedEventArgs e)
        {
            if (string.IsNullOrEmpty(this._sliderText)) return;

            this.LabelSlider.AsyncInvokeHandler(() =>
            {
                if (!this._needSliding)
                {
                    this.LabelSlider.Text = this._sliderText; 
                    return;
                }

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

        void OnPositionTimerHit(object sender, ElapsedEventArgs e)
        {
            this.labelPosition.AsyncInvokeHandler(() =>
            {
                var currentPosition = TimeSpan.FromSeconds(this.MediaPlayer.Ctlcontrols.currentPosition);
                this.labelPosition.Text = string.Format("{0:D2}:{1:D2}:{2:D2}", currentPosition.Hours, currentPosition.Minutes, currentPosition.Seconds);
            });
        }


        private void SwitchMode(object sender, MouseEventArgs e)
        {
            this.SwitchMode();
        }

        private void SwitchMode(object sender,EventArgs e)
        {
            this.SwitchMode();
        }

        private void SwitchMode()
        {
            switch (this._windowMode)
            {
                case WindowMode.Normal:
                    this.SwitchBorderlessMode(this, EventArgs.Empty);
                    this.SwitchCompactMode();
                    break;
                case WindowMode.Compact:
                    this.SwitchNormalMode();
                    this.SwitchBorderlessMode(this, EventArgs.Empty);
                    break;
            }
        }

        private void pictureMuteControl_Click(object sender, EventArgs e)
        {
            switch (this._muteControl)
            {
                case MuteControl.Unmuted:
                    this._muteControl = MuteControl.Muted;
                    this.pictureMuteControl.Image = Assets.Images.Icons.Png._16.podcast_muted;
                    this.MediaPlayer.settings.mute = true;
                    break;
                case MuteControl.Muted:
                    this._muteControl = MuteControl.Unmuted;
                    this.pictureMuteControl.Image = Assets.Images.Icons.Png._16.podcast_unmuted;
                    this.MediaPlayer.settings.mute = false;
                    break;
            }
        }

        private void MediaPlayer_PlayStateChange(object sender, AxWMPLib._WMPOCXEvents_PlayStateChangeEvent e)
        {
            var playState = (PlayState) e.newState;
            switch (playState)
            {
                case PlayState.Buffering:
                    this.SetSliderText("Buffering..");            
                    break;
                case PlayState.MediaEnded:
                    this.SetSliderText("End of playback.");
                    break;
                case PlayState.Paused:
                    this.SetSliderText(string.Format("     {0} [paused]    ", this._episode.Title));
                    break;
                case PlayState.Reconnecting:
                    this.SetSliderText("Reconnecting..");
                    break;
                case PlayState.ScanForward:
                    this.SetSliderText("Scanning forward..");
                    break;
                case PlayState.ScanReserve:
                    this.SetSliderText("Scanning backward..");
                    break;
                case PlayState.Stopped:
                    this.SetSliderText(string.Format("     {0} [stopped]    ", this._episode.Title));
                    break;
                case PlayState.Transitioning:
                    this.SetSliderText("Connecting..");
                    break;
                case PlayState.Playing:
                    this.SetSliderText(string.Format("     {0}     ", this._episode.Title));
                    break;
                case PlayState.Waiting:
                    this.SetSliderText("Waiting for data..");
                    break;
            }
        }
        
        /* http://msdn.microsoft.com/en-us/library/dd564881(v=VS.85).aspx */
        private enum PlayState
        {
            Undefined = 0,
            Stopped = 1,
            Paused = 2,
            Playing = 3,
            ScanForward = 4,
            ScanReserve = 5,
            Buffering = 6,
            Waiting = 7,
            MediaEnded = 8,
            Transitioning = 9,
            Ready = 10,
            Reconnecting = 11
        }

        private enum SliderDiretion
        {
            Left,
            Right
        }

        private enum WindowMode
        {
            Normal,
            Compact
        }

        private enum MuteControl
        {
            Unmuted,
            Muted
        }
    }
}
