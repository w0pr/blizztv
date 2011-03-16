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
using System.Windows.Forms;
using BlizzTV.Assets.i18n;
using BlizzTV.EmbeddedModules.Feeds.Parsers;
using BlizzTV.InfraStructure.Modules;
using BlizzTV.Log;
using BlizzTV.Utility.Imaging;

namespace BlizzTV.EmbeddedModules.Feeds
{
    public class Feed : ModuleNode
    {
        private bool _disposed = false;

        /// <summary>
        /// Feed Url.
        /// </summary>
        public string Url { get; private set; }

        public List<Story> Stories = new List<Story>();

        public Feed(FeedSubscription subscription)
            : base(subscription.Name)
        {
            this.Text = subscription.Name;
            this.Url = subscription.Url;

            this.Icon = new NodeIcon("feed", Assets.Images.Icons.Png._16.feed);

            this.Menu.Add("markasread", new ToolStripMenuItem(i18n.MarkAsRead, Assets.Images.Icons.Png._16.read, new EventHandler(MenuMarkAllAsReadClicked))); 
            this.Menu.Add("markasunread", new ToolStripMenuItem(i18n.MarkAsUnread, Assets.Images.Icons.Png._16.unread, new EventHandler(MenuMarkAllAsUnReadClicked)));             
        }

        public bool IsValid()
        {
            return this.Parse();
        }

        public bool Update()
        {
            if (this.Parse())
            {
                foreach (Story story in this.Stories) { story.CheckForNotifications(); }
                return true;
            }
            return false;
        }

        private bool Parse()
        {
            var items = new List<FeedItem>();

            if (!FeedParser.Instance.Parse(this.Url, ref items))
            {
                this.State = State.Error;
                this.Icon = new NodeIcon("error", Assets.Images.Icons.Png._16.error);
                return false;
            }

            foreach(FeedItem item in items)
            {
                try
                {
                    var story = new Story(this.Text, item);
                    story.StateChanged += OnChildStateChanged;
                    this.Stories.Add(story);
                }
                catch(Exception e)
                {
                    LogManager.Instance.Write(LogMessageTypes.Error, string.Format("Feed parser caught an exception: {0}", e));
                }
            }

            return true;
        }

        private void OnChildStateChanged(object sender, EventArgs e)
        {
            if (this.State == ((Story)sender).State) return;

            int unread = (from ModuleNode node in this.Stories select node.State).Count(state => state == State.Fresh || state == State.Unread);
            this.State=unread > 0 ? State.Unread : State.Read;
        }

        private void MenuMarkAllAsReadClicked(object sender, EventArgs e)
        {
            foreach (Story story in this.Nodes) { story.State=State.Read; } // marked all stories as read.
        }

        private void MenuMarkAllAsUnReadClicked(object sender, EventArgs e)
        {
            foreach (Story story in this.Nodes) { story.State=State.Unread; } // marked all stories as read.
        }
    }
}
