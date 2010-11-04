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
using System.Threading;

namespace LibBlizzTV.Streams
{
    public class LoadStreamsCompletedEventArgs : EventArgs
    {
        private bool _success=false;
        private int _count=0;

        public bool Success { get { return this._success; } }
        public int Count { get { return this._count; } }

        public LoadStreamsCompletedEventArgs(bool Success, int Count)
        {
            this._success = Success;
            this._count = Count;
        }
    }

    public class UpdateStreamsCompletedEventsArgs : EventArgs
    {
        private bool _success=false;
        private int _count=0;

        public bool Success { get { return this._success; } }
        public int Count { get { return this._count; } }

        public UpdateStreamsCompletedEventsArgs(bool Success, int Count)
        {
            this._success = Success;
            this._count = Count;
        }
    }

    public class NotifyStreamUpdateEventArgs : EventArgs
    {
        private Stream _stream;
        public Stream Stream { get { return this._stream; } }

        public NotifyStreamUpdateEventArgs(Stream Stream)
        {
            this._stream = Stream;
        }
    }

    public sealed class Streams
    {
        public Dictionary<string, Stream> List = new Dictionary<string, Stream>();

        private object _lock;
        private Thread _worker;

        public delegate void LoadStreamsCompletedEventHandler(object sender, LoadStreamsCompletedEventArgs e);
        public event LoadStreamsCompletedEventHandler OnStreamsLoadCompleted;

        public delegate void UpdateStreamsCompletedEventHandler(object sender, UpdateStreamsCompletedEventsArgs e);
        public event UpdateStreamsCompletedEventHandler OnStreamsUpdateCompleted;

        public delegate void NotifyStreamUpdateEventHandler(object sender,NotifyStreamUpdateEventArgs e);
        public event NotifyStreamUpdateEventHandler OnStreamUpdateStep;
        
        public Streams() { }

        public void Load()
        {
            _worker = new Thread(_load_streams) { IsBackground = true };
            _worker.Start();
        }

        public void Update()
        {
            _worker = new Thread(_update_streams) { IsBackground = true };
            _worker.Start();
        }

        private void _load_streams()
        {
            _lock = new object();
            lock (_lock)
            {

                XDocument xdoc = XDocument.Load("Streams.xml");

                var entries = from stream in xdoc.Descendants("Stream")
                              select new
                              {
                                  Name = stream.Element("Name").Value,
                                  Slug = stream.Element("Slug").Value,
                                  Provider = stream.Element("Provider").Value,
                                  Game = stream.Element("Game").Value
                              };

                foreach (var entry in entries)
                {
                    Stream stream = StreamFactory.CreateStream(entry.Provider);
                    stream.Slug = entry.Slug;
                    stream.Name = entry.Name;
                    stream.Provider = entry.Provider;

                    switch (entry.Game)
                    {
                        case "Starcraft": stream.Game = Game.Starcraft; break;
                        default: break;
                    }

                    this.List.Add(entry.Slug, stream);
                }
            }

            if (OnStreamsLoadCompleted != null) OnStreamsLoadCompleted(this, new LoadStreamsCompletedEventArgs(true, this.List.Count));

            Thread.CurrentThread.Abort();
            _worker = null;
        }

        private void _update_streams()
        {
            _lock = new object();
            lock (_lock)
            {
                foreach (KeyValuePair<string, Stream> pair in this.List)
                {
                    pair.Value.Update();
                    if (OnStreamUpdateStep != null) OnStreamUpdateStep(this, new NotifyStreamUpdateEventArgs(pair.Value));
                }
            }

            if (OnStreamsUpdateCompleted != null) OnStreamsUpdateCompleted(this, new UpdateStreamsCompletedEventsArgs(true, this.List.Count));

            Thread.CurrentThread.Abort();
            _worker = null;
        }
    }
}
