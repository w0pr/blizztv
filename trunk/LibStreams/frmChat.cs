using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using LibBlizzTV.Utils;

namespace LibStreams
{
    public partial class frmChat : Form
    {
        private Stream _stream; // the stream.

        public frmChat(Stream Stream)
        {
            InitializeComponent();

            this._stream = Stream; // set the stream.
            this.Width = (StreamsPlugin.Instance.Settings as Settings).StreamChatWindowWidth;
            this.Height = (StreamsPlugin.Instance.Settings as Settings).StreamChatWindowHeight;
        }

        private void frmChat_Load(object sender, EventArgs e)
        {
            try
            {
                this.Text = string.Format("Chat: {0}@{1}", this._stream.Name, this._stream.Provider); // set the window title.
                this.Chat.LoadMovie(0, string.Format("{0}", this._stream.ChatMovie)); // load the movie.
            }
            catch (Exception exc)
            {
                Log.Instance.Write(LogMessageTypes.ERROR, string.Format("StreamsPlugin Chat Window Error: \n {0}", exc.ToString()));
                System.Windows.Forms.MessageBox.Show(string.Format("An error occured in stream chat window. \n\n[Error Details: {0}]", exc.Message), "Streams Plugin Error", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
            }
        }
    }
}
