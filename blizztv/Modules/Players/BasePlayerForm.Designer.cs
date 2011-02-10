namespace BlizzTV.Modules.Players
{
    partial class BasePlayerForm
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
            this.LoadingCircle = new BlizzTV.Controls.LoadingCircle.LoadingCircle();
            this.SuspendLayout();
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
            this.LoadingCircle.StylePreset = BlizzTV.Controls.LoadingCircle.LoadingCircle.StylePresets.IE7;
            this.LoadingCircle.TabIndex = 3;
            // 
            // PlayerWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Black;
            this.ClientSize = new System.Drawing.Size(624, 347);
            this.Controls.Add(this.LoadingCircle);
            this.Name = "PlayerWindow";
            this.Text = "PlayerWindow";
            this.ResizeEnd += new System.EventHandler(this.PlayerWindow_ResizeEnd);
            this.ResumeLayout(false);

        }

        #endregion

        protected Controls.LoadingCircle.LoadingCircle LoadingCircle;
    }
}