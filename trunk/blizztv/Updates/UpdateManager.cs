using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using System.Text;
using System.Reflection;
using LibBlizzTV;
using LibBlizzTV.Utils;

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

        public delegate void NewAvailableUpdateFoundEventHandler();
        public event NewAvailableUpdateFoundEventHandler OnFoundNewAvailableUpdate;

        public void Check()
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
                        if ((u.Valid) && ((u.UpdateType == UpdateTypes.STABLE) || (u.UpdateType == UpdateTypes.BETA && SettingsStorage.Instance.Settings.AllowBetaVersionNotifications)) && (u.Version > latest_version))
                        {
                            this._found_update = u;
                            this._update_available = true;
                            latest_version = u.Version;
                        }
                    }

                    if (this._found_update != null && this.OnFoundNewAvailableUpdate != null)
                    {
                        Log.Instance.Write(LogMessageTypes.INFO, string.Format("Found update: {0} - {1}", this._found_update.UpdateType, this._found_update.Version));
                        this.OnFoundNewAvailableUpdate();
                    }
                }
                else
                {
                    this._update_available = false;
                    this._found_update = null;
                }
            }
        }
    }
}
