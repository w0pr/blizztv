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
        #region members

        private string _video_id; // the video id.
        private string _guid; // the story-guid.
        private string _link; // the video link.
        private string _provider; // the video provider.
        private string _movie; // the movie template.
        private string _flash_vars; // the flash vars.
        private Statutes _status = Statutes.UNKNOWN;
        private frmPlayer _player = null;

        public string VideoID { get { return this._video_id; } internal set { this._video_id = value; } }
        public string Link { get { return this._link; } internal set { this._link = value; } }
        public string Provider { get { return this._provider; } set { this._provider = value; } }
        public string Movie { get { return this._movie; } set { this._movie = value; } }
        public string FlashVars { get { return this._flash_vars; } set { this._flash_vars = value; } }
        public string GUID { get { return this._guid; } protected set { this._guid = value; } }

        public Statutes Status
        {
            get
            {
                if (this._status == Statutes.UNKNOWN)
                {
                    if (!StatusStorage.Instance.Exists(string.Format("video.{0}", this.GUID))) this.Status = Statutes.FRESH;
                    else
                    {
                        this._status = (Statutes)StatusStorage.Instance[string.Format("video.{0}", this.GUID)];
                        if (this._status == Statutes.FRESH) this.Status = Statutes.UNWATCHED;
                        else if (this._status == Statutes.UNWATCHED) this.Style = ItemStyle.BOLD;
                    }
                }
                else
                {
                    switch (this._status)
                    {
                        case Statutes.FRESH:
                        case Statutes.UNWATCHED:
                            if (this.Style != ItemStyle.BOLD) this.Style = ItemStyle.BOLD;
                            break;
                        case Statutes.WATCHED:
                            if (this.Style != ItemStyle.REGULAR) this.Style = ItemStyle.REGULAR;
                            break;
                    }
                }
                return this._status;
            }
            set
            {
                this._status = value;
                StatusStorage.Instance[string.Format("video.{0}", this.GUID)] = (byte)this._status;
                switch (this._status)
                {
                    case Statutes.FRESH:
                    case Statutes.UNWATCHED:
                        this.Style = ItemStyle.BOLD;
                        break;
                    case Statutes.WATCHED:
                        this.Style = ItemStyle.REGULAR;
                        break;
                    default:
                        break;
                }
            }
        }

        #endregion

        #region ctor

        public Video(string Title, string Guid, string Link, string Provider)
            : base(Title)
        {            
            this.GUID = Guid;
            this.Link = Link;
            this.Provider = Provider;

            // register context menus.
            this.ContextMenus.Add("markaswatched", new System.Windows.Forms.ToolStripMenuItem("Mark As Watched", null, new EventHandler(MenuMarkAsWatchedClicked))); // mark as read menu.
            this.ContextMenus.Add("markasunwatched", new System.Windows.Forms.ToolStripMenuItem("Mark As Unwatched", null, new EventHandler(MenuMarkAsUnWatchedClicked))); // mark as unread menu.            

            this.Icon = Properties.Resources.video_16;
        }

        #endregion

        #region internal logic

        public void CheckForNotifications()
        {
            if (this.Status == Statutes.FRESH) NotificationManager.Instance.Show(this, new NotificationEventArgs(this.Title, "Click to watch.", System.Windows.Forms.ToolTipIcon.Info));
        }

        public virtual void Process() // get the stream data by replacing provider variables. 
        {
            this._movie = (Providers.Instance.Dictionary[this.Provider] as VideoProvider).Movie; // provider supplied movie source. 
            this._flash_vars = (Providers.Instance.Dictionary[this.Provider] as VideoProvider).FlashVars; // provider supplied flashvars.

            this._movie = this._movie.Replace("%video_id%", this._video_id); // replace movie source variables
            this._movie = this._movie.Replace("%auto_play%", (GlobalSettings.Instance.AutoPlayVideos) ? "1" : "0");
            
            this._flash_vars = this._flash_vars.Replace("%video_id%", this._video_id); // replace flashvars variables.
            this._flash_vars = this._flash_vars.Replace("%auto_play%", (GlobalSettings.Instance.AutoPlayVideos)?"1":"0");
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
            if (this.Status != Statutes.WATCHED) this.Status = Statutes.WATCHED;
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
                case Statutes.FRESH:
                case Statutes.UNWATCHED:
                    this.ContextMenus["markaswatched"].Visible = true; // make mark as watched menu visible.
                    break;
                case Statutes.WATCHED:
                    this.ContextMenus["markasunwatched"].Visible = true; // make mark as unwatched menu visible.
                    break;
            }
        }

        private void MenuMarkAsWatchedClicked(object sender, EventArgs e)
        {
            this.Status = Statutes.WATCHED;
        }

        private void MenuMarkAsUnWatchedClicked(object sender, EventArgs e)
        {
            this.Status = Statutes.UNWATCHED;         
        }

        #endregion

        public enum Statutes
        {
            UNKNOWN,
            FRESH,
            UNWATCHED,
            WATCHED
        }
    }
}
