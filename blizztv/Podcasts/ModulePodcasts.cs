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
using BlizzTV.Log;
using BlizzTV.Modules;
using BlizzTV.Utility.Imaging;

namespace BlizzTV.Podcasts
{
    [ModuleAttributes("Podcasts", "Podcast aggregator module.", "podcast")]
    public class ModulePodcasts:Module
    {
        private Dictionary<string, Podcast> _podcasts = new Dictionary<string, Podcast>(); // list of feeds.

        public ModulePodcasts()
        {
            this.RootListItem = new ListItem("Podcasts")
                                    {
                                        Icon = new NamedImage("podcast", Assets.Images.Icons.Png._16.podcast)
                                    };
        }

        public override void Run()
        {
            this.UpdatePodcasts();
        }

        private void UpdatePodcasts()
        {
            if (this.Updating) return;

            this.Updating = true;
            this.NotifyUpdateStarted();


            if (this._podcasts.Count > 0) // clear previous entries before doing an update.
            {
                this._podcasts.Clear();
                this.RootListItem.Childs.Clear();
            }

            this.RootListItem.SetTitle("Updating podcasts..");

            foreach (KeyValuePair<string, PodcastSubscription> pair in Subscriptions.Instance.Dictionary)
            {
                Podcast podcast = new Podcast(pair.Value);
                this._podcasts.Add(pair.Value.Url, podcast);
            }

            Workload.WorkloadManager.Instance.Add(this._podcasts.Count);

            foreach(KeyValuePair<string,Podcast> pair in this._podcasts)
            {
                try
                {
                    pair.Value.Update();
                    this.RootListItem.Childs.Add(pair.Key, pair.Value);
                    foreach (Chapter chapter in pair.Value.Chapters) { pair.Value.Childs.Add(chapter.Guid, chapter); } // register the chapter items.
                }
                catch (Exception e) { LogManager.Instance.Write(LogMessageTypes.Error, string.Format("Module podcasts caught an exception while updating feeds: {0}", e)); }
                Workload.WorkloadManager.Instance.Step();
            }

            this.RootListItem.SetTitle("Podcasts");
            this.NotifyUpdateComplete(new PluginUpdateCompleteEventArgs(true));
            this.Updating = false;
        }
    }
}
