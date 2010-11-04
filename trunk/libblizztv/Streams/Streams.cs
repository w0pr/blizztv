using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace LibBlizzTV.Streams
{
    public sealed class Streams
    {
        private static readonly Streams _instance = new Streams();
        public static Streams Instance { get { return _instance; } }

        public Dictionary<string, Stream> List = new Dictionary<string, Stream>();

        private Streams()
        {
            XDocument xdoc = XDocument.Load("Streams.xml");

            var entries = from stream in xdoc.Descendants("Stream")
                          select new
                          {
                              Name = stream.Element("Name").Value,
                              ID=stream.Element("ID").Value,
                              Provider = stream.Element("Provider").Value,
                          };

            foreach (var entry in entries)
            {
                this.List.Add(entry.Name, new Stream(entry.ID, entry.Name, entry.Provider));
            }
        }
    }
}
