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
using BlizzTV.CommonLib.Logger;

namespace BlizzTV.Modules.Streams
{
    public partial class frmChat : Form
    {
        private readonly Stream _stream; // the stream.
        private readonly frmPlayer _parent;
        private bool _snapParent = false;

        public frmChat(frmPlayer parent, Stream stream)
        {
            InitializeComponent();

            this._stream = stream; // set the stream.
            this._parent = parent;
            this._parent.Move += OnParentMove;
            this._parent.Resize += OnParentResize;
            this._parent.FormClosed += OnParentClose;

            this.Width = Settings.Instance.StreamChatWindowWidth;
            this.Height = Settings.Instance.StreamChatWindowHeight;
        }

        private void frmChat_Load(object sender, EventArgs e)
        {
            try
            {
                this._snapParent = true;
                this.SnapToParent();
                this.Move += frmChat_Move;
                this.Text = string.Format("Chat: {0}@{1}", this._stream.Name, this._stream.Provider); // set the window title.
                this.Chat.LoadMovie(0, string.Format("{0}", this._stream.ChatMovie)); // load the movie.
            }
            catch (Exception exc)
            {
                Log.Instance.Write(LogMessageTypes.Error, string.Format("StreamsPlugin Chat Window Error: \n {0}", exc));
                MessageBox.Show(string.Format("An error occured in stream chat window. \n\n[Error Details: {0}]", exc.Message), "Streams Plugin Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
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

        private void frmChat_Move(object sender, EventArgs e)
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
