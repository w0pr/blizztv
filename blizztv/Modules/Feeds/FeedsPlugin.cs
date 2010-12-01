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
 * $Id: FeedsPlugin.cs 158 2010-11-30 14:06:50Z shalafiraistlin@gmail.com $
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using System.Xml.XPath;
using System.Timers;
using BlizzTV.ModuleLib;
using BlizzTV.ModuleLib.Utils;
using BlizzTV.ModuleLib.Settings;
using BlizzTV.ModuleLib.Notifications;

namespace BlizzTV.Modules.Feeds
{
    [ModuleAttributes("Feeds","Feed aggregator plugin.","feed_16")]
    public class FeedsPlugin:Module
    {
        #region members

        private string _xml_file = @"modules\feeds\xml\feeds.xml";
        internal Dictionary<string,Feed> _feeds = new Dictionary<string,Feed>(); // the feeds list 
        private Timer _update_timer;
        private bool _updating = false;
        private bool disposed = false;

        public static FeedsPlugin Instance;

        #endregion

        #region ctor

        public FeedsPlugin(): base()
        {
            FeedsPlugin.Instance = this;
            this.RootListItem = new ListItem("Feeds");

            // register context menu's.
            this.RootListItem.ContextMenus.Add("manualupdate", new System.Windows.Forms.ToolStripMenuItem("Update Feeds", null, new EventHandler(RunManualUpdate))); // mark as unread menu.
            this.RootListItem.ContextMenus.Add("markallasread", new System.Windows.Forms.ToolStripMenuItem("Mark All As Read", null, new EventHandler(MenuMarkAllAsReadClicked))); // mark as read menu.
            this.RootListItem.ContextMenus.Add("markallasunread", new System.Windows.Forms.ToolStripMenuItem("Mark All As Unread", null, new EventHandler(MenuMarkAllAsUnReadClicked))); // mark as unread menu.            
        }

        #endregion

        #region API handlers

        public override void Run()
        {
            this.UpdateFeeds();
            this.SetupUpdateTimer();
        }

        public override System.Windows.Forms.Form GetPreferencesForm()
        {
            return new frmSettings();
        }

        #endregion

        #region internal logic

