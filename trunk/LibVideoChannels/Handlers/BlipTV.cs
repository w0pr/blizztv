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
            if (response != null)
            {
                XDocument xdoc = XDocument.Parse(response); // parse the api response.
                XNamespace xmlns = "http://blip.tv/dtd/blip/1.0"; 
                var entries = from item in xdoc.Descendants("item") // get the videos
                              select new
                              {
                                  GUID = item.Element("guid").Value,
                                  Title = item.Element("title").Value,
                                  Link = item.Element("link").Value,
                                  VideoID = item.Element(xmlns + "posts_id").Value
                              };

                int i = 0;

                foreach (var entry in entries) // create the video items.
                {
                    BlipTVVideo v = new BlipTVVideo(entry.Title, entry.GUID, entry.Link, this.Provider);
                    v.VideoID = entry.VideoID;
                    this.Videos.Add(v);
                    i++;
                    if (i >= (VideoChannelsPlugin.Instance.Settings as Settings).NumberOfVideosToQueryChannelFor) break;
                }
            }

            this.Process();  
        }
    }
}
