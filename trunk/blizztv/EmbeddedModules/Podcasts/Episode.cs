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
using System.IO;
using System.Windows.Forms;
using BlizzTV.Assets.i18n;
using BlizzTV.Downloads;
using BlizzTV.EmbeddedModules.Podcasts.Parsers;
using BlizzTV.EmbeddedModules.Podcasts.UI;
using BlizzTV.InfraStructure.Modules;
using BlizzTV.Notifications;
using BlizzTV.Settings;
using BlizzTV.Utility.Imaging;

namespace BlizzTV.EmbeddedModules.Podcasts
{
    public class Episode : ModuleNode
    {
        private PlayerForm _player = null;

        public string PodcastName { get; private set; }
        public string Link { get; private set; }
        private string Enclosure { get; set; }

        private bool Downloaded
        {
            get { return File.Exists(string.Format("{0}\\{1}\\{2}", PodcastsStoragePath, this.PodcastName, Path.GetFileName(this.Enclosure))); }
        }

        public string MediaLocation
        {
            get { return this.Downloaded ? string.Format("{0}\\{1}\\{2}", PodcastsStoragePath, this.PodcastName, Path.GetFileName(this.Enclosure)) : this.Enclosure; }
        }

        private static readonly string PodcastsStoragePath;

        public Episode(string podcastName, PodcastItem item)
            : base(item.Title)
        {
            this.PodcastName = podcastName;
            this.Link = item.Link;
            this.Enclosure = item.Enclosure;
            this.Guid = item.Id;

            this.Icon = new NodeIcon("podcast", Assets.Images.Icons.Png._16.podcast);
            this.RememberState = true;

            this.Menu.Add("markasread", new ToolStripMenuItem(i18n.MarkAsRead, Assets.Images.Icons.Png._16.read, new EventHandler(MenuMarkAsReadClicked)));
            this.Menu.Add("markasunread", new ToolStripMenuItem(i18n.MarkAsUnread, Assets.Images.Icons.Png._16.unread, new EventHandler(MenuMarkAsUnReadClicked)));
            var menuDownloadEpisode = new ToolStripMenuItem(i18n.DownloadEpisode, Assets.Images.Icons.Png._16.download,new EventHandler(MenuDownloadEpisode));
            this.Menu.Add("download", menuDownloadEpisode);
            if (this.Downloaded) menuDownloadEpisode.Text = i18n.ReDownloadPodcastEpisode;
        }

        static Episode()
        {
            PodcastsStoragePath = string.Format("{0}\\Podcasts", Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments));
            if (!Directory.Exists(PodcastsStoragePath)) Directory.CreateDirectory(PodcastsStoragePath); // if directory does not exist, create it.   
        }

        public void CheckForNotifications()
        {
            if (Settings.ModuleSettings.Instance.NotificationsEnabled && this.State == State.Fresh) NotificationManager.Instance.Show(this, new NotificationEventArgs(this.Text, string.Format("A new podcast episode is available on {0}, click to open it.", this.PodcastName), System.Windows.Forms.ToolTipIcon.Info));
        }

        public override void Open(object sender, EventArgs e)
        {
            this.Play();
        }

        public override void NotificationClicked()
        {
            this.Play();
        }

        private void Play()
        {
            if (GlobalSettings.Instance.UseInternalViewers) // if internal-viewers method is selected
            {
                if (this._player == null)
                {
                    this._player = new PlayerForm(this);
                    this._player.FormClosed += PlayerClosed;
                    this._player.Show();
                }
                else this._player.Focus();
            }
            else System.Diagnostics.Process.Start(this.MediaLocation, null);
            if (this.State != State.Read) this.State = (State.Read);
        }

        void PlayerClosed(object sender, FormClosedEventArgs e)
        {
            this._player = null;
        }

        public override void RightClicked(object sender, EventArgs e) // manage the context-menus based on our item state.
        {
            // make conditional context-menus invisible.
            this.Menu["markasread"].Enabled = false;
            this.Menu["markasunread"].Enabled = false;

            switch (this.State)
            {
                case State.Fresh:
                case State.Unread:
                    this.Menu["markasread"].Enabled = true;
                    break;
                case State.Read:
                    this.Menu["markasunread"].Enabled = true;
                    break;
            }
        }

        private void MenuMarkAsReadClicked(object sender, EventArgs e)
        {
            this.State=State.Read;
        }

        private void MenuMarkAsUnReadClicked(object sender, EventArgs e)
        {
            this.State = State.Unread;
        }

        private void MenuDownloadEpisode(object sender, EventArgs e)
        {
            var podcastDirectory = string.Format("{0}\\{1}", PodcastsStoragePath, this.PodcastName);
            if (!Directory.Exists(podcastDirectory)) Directory.CreateDirectory(podcastDirectory);

            var downloadForm = new DownloadForm(string.Format(i18n.DownloadingPodcastEpisode, this.Text));
            downloadForm.StartDownload(new Download(this.Enclosure,string.Format("{0}\\{1}", podcastDirectory,Path.GetFileName(this.Enclosure))));
            downloadForm.Show();
        }
    }
}
