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
        private Channel _channel;

        public Player(Channel Channel)
        {
            InitializeComponent();
            this._channel = Channel;
            this.Width = VideoChannelsPlugin.GlobalSettings.VideoPlayerWidth;
            this.Height = VideoChannelsPlugin.GlobalSettings.VideoPlayerHeight;
            foreach (Video v in this._channel.Videos)
            {
                v.Process();
            }
        }

        private void Player_Load(object sender, EventArgs e)
        {
            this.Text = string.Format("Video Channel: {0}@{1}", this._channel.Title, this._channel.Provider);
            if (this._channel.Videos.Count > 0)
            {                
                this.Stage.FlashVars = this._channel.Videos[0].FlashVars;
                this.Stage.LoadMovie(0, string.Format("{0}?{1}", this._channel.Videos[0].Movie, this._channel.Videos[0].FlashVars));
            }
        }
    }
}
