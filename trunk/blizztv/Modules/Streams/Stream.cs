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
using BlizzTV.ModuleLib.StatusStorage;
using BlizzTV.ModuleLib.Notifications;

namespace BlizzTV.Modules.Streams
{
    public class Stream:ListItem
    {
        #region members

        private string _name; // the stream name.
        private string _slug; // the stream slug.
        private string _provider; // the stream provider.
        private string _link; // the stream link.
        private bool _is_live = false; // is the stream live?
        private string _description; // the stream description.
        private Int32 _viewer_count = 0; // stream viewers count.
        private string _movie; // the stream's movie source.
        private string _flash_vars; // the streams's flash vars.
        private bool _chat_available = false; // // Is chat functionality available for the provider?
        private string _chat_movie; // the streams chat movie's source.
        private frmPlayer _player = null;

        public string Name { get { return this._name; } internal set { this._name = value; } }
        public string Slug { get { return this._slug; } internal set { this._slug = value; } }
        public string Provider { get { return this._provider; } internal set { this._provider = value; } }
        public string Link { get { return this._link; } internal set { this._link = value; } }
        public string Description { get { return this._description; } internal set { this._description = value; } }
        public Int32 ViewerCount { get { return this._viewer_count; } internal set { this._viewer_count = value; } }
        public string Movie { get { return this._movie; } internal set { this._movie = value; } }
        public string FlashVars { get { return this._flash_vars; } internal set { this._flash_vars = value; } }
        public string ChatMovie { get { return this._chat_movie; } internal set { this._chat_movie = value; } }
        public bool ChatAvailable { get { return this._chat_available; } }

        public bool IsLive
        {
            get { return this._is_live; }
            internal set 
            {
                bool wasOnline=false;
                this._is_live = value; 

                if (StatusStorage.Instance.Exists(string.Format("stream.{0}", this.Name))) wasOnline = Convert.ToBoolean(StatusStorage.Instance[string.Format("stream.{0}", this.Name)]);
                if (!wasOnline && this._is_live) Notifications.Instance.Show(this, this.Title, "Stream is online. Click to watch.", System.Windows.Forms.ToolTipIcon.Info);
                StatusStorage.Instance[string.Format("stream.{0}", this.Name)] = Convert.ToByte(this._is_live);

                // TODO: when the application goes offline the stream should set to offline in status storage.
            }
        }

        #endregion

        #region ctor

        public Stream(StreamSubscription subscription)
            : base(subscription.Name)
        {
            this._name = subscription.Name;
            this._slug = subscription.Slug;
            this._provider = subscription.Provider;
        }

        #endregion

        #region internal logic 

        public virtual void Process() // get the stream data by replacing provider variables. handler's can override this method to run their own routines, though base.Process() should be called also.
        {
            this._movie = (Providers.Instance.Dictionary[this.Provider] as StreamProvider).Movie; // provider supplied movie source. 
            this._flash_vars = (Providers.Instance.Dictionary[this.Provider] as StreamProvider).FlashVars; // provider supplied flashvars.
            this._chat_available = (Providers.Instance.Dictionary[this.Provider] as StreamProvider).ChatAvailable; // Is chat functionality available for the provider?
            if (this._chat_available) this._chat_movie = (Providers.Instance.Dictionary[this.Provider] as StreamProvider).ChatMovie; // the streams chat movie's source.

            this._movie = this._movie.Replace("%slug%", this.Slug); // replace slug variable in movie source.
            this._flash_vars = this._flash_vars.Replace("%slug%", this.Slug); // replace slug variable in flashvars.            
            if(this._chat_available) this._chat_movie = this._chat_movie.Replace("%slug%", this.Slug); // replace slug variable in flashvars.            
        }

        public override void DoubleClicked(object sender, EventArgs e) // double-click handler
        {
            this.Play();
        }

        public override void BalloonClicked(object sender, EventArgs e)
        {
            this.Play();
        }

        private void Play()
        {
            if (GlobalSettings.Instance.UseInternalViewers) // if internal-viewers method is selected
            {
                if (this._player == null)
                {
                    this._player = new frmPlayer(this); // render the stream with our own video player
                    this._player.FormClosed += PlayerClosed;
                    this._player.Show();
                }
                else this._player.Focus();
            }
            else System.Diagnostics.Process.Start(this.Link, null); // render the stream with default web-browser.
        }

        void PlayerClosed(object sender, System.Windows.Forms.FormClosedEventArgs e)
        {
            this._player = null;
        }

        public virtual void Update() { throw new NotImplementedException(); } // the stream updater. 

        #endregion
    }
}
