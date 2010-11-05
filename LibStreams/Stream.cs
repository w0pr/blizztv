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
        private string _slug;
        private string _provider;

        private bool _is_live = false;
        private string _description;
        private Int32 _viewer_count;

        public string Slug { get { return this._slug; } set { this._slug = value; } }
        public string Provider { get { return this._provider; } set { this._provider = value; } }

        public bool IsLive { get { return this._is_live; } internal set { this._is_live = value; } }
        public string Description { get { return this._description; } internal set { this._description = value; } }
        public Int32 ViewerCount { get { return this._viewer_count; } internal set { this._viewer_count = value; } }

        public Stream()
        {
        }

        public virtual string VideoEmbedCode()
        {
            string embed_template = Providers.Instance.List[this.Provider].VideoTemplate;
            embed_template = embed_template.Replace("%slug%", this.Slug);
            return embed_template;
        }

        public virtual void Update() { throw new NotImplementedException(); }
    }
}
