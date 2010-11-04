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

        public StreamPlayer(LibBlizzTV.Streams.Stream Stream)
        {
            InitializeComponent();
            this._stream = Stream;
        }

        private void StreamPlayer_Load(object sender, EventArgs e)
        {
            string file = Path.GetTempFileName() + ".htm";           
            FileStream fs = new FileStream(file, FileMode.Create);
            StreamWriter sw = new StreamWriter(fs);
            sw.Write(this._stream.VideoEmbedCode());
            sw.Flush();
            sw.Close();
            fs.Close();
            browser.Navigate(file);
        }
    }
}
