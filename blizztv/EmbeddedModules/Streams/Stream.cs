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
using BlizzTV.EmbeddedModules.Streams.UI;
using BlizzTV.InfraStructure.Modules;
using BlizzTV.InfraStructure.Modules.Storage;
using BlizzTV.Notifications;
using BlizzTV.Settings;
using BlizzTV.Utility.Imaging;

namespace BlizzTV.EmbeddedModules.Streams
{
    public class Stream:ListItem
    {
        private bool _isLive = false; // is the stream live?
        private PlayerForm _player = null;

        public string Name { get; internal set; }
        public string Slug { get; internal set; }
        public string Provider { get; internal set; }
        public string Link { get; internal set; }
        public string Description { get; internal set; }
        public int ViewerCount { get; internal set; }
        public string Movie { get; internal set; }
        public string ChatMovie { get; internal set; }
        public bool ChatAvailable { get; private set; }

        public bool IsLive
        {
            get { return this._isLive; }
            internal set 
            {
                bool wasOnline=false;
                this._isLive = value;

                if (StateStorage.Instance.Exists(string.Format("stream.{0}", this.Name))) wasOnline = Convert.ToBoolean(StateStorage.Instance[string.Format("stream.{0}", this.Name)]);
                if (EmbeddedModules.Streams.Settings.ModuleSettings.Instance.NotificationsEnabled && !wasOnline && this._isLive) NotificationManager.Instance.Show(this, new NotificationEventArgs(this.Title, i18n.StreamOnlineNotification, System.Windows.Forms.ToolTipIcon.Info));
                StateStorage.Instance[string.Format("stream.{0}", this.Name)] = Convert.ToByte(this._isLive);
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
            this.ChatAvailable = ((StreamProvider) Providers.Instance.Dictionary[this.Provider]).ChatAvailable; // Is chat functionality available for the provider?
            if (this.ChatAvailable) this.ChatMovie = ((StreamProvider) Providers.Instance.Dictionary[this.Provider]).ChatMovie; // the streams chat movie's source.

            this.Movie = this.Movie.Replace("%slug%", this.Slug); // replace slug variable in movie source.
            if(this.ChatAvailable) this.ChatMovie = this.ChatMovie.Replace("%slug%", this.Slug); // replace slug variable in flashvars.            
        }

        public override void Open(object sender, EventArgs e) // double-click handler
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
                    this._player = new PlayerForm(this);                    
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
