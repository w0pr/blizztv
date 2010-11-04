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
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

using LibBlizzTV.Streams;

namespace BlizzTV
{
    public partial class StreamPlayer : Form
    {
        private LibBlizzTV.Streams.Stream _stream;
        
        private string template="<html><head><style type='text/css'>BODY {margin:  0px;padding: 0px;background-color: #000000;}</style></head><body>%video%<body></html>";

        public StreamPlayer(LibBlizzTV.Streams.Stream Stream)
        {
            InitializeComponent();
            this._stream = Stream;
        }

        private void StreamPlayer_Load(object sender, EventArgs e)
        {
            string html = template;
            html = html.Replace("%video%", this._stream.VideoEmbedCode());
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
    }
}
