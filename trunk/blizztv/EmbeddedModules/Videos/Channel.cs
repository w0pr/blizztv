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
using BlizzTV.InfraStructure.Modules;
using BlizzTV.Utility.Imaging;

namespace BlizzTV.EmbeddedModules.Videos
{
    public class Channel : ModuleNode
    {
        private bool _disposed = false;

        protected string Slug { get; set; }
        protected string Provider { get; set; }

        public readonly List<Video> Videos = new List<Video>();

        public Channel(VideoSubscription subscription)
            : base(subscription.Name)
        {
            this.Text = subscription.Name;
            this.Slug = subscription.Slug;
            this.Provider = subscription.Provider;

            this.Icon = new NodeIcon("video", Assets.Images.Icons.Png._16.video);

            this.Menu.Add("markaswatched", new ToolStripMenuItem(i18n.MarkAsWatched, Assets.Images.Icons.Png._16.read, new EventHandler(MenuMarkAllAsWatchedClicked)));
            this.Menu.Add("markasunwatched", new ToolStripMenuItem(i18n.MarkAsUnwatched, Assets.Images.Icons.Png._16.unread, new EventHandler(MenuMarkAllAsUnWatchedClicked))); 
        }

        public bool IsValid()
        {
            return this.Parse();
        }

        public bool Update()
        {
            if (this.Parse())
            {
                foreach (Video video in this.Videos) { video.CheckForNotifications(); }
                return true;
            }

            return false;
        }

        public virtual bool Parse() { throw new NotImplementedException(); }

        private void MenuMarkAllAsWatchedClicked(object sender, EventArgs e)
        {
            foreach (Video video in this.Nodes) { video.State=State.Read; } // marked all videos as watched.
        }

        private void MenuMarkAllAsUnWatchedClicked(object sender, EventArgs e)
        {
            foreach (Video video in this.Nodes) { video.State=State.Read; } // marked all videos as un-watched.
        }

        protected void OnChildStateChanged(object sender, EventArgs e)
        {
            if (this.State == ((Video)sender).State) return;

            int unread = (from ModuleNode node in this.Videos select node.State).Count(state => state == State.Fresh || state == State.Unread);
            this.State = unread > 0 ? State.Unread : State.Read;
        }
    }
}
