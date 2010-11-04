using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LibBlizzTV.Streams
{
    public class Provider
    {
        public string _name;
        public string _video_template;

        public string Name { get { return this._name; } internal set { this._name = value; } }
        public string VideoTemplate { get { return this._video_template; } internal set { this._video_template = value; } }

        public Provider(string Name, string VideoTemplate)
        {
            this._name = Name;
            this._video_template = VideoTemplate;
        }
    }
}
