using BlizzTV.Controls.FlashPlayer;

namespace BlizzTV.Videos
{
    partial class PlayerForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PlayerForm));
            this.PlayerContextMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.MenuAlwaysOnTop = new System.Windows.Forms.ToolStripMenuItem();
            this.FlashPlayer = new BlizzTV.Controls.FlashPlayer.FlashPlayer();
            this.PlayerContextMenu.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.FlashPlayer)).BeginInit();
            this.SuspendLayout();
            // 
            // PlayerContextMenu
            // 
            this.PlayerContextMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.MenuAlwaysOnTop});
            this.PlayerContextMenu.Name = "PlayerContextMenu";
            this.PlayerContextMenu.Size = new System.Drawing.Size(155, 48);
            // 
            // MenuAlwaysOnTop
            // 
            this.MenuAlwaysOnTop.Image = ((System.Drawing.Image)(resources.GetObject("MenuAlwaysOnTop.Image")));
            this.MenuAlwaysOnTop.Name = "MenuAlwaysOnTop";
            this.MenuAlwaysOnTop.Size = new System.Drawing.Size(154, 22);
            this.MenuAlwaysOnTop.Text = "Always On Top";
            this.MenuAlwaysOnTop.Click += new System.EventHandler(this.MenuAlwaysOnTop_Click);
            // 
            // FlashPlayer
            // 
            this.FlashPlayer.ContextMenuStrip = this.PlayerContextMenu;
            this.FlashPlayer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.FlashPlayer.Enabled = true;
            this.FlashPlayer.Location = new System.Drawing.Point(0, 0);
            this.FlashPlayer.Name = "FlashPlayer";
            this.FlashPlayer.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("FlashPlayer.OcxState")));
            this.FlashPlayer.Size = new System.Drawing.Size(624, 347);
            this.FlashPlayer.TabIndex = 0;
            this.FlashPlayer.Visible = false;
            this.FlashPlayer.OnReadyStateChange += new AxShockwaveFlashObjects._IShockwaveFlashEvents_OnReadyStateChangeEventHandler(this.FlashPlayer_OnReadyStateChange);
            // 
            // PlayerForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Black;
            this.ClientSize = new System.Drawing.Size(624, 347);
            this.Controls.Add(this.FlashPlayer);
            this.Cursor = System.Windows.Forms.Cursors.Default;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "PlayerForm";
            this.Text = "Player";
            this.Load += new System.EventHandler(this.PlayerForm_Load);
            this.ResizeEnd += new System.EventHandler(this.PlayerForm_ResizeEnd);
            this.Controls.SetChildIndex(this.FlashPlayer, 0);
            this.PlayerContextMenu.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.FlashPlayer)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private FlashPlayer FlashPlayer;
        private System.Windows.Forms.ContextMenuStrip PlayerContextMenu;
        private System.Windows.Forms.ToolStripMenuItem MenuAlwaysOnTop;
    }
}