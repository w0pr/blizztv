namespace LibStreams
{
    partial class Player
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Player));
            this.Stage = new LibBlizzTV.Players.FlashPlayer();
            this.PlayerContextMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.MenuAlwaysOnTop = new System.Windows.Forms.ToolStripMenuItem();
            this.openChatToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)(this.Stage)).BeginInit();
            this.PlayerContextMenu.SuspendLayout();
            this.SuspendLayout();
            // 
            // Stage
            // 
            this.Stage.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Stage.Enabled = true;
            this.Stage.Location = new System.Drawing.Point(0, 0);
            this.Stage.Name = "Stage";
            this.Stage.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("Stage.OcxState")));
            this.Stage.Size = new System.Drawing.Size(624, 347);
            this.Stage.TabIndex = 0;
            // 
            // PlayerContextMenu
            // 
            this.PlayerContextMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.MenuAlwaysOnTop,
            this.openChatToolStripMenuItem});
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
            // openChatToolStripMenuItem
            // 
            this.openChatToolStripMenuItem.Name = "openChatToolStripMenuItem";
            this.openChatToolStripMenuItem.Size = new System.Drawing.Size(154, 22);
            this.openChatToolStripMenuItem.Text = "Open Chat";
            // 
            // Player
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Black;
            this.ClientSize = new System.Drawing.Size(624, 347);
            this.Controls.Add(this.Stage);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Player";
            this.Text = "Player";
            this.Load += new System.EventHandler(this.Player_Load);
            ((System.ComponentModel.ISupportInitialize)(this.Stage)).EndInit();
            this.PlayerContextMenu.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private LibBlizzTV.Players.FlashPlayer Stage;
        private System.Windows.Forms.ContextMenuStrip PlayerContextMenu;
        private System.Windows.Forms.ToolStripMenuItem MenuAlwaysOnTop;
        private System.Windows.Forms.ToolStripMenuItem openChatToolStripMenuItem;
    }
}