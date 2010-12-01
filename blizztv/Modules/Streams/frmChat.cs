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
 * $Id: frmChat.cs 158 2010-11-30 14:06:50Z shalafiraistlin@gmail.com $
 */

using System;
using System.Windows.Forms;
using BlizzTV.ModuleLib.Players;
using BlizzTV.ModuleLib.Utils;

namespace BlizzTV.Modules.Streams
{
    public partial class frmChat : Form
    {
        private Stream _stream; // the stream.

        public frmChat(Stream Stream)
        {
            InitializeComponent();

            this._stream = Stream; // set the stream.
            this.Width = Settings.Instance.StreamChatWindowWidth;
            this.Height = Settings.Instance.StreamChatWindowHeight;
        }

        private void frmChat_Load(object sender, EventArgs e)
        {
            try
            {
                this.Text = string.Format("Chat: {0}@{1}", this._stream.Name, this._stream.Provider); // set the window title.
                this.Chat.LoadMovie(0, string.Format("{0}", this._stream.ChatMovie)); // load the movie.
            }
            catch (Exception exc)
            {
                Log.Instance.Write(LogMessageTypes.ERROR, string.Format("StreamsPlugin Chat Window Error: \n {0}", exc.ToString()));
                System.Windows.Forms.MessageBox.Show(string.Format("An error occured in stream chat window. \n\n[Error Details: {0}]", exc.Message), "Streams Plugin Error", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
            }
        }
    }
}
