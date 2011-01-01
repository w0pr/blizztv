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
using BlizzTV.ModuleLib;
using BlizzTV.ModuleLib.StatusStorage;
using BlizzTV.CommonLib.Notifications;
using BlizzTV.CommonLib.Utils;

namespace BlizzTV.Modules.BlizzBlues.Game
{
    public class BlueStory:ListItem
    {
        public BlueType Type { get; private set; }
        public Region Region { get; private set; }
        public string Link { get; private set; }
        public string TopicId { get; private set; }
        public string PostId { get; private set; }
        public Dictionary<string, BlueStory> More = new Dictionary<string, BlueStory>();
    
        public BlueStory(BlueType type, string title, Region region, string link, string topicId, string postId)
            : base(title)
        {
            this.Type = type;
            this.Region = region;
            this.Link = link;
            this.TopicId = topicId;
            this.PostId = postId;
            this.Guid = string.Format("{0}.{1}#{2}", this.Region, this.TopicId, this.PostId);

            // register context menus.
            this.ContextMenus.Add("markasread", new System.Windows.Forms.ToolStripMenuItem("Mark As Read", Assets.Images.Icons.Png._16.read, new EventHandler(MenuMarkAsReadClicked))); // mark as read menu.
            this.ContextMenus.Add("markasunread", new System.Windows.Forms.ToolStripMenuItem("Mark As Unread", Assets.Images.Icons.Png._16.unread, new EventHandler(MenuMarkAsUnReadClicked))); // mark as unread menu.                            

            if (this.Region == Game.Region.Eu) this.Icon = new NamedImage("eu", Assets.Images.Icons.Png._16.eu);
            else if (this.Region == Game.Region.Us) this.Icon = new NamedImage("us", Assets.Images.Icons.Png._16.us);
        }

        public void AddPost(BlueStory blueStory)
        {
            this.More.Add(blueStory.PostId,blueStory);
            blueStory.OnStateChange += OnChildStateChange;
        }

        private void OnChildStateChange(object sender, EventArgs e)
        {
            if (this.State == (sender as BlueStory).State) return;
            int unread = this.More.Count(pair => pair.Value.State == ModuleLib.State.Fresh || pair.Value.State == ModuleLib.State.Unread);
            this.State = unread > 0 ? State.Unread : State.Read;
        }

        public void CheckForNotifications()
        {
            if (Settings.Instance.NotificationsEnabled && this.State ==  ModuleLib.State.Fresh) NotificationManager.Instance.Show(this, new NotificationEventArgs(string.Format("{0}", this.Title), string.Format("A new {0} blue-post is available, click to open it.",this.Type) , System.Windows.Forms.ToolTipIcon.Info));
        }

        public override void DoubleClicked(object sender, System.EventArgs e)
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
            if (this.State != ModuleLib.State.Read) this.State = ModuleLib.State.Read;
        }

        private void MenuMarkAsReadClicked(object sender, EventArgs e)
        {
            this.State = ModuleLib.State.Read;
            foreach (KeyValuePair<string, BlueStory> post in this.More) { post.Value.State = State.Read; }
        }

        private void MenuMarkAsUnReadClicked(object sender, EventArgs e)
        {
            this.State = ModuleLib.State.Unread;
            foreach (KeyValuePair<string, BlueStory> post in this.More) { post.Value.State = State.Unread; }
        }
    }
}
