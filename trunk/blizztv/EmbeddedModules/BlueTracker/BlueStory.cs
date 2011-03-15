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
using BlizzTV.EmbeddedModules.BlueTracker.Parsers;
using BlizzTV.InfraStructure.Modules;
using BlizzTV.Notifications;
using BlizzTV.Utility.Imaging;
using BlizzTV.Assets.i18n;

namespace BlizzTV.EmbeddedModules.BlueTracker
{
    public class BlueStory : ModuleNode
    {
        public BlueType Type { get; private set; }
        public Region Region { get; private set; }
        public string Link { get; private set; }
        public string TopicId { get; private set; }
        public string PostId { get; private set; }

        public readonly List<BlueStory> Successors = new List<BlueStory>(); // successor posts.
    
        public BlueStory(BlueType type, string title, Region region, string link, string topicId, string postId)
            : base(title)
        {
            this.Type = type;
            this.Region = region;
            this.Link = link;
            this.TopicId = topicId;
            this.PostId = postId;
            this.Guid = string.Format("{0}.{1}#{2}", this.Region, this.TopicId, this.PostId);

            switch (this.Region)
            {
                case Region.Eu:
                    this.Icon = new NamedImage("eu", Assets.Images.Icons.Png._16.eu);
                    break;
                case Region.Us:
                    this.Icon = new NamedImage("us", Assets.Images.Icons.Png._16.us);
                    break;
            }

            this.Menu.Add("markasread", new ToolStripMenuItem(i18n.MarkAsRead, Assets.Images.Icons.Png._16.read, new EventHandler(MenuMarkAsReadClicked)));
            this.Menu.Add("markasunread", new ToolStripMenuItem(i18n.MarkAsUnread, Assets.Images.Icons.Png._16.unread, new EventHandler(MenuMarkAsUnReadClicked))); 
        }

        public void AddSuccessorPost(BlueStory blueStory)
        {
            this.Successors.Add(blueStory);
            blueStory.StateChanged += OnChildStateChange;
        }

        private void OnChildStateChange(object sender, EventArgs e)
        {
            if (this.State == ((BlueStory)sender).State) return;

            int unread = (from ModuleNode node in this.Successors select node.State).Count(state => state == State.Fresh || state == State.Unread);
            this.State = unread > 0 ? State.Unread : State.Read;
        }

        public void CheckForNotifications()
        {
            if (Settings.ModuleSettings.Instance.NotificationsEnabled && this.State ==  State.Fresh) NotificationManager.Instance.Show(this, new NotificationEventArgs(string.Format("{0}", this.Text), string.Format("A new {0} blue-post is available, click to open it.",this.Type) , System.Windows.Forms.ToolTipIcon.Info));
        }

        public override void Open(object sender, EventArgs e)
        {
            this.Navigate();
        }

        public override void NotificationClicked()
        {
            this.Navigate();
        }

        private void Navigate()
        {
            System.Diagnostics.Process.Start(this.Link, null);
            if (this.State != State.Read) this.State = State.Read;
        }

        private void MenuMarkAsReadClicked(object sender, EventArgs e)
        {
            this.State = State.Read;
            foreach (BlueStory post in this.Successors) { post.State = State.Read; }
        }

        private void MenuMarkAsUnReadClicked(object sender, EventArgs e)
        {
            this.State = State.Unread;
            foreach (BlueStory post in this.Successors) { post.State=State.Unread; }
        }
    }
}
