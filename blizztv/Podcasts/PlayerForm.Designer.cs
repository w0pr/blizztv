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
            this.MediaPlayer = new BlizzTV.Controls.MediaPlayer.MediaPlayer();
            this.LabelSlider = new System.Windows.Forms.Label();
            this.pictureSlider = new System.Windows.Forms.PictureBox();
            this.gradientPanel = new BlizzTV.Controls.Panels.GradientPanel();
            ((System.ComponentModel.ISupportInitialize)(this.MediaPlayer)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureSlider)).BeginInit();
            this.gradientPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // MediaPlayer
            // 
            this.MediaPlayer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.MediaPlayer.Enabled = true;
            this.MediaPlayer.Location = new System.Drawing.Point(0, 0);
            this.MediaPlayer.Name = "MediaPlayer";
            this.MediaPlayer.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("MediaPlayer.OcxState")));
            this.MediaPlayer.Size = new System.Drawing.Size(623, 406);
            this.MediaPlayer.TabIndex = 4;
            this.MediaPlayer.OpenStateChange += new AxWMPLib._WMPOCXEvents_OpenStateChangeEventHandler(this.AudioPlayer_OpenStateChange);
            // 
            // LabelSlider
            // 
            this.LabelSlider.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.LabelSlider.BackColor = System.Drawing.Color.Black;
            this.LabelSlider.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.LabelSlider.ForeColor = System.Drawing.Color.LightGreen;
            this.LabelSlider.Location = new System.Drawing.Point(25, 3);
            this.LabelSlider.Name = "LabelSlider";
            this.LabelSlider.Size = new System.Drawing.Size(150, 16);
            this.LabelSlider.TabIndex = 5;
            this.LabelSlider.Text = "Lorem Ipsum is simply dummy text of the printing and typesetting industry.";
            this.LabelSlider.Visible = false;
            // 
            // pictureSlider
            // 
            this.pictureSlider.BackColor = System.Drawing.Color.Transparent;
            this.pictureSlider.Image = ((System.Drawing.Image)(resources.GetObject("pictureSlider.Image")));
            this.pictureSlider.Location = new System.Drawing.Point(3, 3);
            this.pictureSlider.Name = "pictureSlider";
            this.pictureSlider.Size = new System.Drawing.Size(16, 16);
            this.pictureSlider.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pictureSlider.TabIndex = 6;
            this.pictureSlider.TabStop = false;
            this.pictureSlider.Visible = false;
            // 
            // gradientPanel
            // 
            this.gradientPanel.BorderColor = System.Drawing.Color.Gray;
            this.gradientPanel.Controls.Add(this.pictureSlider);
            this.gradientPanel.Controls.Add(this.LabelSlider);
            this.gradientPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gradientPanel.GradientEndColor = System.Drawing.Color.Black;
            this.gradientPanel.GradientStartColor = System.Drawing.Color.DimGray;
            this.gradientPanel.Location = new System.Drawing.Point(0, 0);
            this.gradientPanel.Name = "gradientPanel";
            this.gradientPanel.ShadowOffSet = 0;
            this.gradientPanel.Size = new System.Drawing.Size(623, 406);
            this.gradientPanel.TabIndex = 7;
            this.gradientPanel.Visible = false;
            // 
            // PlayerForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(623, 406);
            this.Controls.Add(this.MediaPlayer);
            this.Controls.Add(this.gradientPanel);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "PlayerForm";
            this.Text = "PlayerForm";
            this.Load += new System.EventHandler(this.PlayerForm_Load);
            this.Controls.SetChildIndex(this.gradientPanel, 0);
            this.Controls.SetChildIndex(this.MediaPlayer, 0);
            ((System.ComponentModel.ISupportInitialize)(this.MediaPlayer)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureSlider)).EndInit();
            this.gradientPanel.ResumeLayout(false);
            this.gradientPanel.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private MediaPlayer MediaPlayer;
        private System.Windows.Forms.Label LabelSlider;
        private System.Windows.Forms.PictureBox pictureSlider;
        private Controls.Panels.GradientPanel gradientPanel;


    }
}