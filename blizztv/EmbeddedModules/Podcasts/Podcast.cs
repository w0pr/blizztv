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
using BlizzTV.EmbeddedModules.Podcasts.Parsers;
using BlizzTV.InfraStructure.Modules;
using BlizzTV.Log;
using BlizzTV.Utility.Imaging;

namespace BlizzTV.EmbeddedModules.Podcasts
{
    public class Podcast : ListItem
    {
        /// <summary>
        /// Feed Name.
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        /// Feed Url.
        /// </summary>
        public string Url { get; private set; }

        /// <summary>
        /// The feed's stories.
        /// </summary>
        public List<Episode> Episodes = new List<Episode>();

        public Podcast(PodcastSubscription subscription)
            : base(subscription.Name)
        {
            this.Name = subscription.Name;
            this.Url = subscription.Url;

            this.ContextMenus.Add("markasread", new ToolStripMenuItem(i18n.MarkAsRead, Assets.Images.Icons.Png._16.read, new EventHandler(MenuMarkAllAsReadClicked)));
            this.ContextMenus.Add("markasunread", new ToolStripMenuItem(i18n.MarkAsUnread, Assets.Images.Icons.Png._16.unread, new EventHandler(MenuMarkAllAsUnReadClicked)));

            this.Icon = new NamedImage("podcast", Assets.Images.Icons.Png._16.podcast);
        }

        public bool IsValid()
        {
            return this.Parse();
        }

        public bool Update()
        {
            if (this.Parse())
            {
                foreach (Episode episode in this.Episodes) { episode.CheckForNotifications(); }
                return true;
            }
            return false;
        }

        private bool Parse()
        {
            List<PodcastItem> items = null;

            if (!PodcastParser.Instance.Parse(this.Url, ref items))
            {
                this.State = State.Error;
                this.Icon = new NamedImage("error", Assets.Images.Icons.Png._16.error);
                return false;
            }

            foreach (PodcastItem item in items)
            {
                try
                {
                    Episode episode = new Episode(this.Title, item);
                    episode.OnStateChange += OnChildStateChange;
                    this.Episodes.Add(episode);
                }
                catch (Exception e) { LogManager.Instance.Write(LogMessageTypes.Error, string.Format("Podcast parser caught an exception: {0}", e)); }
            }
            return true;
        }

        private void OnChildStateChange(object sender, EventArgs e)
        {
            if (this.State == ((Episode)sender).State) return;

            int unread = this.Episodes.Count(episode => episode.State == State.Fresh || episode.State == State.Unread);
            this.State = unread > 0 ? State.Unread : State.Read;
        }

        private void MenuMarkAllAsReadClicked(object sender, EventArgs e)
        {
            foreach (Episode episode in this.Episodes) { episode.State = State.Read; } // marked all episodes as read.
        }

        private void MenuMarkAllAsUnReadClicked(object sender, EventArgs e)
        {
            foreach (Episode episode in this.Episodes) { episode.State = State.Unread; } // marked all episodes as unread.
        }
    }
}
