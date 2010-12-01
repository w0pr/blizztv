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
using BlizzTV.ModuleLib.Utils;

namespace BlizzTV.Updates
{
    internal sealed class UpdateManager
    {
        private static UpdateManager _instance = new UpdateManager();
        private Update _found_update = null;
        private bool _update_available = false;

        public static UpdateManager Instance { get { return _instance; } }
        public Update FoundUpdate { get { return this._found_update; } }
        public bool UpdateAvailable { get { return this._update_available; } }

        private UpdateManager() { }

        public delegate void NewAvailableUpdateFoundEventHandler(bool FoundUpdate);
        public event NewAvailableUpdateFoundEventHandler OnFoundNewAvailableUpdate;

        public void Check()
        {
            Log.Instance.Write(LogMessageTypes.INFO, "UpdateManager thread running..");
            ThreadStart update_thread = delegate { CheckUpdates(); };
            Thread t = new Thread(update_thread) { IsBackground = true, Name = string.Format("update-thread-{0}", DateTime.Now.TimeOfDay.ToString()) };
            t.Start();
        }

        private void CheckUpdates()
        {
            if (!this.UpdateAvailable)
            {
                List<Update> updates = new List<Update>();
                Version latest_version = Assembly.GetExecutingAssembly().GetName().Version;

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
                        if ((u.Valid) && ((u.UpdateType == UpdateTypes.STABLE) || (u.UpdateType == UpdateTypes.BETA && Settings.Instance.AllowBetaVersionNotifications)) && (u.Version > latest_version))
                        {
                            this._found_update = u;
                            this._update_available = true;
                            latest_version = u.Version;
                        }
                    }

                    if (this._found_update != null) Log.Instance.Write(LogMessageTypes.INFO, string.Format("Found update: {0} - {1}", this._found_update.UpdateType, this._found_update.Version));
                }
                else
                {
                    this._update_available = false;
                    this._found_update = null;
                }

                if (this.OnFoundNewAvailableUpdate != null)
                {

                    if (this._found_update != null) this.OnFoundNewAvailableUpdate(true);
                    this.OnFoundNewAvailableUpdate(false);
                }
            }
        }
    }
}
