﻿/*   
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
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using System.Text;
using System.Timers;
using LibBlizzTV;
using LibBlizzTV.Utils;

namespace LibStreams
{
    [Plugin("Streams", "Stream aggregator plugin for BlizzTV","stream_16.png")]
    public class StreamsPlugin:Plugin
    {
        #region members

        private ListItem _root_item = new ListItem("Streams"); // root item on treeview.
        private List<Stream> _streams = new List<Stream>();
        private bool disposed = false;

        #endregion

        #region ctor

        public StreamsPlugin()
        {
            this.Menus.Add("subscriptions", new System.Windows.Forms.ToolStripMenuItem("Subscriptions", null, new EventHandler(MenuSubscriptionsClicked))); // register subscriptions menu.         
        }

        #endregion

        #region API handlers

        public override void Load(PluginSettings ps)
        {
            StreamsPlugin.PluginSettings = ps;
            this.RegisterListItem(_root_item); // register root item.                        
            PluginLoadComplete(new PluginLoadCompleteEventArgs(UpdateStreams())); // parse the streams.

            // setup update timer for next data updates
            Timer update_timer = new Timer(1000 * 60 * 5);
            update_timer.Elapsed += new ElapsedEventHandler(OnTimerHit);
            update_timer.Enabled = true;
        }

        #endregion

        #region internal logic

        private bool UpdateStreams()
        {
            bool success = true;

            this._root_item.SetTitle("Updating streams..");
            if (this._streams.Count > 0) this.DeleteExistingStreams(); // clear previous entries before doing an update.

            try
            {
                XDocument xdoc = XDocument.Load("Streams.xml"); // load the xml.
                var entries = from stream in xdoc.Descendants("Stream") // get the streams.
                              select new
                              {
                                  Name = stream.Element("Name").Value,
                                  Slug = stream.Element("Slug").Value,
                                  Provider = stream.Element("Provider").Value.ToLower(),
                              };

                foreach (var entry in entries) // create up the stream items.
                {
                    Stream s = StreamFactory.CreateStream(entry.Name, entry.Slug, entry.Provider);
                    this._streams.Add(s);
                }
            }
            catch (Exception e)
            {
                success = false;
                Log.Instance.Write(LogMessageTypes.ERROR, string.Format("StreamsPlugin ParseStreams() Error: \n {0}", e.ToString()));
                System.Windows.Forms.MessageBox.Show(string.Format("An error occured while parsing your streams.xml. Please correct the error and re-start the plugin. \n\n[Error Details: {0}]", e.Message), "Streams Plugin Error", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
            }

            if (success) // if parsing of streams.xml all okay
            {
                int available_count = 0; // available live streams count

                foreach (Stream stream in this._streams) // loop through all streams
                {
                    try
                    {
                        stream.Update(); // update the stream
                        if (stream.IsLive) // if it's live
                        {
                            stream.SetTitle(string.Format("{0} ({1})", stream.Title, stream.ViewerCount)); // put stream viewers count on title.
                            available_count++; // increment available live streams count.
                            RegisterListItem(stream, _root_item); // register the stream item.
                        }
                    }
                    catch (Exception e) { Log.Instance.Write(LogMessageTypes.ERROR, string.Format("StreamsPlugin ParseStreams() Error: \n {0}", e.ToString())); } // catch errors for inner stream-handlers.
                }

                _root_item.SetTitle(string.Format("Streams ({0})", available_count));  // put available streams count on root object's title.
            }

            return success;
        }

        private void DeleteExistingStreams() // removes all current feeds.
        {
            foreach (Stream s in this._streams) { s.Delete(); } // Delete the feeds.
            this._streams.Clear(); // remove them from the list.
            GC.Collect();
            GC.WaitForPendingFinalizers();
        }

        private void OnTimerHit(object source, ElapsedEventArgs e)
        {
            PluginDataUpdateComplete(new PluginDataUpdateCompleteEventArgs(UpdateStreams()));
        }

        public void MenuSubscriptionsClicked(object sender, EventArgs e) // subscriptions menu handler
        {
            frmDataEditor f = new frmDataEditor("Streams.xml", "Stream");
            f.Show();
        }

        #endregion

        #region de-ctor

        ~StreamsPlugin() { Dispose(false); }

        private void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing) // managed resources
                {
                    foreach (Stream s in this._streams) { s.Dispose(); }
                    this._streams.Clear();
                    this._streams = null;
                }
                disposed = true;
            }
        }

        #endregion
    }
}