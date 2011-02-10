/*    
 * Copyright (C) 2010-2011, BlizzTV Project - http://code.google.com/p/blizztv/
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
 * $Id: Settings.cs 355 2011-02-07 12:05:26Z shalafiraistlin@gmail.com $
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using System.Reflection;
using System.Threading;
using System.Windows.Forms;
using BlizzTV.Assets.i18n;
using BlizzTV.Log;
using BlizzTV.Settings;
using BlizzTV.Utility.Web;

namespace BlizzTV.Updates
{
    internal sealed class UpdateManager
    {
        #region instance

        private static UpdateManager _instance = new UpdateManager();
        public static UpdateManager Instance { get { return _instance; } }

        #endregion

        private UpdateManager() { }

        public void Check(bool fullVerbose = false)
        {
            Update foundUpdate = null; 
            LogManager.Instance.Write(LogMessageTypes.Info, "UpdateManager checking for available update..");

            Thread updateThread = new Thread(() => { foundUpdate = CheckForUpdates(); }) { IsBackground = true }; // check for available updates in a new thread.
            updateThread.Start();
            updateThread.Join();

            if (foundUpdate != null) // if a new update is available
            {
                DialogResult result = MessageBox.Show(string.Format(i18n.FoundANewUpdateMessage, foundUpdate.UpdateType), string.Format(i18n.FoundANewUpdateTitle, foundUpdate.UpdateType), MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result == DialogResult.Yes) // if user approves installing the update
                {
                    UpdaterForm f = new UpdaterForm(foundUpdate);
                    f.ShowDialog();
                }
            }
            // if we're in full-verbose mode, notify also about when no update is found.            
            else if (fullVerbose) MessageBox.Show(i18n.NoAvailableUpdateFoundMessage, i18n.NoAvailableUpdateFoundTitle, MessageBoxButtons.OK, MessageBoxIcon.Information); 
        }

        private Update CheckForUpdates()
        {
            Update availableUpdate = null;
            Version currentVersion = Assembly.GetExecutingAssembly().GetName().Version; // the running-version.
            
            WebReader.Result result = WebReader.Read("http://code.google.com/feeds/p/blizztv/downloads/basic/"); // read the updates list.

            if (result.State != WebReader.States.Success)
            {
                LogManager.Instance.Write(LogMessageTypes.Error, string.Format("Error reading the updates list. Response status code: {0}", result.State));
                return null;
            } 

            XDocument xdoc = XDocument.Parse(result.Response); // start parsing the xml.
            XNamespace xmlns = "http://www.w3.org/2005/Atom"; // apply the namespace.

            var entries = from entry in xdoc.Descendants(xmlns + "entry")
                          select new
                                     {
                                         date = entry.Element(xmlns + "updated").Value,
                                         link = entry.Element(xmlns + "link").Attribute("href").Value,
                                         title = entry.Element(xmlns + "title").Value.Replace('\n', ' ').Trim(),
                                         details = entry.Element(xmlns + "content").Value
                                     };


            List<Update> updatesList = entries.Select(e => new Update(e.date, e.link, e.title, e.details)).ToList(); // list of all updates.

            foreach (Update u in updatesList) // check all the found updates and see if there's an available fresh version.
            {
                // for beta updates, AllowBetaVersionNotifications should be set on configuration.
                if ((u.Valid) && ((u.UpdateType == UpdateTypes.Stable) || (u.UpdateType == UpdateTypes.Beta && GlobalSettings.Instance.AllowBetaVersionNotifications)) && (u.Version > currentVersion))
                {
                    availableUpdate = u;
                    currentVersion = u.Version; // set the current version to last-found update version, so we can find out the latest available update.
                }
            }

            if (availableUpdate!=null) LogManager.Instance.Write(LogMessageTypes.Info, string.Format("Found an update: {0} - {1}", availableUpdate.UpdateType, availableUpdate.Version));            
            return availableUpdate;
        }
    }
}
