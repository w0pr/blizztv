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

namespace BlizzTV.EmbeddedModules.Streams.UI
{
    public partial class PlayerForm : BasePlayer // The stream player.
    {
        private readonly Stream _stream; // the stream.
        private ChatForm _chatWindow = null;

        public PlayerForm(Stream stream)
        {            
            InitializeComponent();
            
            this.SwitchTopMostMode(GlobalSettings.Instance.PlayerWindowsAlwaysOnTop); // set the form's top-most mode.                        
            this.Size = new Size(EmbeddedModules.Streams.Settings.ModuleSettings.Instance.PlayerWidth, EmbeddedModules.Streams.Settings.ModuleSettings.Instance.PlayerHeight); // set the window size based on last saved values.

            this._stream = stream; // set the stream.
            this._stream.Process(); // process the stream so that it's template variables are replaced.

            if (((StreamProvider) Providers.Instance.Dictionary[this._stream.Provider]).Player == "Flash") this.SetupFlashPlayer();
            else if (((StreamProvider) Providers.Instance.Dictionary[this._stream.Provider]).Player == "Browser") this.SetupBrowserPlayer();

            if (!((StreamProvider) Providers.Instance.Dictionary[this._stream.Provider]).ChatAvailable) this.MenuOpenChat.Enabled = false; 
        }

        private void PlayerForm_Load(object sender, EventArgs e) 
        {
            try
            {
                this.LoadingStarted();
                this.Text = string.Format("Stream: {0}", this._stream.Name); // set the window title.                
                if (this._stream.Provider == "JustinTV") this.WebBrowser.Navigate(string.Format("http://service.blizztv.com/stream/embed/{0}/{1}", this._stream.Provider, this._stream.Slug));
                else this.FlashPlayer.LoadMovie(0, this._stream.Movie); // load the movie.

                if (this._stream.ChatAvailable && EmbeddedModules.Streams.Settings.ModuleSettings.Instance.AutomaticallyOpenChat) this.OpenChatWindow();
            }
            catch (Exception exc)
            {
                LogManager.Instance.Write(LogMessageTypes.Error, string.Format("StreamsPlugin Player Error: \n {0}", exc));
                MessageBox.Show(string.Format("An error occured in stream player. \n\n[Error Details: {0}]", exc.Message), "Streams Plugin Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void SetupFlashPlayer()
        {
            this.FlashPlayer.DoubleClick += SwitchBorderlessMode; // setup mouse handlers.
            this.FlashPlayer.MouseDown += FormDragStart;
            this.FlashPlayer.MouseUp += FormDragEnd;
            this.FlashPlayer.MouseMove += FormDrag;
            this.FlashPlayer.Dock = DockStyle.Fill;
        }

        private void FlashPlayer_OnReadyStateChange(object sender, AxShockwaveFlashObjects._IShockwaveFlashEvents_OnReadyStateChangeEvent e)
        {
            // Loading=0, Uninitialized=1, Loaded=2, Interactive=3, Complete=4.
            if (this.FlashPlayer.Visible == false && e.newState == 4)
            {
                this.FlashPlayer.Visible = true;
                this.LoadingFinished();
            }
        }

        private void SetupBrowserPlayer()
        {
            this.WebBrowser.AllowNavigation = false;
            this.WebBrowser.AllowWebBrowserDrop = false;
            this.WebBrowser.IsWebBrowserContextMenuEnabled = false;
            this.WebBrowser.ScrollBarsEnabled = false;
            this.WebBrowser.ScriptErrorsSuppressed = true;
            this.WebBrowser.Dock = DockStyle.Fill;
            this.WebBrowser.WebBrowserShortcutsEnabled = false;
        }

        private void WebBrowser_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            this.WebBrowser.Visible = true;
            this.LoadingFinished();
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
                this._chatWindow = new ChatForm(this,this._stream);
                this._chatWindow.FormClosed += ChatWindowClosed;
                this._chatWindow.Show();
            }
            else this._chatWindow.Focus();
        }

        void ChatWindowClosed(object sender, FormClosedEventArgs e)
        {
            this._chatWindow = null;
        }

        private void PlayerForm_ResizeEnd(object sender, EventArgs e)
        {
            if (this.Size.Width == EmbeddedModules.Streams.Settings.ModuleSettings.Instance.PlayerWidth && this.Size.Height == EmbeddedModules.Streams.Settings.ModuleSettings.Instance.PlayerHeight) return;

            EmbeddedModules.Streams.Settings.ModuleSettings.Instance.PlayerWidth = this.Size.Width;
            EmbeddedModules.Streams.Settings.ModuleSettings.Instance.PlayerHeight = this.Size.Height;
            EmbeddedModules.Streams.Settings.ModuleSettings.Instance.Save();
        }
    }
}
