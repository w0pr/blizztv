using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using System.Text;

using LibBlizzTV;

namespace LibFeeds
{
    [Plugin("LibFeeds","Feed aggregator plugin for BlizzTV")]
    public class FeedsPlugin:Plugin
    {
        private List<Feed> _feeds = new List<Feed>();

        public FeedsPlugin()
        {
            this._load();
        }

        private void _load()
        {
            XDocument xdoc = XDocument.Load("Feeds.xml");
            var entries = from feed in xdoc.Descendants("Feed")
                          select new
                          {
                              Name = feed.Element("Name").Value,
                              Url = feed.Element("Url").Value,
                          };


            foreach (var entry in entries)
            {
                Feed f = new Feed();
                f.Name = entry.Name;
                f.Url = entry.Url;
                this._feeds.Add(f);
            }
        }

        public override void  Update()
        {
            foreach (Feed feed in this._feeds)
            {
                feed.Update();
            }
        }
    }
}
