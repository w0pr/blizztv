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
using System.Reflection;
using System.Threading;
using BlizzTV.CommonLib.Logger;
using BlizzTV.CommonLib.Web;
using BlizzTV.UI;

namespace BlizzTV.Updates
{
    internal sealed class UpdateManager
    {
        #region instance

        private static UpdateManager _instance = new UpdateManager();
        public static UpdateManager Instance { get { return _instance; } }

        #endregion

        public Update FoundUpdate { get; private set; }
        public bool UpdateAvailable { get; private set; }

        private UpdateManager() { }

        public delegate void NewAvailableUpdateFoundEventHandler(bool foundUpdate);
        public event NewAvailableUpdateFoundEventHandler OnFoundNewAvailableUpdate;

        public void Check()
        {
            Log.Instance.Write(LogMessageTypes.Info, "UpdateManager thread running..");
            ThreadStart updateThread = CheckUpdates;
            Thread thread = new Thread(updateThread) {IsBackground = true};
            thread.Start();
        }

        private void CheckUpdates()
        {
            if (this.UpdateAvailable) return;

            List<Update> updates = new List<Update>();
            Version latestVersion = Assembly.GetExecutingAssembly().GetName().Version;

            string response = WebReader.Read("http://code.google.com/feeds/p/blizztv/downloads/basic/");

            if (response != null)
            {
                XDocument xdoc = XDocument.Parse(response); // parse the xml
                XNamespace xmlns = "http://www.w3.org/2005/Atom";

                var entries = from entry in xdoc.Descendants(xmlns + "entry")
                              select new
                                         {
                                             date = entry.Element(xmlns + "updated").Value,
                                             id = entry.Element(xmlns + "id").Value,
                                             link = entry.Element(xmlns + "link").Attribute("href").Value,
                                             filename = entry.Element(xmlns + "title").Value.Replace('\n', ' ').Trim(),
                                             details = entry.Element(xmlns + "content").Value
                                         };


                foreach (var e in entries) { updates.Add(new Update(e.id, e.date, e.link, e.filename, e.details)); } // parse update details.

                foreach (Update u in updates)
                {
                    if ((u.Valid) && ((u.UpdateType == UpdateTypes.Stable) || (u.UpdateType == UpdateTypes.Beta && Settings.Instance.AllowBetaVersionNotifications)) && (u.Version > latestVersion))
                    {
                        this.FoundUpdate = u;
                        this.UpdateAvailable = true;
                        latestVersion = u.Version;
                    }
                }

                if (this.UpdateAvailable) Log.Instance.Write(LogMessageTypes.Info, string.Format("Found update: {0} - {1}", this.FoundUpdate.UpdateType, this.FoundUpdate.Version));
            }
            else
            {
                this.UpdateAvailable = false;
                this.FoundUpdate = null;
            }

            if (this.OnFoundNewAvailableUpdate == null) return;
            this.OnFoundNewAvailableUpdate(this.UpdateAvailable);
        }
    }
}
