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
    public class Youtube : Channel
    {
        public Youtube(string Title, string Slug, string Provider) : base(Title, Slug, Provider) { }

        public override void Update()
        {
            try
            {
                string api_url = string.Format("http://gdata.youtube.com/feeds/api/users/{0}/uploads?alt=rss&max-results={1}", this.Slug, (VideoChannelsPlugin.Instance.Settings as Settings).NumberOfVideosToQueryChannelFor); // the api url.
                string response = WebReader.Read(api_url); // read the api response.
                if (response != null)
                {
                    XDocument xdoc = XDocument.Parse(response); // parse the api response.
                    var entries = from item in xdoc.Descendants("item") // get the videos
                                  select new
                                  {
                                      GUID = item.Element("guid").Value,
                                      Title = item.Element("title").Value,
                                      Link = item.Element("link").Value
                                  };

                    foreach (var entry in entries) // create the video items.
                    {
                        YoutubeVideo v = new YoutubeVideo(entry.Title, entry.GUID, entry.Link, this.Provider);
                        this.Videos.Add(v);
                    }
                }
                else this.Valid = false;
            }
            catch (Exception e)
            {
                this.Valid = false;
                Log.Instance.Write(LogMessageTypes.ERROR, string.Format("VideoChannels Plugin - Channel - Update() Error: \n {0}", e.ToString()));
            }

            if (this.Valid)
            {
                int unread = 0; // non-watched videos count.
                foreach (Video v in this.Videos) { if (v.State == ItemState.UNREAD) unread++; }

                if (unread > 0) // if there non-watched channel videos.
                {
                    this.SetTitle(string.Format("{0} ({1})", this.Title, unread.ToString()));
                    this.SetState(ItemState.UNREAD); // then mark the channel itself as unread also
                }
            }
            else
            {
                Video error = new Video("Error updating channel.", "", "", this.Provider);
                error.SetState(ItemState.ERROR);
                this.Videos.Add(error);
            }
        }
    }
}
