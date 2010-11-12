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
using LibBlizzTV;

namespace LibStreams
{
    public class Stream:ListItem
    {
        #region members

        private string _slug; // the stream slug.
        private string _provider; // the stream provider.
        private string _link; // the stream link.
        private bool _is_live = false; // is the stream live?
        private string _description; // the stream description.
        private Int32 _viewer_count = 0; // stream viewers count.
        private string _movie; // the stream's movie source.
        private string _flash_vars; // the streams's flash vars.

        public string Slug { get { return this._slug; } internal set { this._slug = value; } }
        public string Provider { get { return this._provider; } internal set { this._provider = value; } }
        public string Link { get { return this._link; } internal set { this._link = value; } }
        public bool IsLive { get { return this._is_live; } internal set { this._is_live = value; } }
        public string Description { get { return this._description; } internal set { this._description = value; } }
        public Int32 ViewerCount { get { return this._viewer_count; } internal set { this._viewer_count = value; } }
        public string Movie { get { return this._movie; } internal set { this._movie = value; } }
        public string FlashVars { get { return this._flash_vars; } internal set { this._flash_vars = value; } }

        #endregion

        #region ctor

        public Stream(string Title, string Slug, string Provider)
            : base(Title)
        {
            this._slug = Slug;
            this._provider = Provider;
        }

        #endregion

        #region internal logic 

        public virtual void Process() // get the stream data by replacing provider variables. handler's can override this method to run their own routines, though base.Process() should be called also.
        {
            this._movie = Providers.Instance.List[this.Provider].Movie; // provider supplied movie source. 
            this._flash_vars = Providers.Instance.List[this.Provider].FlashVars; // provider supplied flashvars.

            this._movie = this._movie.Replace("%slug%", this.Slug); // replace slug variable in movie source.
            this._flash_vars = this._flash_vars.Replace("%slug%", this.Slug); // replace slug variable in flashvars.
        }

        public override void DoubleClicked(object sender, EventArgs e) // double-click handler
        {
            if (Plugin.Instance.GlobalSettings.ContentViewer == ContentViewMethods.INTERNAL_VIEWERS) // if internal-viewers method is selected
            {
                Player p = new Player(this); // render the stream with our own video player
                p.Show();
            }
            else System.Diagnostics.Process.Start(this.Link, null); // render the stream with default web-browser.
        }

        public virtual void Update() { throw new NotImplementedException(); } // the stream updater. 

        #endregion
    }
}
