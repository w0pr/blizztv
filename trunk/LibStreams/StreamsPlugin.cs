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
using System.Text;
using LibBlizzTV;

namespace LibStreams
{
    [Plugin("LibStreams", "Stream aggregator plugin for BlizzTV","stream_16.png")]
    public class StreamsPlugin:Plugin
    {
        private List<Stream> _streams = new List<Stream>();

        public StreamsPlugin() { }

        public override void Load(PluginSettings ps)
        {
            StreamsPlugin.PluginSettings = ps;

            ListItem root = new ListItem("Streams");
            RegisterListItem(root);            

            XDocument xdoc = XDocument.Load("Streams.xml");
            var entries = from stream in xdoc.Descendants("Stream")
                          select new
                          {
                              Name = stream.Element("Name").Value,
                              Slug = stream.Element("Slug").Value,
                              Provider = stream.Element("Provider").Value,
                          };

            foreach (var entry in entries)
            {
                Stream s = StreamFactory.CreateStream(entry.Name,entry.Slug,entry.Provider);
                this._streams.Add(s);
            }            

            foreach (Stream stream in this._streams)
            {                
                stream.Update();
                if (stream.IsLive) stream.SetTitle(string.Format("{0} ({1})",stream.Title, stream.ViewerCount));                    
                RegisterListItem(stream, root);
            }

            PluginLoadComplete(new PluginLoadCompleteEventArgs(true));
        }
    }
}
