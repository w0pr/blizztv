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
using System.Text.RegularExpressions;
using System.IO;
using System.Diagnostics;
using System.Windows.Forms;
using BlizzTV.Assets.i18n;
using BlizzTV.Helpers;
using BlizzTV.Log;

namespace BlizzTV.Updates
{
    public class Update
    {
        private static readonly Regex RegexUpdateTypeAndVersion = new Regex(@"\[r\:(.*)-v\:(.*)\]", RegexOptions.Compiled); // regex for matching update type and version. 
        private static readonly Regex RegexUpdateLink=new Regex(@"http\://code.google.com/p/blizztv/downloads/detail\?name\=(.*)", RegexOptions.Compiled); // regex for matching update link.
        
        public Version Version { get; private set; }
        public UpdateTypes UpdateType { get; private set; }
        public string FileName { get; private set; }
        public string DownloadLink { get; private set; }
        public string Date { get; private set; }
        public bool Valid { get; private set; }

        private readonly string _details;
        private readonly string _title;
        private readonly string _link;

        public Update(string date, string link, string title,string details) /* parses the update data */
        {
            try
            {
                this._title = title;
                this._link = link;
                this._details = details;
                this.Date = date;

                /* match the update type and version */
                Match matchTypeAndVersion = RegexUpdateTypeAndVersion.Match(this._details);
                if (!matchTypeAndVersion.Success) { this.Valid = false; return; }

                /* get the update-type */
                switch (matchTypeAndVersion.Groups[1].Value.ToLower())
                {
                    case "beta": this.UpdateType = UpdateTypes.Beta; break;
                    case "stable": this.UpdateType = UpdateTypes.Stable; break;
                    default: this.UpdateType = UpdateTypes.Invalid; this.Valid = false; return;
                }

                /* get the update version */
                this.Version = Version.Parse(matchTypeAndVersion.Groups[2].Value);

                /* match the update link */
                Match matchLink = RegexUpdateLink.Match(this._link);
                if (!matchLink.Success) { this.Valid = false; return; }
                this.FileName = matchLink.Groups[1].Value.ToLower();
                this.DownloadLink = string.Format("http://blizztv.googlecode.com/files/{0}", matchLink.Groups[1].Value);

                this.Valid = true;
            }
            catch (Exception e)
            {                
                LogManager.Instance.Write(LogMessageTypes.Error, string.Format("Update parse exception: {0}", e));
                this.Valid = false;
            }
        }

        public void Install() /* installs an update */
        {
            if (!this.Valid)
            {
                LogManager.Instance.Write(LogMessageTypes.Error, string.Format("Can't install the update {0} as it's not valid.", this.Version));
                return;
            }

            string fileExtension = Path.GetExtension(this.FileName); /* get the update file extension */
            
            /* check the update file extension */
            if (!(fileExtension == ".exe" || fileExtension == ".zip")) 
            {
                LogManager.Instance.Write(LogMessageTypes.Error, string.Format("Can't install the update {0} as downloaded file's extension {1} is not a valid extension for updates.", this.Version,fileExtension));
                MessageBox.Show(i18n.DownloadedUpdateIsNotValidMessage, i18n.DownloadedUpdateIsNotValidTitle, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            MessageBox.Show(i18n.ApplicationWillExitToContinueInstallationOfUpdateMessage, i18n.InstallingUpdateTitle, MessageBoxButtons.OK, MessageBoxIcon.Information);

            if (fileExtension == ".exe") // if update contains a setup
            {
                Process.Start(this.FileName, null); // run the setup.
            }
            else if (fileExtension == ".zip") // if update is an archive.
            {
                Zip.Extract(this.FileName, "update"); // extract the archive contents to 'update' folder.
                StreamWriter writer = new StreamWriter("update.bat", false); // create an updater batch file.
                writer.WriteLine(@"ping 127.0.0.1"); // give blizztv time to exit.
                writer.WriteLine(@"del /F /Q *.exe"); // delete all existing *.exe files.
                writer.WriteLine(@"del /F /Q *.dll"); // delete all existing *.dll files.
                writer.WriteLine(@"move /Y update\*.* ."); // move archives extracted content to our installation folder.
                writer.WriteLine(string.Format("del /F /Q {0}", this.FileName)); // delete the downloaded update archive.
                writer.WriteLine(@"rmdir /S /Q update"); // remove the extracted update archive folder.
                writer.WriteLine(@"del /F /Q update.bat"); // remove the batch file itself.
                writer.Flush(); 
                writer.Close();

                Process cmd = new Process
                                  {
                                      StartInfo =
                                          {
                                              FileName = "cmd.exe",
                                              Arguments = "/q /c update.bat", /* /q - Turns echo off, /c - Carries out the command specified by string and then terminates */
                                              WindowStyle = ProcessWindowStyle.Hidden /* hide the command prompt */
                                          }
                                  };

                cmd.Start(); // run the update.bat file.
            }
            Application.ExitThread(); // exit the process so update.batch can move & delete files.
        }
    }

    public enum UpdateTypes
    {
        Invalid, 
        Beta,
        Stable
    }
}
