/*    
 * Copyright (C) 2010, BlizzTV Project - http://code.google.com/p/blizztv/
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
using System.Text;
using System.Text.RegularExpressions;
using BlizzTV.Log;
using HtmlAgilityPack;
using BlizzTV.ModuleLib;
using BlizzTV.CommonLib.Web;
using BlizzTV.CommonLib.Utils;

namespace BlizzTV.Modules.BlizzBlues.Game
{
    public class BlueParser:ListItem
    {
        private static Regex RegexBlueId = new Regex(@"\.\./topic/(?<TopicID>.*?)(\?page\=.*?)?#(?<PostID>.*)", RegexOptions.Compiled);
        protected BlueSource[] Sources;

        public BlueType Type;
        public Dictionary<string,BlueStory> Stories = new Dictionary<string,BlueStory>();

        public BlueParser(BlueType type)
            : base(type.ToString())
        {
            // register context menus.
            this.ContextMenus.Add("markallasread", new System.Windows.Forms.ToolStripMenuItem("Mark As Read", Assets.Images.Icons.Png._16.read, new EventHandler(MenuMarkAllAsReadClicked))); // mark as read menu.
            this.ContextMenus.Add("markallasunread", new System.Windows.Forms.ToolStripMenuItem("Mark As Unread", Assets.Images.Icons.Png._16.unread, new EventHandler(MenuMarkAllAsUnReadClicked))); // mark as unread menu.
        }

        public void Update()
        {
            this.Parse();
            foreach (KeyValuePair<string, BlueStory> pair in this.Stories) { pair.Value.CheckForNotifications(); }                
        }

        internal bool Parse()
        {
            foreach(BlueSource source in this.Sources)
            {
                try
                {
                    WebReader.Result result = WebReader.Read(source.Url);
                    if (result.Status != WebReader.Status.Success)
                    {
                        this.State = ModuleLib.State.Error;
                        this.Icon = new NamedImage("error", Assets.Images.Icons.Png._16.error);
                        return false;
                    }

                    HtmlDocument doc = new HtmlDocument();
                    doc.LoadHtml(result.Response);

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

                            BlueStory b = new BlueStory(this.Type, postTitle, source.Region, postLink, topicId, postId);
                            b.OnStateChange += OnChildStateChange;

                            if (!this.Stories.ContainsKey(topicId)) this.Stories.Add(topicId, b);
                            else this.Stories[topicId].AddPost(b);
                        }
                    }
                }
                catch (Exception e)
                {
                    LogManager.Instance.Write(LogMessageTypes.Error, string.Format("BlueParser error: {0}", e));
                    return false;
                }
            }

            return true;
        }

        private void OnChildStateChange(object sender, EventArgs e)
        {
            if (this.State == (sender as BlueStory).State) return;
            int unread = this.Stories.Count(pair => pair.Value.State == ModuleLib.State.Fresh || pair.Value.State == ModuleLib.State.Unread);
            this.State = unread > 0 ? ModuleLib.State.Unread : ModuleLib.State.Read;
        }

        private void MenuMarkAllAsReadClicked(object sender, EventArgs e)
        {
            foreach (KeyValuePair<string, BlueStory> pair in this.Stories)
            {
                pair.Value.State = ModuleLib.State.Read;
                foreach (KeyValuePair<string, BlueStory> post in pair.Value.More) { post.Value.State = State.Read; }
            }
        }

        private void MenuMarkAllAsUnReadClicked(object sender, EventArgs e)
        {
            foreach (KeyValuePair<string, BlueStory> pair in this.Stories)
            {
                pair.Value.State = ModuleLib.State.Unread;
                foreach (KeyValuePair<string, BlueStory> post in pair.Value.More) { post.Value.State = State.Unread; }
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

    public enum BlueType
    {
        WOW,
        Starcraft
    }
}
