namespace BlizzTV.CommonLib.Updates
{
    partial class frmUpdater
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
            this.LabelStatus = new System.Windows.Forms.Label();
            this.progressBarUpdater = new System.Windows.Forms.ProgressBar();
            this.SuspendLayout();
            // 
            // LabelStatus
            // 
            this.LabelStatus.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.LabelStatus.Location = new System.Drawing.Point(12, 48);
            this.LabelStatus.Name = "LabelStatus";
            this.LabelStatus.Size = new System.Drawing.Size(367, 14);
            this.LabelStatus.TabIndex = 0;
            this.LabelStatus.Text = "Please wait while update is being downloaded..";
            this.LabelStatus.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // progressBarUpdater
            // 
            this.progressBarUpdater.Location = new System.Drawing.Point(12, 12);
            this.progressBarUpdater.Name = "progressBarUpdater";
            this.progressBarUpdater.Size = new System.Drawing.Size(367, 23);
            this.progressBarUpdater.TabIndex = 1;
            // 
            // frmUpdater
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(391, 71);
            this.Controls.Add(this.progressBarUpdater);
            this.Controls.Add(this.LabelStatus);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmUpdater";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Updater";
            this.Load += new System.EventHandler(this.frmUpdater_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label LabelStatus;
        private System.Windows.Forms.ProgressBar progressBarUpdater;
    }
}