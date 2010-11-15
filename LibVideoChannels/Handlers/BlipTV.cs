using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using System.Text;
using LibBlizzTV;
using LibBlizzTV.Utils;
using LibVideoChannels;

namespace LibVideoChannels.Handlers
{
    public class BlipTV : Channel
    {
        public BlipTV(string Title, string Slug, string Provider) : base(Title, Slug, Provider) { }

        public override void Update()
        {
            string api_url = string.Format("http://{0}.blip.tv/rss", this.Slug); // the api url.
            string response = WebReader.Read(api_url); // read the api response.
        }
    }
}
