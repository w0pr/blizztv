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
 * $Id$
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using System.Xml.Serialization;
using BlizzTV.CommonLib.Logger;
using BlizzTV.ModuleLib.Subscriptions.Providers;

namespace BlizzTV.Modules.Streams
{
    public sealed class Providers : IDisposable // stream providers
    {
        #region members

        private string _xml_file = @"modules\streams\xml\providers.xml";
        private static readonly Providers _instance = new Providers(); // the providers instance.
        public static Providers Instance { get { return _instance; } }

        public Dictionary<string, Provider> List = new Dictionary<string, Provider>(); // the list of defined providers.
        private bool disposed = false;

        #endregion

        #region ctor

        private Providers()
        {
            try
            {
                XDocument xdoc = XDocument.Load(this._xml_file); // read providers xml.
                var entries = from provider in xdoc.Descendants("Provider")
                              select new
                              {
                                  Name = provider.Element("Name").Value.ToLower(),
                                  Movie = provider.Element("Movie").Value,
                                  FlashVars = provider.Element("FlashVars").Value,
                                  ChatMovie = (string)provider.Element("ChatMovie") ?? ""
                              };

                foreach (var entry in entries) // add provider's to the list.
                {
                    this.List.Add(entry.Name, new Provider(entry.Name, entry.Movie, entry.FlashVars,entry.ChatMovie));
                }
            }
            catch (Exception e)
            {
                Log.Instance.Write(LogMessageTypes.ERROR, string.Format("StreamsPlugin Providers:Providers() Error: \n {0}", e.ToString()));
                System.Windows.Forms.MessageBox.Show(string.Format("An error occured while parsing your providers.xml. Please correct the error and re-start the plugin. \n\n[Error Details: {0}]", e.Message), "Streams Plugin Error", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
            }
        }

        #endregion ctor

        #region de-ctor

        ~Providers() { Dispose(false); }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        private void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing) // managed resources
                {
                    this.List.Clear();
                    this.List = null;
                }
                disposed = true;
            }
        }

        #endregion
    }

    #region Provider

    public class Provider
    {
        private string _name; // provider name.
        private string _movie; // provider movie template.
        private string _flash_vars; // provider flash template.
        private bool _chat_available = false; // Is chat functionality available for the provider?
        private string _chat_movie; // If chat is enabled, the movie source for chat window.

        public string Name { get { return this._name; } }
        public string Movie { get { return this._movie; } }
        public string FlashVars { get { return this._flash_vars; } }
        public bool ChatAvailable { get { return this._chat_available; } }
        public string ChatMovie { get { return this._chat_movie; } }

        public Provider(string Name, string Movie, string FlashVars,string ChatMovie=null)
        {
            this._name = Name.ToLower();
            this._movie = Movie;
            this._flash_vars = FlashVars;
            if (ChatMovie.Trim() != "")
            {
                this._chat_available = true;
                this._chat_movie = ChatMovie;
            }
        }
    }

    [Serializable]
    [XmlType("Stream")]
    public class StreamProvider : IProvider
    {
        [XmlAttribute("Movie")]
        public string Movie { get; set; }

        [XmlAttribute("FlashVars")]
        public string FlashVars { get; set; }

        [XmlAttribute("ChatMovie")]
        public string ChatMovie { get; set; }

        [XmlIgnoreAttribute]
        public bool ChatAvailable { get; private set; }

        public StreamProvider() 
        {
            if (ChatMovie == null)
            {
                ChatMovie = "";
                ChatAvailable = false;
            }
        }
    }

    public sealed class StreamProviders : ProvidersHandler
    {
        private static StreamProviders _instance = new StreamProviders();
        public static StreamProviders Instance { get { return _instance; } }

        private StreamProviders() : base(typeof(StreamProvider)) { }
    }

    #endregion
}
