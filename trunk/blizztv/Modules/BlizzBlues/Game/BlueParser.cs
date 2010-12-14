using System;
using System.Collections.Generic;
using System.Linq;

using System.Text;
using System.Text.RegularExpressions;
using HtmlAgilityPack;
using BlizzTV.ModuleLib;
using BlizzTV.CommonLib.Web;
using BlizzTV.CommonLib.Logger;

namespace BlizzTV.Modules.BlizzBlues.Game
{
    public class BlueParser:ListItem
    {
        private static Regex RegexBlueId = new Regex(@"\.\./topic/(?<TopicID>.*?)(\?page\=.*?)?#(?<PostID>.*)", RegexOptions.Compiled);

        protected BlueSource[] Sources;
        public Dictionary<string,BlueStory> Stories = new Dictionary<string,BlueStory>();

        public BlueParser(string game) : base(game) { }

        public bool Update()
        {
            if (this.Parse())
            {
                foreach (KeyValuePair<string, BlueStory> pair in this.Stories) { pair.Value.CheckForNotifications(); }
                return true;
            }
            return false;
        }

        internal bool Parse()
        {
            foreach(BlueSource source in this.Sources)
            {
                try
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
                        string topicId = "";
                        string postId = "";

                        Match m = RegexBlueId.Match(subNodes[1].Attributes["href"].Value);
                        if (m.Success)
                        {
                            topicId = m.Groups["TopicID"].Value;
                            postId = m.Groups["PostID"].Value;

                            BlueStory b = new BlueStory(postTitle, source.Region, postLink, topicId, postId);
                            b.OnStyleChange += ChildStyleChange;

                            if (!this.Stories.ContainsKey(topicId)) this.Stories.Add(topicId, b);
                            else this.Stories[topicId].More.Add(postId, b);                            
                        }
                    }                    
                }
                catch (Exception e) { Log.Instance.Write(LogMessageTypes.Error, string.Format("BlueParser error: {0}", e)); return false; }
            }
            return true;
        }

        void ChildStyleChange(ItemStyle style)
        {
            throw new NotImplementedException();
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
