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
using LibBlizzTV.Utils;

namespace LibVideos
{
    public sealed class Providers : IDisposable // video providers
    {
        #region members

        private string _xml_file = @"plugins\xml\videos\providers.xml";
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
                                  Name = provider.Element("Name").Value,
                                  Movie = provider.Element("Movie").Value,
                                  FlashVars = provider.Element("FlashVars").Value,
                              };

                foreach (var entry in entries) // add provider's to the list.
                {
                    this.List.Add(entry.Name, new Provider(entry.Name, entry.Movie, entry.FlashVars));
                }
            }
            catch (Exception e)
            {
                Log.Instance.Write(LogMessageTypes.ERROR, string.Format("VideoChannelsPlugin Providers:Providers() Error: \n {0}", e.ToString()));
                System.Windows.Forms.MessageBox.Show(string.Format("An error occured while parsing your videoproviders.xml. Please correct the error and re-start the plugin. \n\n[Error Details: {0}]", e.Message), "Video Channels Plugin Error", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
            }
        }

        #endregion

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

        public string Name { get { return this._name; } internal set { this._name = value; } }
        public string Movie { get { return this._movie; } internal set { this._movie = value; } }
        public string FlashVars { get { return this._flash_vars; } internal set { this._flash_vars = value; } }

        public Provider(string Name, string Movie,string FlashVars)
        {
            this._name = Name;
            this._movie = Movie;
            this._flash_vars = FlashVars;
        }
    }

    #endregion
}
