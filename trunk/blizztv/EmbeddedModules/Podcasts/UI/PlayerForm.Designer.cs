using BlizzTV.Media.Controls.WMP;

namespace BlizzTV.EmbeddedModules.Podcasts.UI
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
            this.MediaPlayer = new WMPPlayer();
            this.PlayerContextMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.MenuAlwaysOnTop = new System.Windows.Forms.ToolStripMenuItem();
            this.LabelSlider = new System.Windows.Forms.Label();
            this.gradientPanel = new BlizzTV.Controls.Panels.GradientPanel();
            this.labelPosition = new System.Windows.Forms.Label();
            this.pictureMuteControl = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.MediaPlayer)).BeginInit();
            this.PlayerContextMenu.SuspendLayout();
            this.gradientPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureMuteControl)).BeginInit();
            this.SuspendLayout();
            // 
            // MediaPlayer
            // 
            this.MediaPlayer.ContextMenuStrip = this.PlayerContextMenu;
            this.MediaPlayer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.MediaPlayer.Enabled = true;
            this.MediaPlayer.Location = new System.Drawing.Point(0, 0);
            this.MediaPlayer.Name = "MediaPlayer";
            this.MediaPlayer.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("MediaPlayer.OcxState")));
            this.MediaPlayer.Size = new System.Drawing.Size(259, 112);
            this.MediaPlayer.TabIndex = 4;
            this.MediaPlayer.PlayStateChange += new AxWMPLib._WMPOCXEvents_PlayStateChangeEventHandler(this.MediaPlayer_PlayStateChange);
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
            // LabelSlider
            // 
            this.LabelSlider.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.LabelSlider.BackColor = System.Drawing.Color.Black;
            this.LabelSlider.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.LabelSlider.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(160)))), ((int)(((byte)(171)))), ((int)(((byte)(177)))));
            this.LabelSlider.Location = new System.Drawing.Point(90, 0);
            this.LabelSlider.Name = "LabelSlider";
            this.LabelSlider.Size = new System.Drawing.Size(150, 17);
            this.LabelSlider.TabIndex = 5;
            this.LabelSlider.Visible = false;
            // 
            // gradientPanel
            // 
            this.gradientPanel.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(194)))));
            this.gradientPanel.Controls.Add(this.labelPosition);
            this.gradientPanel.Controls.Add(this.pictureMuteControl);
            this.gradientPanel.Controls.Add(this.LabelSlider);
            this.gradientPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gradientPanel.GradientEndColor = System.Drawing.Color.FromArgb(((int)(((byte)(135)))), ((int)(((byte)(136)))), ((int)(((byte)(141)))));
            this.gradientPanel.GradientStartColor = System.Drawing.Color.FromArgb(((int)(((byte)(54)))), ((int)(((byte)(58)))), ((int)(((byte)(67)))));
            this.gradientPanel.Location = new System.Drawing.Point(0, 0);
            this.gradientPanel.Name = "gradientPanel";
            this.gradientPanel.ShadowOffSet = 0;
            this.gradientPanel.Size = new System.Drawing.Size(259, 112);
            this.gradientPanel.TabIndex = 7;
            this.gradientPanel.Visible = false;
            // 
            // labelPosition
            // 
            this.labelPosition.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.labelPosition.BackColor = System.Drawing.Color.Black;
            this.labelPosition.Font = new System.Drawing.Font("Arial", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.labelPosition.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(175)))), ((int)(((byte)(188)))), ((int)(((byte)(196)))));
            this.labelPosition.Location = new System.Drawing.Point(40, 1);
            this.labelPosition.Name = "labelPosition";
            this.labelPosition.Size = new System.Drawing.Size(44, 16);
            this.labelPosition.TabIndex = 7;
            this.labelPosition.Text = "00:00:00";
            this.labelPosition.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.labelPosition.Visible = false;
            // 
            // pictureMuteControl
            // 
            this.pictureMuteControl.BackColor = System.Drawing.Color.Transparent;
            this.pictureMuteControl.Image = ((System.Drawing.Image)(resources.GetObject("pictureMuteControl.Image")));
            this.pictureMuteControl.Location = new System.Drawing.Point(2, 1);
            this.pictureMuteControl.Name = "pictureMuteControl";
            this.pictureMuteControl.Size = new System.Drawing.Size(16, 16);
            this.pictureMuteControl.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureMuteControl.TabIndex = 6;
            this.pictureMuteControl.TabStop = false;
            this.pictureMuteControl.Visible = false;
            this.pictureMuteControl.Click += new System.EventHandler(this.pictureMuteControl_Click);
            // 
            // PlayerForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(259, 112);
            this.Controls.Add(this.gradientPanel);
            this.Controls.Add(this.MediaPlayer);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "PlayerForm";
            this.Text = "PlayerForm";
            this.Load += new System.EventHandler(this.PlayerForm_Load);
            this.ResizeEnd += new System.EventHandler(this.PlayerForm_ResizeEnd);
            this.Controls.SetChildIndex(this.MediaPlayer, 0);
            this.Controls.SetChildIndex(this.gradientPanel, 0);
            ((System.ComponentModel.ISupportInitialize)(this.MediaPlayer)).EndInit();
            this.PlayerContextMenu.ResumeLayout(false);
            this.gradientPanel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureMuteControl)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private WMPPlayer MediaPlayer;
        private System.Windows.Forms.Label LabelSlider;
        private System.Windows.Forms.PictureBox pictureMuteControl;
        private Controls.Panels.GradientPanel gradientPanel;
        private System.Windows.Forms.Label labelPosition;
        private System.Windows.Forms.ContextMenuStrip PlayerContextMenu;
        private System.Windows.Forms.ToolStripMenuItem MenuAlwaysOnTop;


    }
}