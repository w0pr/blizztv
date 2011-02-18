using BlizzTV.Controls.MediaPlayer;

namespace BlizzTV.Podcasts
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PlayerForm));
            this.AudioPlayer = new BlizzTV.Controls.MediaPlayer.MediaPlayer();
            ((System.ComponentModel.ISupportInitialize)(this.AudioPlayer)).BeginInit();
            this.SuspendLayout();
            // 
            // LoadingCircle
            // 
            this.LoadingCircle.Size = new System.Drawing.Size(360, 84);
            // 
            // AudioPlayer
            // 
            this.AudioPlayer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.AudioPlayer.Enabled = true;
            this.AudioPlayer.Location = new System.Drawing.Point(0, 0);
            this.AudioPlayer.Name = "AudioPlayer";
            this.AudioPlayer.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("AudioPlayer.OcxState")));
            this.AudioPlayer.Size = new System.Drawing.Size(360, 84);
            this.AudioPlayer.TabIndex = 4;
            this.AudioPlayer.OpenStateChange += new AxWMPLib._WMPOCXEvents_OpenStateChangeEventHandler(this.AudioPlayer_OpenStateChange);
            // 
            // PlayerForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(360, 84);
            this.Controls.Add(this.AudioPlayer);
            this.Name = "PlayerForm";
            this.Text = "PlayerForm";
            this.Load += new System.EventHandler(this.PlayerForm_Load);
            this.Controls.SetChildIndex(this.AudioPlayer, 0);
            this.Controls.SetChildIndex(this.LoadingCircle, 0);
            ((System.ComponentModel.ISupportInitialize)(this.AudioPlayer)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private MediaPlayer AudioPlayer;


    }
}