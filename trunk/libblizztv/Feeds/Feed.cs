using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using System.Text;

namespace LibBlizzTV.Feeds
{
    public class Feed
    {
        public string Name;
        public string Url;
        public List<Story> Stories = new List<Story>();


        public virtual void Update()
        {
            string response = Utils.WebReader.Read(this.Url);
            XDocument xdoc = XDocument.Parse(response);

            var entries = from item in xdoc.Descendants("item")
                          select new
                          {
                              GUID = item.Element("guid").Value,
                              Title = item.Element("title").Value,
                              Link = item.Element("link").Value,
                              Description=item.Element("description").Value
                          };

            foreach (var entry in entries)
            {
                Story s = new Story();
                s.GUID = entry.GUID;
                s.Link = entry.Link;
                s.Title = entry.Title;
                s.Description = entry.Description;
                this.Stories.Add(s);
            }
        }
    }
}
