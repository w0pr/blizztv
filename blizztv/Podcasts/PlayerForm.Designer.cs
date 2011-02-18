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
            this.LabelSlider = new System.Windows.Forms.Label();
            this.pictureSlider = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.AudioPlayer)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureSlider)).BeginInit();
            this.SuspendLayout();
            // 
            // AudioPlayer
            // 
            this.AudioPlayer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.AudioPlayer.Enabled = true;
            this.AudioPlayer.Location = new System.Drawing.Point(0, 0);
            this.AudioPlayer.Name = "AudioPlayer";
            this.AudioPlayer.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("AudioPlayer.OcxState")));
            this.AudioPlayer.Size = new System.Drawing.Size(419, 134);
            this.AudioPlayer.TabIndex = 4;
            this.AudioPlayer.OpenStateChange += new AxWMPLib._WMPOCXEvents_OpenStateChangeEventHandler(this.AudioPlayer_OpenStateChange);
            // 
            // LabelSlider
            // 
            this.LabelSlider.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.LabelSlider.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.LabelSlider.ForeColor = System.Drawing.Color.LawnGreen;
            this.LabelSlider.Location = new System.Drawing.Point(24, 2);
            this.LabelSlider.Name = "LabelSlider";
            this.LabelSlider.Size = new System.Drawing.Size(150, 16);
            this.LabelSlider.TabIndex = 5;
            this.LabelSlider.Text = "Lorem Ipsum is simply dummy text of the printing and typesetting industry.";
            this.LabelSlider.Visible = false;
            // 
            // pictureSlider
            // 
            this.pictureSlider.Image = ((System.Drawing.Image)(resources.GetObject("pictureSlider.Image")));
            this.pictureSlider.Location = new System.Drawing.Point(2, 2);
            this.pictureSlider.Name = "pictureSlider";
            this.pictureSlider.Size = new System.Drawing.Size(16, 16);
            this.pictureSlider.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pictureSlider.TabIndex = 6;
            this.pictureSlider.TabStop = false;
            this.pictureSlider.Visible = false;
            // 
            // PlayerForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(419, 134);
            this.Controls.Add(this.pictureSlider);
            this.Controls.Add(this.LabelSlider);
            this.Controls.Add(this.AudioPlayer);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "PlayerForm";
            this.Text = "PlayerForm";
            this.Load += new System.EventHandler(this.PlayerForm_Load);
            this.Controls.SetChildIndex(this.AudioPlayer, 0);
            this.Controls.SetChildIndex(this.LabelSlider, 0);
            this.Controls.SetChildIndex(this.pictureSlider, 0);
            ((System.ComponentModel.ISupportInitialize)(this.AudioPlayer)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureSlider)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private MediaPlayer AudioPlayer;
        private System.Windows.Forms.Label LabelSlider;
        private System.Windows.Forms.PictureBox pictureSlider;


    }
}