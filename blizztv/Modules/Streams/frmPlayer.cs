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
using System.Drawing;
using BlizzTV.CommonLib.Logger;
using BlizzTV.CommonLib.Settings;

namespace BlizzTV.Modules.Streams
{
    public partial class frmPlayer : Form // The stream player.
    {
        private readonly Stream _stream; // the stream.
        private frmChat _chatWindow = null;
        private bool _borderless = false;
        private bool _dragging = false;
        private Point _drag_offset;

        public frmPlayer(Stream stream)
        {            
            InitializeComponent();

            this.Player.DoubleClick += PlayerDoubleClick; // setup mouse handlers.
            this.Player.MouseDown += PlayerMouseDown;
            this.Player.MouseUp += PlayerMouseUp;
            this.Player.MouseMove += PlayerMouseMove;

            this._stream = stream; // set the stream.
            this.SwitchTopMostMode(GlobalSettings.Instance.PlayerWindowsAlwaysOnTop); // set the form's top-most mode.                        
            this.Width = GlobalSettings.Instance.VideoPlayerWidth; // get the default player width.
            this.Height = GlobalSettings.Instance.VideoPlayerHeight; // get the default player height.
            this._stream.Process(); // process the stream so that it's template variables are replaced.

            if (!((StreamProvider) Providers.Instance.Dictionary[this._stream.Provider]).ChatAvailable) this.MenuOpenChat.Enabled = false; 
        }

        private void Player_Load(object sender, EventArgs e) 
        {
            try
            {
                this.Text = string.Format("Stream: {0}@{1}", this._stream.Name, this._stream.Provider); // set the window title.
                this.Player.LoadMovie(0, this._stream.Movie); // load the movie.

                if (this._stream.ChatAvailable && Settings.Instance.AutomaticallyOpenChat) this.OpenChatWindow();
            }
            catch (Exception exc)
            {
                Log.Instance.Write(LogMessageTypes.Error, string.Format("StreamsPlugin Player Error: \n {0}", exc));
                MessageBox.Show(string.Format("An error occured in stream player. \n\n[Error Details: {0}]", exc.Message), "Streams Plugin Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void PlayerDoubleClick(object sender, MouseEventArgs e)
        {
            this._dragging = false;
            if (this._borderless) { this.FormBorderStyle = FormBorderStyle.Sizable; this._borderless = false; }
            else { this.FormBorderStyle = FormBorderStyle.None; this._borderless = true; }
        }

        private void PlayerMouseDown(object sender, MouseEventArgs e)
        {
            if (this._borderless)
            {
                this._drag_offset = new Point(e.X - this.Location.X, e.Y - this.Location.Y);
                this.Cursor = Cursors.SizeAll;
                this._dragging = true;
            }
        }

        private void PlayerMouseUp(object sender, MouseEventArgs e)
        {
            this._dragging = false;
            this.Cursor = Cursors.Default;
        }

        private void PlayerMouseMove(object sender, MouseEventArgs e)
        {
            if (this._borderless && this._dragging) this.Location = new System.Drawing.Point(e.X - this._drag_offset.X, e.Y - this._drag_offset.Y);
        }

        private void MenuAlwaysOnTop_Click(object sender, EventArgs e)
        {
            SwitchTopMostMode(!this.MenuAlwaysOnTop.Checked);
        }

        private void SwitchTopMostMode(bool TopMost)
        {
            if (TopMost)
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

        private void MenuOpenChat_Click(object sender, EventArgs e)
        {
            this.OpenChatWindow();
        }

        private void OpenChatWindow()
        {
            if (this._chatWindow == null)
            {
                this._chatWindow = new frmChat(this,this._stream);
                this._chatWindow.FormClosed += ChatWindowClosed;
                this._chatWindow.Show();
            }
            else this._chatWindow.Focus();
        }

        void ChatWindowClosed(object sender, FormClosedEventArgs e)
        {
            this._chatWindow = null;
        }
    }
}
