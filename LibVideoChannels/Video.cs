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
using LibBlizzTV;

namespace LibVideoChannels
{
    public class Video:ListItem
    {
        private string _guid;
        private string _video_id;
        private string _link;
        private string _provider;

        private string _movie;
        private string _flash_vars;

        public string GUID { get { return this._guid; } internal set { this._guid = value; } }
        public string VideoID { get { return this._video_id; } internal set { this._video_id = value; } }
        public string Link { get { return this._link; } internal set { this._link = value; } }
        public string Provider { get { return this._provider; } set { this._provider = value; } }
        public string Movie { get { return this._movie; } set { this._movie = value; } }
        public string FlashVars { get { return this._flash_vars; } set { this._flash_vars = value; } }

        public Video()
        {
        }

        public virtual void Process()
        {
            this._movie = Providers.Instance.List[this.Provider].Movie;
            this._flash_vars = Providers.Instance.List[this.Provider].FlashVars;

            this._movie = this._movie.Replace("%video_id%", this._video_id);
            this._flash_vars = this._flash_vars.Replace("%slug%", this._video_id);
        }

        public override void DoubleClick(object sender, EventArgs e)
        {
            if (VideoChannelsPlugin.GlobalSettings.ContentViewer == ContentViewMethods.INTERNAL_VIEWERS)
            {
                Player p = new Player(this);
                p.Show();
            }
            else System.Diagnostics.Process.Start(this.Link, null);    
        }
    }
}
