namespace LibEvents
{
    partial class frmEventViewer
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
            this.LabelFullTitle = new System.Windows.Forms.Label();
            this.LabelStatus = new System.Windows.Forms.Label();
            this.LabelLocalTime = new System.Windows.Forms.Label();
            this.RichTextboxDescription = new System.Windows.Forms.RichTextBox();
            this.ButtonClose = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // LabelFullTitle
            // 
            this.LabelFullTitle.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.LabelFullTitle.Font = new System.Drawing.Font("Tahoma", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.LabelFullTitle.Location = new System.Drawing.Point(8, 8);
            this.LabelFullTitle.Name = "LabelFullTitle";
            this.LabelFullTitle.Size = new System.Drawing.Size(376, 32);
            this.LabelFullTitle.TabIndex = 0;
            this.LabelFullTitle.Text = "full-title";
            // 
            // LabelStatus
            // 
            this.LabelStatus.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.LabelStatus.Location = new System.Drawing.Point(8, 48);
            this.LabelStatus.Name = "LabelStatus";
            this.LabelStatus.Size = new System.Drawing.Size(184, 16);
            this.LabelStatus.TabIndex = 3;
            this.LabelStatus.Text = "status";
            // 
            // LabelLocalTime
            // 
            this.LabelLocalTime.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.LabelLocalTime.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.LabelLocalTime.Location = new System.Drawing.Point(200, 48);
            this.LabelLocalTime.Name = "LabelLocalTime";
            this.LabelLocalTime.Size = new System.Drawing.Size(184, 16);
            this.LabelLocalTime.TabIndex = 4;
            this.LabelLocalTime.Text = "local time";
            this.LabelLocalTime.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // RichTextboxDescription
            // 
            this.RichTextboxDescription.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.RichTextboxDescription.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.RichTextboxDescription.Location = new System.Drawing.Point(8, 72);
            this.RichTextboxDescription.Name = "RichTextboxDescription";
            this.RichTextboxDescription.ReadOnly = true;
            this.RichTextboxDescription.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.Vertical;
            this.RichTextboxDescription.Size = new System.Drawing.Size(376, 80);
            this.RichTextboxDescription.TabIndex = 5;
            this.RichTextboxDescription.TabStop = false;
            this.RichTextboxDescription.Text = "";
            this.RichTextboxDescription.LinkClicked += new System.Windows.Forms.LinkClickedEventHandler(this.RichTextboxDescription_LinkClicked);
            // 
            // ButtonClose
            // 
            this.ButtonClose.Location = new System.Drawing.Point(309, 158);
            this.ButtonClose.Name = "ButtonClose";
            this.ButtonClose.Size = new System.Drawing.Size(75, 23);
            this.ButtonClose.TabIndex = 6;
            this.ButtonClose.Text = "Close";
            this.ButtonClose.UseVisualStyleBackColor = true;
            this.ButtonClose.Click += new System.EventHandler(this.ButtonClose_Click);
            // 
            // frmEventViewer
            // 
            this.AcceptButton = this.ButtonClose;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(393, 189);
            this.Controls.Add(this.ButtonClose);
            this.Controls.Add(this.RichTextboxDescription);
            this.Controls.Add(this.LabelLocalTime);
            this.Controls.Add(this.LabelStatus);
            this.Controls.Add(this.LabelFullTitle);
            this.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmEventViewer";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Event Viewer";
            this.Load += new System.EventHandler(this.frmEventViewer_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label LabelFullTitle;
        private System.Windows.Forms.Label LabelStatus;
        private System.Windows.Forms.Label LabelLocalTime;
        private System.Windows.Forms.RichTextBox RichTextboxDescription;
        private System.Windows.Forms.Button ButtonClose;

    }
}