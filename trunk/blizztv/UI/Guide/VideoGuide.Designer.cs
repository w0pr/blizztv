namespace BlizzTV.UI.Guide
{
    partial class VideoGuide
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(VideoGuide));
            this.FlashPlayer = new BlizzTV.Media.Controls.Flash.FlashPlayer();
            ((System.ComponentModel.ISupportInitialize)(this.FlashPlayer)).BeginInit();
            this.SuspendLayout();
            // 
            // FlashPlayer
            // 
            this.FlashPlayer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.FlashPlayer.Enabled = true;
            this.FlashPlayer.Location = new System.Drawing.Point(0, 0);
            this.FlashPlayer.Name = "FlashPlayer";
            this.FlashPlayer.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("FlashPlayer.OcxState")));
            this.FlashPlayer.Size = new System.Drawing.Size(624, 347);
            this.FlashPlayer.TabIndex = 17;
            this.FlashPlayer.Visible = false;
            this.FlashPlayer.OnReadyStateChange += new AxShockwaveFlashObjects._IShockwaveFlashEvents_OnReadyStateChangeEventHandler(this.FlashPlayer_OnReadyStateChange);
            // 
            // VideoGuide
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(624, 347);
            this.Controls.Add(this.FlashPlayer);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "VideoGuide";
            this.Text = "VideoGuide";
            this.Load += new System.EventHandler(this.VideoGuide_Load);
            this.Controls.SetChildIndex(this.FlashPlayer, 0);
            ((System.ComponentModel.ISupportInitialize)(this.FlashPlayer)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Media.Controls.Flash.FlashPlayer FlashPlayer;
    }
}