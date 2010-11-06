using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace LibVideoChannels
{
    public partial class Player : Form
    {
        private Video _video;

        public Player(Video Video)
        {
            InitializeComponent();
            this._video = Video;
            this.Width = VideoChannelsPlugin.GlobalSettings.VideoPlayerWidth;
            this.Height = VideoChannelsPlugin.GlobalSettings.VideoPlayerHeight;
            this._video.Process();
        }

        private void Player_Load(object sender, EventArgs e)
        {
            this.Text = string.Format("Video Channel: {0}@{1}", this._video.Title, this._video.Provider);
            this.Stage.FlashVars = this._video.FlashVars;
            this.Stage.LoadMovie(0, string.Format("{0}?{1}", this._video.Movie, this._video.FlashVars));
        }
    }
}
