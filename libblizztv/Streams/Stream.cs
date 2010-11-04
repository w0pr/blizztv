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
using System.Linq;
using System.Text;

namespace LibBlizzTV.Streams
{
    public class Stream
    {
        private string _id;
        private string _name;
        private string _provider;

        public string ID { get { return this._id; } set { this._id = value; } }
        public string Name { get { return this._name; } set { this._name = value; } }
        public string Provider { get { return this._provider; } set { this._provider = value; } }

        public Stream(string ID, string Name,string Provider)
        {
            this._id = ID;
            this._name = Name;
            this._provider = Provider;
            this.Process();
        }

        public string VideoEmbedCode()
        {
            string embed_template = Providers.Instance.List[this.Provider].VideoTemplate;
            embed_template = embed_template.Replace("%stream_id%", this.ID);
            return embed_template;
        }

        protected virtual void Process() { throw new NotImplementedException(); }
    }
}
