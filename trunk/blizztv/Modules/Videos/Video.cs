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
using BlizzTV.CommonLib.Settings;
using BlizzTV.ModuleLib;
using BlizzTV.CommonLib.Notifications;
using BlizzTV.ModuleLib.StatusStorage;

namespace BlizzTV.Modules.Videos
{
    public class Video:ListItem
    {
        private Statutes _status = Statutes.Unknown;
        private frmPlayer _player = null;

        public string VideoId { get; internal set; }
        public string Link { get; internal set; }
        public string Provider { get; set; }
        public string Movie { get; set; }
        public string FlashVars { get; set; }
        public string Guid { get; protected set; }

        public Statutes Status
        {
            get
            {
                if (this._status == Statutes.Unknown)
                {
                    if (!StatusStorage.Instance.Exists(string.Format("video.{0}", this.Guid))) this.Status = Statutes.Fresh;
                    else
                    {
                        this._status = (Statutes)StatusStorage.Instance[string.Format("video.{0}", this.Guid)];
                        if (this._status == Statutes.Fresh) this.Status = Statutes.Unwatched;
                        else if (this._status == Statutes.Unwatched) this.Style = ItemStyle.Bold;
                    }
                }
                else
                {
                    switch (this._status)
                    {
                        case Statutes.Fresh:
                        case Statutes.Unwatched:
                            if (this.Style != ItemStyle.Bold) this.Style = ItemStyle.Bold;
                            break;
                        case Statutes.Watched:
                            if (this.Style != ItemStyle.Regular) this.Style = ItemStyle.Regular;
                            break;
                    }
                }
                return this._status;
            }
            set
            {
                this._status = value;
                StatusStorage.Instance[string.Format("video.{0}", this.Guid)] = (byte)this._status;
                switch (this._status)
                {
                    case Statutes.Fresh:
                    case Statutes.Unwatched:
                        this.Style = ItemStyle.Bold;
                        break;
                    case Statutes.Watched:
                        this.Style = ItemStyle.Regular;
                        break;
                    default:
                        break;
                }
            }
        }

        public Video(string title, string guid, string link, string provider)
            : base(title)
        {            
            this.Guid = guid;
            this.Link = link;
            this.Provider = provider;

            // register context menus.
            this.ContextMenus.Add("markaswatched", new System.Windows.Forms.ToolStripMenuItem("Mark As Watched", null, new EventHandler(MenuMarkAsWatchedClicked))); // mark as read menu.
            this.ContextMenus.Add("markasunwatched", new System.Windows.Forms.ToolStripMenuItem("Mark As Unwatched", null, new EventHandler(MenuMarkAsUnWatchedClicked))); // mark as unread menu.            

            this.Icon = Properties.Resources.video_16;
        }

        public void CheckForNotifications()
        {
            if (this.Status == Statutes.Fresh) NotificationManager.Instance.Show(this, new NotificationEventArgs(this.Title, "Click to watch.", System.Windows.Forms.ToolTipIcon.Info));
        }

        public virtual void Process() // get the stream data by replacing provider variables. 
        {
            this.Movie = ((VideoProvider) Providers.Instance.Dictionary[this.Provider]).Movie; // provider supplied movie source. 
            this.FlashVars = ((VideoProvider) Providers.Instance.Dictionary[this.Provider]).FlashVars; // provider supplied flashvars.

            this.Movie = this.Movie.Replace("%video_id%", this.VideoId); // replace movie source variables
            this.Movie = this.Movie.Replace("%auto_play%", (GlobalSettings.Instance.AutoPlayVideos) ? "1" : "0");
            
            this.FlashVars = this.FlashVars.Replace("%video_id%", this.VideoId); // replace flashvars variables.
            this.FlashVars = this.FlashVars.Replace("%auto_play%", (GlobalSettings.Instance.AutoPlayVideos)?"1":"0");
        }

        public override void DoubleClicked(object sender, EventArgs e)
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
            if (this.Status != Statutes.Watched) this.Status = Statutes.Watched;
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

            switch (this.Status) // switch on the item state.
            {
                case Statutes.Fresh:
                case Statutes.Unwatched:
                    this.ContextMenus["markaswatched"].Visible = true; // make mark as watched menu visible.
                    break;
                case Statutes.Watched:
                    this.ContextMenus["markasunwatched"].Visible = true; // make mark as unwatched menu visible.
                    break;
            }
        }

        private void MenuMarkAsWatchedClicked(object sender, EventArgs e)
        {
            this.Status = Statutes.Watched;
        }

        private void MenuMarkAsUnWatchedClicked(object sender, EventArgs e)
        {
            this.Status = Statutes.Unwatched;         
        }

        public enum Statutes
        {
            Unknown,
            Fresh,
            Unwatched,
            Watched
        }
    }
}
