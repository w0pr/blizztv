using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace LibBlizzTV.Streams
{
    public sealed class Providers
    {
        private static readonly Providers _instance = new Providers();
        public static Providers Instance { get { return _instance; } }

        public Dictionary<string, Provider> List = new Dictionary<string, Provider>();

        private Providers()
        {
            XDocument xdoc = XDocument.Load("StreamProviders.xml");

            var entries = from provider in xdoc.Descendants("Provider") 
                          select new {
                                Name = provider.Element("Name").Value,
                                VideoTemplate = provider.Element("VideoTemplate").Value,    
                            };

            foreach (var entry in entries)
            {
                this.List.Add(entry.Name, new Provider(entry.Name, entry.VideoTemplate));
            }
        }
    }
}
