﻿/*    
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
using BlizzTV.Log;
using BlizzTV.Modules;
using BlizzTV.Utility.Imaging;
using BlizzTV.Utility.Web;
using HtmlAgilityPack;

namespace BlizzTV.BlizzBlues.Parsers
{
    /// <summary>
    /// Parser for Blizzard forums that can extract Blue-GM posts.
    /// </summary>
    public class BlueParser:ListItem
    {
        private static readonly Regex RegexBlueId = new Regex(@"\.\./topic/(?<TopicID>.*?)(\?page\=.*?)?#(?<PostID>.*)", RegexOptions.Compiled); // The post details regex.
        private readonly BlueType _type;        
        protected BlueSource[] Sources;
        
        public readonly Dictionary<string,BlueStory> Stories = new Dictionary<string,BlueStory>(); // list of stories.

        public BlueParser(BlueType type)
            : base(type.ToString())
        {
            this._type = type;

            // register context menus.
            this.ContextMenus.Add("markallasread", new System.Windows.Forms.ToolStripMenuItem("Mark As Read", Assets.Images.Icons.Png._16.read, new EventHandler(MenuMarkAllAsReadClicked))); // mark as read menu.
            this.ContextMenus.Add("markallasunread", new System.Windows.Forms.ToolStripMenuItem("Mark As Unread", Assets.Images.Icons.Png._16.unread, new EventHandler(MenuMarkAllAsUnReadClicked))); // mark as unread menu.
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

                    HtmlDocument doc = new HtmlDocument();
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

                        BlueStory b = new BlueStory(this._type, postTitle, source.Region, postLink, topicId, postId);
                        b.OnStateChange += OnChildStateChange;

                        if (!this.Stories.ContainsKey(topicId)) this.Stories.Add(topicId, b);
                        else this.Stories[topicId].AddSuccessorPost(b);
                    }
                }
                catch (Exception e)
                {
                    LogManager.Instance.Write(LogMessageTypes.Error, string.Format("Module BlizzBlues caught an exception while parsing: {0}", e));
                    return;
                }
            }
        }

        private void OnChildStateChange(object sender, EventArgs e)
        {
            if (this.State == ((BlueStory) sender).State) return;

            int unread = this.Stories.Count(pair => pair.Value.State == State.Fresh || pair.Value.State == State.Unread);
            this.State = unread > 0 ? State.Unread : State.Read;
        }

        private void MenuMarkAllAsReadClicked(object sender, EventArgs e)
        {
            foreach (KeyValuePair<string, BlueStory> pair in this.Stories)
            {
                pair.Value.State = State.Read;
                foreach (KeyValuePair<string, BlueStory> post in pair.Value.Successors) { post.Value.State = State.Read; }
            }
        }

        private void MenuMarkAllAsUnReadClicked(object sender, EventArgs e)
        {
            foreach (KeyValuePair<string, BlueStory> pair in this.Stories)
            {
                pair.Value.State = State.Unread;
                foreach (KeyValuePair<string, BlueStory> post in pair.Value.Successors) { post.Value.State = State.Unread; }
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