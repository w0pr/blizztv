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
 */

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace BlizzTV
{
    public partial class ChannelStage : Form
    {
        /*
        private string template = "<html><head><style type='text/css'>BODY {margin:  0px;padding: 0px;background-color: #000000;}</style></head><body>%video%<body></html>";

        public ChannelStage(Channel Channel)
        {
            InitializeComponent();
            this._channel = Channel;
        }

        private void ChannelStage_Load(object sender, EventArgs e)
        {
            this.Text = string.Format("Channel: {0}@{1}", this._channel.Name, this._channel.Provider);
            if(this._channel.Videos.Count>0) this.LoadVideo(this._channel.Videos[0]);
        }

        private void LoadVideo(Video v)
        {
            string html = template;
            html = html.Replace("%video%", this._channel.VideoEmbedCode());
            html = html.Replace("%video_id%", v.VideoID);
            html = html.Replace("%width%", "640");
            html = html.Replace("%height%", "385");

            string file = Path.GetTempFileName() + ".html";
            FileStream fs = new FileStream(file, FileMode.Create);
            StreamWriter sw = new StreamWriter(fs);
            sw.Write(html);
            sw.Flush();
            sw.Close();
            fs.Close();
            browser.Navigate(file);
        }

        private void browser_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            this.browser.Visible = true;
        }
         */
    }
}
