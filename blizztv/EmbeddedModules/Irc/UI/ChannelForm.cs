using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using BlizzTV.Utility.Extensions;
using BlizzTV.Win32API;

namespace BlizzTV.EmbeddedModules.Irc.UI
{
    public partial class ChannelForm : Form
    {
        private int _backlogOffset = 0;
        private readonly IrcChannel _channel;

        public ChannelForm(IrcChannel channel)
        {
            InitializeComponent();

            this._channel = channel;
            this._channel.BackLogUpdated += BacklogUpdated;
        }

        private void ChannelForm_Load(object sender, EventArgs e)
        {
            this.Text = this._channel.Name;
            this.LoadUsers();
            this.UpdateChannelFromBacklog();
        }

        private void LoadUsers()
        {
            foreach(string user in this._channel.Users)
            {
                this.usersList.Items.Add(user);
            }
        }

        private void UpdateChannelFromBacklog()
        {
            this.channelText.AsyncInvokeHandler(() =>
            {
                for (int i = this._backlogOffset; i < this._channel.MessageBackLog.Count; i++)
                {
                    var message = this._channel.MessageBackLog[i];
                    this.channelText.SelectionColor = message.ForeColor();
                    this.channelText.SelectedText += string.Format("{0}{1}", message, Environment.NewLine);
                }
                WindowMessaging.SendMessage(this.channelText.Handle, WindowMessaging.WM_VSCROLL, WindowMessaging.SB_BOTTOM, 0);
                this._backlogOffset = this._channel.MessageBackLog.Count;
            });
        }

        private void BacklogUpdated(object sender, EventArgs e)
        {
            this.UpdateChannelFromBacklog();
        }
    }
}
