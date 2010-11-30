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
 * $Id: Video.cs 158 2010-11-30 14:06:50Z shalafiraistlin@gmail.com $
 */

using System;
using BlizzTV.Module;
using BlizzTV.Module.Settings;

namespace BlizzTV.Modules.Videos
{
    public class Video:ListItem
    {
        #region members

        private string _video_id; // the video id.
        private string _link; // the video link.
        private string _provider; // the video provider.
        private string _movie; // the movie template.
        private string _flash_vars; // the flash vars.

        public string VideoID { get { return this._video_id; } internal set { this._video_id = value; } }
        public string Link { get { return this._link; } internal set { this._link = value; } }
        public string Provider { get { return this._provider; } set { this._provider = value; } }
        public string Movie { get { return this._movie; } set { this._movie = value; } }
        public string FlashVars { get { return this._flash_vars; } set { this._flash_vars = value; } }

        #endregion

        #region ctor

        public Video(string Title, string Guid, string Link, string Provider)
            : base(Title,true)
        {            
            this.GUID = Guid;
            this.Link = Link;
            this.Provider = Provider;

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
            this._movie = this._movie.Replace("%auto_play%", (Global.Instance.AutoPlayVideos) ? "1" : "0");
            
            this._flash_vars = this._flash_vars.Replace("%video_id%", this._video_id); // replace flashvars variables.
            this._flash_vars = this._flash_vars.Replace("%auto_play%", (Global.Instance.AutoPlayVideos)?"1":"0");
        }

        public override void DoubleClicked(object sender, EventArgs e)
        {
            if (this.State != ItemState.ERROR)
            {
                if (Global.Instance.UseInternalViewers) // if internal-viewers method is selected
                {
                    frmPlayer p = new frmPlayer(this); // render the video with our own video player
                    p.Show();
                }
                else System.Diagnostics.Process.Start(this.Link, null); // render the video with default web-browser.

                this.State = ItemState.READ; // set the video state to READ.
            }
        }

        public override void BalloonClicked(object sender, EventArgs e)
        {
            if (Global.Instance.UseInternalViewers) // if internal-viewers method is selected
            {
                frmPlayer p = new frmPlayer(this); // render the video with our own video player
                p.Show();
            }
            else System.Diagnostics.Process.Start(this.Link, null); // render the video with default web-browser.

            this.State = ItemState.READ; // set the video state to READ.
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
            }
        }

        private void MenuMarkAsWatchedClicked(object sender, EventArgs e)
        {
            this.State = ItemState.READ; // set the video state as read.          
        }

        private void MenuMarkAsUnWatchedClicked(object sender, EventArgs e)
        {
            this.State = ItemState.UNREAD;            
        }

        #endregion
    }
}
