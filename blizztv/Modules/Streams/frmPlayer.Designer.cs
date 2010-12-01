using BlizzTV.ModuleLib.Players;

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
            this.Player = new FlashPlayer();
            this.PlayerContextMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.MenuAlwaysOnTop = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuOpenChat = new System.Windows.Forms.ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)(this.Player)).BeginInit();
            this.PlayerContextMenu.SuspendLayout();
            this.SuspendLayout();
            // 
            // Player
            // 
            this.Player.ContextMenuStrip = this.PlayerContextMenu;
            this.Player.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Player.Enabled = true;
            this.Player.Location = new System.Drawing.Point(0, 0);
            this.Player.Name = "Player";
            this.Player.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("Player.OcxState")));
            this.Player.Size = new System.Drawing.Size(624, 347);
            this.Player.TabIndex = 0;
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
            this.MenuAlwaysOnTop.Name = "MenuAlwaysOnTop";
            this.MenuAlwaysOnTop.Size = new System.Drawing.Size(154, 22);
            this.MenuAlwaysOnTop.Text = "Always On Top";
            this.MenuAlwaysOnTop.Click += new System.EventHandler(this.MenuAlwaysOnTop_Click);
            // 
            // MenuOpenChat
            // 
            this.MenuOpenChat.Name = "MenuOpenChat";
            this.MenuOpenChat.Size = new System.Drawing.Size(154, 22);
            this.MenuOpenChat.Text = "Open Chat";
            this.MenuOpenChat.Click += new System.EventHandler(this.MenuOpenChat_Click);
            // 
            // frmPlayer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Black;
            this.ClientSize = new System.Drawing.Size(624, 347);
            this.Controls.Add(this.Player);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmPlayer";
            this.Text = "Player";
            this.Load += new System.EventHandler(this.Player_Load);
            ((System.ComponentModel.ISupportInitialize)(this.Player)).EndInit();
            this.PlayerContextMenu.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private FlashPlayer Player;
        private System.Windows.Forms.ContextMenuStrip PlayerContextMenu;
        private System.Windows.Forms.ToolStripMenuItem MenuAlwaysOnTop;
        private System.Windows.Forms.ToolStripMenuItem MenuOpenChat;
    }
}