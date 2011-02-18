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
using BlizzTV.Audio;
using BlizzTV.Modules;
using BlizzTV.Settings;
using BlizzTV.Utility.Imaging;

namespace BlizzTV.Podcasts
{
    public class Chapter:ListItem
    {
        public string PodcastName { get; private set; }
        public string Link { get; private set; }
        public string AudioLink { get; private set; }

        public Chapter(string podcastName, PodcastItem item)
            : base(item.Title)
        {
            this.PodcastName = podcastName;
            this.Link = item.Link;
            this.AudioLink = item.Enclosure;
            this.Guid = item.Id;

            this.Icon = new NamedImage("podcast", Assets.Images.Icons.Png._16.podcast);
        }

        public override void Open(object sender, EventArgs e)
        {
            this.Navigate();
        }

        private void Navigate()
        {
            if (GlobalSettings.Instance.UseInternalViewers) // if internal-viewers method is selected
            {
                AudioManager.Instance.PlayInternetStream(this.AudioLink);
            }
            else System.Diagnostics.Process.Start(this.AudioLink, null); 
        }

    }
}
