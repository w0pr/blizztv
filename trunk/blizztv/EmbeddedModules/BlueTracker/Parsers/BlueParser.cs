/*    
 * Copyright (C) 2010-2011, BlizzTV Project - http://code.google.com/p/blizztv/
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
 * 
 * $Id$
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using BlizzTV.Assets.i18n;
using BlizzTV.InfraStructure.Modules;
using BlizzTV.Log;
using BlizzTV.Utility.Imaging;
using BlizzTV.Utility.Web;
using HtmlAgilityPack;

namespace BlizzTV.EmbeddedModules.BlueTracker.Parsers
{
    /// <summary>
    /// Parser for Blizzard forums that can extract Blue-GM posts.
    /// </summary>
    public class BlueParser : ModuleNode
    {
        private static readonly Regex RegexBlueId = new Regex(@"\.\./topic/(?<TopicID>.*?)(\?page\=.*?)?#(?<PostID>.*)", RegexOptions.Compiled); // The post details regex.
        private readonly BlueType _type;        
        protected BlueSource[] Sources;
        
        public readonly Dictionary<string,BlueStory> Stories = new Dictionary<string,BlueStory>(); // list of stories.

        public BlueParser(BlueType type)
            : base(type.ToString())
        {
            this._type = type;
           
            this.Menu.Add("markallasread", new System.Windows.Forms.ToolStripMenuItem(i18n.MarkAsRead, Assets.Images.Icons.Png._16.read, new EventHandler(MenuMarkAllAsReadClicked)));
            this.Menu.Add("markallasunread", new System.Windows.Forms.ToolStripMenuItem(i18n.MarkAllAsUnread, Assets.Images.Icons.Png._16.unread, new EventHandler(MenuMarkAllAsUnReadClicked))); 
        }

        public void Update()
        {
            this.Parse();
            foreach (KeyValuePair<string, BlueStory> pair in this.Stories) { pair.Value.CheckForNotifications(); }                
        }

        private void Parse()
        {
            foreach(BlueSource source in this.Sources)
            {
                try
                {
                    WebReader.Result result = WebReader.Read(source.Url);
                    if (result.State != WebReader.States.Success)
                    {
                        this.State = State.Error;
                        this.Icon = new NamedImage("error", Assets.Images.Icons.Png._16.error);
                        return;
                    }

                    var doc = new HtmlDocument();
                    doc.LoadHtml(result.Response);

                    foreach (HtmlNode node in doc.DocumentNode.SelectNodes("//tr[@class='blizzard']"))
                    {
                        HtmlNodeCollection subNodes = node.SelectNodes("td[@class='post-title']//div[@class='desc']//a[@href]");
                        string postForum = subNodes[0].InnerText;
                        string postTitle = subNodes[1].InnerText;
                        string postLink = string.Format("{0}{1}", source.Url, subNodes[1].Attributes["href"].Value);

                        Match m = RegexBlueId.Match(subNodes[1].Attributes["href"].Value);
                        if (!m.Success) continue; // if no match is found, continue the loop with next element.

                        string topicId = m.Groups["TopicID"].Value;
                        string postId = m.Groups["PostID"].Value;

                        var blueStory = new BlueStory(this._type, postTitle, source.Region, postLink, topicId, postId);
                        blueStory.StateChanged += OnChildStateChanged;

                        if (!this.Stories.ContainsKey(topicId)) this.Stories.Add(topicId, blueStory);
                        else this.Stories[topicId].AddSuccessorPost(blueStory);
                    }
                }
                catch (Exception e)
                {
                    LogManager.Instance.Write(LogMessageTypes.Error, string.Format("Module BlizzBlues caught an exception while parsing: {0}", e));
                    return;
                }
            }
        }

        private void OnChildStateChanged(object sender, EventArgs e)
        {
            if (this.State == ((BlueStory)sender).State) return;

            int unread = this.Stories.Count(pair => pair.Value.State == State.Fresh || pair.Value.State == State.Unread);
            this.State=unread > 0 ? State.Unread : State.Read;
        }

        private void MenuMarkAllAsReadClicked(object sender, EventArgs e)
        {
            foreach (KeyValuePair<string, BlueStory> pair in this.Stories)
            {
                pair.Value.State = State.Read;
                foreach (BlueStory post in pair.Value.Successors) { post.State=State.Read; }
            }
        }

        private void MenuMarkAllAsUnReadClicked(object sender, EventArgs e)
        {
            foreach (KeyValuePair<string, BlueStory> pair in this.Stories)
            {
                pair.Value.State = State.Unread;
                foreach (BlueStory post in pair.Value.Successors) { post.State=State.Unread; }
            }
        }
    }

    /// <summary>
    /// Inditicates a Blizzard forum with url and region.
    /// </summary>
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

    public enum BlueType
    {
        WorldofWarcraft,
        Starcraft
    }
}
