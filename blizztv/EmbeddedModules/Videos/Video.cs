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
using BlizzTV.Assets.i18n;
using BlizzTV.EmbeddedModules.Videos.UI;
using BlizzTV.InfraStructure.Modules;
using BlizzTV.Notifications;
using BlizzTV.Settings;
using BlizzTV.Utility.Imaging;
using System.Windows.Forms;

namespace BlizzTV.EmbeddedModules.Videos
{
    public class Video : ModuleNode
    {
        private PlayerForm _player = null;

        public string ChannelName { get; internal set; }
        public string VideoId { get; internal set; }
        public string Link { get; internal set; }
        public string Provider { get; set; }
        public string Movie { get; set; }
       
        public Video(string channelName, string title, string guid, string link, string provider)
            : base(title)
        {
            this.ChannelName = channelName;
            this.Guid = guid;
            this.Link = link;
            this.Provider = provider;

            this.Icon = new NamedImage("video", Assets.Images.Icons.Png._16.video);
            this.RememberState = true;

            this.Menu.Add("markaswatched", new ToolStripMenuItem(i18n.MarkAsWatched, Assets.Images.Icons.Png._16.read, new EventHandler(MenuMarkAsWatchedClicked)));
            this.Menu.Add("markasunwatched", new ToolStripMenuItem(i18n.MarkAsUnwatched, Assets.Images.Icons.Png._16.unread, new EventHandler(MenuMarkAsUnWatchedClicked)));
        }

        public void CheckForNotifications()
        {
            if (Settings.ModuleSettings.Instance.NotificationsEnabled &&  this.State == State.Fresh) NotificationManager.Instance.Show(this, new NotificationEventArgs(this.Text, string.Format("A new video is avaiable over {0}'s channel, click to start watching it.",this.ChannelName), System.Windows.Forms.ToolTipIcon.Info));
        }

        public virtual void Process() // get the stream data by replacing provider variables. 
        {
            this.Movie = ((VideoProvider) Providers.Instance.Dictionary[this.Provider]).Movie; // provider supplied movie source. 
            this.Movie = this.Movie.Replace("%video_id%", this.VideoId); // replace movie source variables
        }

        public override void Open(object sender, EventArgs e)
        {
            this.Play();
        }

        public override void NotificationClicked()
        {
            this.Play();
        }
       
        private void Play()
        {
            if (GlobalSettings.Instance.UseInternalViewers)
            {
                if (this._player == null)
                {
                    this._player = new PlayerForm(this); // render the video with our own video player
                    this._player.FormClosed += PlayerClosed;
                    this._player.Show();
                }
                else this._player.Focus();
            }
            else System.Diagnostics.Process.Start(this.Link, null); // render the video with default web-browser.
            if (this.State != State.Read) this.State = State.Read;
        }

        void PlayerClosed(object sender, FormClosedEventArgs e)
        {
            this._player = null;
        }

        public override void RightClicked(object sender, EventArgs e) // manage the context-menus based on our item state.
        {
            // make conditional context-menus invisible.
            this.Menu["markaswatched"].Enabled = false;
            this.Menu["markasunwatched"].Enabled = false;

            switch (this.State) // switch on the item state.
            {
                case State.Fresh:
                case State.Unread:
                    this.Menu["markaswatched"].Enabled = true; // make mark as watched menu visible.
                    break;
                case State.Read:
                    this.Menu["markasunwatched"].Enabled = true; // make mark as unwatched menu visible.
                    break;
            }
        }

        private void MenuMarkAsWatchedClicked(object sender, EventArgs e)
        {
            this.State = State.Read;
        }

        private void MenuMarkAsUnWatchedClicked(object sender, EventArgs e)
        {
            this.State = State.Unread;
        }
    }
}
