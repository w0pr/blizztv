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
using BlizzTV.CommonLib.Utils;
using BlizzTV.CommonLib.Settings;
using BlizzTV.ModuleLib;
using BlizzTV.ModuleLib.StatusStorage;
using BlizzTV.Notifications;

namespace BlizzTV.Modules.Videos
{
    public class Video:ListItem
    {
        private frmPlayer _player = null;

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

            // register context menus.
            this.ContextMenus.Add("markaswatched", new System.Windows.Forms.ToolStripMenuItem("Mark As Watched", Assets.Images.Icons.Png._16.read, new EventHandler(MenuMarkAsWatchedClicked))); // mark as read menu.
            this.ContextMenus.Add("markasunwatched", new System.Windows.Forms.ToolStripMenuItem("Mark As Unwatched", Assets.Images.Icons.Png._16.unread, new EventHandler(MenuMarkAsUnWatchedClicked))); // mark as unread menu.            

            this.Icon = new NamedImage("video", Assets.Images.Icons.Png._16.video);
        }

        public void CheckForNotifications()
        {
            if (Settings.Instance.NotificationsEnabled &&  this.State == ModuleLib.State.Fresh) NotificationManager.Instance.Show(this, new NotificationEventArgs(this.Title, string.Format("A new video is avaiable over {0}'s channel, click to start watching it.",this.ChannelName), System.Windows.Forms.ToolTipIcon.Info));
        }

        public virtual void Process() // get the stream data by replacing provider variables. 
        {
            this.Movie = ((VideoProvider) Providers.Instance.Dictionary[this.Provider]).Movie; // provider supplied movie source. 

            this.Movie = this.Movie.Replace("%video_id%", this.VideoId); // replace movie source variables
            this.Movie = this.Movie.Replace("%auto_play%", (GlobalSettings.Instance.AutoPlayVideos) ? "1" : "0");            
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
                    this._player = new frmPlayer(this); // render the video with our own video player
                    this._player.FormClosed += PlayerClosed;
                    this._player.Show();
                }
                else this._player.Focus();
            }
            else System.Diagnostics.Process.Start(this.Link, null); // render the video with default web-browser.
            if (this.State != ModuleLib.State.Read) this.State = ModuleLib.State.Read;
        }

        void PlayerClosed(object sender, System.Windows.Forms.FormClosedEventArgs e)
        {
            this._player = null;
        }

        public override void RightClicked(object sender, EventArgs e) // manage the context-menus based on our item state.
        {
            // make conditional context-menus invisible.
            this.ContextMenus["markaswatched"].Visible = false;
            this.ContextMenus["markasunwatched"].Visible = false;

            switch (this.State) // switch on the item state.
            {
                case  ModuleLib.State.Fresh:
                case ModuleLib.State.Unread:
                    this.ContextMenus["markaswatched"].Visible = true; // make mark as watched menu visible.
                    break;
                case ModuleLib.State.Read:
                    this.ContextMenus["markasunwatched"].Visible = true; // make mark as unwatched menu visible.
                    break;
            }
        }

        private void MenuMarkAsWatchedClicked(object sender, EventArgs e)
        {
            this.State = ModuleLib.State.Read;            
        }

        private void MenuMarkAsUnWatchedClicked(object sender, EventArgs e)
        {
            this.State = ModuleLib.State.Unread;
        }
    }
}