        internal void UpdateFeeds()
        {
            bool success = true;

            if (!this._updating)
            {
                this._updating = true;
                this.NotifyUpdateStarted();

                if (this._feeds.Count > 0) // clear previous entries before doing an update.
                {
                    this._feeds.Clear();
                    this.RootListItem.Childs.Clear();
                }

                this.RootListItem.SetTitle("Updating feeds..");

                try
                {
                    XDocument xdoc = XDocument.Load(this._xml_file); // load the xml.
                    var entries = from feed in xdoc.Descendants("Feed") // get the feeds.
                                  select new
                                  {
                                      Title = feed.Attribute("Name").Value,
                                      URL = feed.Element("URL").Value,
                                  };

                    foreach (var entry in entries) // create up the feed items.
                    {
                        Feed f = new Feed(entry.Title, entry.URL);
                        this._feeds.Add(f.Name, f);
                    }
                }
                catch (Exception e)
                {
                    success = false;
                    Log.Instance.Write(LogMessageTypes.ERROR, string.Format("FeedsPlugin ParseFeeds() Error: \n {0}", e.ToString()));
                    System.Windows.Forms.MessageBox.Show(string.Format("An error occured while parsing your feeds.xml. Please correct the error and re-start the plugin. \n\n[Error Details: {0}]", e.Message), "Feeds Plugin Error", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                }

                if (success) // if parsing of feeds.xml all okay.
                {
                    int unread = 0; // feeds with unread stories count.

                    this.AddWorkload(this._feeds.Count);

                    foreach (KeyValuePair<string, Feed> pair in this._feeds) // loop through feeds.
                    {
                        try
                        {
                            pair.Value.Update(); // update the feed.
                            this.RootListItem.Childs.Add(pair.Key, pair.Value);
                            foreach (Story story in pair.Value.Stories) { pair.Value.Childs.Add(story.GUID, story); } // register the story items.
                            if (pair.Value.State == ItemState.UNREAD) unread++;                            
                        }
                        catch (Exception e) { Log.Instance.Write(LogMessageTypes.ERROR, string.Format("Feed Plugin - UpdateFeeds Exception: {0}", e.ToString())); }
                        this.StepWorkload();
                    }

                    this.RootListItem.SetTitle(string.Format("Feeds ({0})", unread.ToString()));  // add unread feeds count to root item's title.

                    foreach (KeyValuePair<string, Feed> pair in this._feeds)
                    {
                        foreach(KeyValuePair<string,ListItem> child_pair in pair.Value.Childs)
                        {
                            if (child_pair.Value.State == ItemState.FRESH)
                            {
                                Notifications.Instance.Show(child_pair.Value, child_pair.Value.Title, "Click to read.", System.Windows.Forms.ToolTipIcon.Info);
                                break;
                            }
                        }
                    }
                }                
                this.NotifyUpdateComplete(new PluginUpdateCompleteEventArgs(success));
                this._updating = false;
            }
        }

        internal void SaveFeedsXML()
        {
            try
            {
                foreach (KeyValuePair<string,Feed> pair in this._feeds)
                {
                    if (pair.Value.CommitOnSave)
                    {
                        XDocument xdoc = XDocument.Load(this._xml_file);
                        xdoc.Element("Feeds").Add(new XElement("Feed", new XAttribute("Name", pair.Value.Name), new XElement("URL", pair.Value.URL)));
                        xdoc.Save(this._xml_file);
                    }
                    else if (pair.Value.DeleteOnSave)
                    {
                        XDocument xdoc = XDocument.Load(this._xml_file);
                        xdoc.XPathSelectElement(string.Format("Feeds/Feed[@Name='{0}']", pair.Value.Name)).Remove();
                        xdoc.Save(this._xml_file);
                    }
                }
                this.RunManualUpdate(this, EventArgs.Empty);
            }
            catch (Exception e)
            {
                Log.Instance.Write(LogMessageTypes.ERROR, string.Format("FeedsPlugin SaveFeedsXML() Error: \n {0}", e.ToString()));
            }
        }

        public void SaveSettings()
        {
            Settings.Instance.Save();
            this.SetupUpdateTimer();
        }

        private void SetupUpdateTimer()
        {
            if (this._update_timer != null)
            {
                this._update_timer.Enabled = false;
                this._update_timer.Elapsed -= OnTimerHit;
                this._update_timer = null;
            }

            _update_timer = new Timer(Settings.Instance.UpdateEveryXMinutes * 60000);
            _update_timer.Elapsed += new ElapsedEventHandler(OnTimerHit);
            _update_timer.Enabled = true;
        }

        private void OnTimerHit(object source, ElapsedEventArgs e)
        {
            if (!GlobalSettings.Instance.InSleepMode) this.UpdateFeeds();
        }

        private void MenuMarkAllAsReadClicked(object sender, EventArgs e)
        {
            foreach (KeyValuePair<string, Feed> pair in this._feeds)
            {
                pair.Value.State = ItemState.READ;
                foreach (Story s in pair.Value.Stories) { s.State = ItemState.READ; }
            }
        }

        private void MenuMarkAllAsUnReadClicked(object sender, EventArgs e)
        {
            foreach (KeyValuePair<string, Feed> pair in this._feeds)
            {
                pair.Value.State = ItemState.UNREAD;
                foreach (Story s in pair.Value.Stories) { s.State = ItemState.UNREAD; }
            }
        }

        private void RunManualUpdate(object sender, EventArgs e)
        {
            System.Threading.Thread t = new System.Threading.Thread(delegate() { this.UpdateFeeds(); }) 
            { IsBackground = true, Name = string.Format("plugin-{0}-{1}", this.Attributes.Name, DateTime.Now.TimeOfDay.ToString()) };
            t.Start();                 
        }

        #endregion

        #region de-ctor

        protected override void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing) // managed resources
                {
                    this._update_timer.Enabled = false;
                    this._update_timer.Elapsed -= OnTimerHit;
                    this._update_timer.Dispose();
                    this._update_timer = null;
                    foreach (KeyValuePair<string,Feed> pair in this._feeds) { pair.Value.Dispose(); }
                    this._feeds.Clear();
                    this._feeds = null;
                }
                base.Dispose(disposing);
            }            
        }

        #endregion
    }
}
