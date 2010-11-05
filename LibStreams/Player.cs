using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace LibStreams
{
    public partial class Player : Form
    {
        private Stream _stream;

        public Player(Stream Stream)
        {
            InitializeComponent();
            this._stream = Stream;
            this._stream.Process();
        }

        private void Player_Load(object sender, EventArgs e)
        {
            this.Text=string.Format("Stream: {0}@{1}",this._stream.Title,this._stream.Provider);
            this.Stage.FlashVars = this._stream.FlashVars;
            this.Stage.MovieData = string.Format("{0}?{1}", this._stream.Movie, this._stream.FlashVars);
            this.Stage.LoadMovie(0, string.Format("{0}?{1}",this._stream.Movie,this._stream.FlashVars));            
        }
    }
}
