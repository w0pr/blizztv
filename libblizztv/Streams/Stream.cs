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

namespace LibBlizzTV.Streams
{
    public class Stream
    {
        private string _slug;
        private string _name;
        private string _provider;
        private Game _game;

        private bool _is_live = false;
        private string _description;
        private Int32 _viewer_count;

        public string Slug { get { return this._slug; } set { this._slug = value; } }
        public string Name { get { return this._name; } set { this._name = value; } }
        public string Provider { get { return this._provider; } set { this._provider = value; } }
        public Game Game { get { return this._game; } set { this._game = value; } }

        public bool IsLive { get { return this._is_live; } protected set { this._is_live = value; } }
        public string Description { get { return this._description; } protected set { this._description = value; } }
        public Int32 ViewerCount { get { return this._viewer_count; } protected set { this._viewer_count = value; } }

        public Stream(string ID, string Name,string Provider)
        {
            this._slug = ID;
            this._name = Name;
            this._provider = Provider;
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
