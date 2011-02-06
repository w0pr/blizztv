using BlizzTV.Controls.FlashPlayer;
using BlizzTV.Controls.LoadingCircle;

namespace BlizzTV.Modules.Streams
{
    partial class frmPlayer
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmPlayer));
            this.FlashPlayer = new FlashPlayer();
            this.PlayerContextMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.MenuAlwaysOnTop = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuOpenChat = new System.Windows.Forms.ToolStripMenuItem();
            this.WebBrowser = new System.Windows.Forms.WebBrowser();
            this.LoadingCircle = new LoadingCircle();
            ((System.ComponentModel.ISupportInitialize)(this.FlashPlayer)).BeginInit();
            this.PlayerContextMenu.SuspendLayout();
            this.SuspendLayout();
            // 
            // FlashPlayer
            // 
            this.FlashPlayer.ContextMenuStrip = this.PlayerContextMenu;
            this.FlashPlayer.Enabled = true;
            this.FlashPlayer.Location = new System.Drawing.Point(12, 12);
            this.FlashPlayer.Name = "FlashPlayer";
            this.FlashPlayer.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("FlashPlayer.OcxState")));
            this.FlashPlayer.Size = new System.Drawing.Size(624, 347);
            this.FlashPlayer.TabIndex = 0;
            this.FlashPlayer.Visible = false;
            this.FlashPlayer.OnReadyStateChange += new AxShockwaveFlashObjects._IShockwaveFlashEvents_OnReadyStateChangeEventHandler(this.FlashPlayer_OnReadyStateChange);
            // 
            // PlayerContextMenu
            // 
            this.PlayerContextMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.MenuAlwaysOnTop,
            this.MenuOpenChat});
            this.PlayerContextMenu.Name = "PlayerContextMenu";
            this.PlayerContextMenu.Size = new System.Drawing.Size(155, 70);
            // 
            // MenuAlwaysOnTop
            // 
            this.MenuAlwaysOnTop.Image = ((System.Drawing.Image)(resources.GetObject("MenuAlwaysOnTop.Image")));
            this.MenuAlwaysOnTop.Name = "MenuAlwaysOnTop";
            this.MenuAlwaysOnTop.Size = new System.Drawing.Size(154, 22);
            this.MenuAlwaysOnTop.Text = "Always On Top";
            this.MenuAlwaysOnTop.Click += new System.EventHandler(this.MenuAlwaysOnTop_Click);
            // 
            // MenuOpenChat
            // 
            this.MenuOpenChat.Image = ((System.Drawing.Image)(resources.GetObject("MenuOpenChat.Image")));
            this.MenuOpenChat.Name = "MenuOpenChat";
            this.MenuOpenChat.Size = new System.Drawing.Size(154, 22);
            this.MenuOpenChat.Text = "Open Chat";
            this.MenuOpenChat.Click += new System.EventHandler(this.MenuOpenChat_Click);
            // 
            // WebBrowser
            // 
            this.WebBrowser.ContextMenuStrip = this.PlayerContextMenu;
            this.WebBrowser.IsWebBrowserContextMenuEnabled = false;
            this.WebBrowser.Location = new System.Drawing.Point(112, 12);
            this.WebBrowser.MinimumSize = new System.Drawing.Size(20, 20);
            this.WebBrowser.Name = "WebBrowser";
            this.WebBrowser.ScriptErrorsSuppressed = true;
            this.WebBrowser.ScrollBarsEnabled = false;
            this.WebBrowser.Size = new System.Drawing.Size(111, 69);
            this.WebBrowser.TabIndex = 1;
            this.WebBrowser.Visible = false;
            this.WebBrowser.DocumentCompleted += new System.Windows.Forms.WebBrowserDocumentCompletedEventHandler(this.WebBrowser_DocumentCompleted);
            // 
            // LoadingCircle
            // 
            this.LoadingCircle.Active = false;
            this.LoadingCircle.Color = System.Drawing.Color.DarkGray;
            this.LoadingCircle.Dock = System.Windows.Forms.DockStyle.Fill;
            this.LoadingCircle.InnerCircleRadius = 6;
            this.LoadingCircle.Location = new System.Drawing.Point(0, 0);
            this.LoadingCircle.Name = "LoadingCircle";
            this.LoadingCircle.NumberSpoke = 24;
            this.LoadingCircle.OuterCircleRadius = 7;
            this.LoadingCircle.RotationSpeed = 50;
            this.LoadingCircle.Size = new System.Drawing.Size(624, 347);
            this.LoadingCircle.SpokeThickness = 3;
            this.LoadingCircle.StylePreset = LoadingCircle.StylePresets.IE7;
            this.LoadingCircle.TabIndex = 2;
            // 
            // frmPlayer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Black;
            this.ClientSize = new System.Drawing.Size(624, 347);
            this.Controls.Add(this.LoadingCircle);
            this.Controls.Add(this.WebBrowser);
            this.Controls.Add(this.FlashPlayer);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmPlayer";
            this.Text = "Player";
            this.Load += new System.EventHandler(this.Player_Load);
            ((System.ComponentModel.ISupportInitialize)(this.FlashPlayer)).EndInit();
            this.PlayerContextMenu.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private FlashPlayer FlashPlayer;
        private System.Windows.Forms.ContextMenuStrip PlayerContextMenu;
        private System.Windows.Forms.ToolStripMenuItem MenuAlwaysOnTop;
        private System.Windows.Forms.ToolStripMenuItem MenuOpenChat;
        private System.Windows.Forms.WebBrowser WebBrowser;
        private LoadingCircle LoadingCircle;
    }
}