﻿using BlizzTV.Controls.Animations;

namespace BlizzTV.Media.Player
{
    partial class BasePlayer
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
            this.LoadingAnimation = new BlizzTV.Controls.Animations.LoadingAnimation();
            this.SuspendLayout();
            // 
            // LoadingAnimation
            // 
            this.LoadingAnimation.Active = false;
            this.LoadingAnimation.Color = System.Drawing.Color.DarkGray;
            this.LoadingAnimation.Dock = System.Windows.Forms.DockStyle.Fill;
            this.LoadingAnimation.InnerCircleRadius = 6;
            this.LoadingAnimation.Location = new System.Drawing.Point(0, 0);
            this.LoadingAnimation.Name = "LoadingAnimation";
            this.LoadingAnimation.NumberSpoke = 24;
            this.LoadingAnimation.OuterCircleRadius = 7;
            this.LoadingAnimation.RotationSpeed = 50;
            this.LoadingAnimation.Size = new System.Drawing.Size(624, 347);
            this.LoadingAnimation.SpokeThickness = 3;
            this.LoadingAnimation.StylePreset = BlizzTV.Controls.Animations.LoadingAnimation.StylePresets.IE7;
            this.LoadingAnimation.TabIndex = 3;
            this.LoadingAnimation.Visible = false;
            // 
            // BasePlayerForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Black;
            this.ClientSize = new System.Drawing.Size(624, 347);
            this.Controls.Add(this.LoadingAnimation);
            this.Name = "BasePlayerForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "PlayerWindow";
            this.ResumeLayout(false);

        }

        #endregion

        private LoadingAnimation LoadingAnimation;
    }
}