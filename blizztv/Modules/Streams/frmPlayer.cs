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
using BlizzTV.CommonLib.Settings;
using BlizzTV.ModuleLib.Players;

namespace BlizzTV.Modules.Streams
{
    public partial class frmPlayer : Form // The stream player.
    {
        private Stream _stream; // the stream.

        public frmPlayer(Stream Stream)
        {            
            InitializeComponent();

            this._stream = Stream; // set the stream.
            this.SwitchTopMostMode(GlobalSettings.Instance.PlayerWindowsAlwaysOnTop); // set the form's top-most mode.                        
            this.Width = GlobalSettings.Instance.VideoPlayerWidth; // get the default player width.
            this.Height = GlobalSettings.Instance.VideoPlayerHeight; // get the default player height.
            this._stream.Process(); // process the stream so that it's template variables are replaced.

            if (!(Providers.Instance.Dictionary[this._stream.Provider] as StreamProvider).ChatAvailable) this.MenuOpenChat.Enabled = false; 
        }

        private void Player_Load(object sender, EventArgs e) 
        {
            try
            {
                this.Text = string.Format("Stream: {0}@{1}", this._stream.Name, this._stream.Provider); // set the window title.
                this.Player.FlashVars = this._stream.FlashVars; // set the flashvars.
                this.Player.LoadMovie(0, string.Format("{0}?{1}", this._stream.Movie, this._stream.FlashVars)); // load the movie.

                if (this._stream.ChatAvailable && Settings.Instance.AutomaticallyOpenChatForAvailableStreams) this.OpenChatWindow();
            }
            catch (Exception exc)
            {
                Log.Instance.Write(LogMessageTypes.ERROR, string.Format("StreamsPlugin Player Error: \n {0}", exc.ToString()));
                System.Windows.Forms.MessageBox.Show(string.Format("An error occured in stream player. \n\n[Error Details: {0}]", exc.Message), "Streams Plugin Error", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
            }
        }

        private void MenuAlwaysOnTop_Click(object sender, EventArgs e)
        {
            SwitchTopMostMode(!this.MenuAlwaysOnTop.Checked);
        }

        private void MenuOpenChat_Click(object sender, EventArgs e)
        {
            this.OpenChatWindow();
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

        private void OpenChatWindow()
        {
            frmChat f = new frmChat(this._stream);
            f.Show();
        }
    }
}
