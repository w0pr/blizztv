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
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using System.Xml.XPath;
using System.Text;
using System.Timers;
using LibBlizzTV;
using LibBlizzTV.Utils;

namespace LibStreams
{
    [PluginAttributes("Streams", "Stream aggregator plugin for BlizzTV","stream_16.png")]
    public class StreamsPlugin:Plugin
    {
        #region members

        private ListItem _root_item = new ListItem("Streams"); // root item on treeview.
        internal Dictionary<string,Stream> _streams = new Dictionary<string,Stream>();
        private Timer _update_timer;
        private bool disposed = false;

        public static StreamsPlugin Instance;

        #endregion

        #region ctor

        public StreamsPlugin(PluginSettings ps):base(ps)
        {
            StreamsPlugin.Instance = this;
        }

        #endregion

        #region API handlers

        public override void Run()
        {
            this.RegisterListItem(_root_item); // register root item.                        
            PluginLoadComplete(new PluginLoadCompleteEventArgs(UpdateStreams())); // parse the streams.

            // setup update timer for next data updates
            _update_timer = new Timer((Settings as Settings).UpdateEveryXMinutes * 60000);
            _update_timer.Elapsed += new ElapsedEventHandler(OnTimerHit);
            _update_timer.Enabled = true;
        }

        public override System.Windows.Forms.Form GetPreferencesForm()
        {
            return new frmSettings();
        }

        #endregion

        #region internal logic

        internal bool UpdateStreams()
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
                                  Name = stream.Attribute("Name").Value,
                                  Slug = stream.Element("Slug").Value,
                                  Provider = stream.Element("Provider").Value.ToLower(),
                              };

                foreach (var entry in entries) // create up the stream items.
                {
                    Stream s = StreamFactory.CreateStream(entry.Name, entry.Slug, entry.Provider);
                    this._streams.Add(entry.Name,s);
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

                this.AddWorkload(this._streams.Count);

                foreach (KeyValuePair<string,Stream> pair in this._streams) // loop through all streams
                {
                    try
                    {
                        pair.Value.Update(); // update the stream
                        if (pair.Value.IsLive) // if it's live
                        {
                            pair.Value.SetTitle(string.Format("{0} ({1})", pair.Value.Title, pair.Value.ViewerCount)); // put stream viewers count on title.
                            available_count++; // increment available live streams count.
                            RegisterListItem(pair.Value, _root_item); // register the stream item.
                        }
                        this.StepWorkload();
                    }
                    catch (Exception e) { Log.Instance.Write(LogMessageTypes.ERROR, string.Format("StreamsPlugin ParseStreams() Error: \n {0}", e.ToString())); } // catch errors for inner stream-handlers.
                }

                _root_item.SetTitle(string.Format("Streams ({0})", available_count));  // put available streams count on root object's title.
            }

            return success;
        }

        internal void SaveStreamsXML()
        {
            try
            {
                foreach (KeyValuePair<string, Stream> pair in this._streams)
                {
                    if (pair.Value.CommitOnSave)
                    {
                        XDocument xdoc = XDocument.Load("Streams.xml");
                        xdoc.Element("Streams").Add(new XElement("Stream", new XAttribute("Name", pair.Value.Name), new XElement("Slug", pair.Value.Slug), new XElement("Provider", pair.Value.Provider)));
                        xdoc.Save("Streams.xml");
                    }
                    else if (pair.Value.DeleteOnSave)
                    {
                        XDocument xdoc = XDocument.Load("Streams.xml");
                        xdoc.XPathSelectElement(string.Format("Streams/Stream[@Name='{0}']", pair.Value.Name)).Remove();
                        xdoc.Save("Streams.xml");
                    }
                }
            }
            catch (Exception e)
            {
                Log.Instance.Write(LogMessageTypes.ERROR, string.Format("FeedsPlugin SaveFeedsXML() Error: \n {0}", e.ToString()));
            }
        }

        private void DeleteExistingStreams() // removes all current feeds.
        {
            foreach (KeyValuePair<string,Stream> pair in this._streams) { pair.Value.Delete(); } // Delete the feeds.
            this._streams.Clear(); // remove them from the list.
            GC.Collect();
            GC.WaitForPendingFinalizers();
        }

        private void OnTimerHit(object source, ElapsedEventArgs e)
        {
            PluginDataUpdateComplete(new PluginDataUpdateCompleteEventArgs(UpdateStreams()));
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
                    foreach (KeyValuePair<string,Stream> pair in this._streams) { pair.Value.Dispose(); }
                    this._streams.Clear();
                    this._streams = null;
                }
                base.Dispose(disposing);
            }
        }

        #endregion
    }
}
