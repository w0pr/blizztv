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
using BlizzTV.CommonLib.Utils;
using BlizzTV.ModuleLib;
using BlizzTV.ModuleLib.StatusStorage;
using BlizzTV.CommonLib.Notifications;

namespace BlizzTV.Modules.Streams
{
    public class Stream:ListItem
    {
        private bool _isLive = false; // is the stream live?
        private frmPlayer _player = null;

        public string Name { get; internal set; }
        public string Slug { get; internal set; }
        public string Provider { get; internal set; }
        public string Link { get; internal set; }
        public string Description { get; internal set; }
        public int ViewerCount { get; internal set; }
        public string Movie { get; internal set; }
        public string FlashVars { get; internal set; }
        public string ChatMovie { get; internal set; }
        public bool ChatAvailable { get; private set; }

        public bool IsLive
        {
            get { return this._isLive; }
            internal set 
            {
                bool wasOnline=false;
                this._isLive = value; 

                if (StatusStorage.Instance.Exists(string.Format("stream.{0}", this.Name))) wasOnline = Convert.ToBoolean(StatusStorage.Instance[string.Format("stream.{0}", this.Name)]);
                if (Settings.Instance.NotificationsEnabled && !wasOnline && this._isLive) NotificationManager.Instance.Show(this, new NotificationEventArgs(this.Title, "Stream is online, click to start watching it.", System.Windows.Forms.ToolTipIcon.Info));
                StatusStorage.Instance[string.Format("stream.{0}", this.Name)] = Convert.ToByte(this._isLive);
                // TODO: when the application goes offline the stream should set to offline in status storage.
            }
        }

        public Stream(StreamSubscription subscription)
            : base(subscription.Name)
        {
            ChatAvailable = false;
            ViewerCount = 0;
            this.Name = subscription.Name;
            this.Slug = subscription.Slug;
            this.Provider = subscription.Provider;

            this.Icon = new NamedImage("stream", Assets.Images.Icons.Png._16.stream);
        }

        public virtual void Process() // get the stream data by replacing provider variables. handler's can override this method to run their own routines, though base.Process() should be called also.
        {
            this.Movie = ((StreamProvider) Providers.Instance.Dictionary[this.Provider]).Movie; // provider supplied movie source. 
            this.FlashVars = ((StreamProvider) Providers.Instance.Dictionary[this.Provider]).FlashVars; // provider supplied flashvars.
            this.ChatAvailable = ((StreamProvider) Providers.Instance.Dictionary[this.Provider]).ChatAvailable; // Is chat functionality available for the provider?
            if (this.ChatAvailable) this.ChatMovie = ((StreamProvider) Providers.Instance.Dictionary[this.Provider]).ChatMovie; // the streams chat movie's source.

            this.Movie = this.Movie.Replace("%slug%", this.Slug); // replace slug variable in movie source.
            this.FlashVars = this.FlashVars.Replace("%slug%", this.Slug); // replace slug variable in flashvars.            
            if(this.ChatAvailable) this.ChatMovie = this.ChatMovie.Replace("%slug%", this.Slug); // replace slug variable in flashvars.            
        }

        public override void DoubleClicked(object sender, EventArgs e) // double-click handler
        {
            this.Play();
        }

        public override void NotificationClicked()
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
    }
}
