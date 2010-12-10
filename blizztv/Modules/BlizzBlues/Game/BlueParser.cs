using System;
using System.Collections.Generic;
using System.Linq;

using System.Text;
using HtmlAgilityPack;
using BlizzTV.ModuleLib;
using BlizzTV.CommonLib.Web;

namespace BlizzTV.Modules.BlizzBlues.Game
{
    public class BlueParser:ListItem
    {
        protected BlueSource[] Sources;        

        public BlueParser(string game) : base(game) { }

        internal void Parse()
        {
            foreach(BlueSource source in this.Sources)
            {
                string xml = WebReader.Read(source.Url);
                HtmlDocument doc = new HtmlDocument();
                doc.LoadHtml(xml);

                foreach (HtmlNode node in doc.DocumentNode.SelectNodes("//tr[@class='blizzard']"))
                {
                    HtmlNodeCollection subNodes = node.SelectNodes("td[@class='post-title']//div[@class='desc']//a[@href]");
                    string postForum = subNodes[0].InnerText;
                    string postTitle = subNodes[1].InnerText;
                    string postLink = string.Format("{0}{1}", source.Url, subNodes[1].Attributes["href"].Value);

                    if (!this.Childs.ContainsKey(postTitle)) this.Childs.Add(postTitle, new BlueStory(postTitle, this.Sources[0].Region, postLink));
                    else this.Childs[postTitle].Childs.Add(string.Format("{0}-{1}",postTitle,postLink), new BlueStory(postTitle, this.Sources[0].Region, postLink));
                }
            }
        }
    }

    public class BlueSource
    {
        public Region Region { get; private set; }
        public string Url { get; private set; }

        public BlueSource(Region region, string url)
        {
            this.Region = region;
            this.Url = url;
        }
    }

    public enum Region
    {
        Us,
        Eu
    }
}
