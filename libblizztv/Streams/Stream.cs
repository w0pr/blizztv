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
        }

        public string VideoEmbedCode()
        {
            string embed_template = Providers.Instance.List[this.Provider].VideoTemplate;
            embed_template = embed_template.Replace("%stream_id%", this.ID);
            embed_template = embed_template.Replace("%width%", "640");
            embed_template = embed_template.Replace("%height%", "385");
            return embed_template;
        }
    }
}
