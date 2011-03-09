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
using System.Windows.Forms;
using BlizzTV.Log;

namespace BlizzTV.EmbeddedModules.Streams.UI
{
    public partial class ChatForm : Form
    {
        private readonly Stream _stream; 
        private readonly PlayerForm _parent;
        private bool _snapParent = false;
        private bool _borderless = false;
        private bool _dragging = false;
        private Point _dragOffset;

        public ChatForm(PlayerForm parent, Stream stream)
        {
            InitializeComponent();

            this.Chat.DoubleClick += ChatDoubleClick; // setup mouse handlers.
            this.Chat.MouseDown += ChatMouseDown;
            this.Chat.MouseUp += ChatMouseUp;
            this.Chat.MouseMove += ChatMouseMove;

            this._stream = stream; // set the stream.
            this._parent = parent;
            this._parent.Move += OnParentMove;
            this._parent.Resize += OnParentResize;
            this._parent.FormClosed += OnParentClose;

            this.Width = EmbeddedModules.Streams.Settings.ModuleSettings.Instance.ChatWindowWidth;
            this.Height = EmbeddedModules.Streams.Settings.ModuleSettings.Instance.ChatWindowHeight;
        }

        private void ChatForm_Load(object sender, EventArgs e)
        {
            try
            {
                this._snapParent = true;
                this.SnapToParent();
                this.Move += ChatForm_Move;
                this.Text = string.Format("Chat: {0}", this._stream.Name); // set the window title.
                this.Chat.LoadMovie(0, string.Format("{0}", this._stream.ChatMovie)); // load the movie.
            }
            catch (Exception exc)
            {
                LogManager.Instance.Write(LogMessageTypes.Error, string.Format("StreamsPlugin Chat Window Error: \n {0}", exc));
                MessageBox.Show(string.Format("An error occured in stream chat window. \n\n[Error Details: {0}]", exc.Message), "Streams Plugin Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ChatDoubleClick(object sender, MouseEventArgs e)
        {
            this._dragging = false;
            if (this._borderless) { this.FormBorderStyle = FormBorderStyle.Sizable; this._borderless = false; }
            else { this.FormBorderStyle = FormBorderStyle.None; this._borderless = true; }
            if (this._snapParent) this.SnapToParent();
        }

        private void ChatMouseDown(object sender, MouseEventArgs e)
        {
            if (this._borderless)
            {
                this._dragOffset = new Point(e.X - this.Location.X, e.Y - this.Location.Y);
                this.Cursor = Cursors.SizeAll;
                this._dragging = true;
            }
        }

        private void ChatMouseUp(object sender, MouseEventArgs e)
        {
            this._dragging = false;
            this.Cursor = Cursors.Default;
        }

        private void ChatMouseMove(object sender, MouseEventArgs e)
        {
            if (this._borderless && this._dragging) this.Location = new System.Drawing.Point(e.X - this._dragOffset.X, e.Y - this._dragOffset.Y);
        }

        void OnParentClose(object sender, FormClosedEventArgs e)
        {
            this.Close();
        }

        void OnParentResize(object sender, EventArgs e)
        {
            if(this._snapParent) this.SnapToParent();
        }

        private void OnParentMove(object sender, EventArgs e)
        {
            if (this._snapParent)  this.SnapToParent();
        }

        private void SnapToParent()
        {
            this.Left = this._parent.Left + this._parent.Width + 2;
            this.Top = this._parent.Top;
            if (this.Height != this._parent.Height) this.Height = this._parent.Height;
        }

        private void ChatForm_Move(object sender, EventArgs e)
        {
            int margin = Math.Abs(this.Left - (this._parent.Left + this._parent.Width));
            if (margin <= 15)
            {
                this._snapParent = true;
                this.SnapToParent();
            }
            else this._snapParent = false;
        }
    }
}
