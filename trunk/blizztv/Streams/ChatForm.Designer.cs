using BlizzTV.Controls.FlashPlayer;

namespace BlizzTV.Streams
{
    partial class ChatForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ChatForm));
            this.Chat = new FlashPlayer();
            ((System.ComponentModel.ISupportInitialize)(this.Chat)).BeginInit();
            this.SuspendLayout();
            // 
            // Chat
            // 
            this.Chat.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Chat.Enabled = true;
            this.Chat.Location = new System.Drawing.Point(0, 0);
            this.Chat.Name = "Chat";
            this.Chat.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("Chat.OcxState")));
            this.Chat.Size = new System.Drawing.Size(334, 351);
            this.Chat.TabIndex = 1;
            // 
            // frmChat
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Black;
            this.ClientSize = new System.Drawing.Size(334, 351);
            this.Controls.Add(this.Chat);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
            this.Name = "frmChat";
            this.Text = "frmChat";
            this.Load += new System.EventHandler(this.ChatForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.Chat)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private FlashPlayer Chat;
    }
}