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
using System.Windows.Forms;
using BlizzTV.CommonLib.Logger;
using BlizzTV.CommonLib.Helpers;

namespace BlizzTV.CommonLib.Updates
{
    public class Update
    {
        private static readonly Regex RegexUpdateTypeAndVersion = new Regex(@"\[r\:(.*)-v\:(.*)\]", RegexOptions.Compiled);
        private static readonly Regex RegexUpdateLink=new Regex(@"http\://code.google.com/p/blizztv/downloads/detail\?name\=(.*)", RegexOptions.Compiled);

        public bool Valid { get; private set; }
        public string Id { get; private set; }
        public string Date { get; private set; }
        public string Title { get; private set; }
        public string Link { get; private set; }
        public string FileName { get; private set; }
        public string DownloadLink { get; private set; }
        public string Details { get; private set; }
        public Version Version { get; private set; }
        public UpdateTypes UpdateType { get; private set; }

        public Update(string id, string date, string link, string title,string details)
        {
            try
            {
                this.Valid = true;
                this.Id = id;
                this.Date = date;
                this.Title = title;
                this.Link = link;
                this.DownloadLink = string.Format("http://blizztv.googlecode.com/files/{0}", this.Title);
                this.Details = details;

                Match matchTypeAndVersion = RegexUpdateTypeAndVersion.Match(this.Details);
                if (!matchTypeAndVersion.Success) { this.Valid = false; return; }

                switch (matchTypeAndVersion.Groups[1].Value.ToLower())
                {
                    case "beta": this.UpdateType = UpdateTypes.Beta; break;
                    case "stable": this.UpdateType = UpdateTypes.Stable; break;
                    default: this.UpdateType = UpdateTypes.Invalid; this.Valid = false; return;
                }

                this.Version = Version.Parse(matchTypeAndVersion.Groups[2].Value);

                Match matchLink = RegexUpdateLink.Match(this.Link);
                if (!matchLink.Success) { this.Valid = false; return; }

                this.FileName = matchLink.Groups[1].Value;
                this.DownloadLink = string.Format("http://blizztv.googlecode.com/files/{0}", matchLink.Groups[1].Value);
            }
            catch (Exception e)
            {
                this.Valid = false;
                Log.Instance.Write(LogMessageTypes.Error, string.Format("Error parsing update data! Exception details: {0}", e));
            }
        }

        public void Install()
        {
            string fileExtension = Path.GetExtension(this.FileName).ToLower();
            
            if (!(fileExtension == ".exe" || fileExtension == ".zip"))
            {
                MessageBox.Show("Downloaded file is not a valid update. Please re-try downloading the update", "Update Not Valid", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            MessageBox.Show("The application will exit to continue installation of update.", "Installing Update", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Information);

            if (fileExtension == ".exe")
            {
                System.Diagnostics.Process.Start(this.FileName, null);
            }
            else if (fileExtension == ".zip")
            {
                Zip.Extract(this.FileName, "update");
                StreamWriter writer = new StreamWriter("update.bat", false);
                writer.WriteLine(@"ping 127.0.0.1");
                writer.WriteLine(@"move /Y update\*.* .");
                writer.WriteLine(@"del /F /Q update");
                writer.WriteLine(@"del /F /Q update.bat");
                writer.Flush();
                writer.Close();
                System.Diagnostics.Process.Start("update.bat", null);
            }

            Application.ExitThread();
        }
    }

    public enum UpdateTypes
    {
        Invalid,
        Beta,
        Stable
    }
}
