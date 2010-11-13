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
 */

using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using LibBlizzTV;

namespace LibVideoChannels
{
    public class Video:ListItem
    {
        #region members

        private string _guid; // the guid.
        private string _video_id; // the video id.
        private string _link; // the video link.
        private string _provider; // the video provider.
        private string _movie; // the movie template.
        private string _flash_vars; // the flash vars.
        private static Regex _regex = new Regex(@"http://www\.youtube\.com/watch\?v\=(.*)\&", RegexOptions.Compiled); // compiled regex for reading video id's.

        public string GUID { get { return this._guid; } internal set { this._guid = value; } }
        public string VideoID { get { return this._video_id; } internal set { this._video_id = value; } }
        public string Link { get { return this._link; } internal set { this._link = value; } }
        public string Provider { get { return this._provider; } set { this._provider = value; } }
        public string Movie { get { return this._movie; } set { this._movie = value; } }
        public string FlashVars { get { return this._flash_vars; } set { this._flash_vars = value; } }

        #endregion

        #region ctor

        public Video(string Title, string Guid, string Link, string Provider)
            : base(Title)
        {            
            this.GUID = Guid;
            this.Link = Link;
            this.Provider = Provider;

            Match m = _regex.Match(this.Link);  
            if (m.Success) this.VideoID = m.Groups[1].Value;

            // check the persistent storage for if the video is watched before.
            if (KeyValueStorage.Instance.KeyExists("video", "state", this.GUID)) this.SetState((ItemState)KeyValueStorage.Instance.Get("video", "state", this.GUID));
            else this.SetState(ItemState.UNREAD);

            // register context menus.
            this.ContextMenus.Add("markaswatched", new System.Windows.Forms.ToolStripMenuItem("Mark As Watched", null, new EventHandler(MenuMarkAsWatchedClicked))); // mark as read menu.
            this.ContextMenus.Add("markasunwatched", new System.Windows.Forms.ToolStripMenuItem("Mark As Unwatched", null, new EventHandler(MenuMarkAsUnWatchedClicked))); // mark as unread menu.
        }

        #endregion

        #region internal logic

        public virtual void Process() // get the stream data by replacing provider variables. 
        {
            this._movie = Providers.Instance.List[this.Provider].Movie; // provider supplied movie source. 
            this._flash_vars = Providers.Instance.List[this.Provider].FlashVars; // provider supplied flashvars.

            this._movie = this._movie.Replace("%video_id%", this._video_id); // replace movie source variables
            this._movie = this._movie.Replace("%auto_play%", (SettingsStorage.Instance.Settings.GlobalSettings.VideoAutoPlay) ? "1" : "0");
            
            this._flash_vars = this._flash_vars.Replace("%video_id%", this._video_id); // replace flashvars variables.
            this._flash_vars = this._flash_vars.Replace("%auto_play%", (SettingsStorage.Instance.Settings.GlobalSettings.VideoAutoPlay)?"1":"0");
        }

        public override void SetState(ItemState State) // override setstate function so that we can commit our state to storage.
        {
            base.SetState(State); // let the base function also do it's own job.
            KeyValueStorage.Instance.Put("video", "state", this.GUID, (byte)this.State); // commit it to persistent storage.
        }

        public override void DoubleClicked(object sender, EventArgs e)
        {
            if (this.State != ItemState.ERROR)
            {
                if (SettingsStorage.Instance.Settings.GlobalSettings.ContentViewer == ContentViewMethods.INTERNAL_VIEWERS) // if internal-viewers method is selected
                {
                    Player p = new Player(this); // render the video with our own video player
                    p.Show();
                }
                else System.Diagnostics.Process.Start(this.Link, null); // render the video with default web-browser.

                this.SetState(ItemState.READ); // set the video state to READ.
                KeyValueStorage.Instance.Put("video", "state", this.GUID, (byte)this.State); // commit it to persistent storage.
            }
        }


        public override void RightClicked(object sender, EventArgs e) // manage the context-menus based on our item state.
        {
            // make conditional context-menus invisible.
            this.ContextMenus["markaswatched"].Visible = false;
            this.ContextMenus["markasunwatched"].Visible = false;

            switch (this.State) // switch on the item state.
            {
                case ItemState.UNREAD:
                    this.ContextMenus["markaswatched"].Visible = true; // make mark as watched menu visible.
                    break;
                case ItemState.READ:
                    this.ContextMenus["markasunwatched"].Visible = true; // make mark as unwatched menu visible.
                    break;
                case ItemState.MARKED:
                    break;
            }
        }

        private void MenuMarkAsWatchedClicked(object sender, EventArgs e)
        {
            this.SetState(ItemState.READ); // set the video state as read.          
        }

        private void MenuMarkAsUnWatchedClicked(object sender, EventArgs e)
        {
            this.SetState(ItemState.UNREAD); // set the video state as unread.          
        }

        #endregion
    }
}
