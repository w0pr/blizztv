namespace BlizzTV.Modules.Events
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmEventViewer));
            this.LabelFullTitle = new System.Windows.Forms.Label();
            this.LabelTimeLeft = new System.Windows.Forms.Label();
            this.RichTextboxDescription = new System.Windows.Forms.RichTextBox();
            this.ButtonClose = new System.Windows.Forms.Button();
            this.ButtonSetupAlarm = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.LabelAlarm = new System.Windows.Forms.Label();
            this.LabelLocalTime = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.PictureAlarmIcon = new System.Windows.Forms.PictureBox();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.PictureAlarmIcon)).BeginInit();
            this.SuspendLayout();
            // 
            // LabelFullTitle
            // 
            this.LabelFullTitle.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.LabelFullTitle.Font = new System.Drawing.Font("Tahoma", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.LabelFullTitle.Location = new System.Drawing.Point(-2, 0);
            this.LabelFullTitle.Name = "LabelFullTitle";
            this.LabelFullTitle.Size = new System.Drawing.Size(456, 32);
            this.LabelFullTitle.TabIndex = 0;
            this.LabelFullTitle.Text = "full-title";
            // 
            // LabelTimeLeft
            // 
            this.LabelTimeLeft.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.LabelTimeLeft.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.LabelTimeLeft.Location = new System.Drawing.Point(241, 32);
            this.LabelTimeLeft.Name = "LabelTimeLeft";
            this.LabelTimeLeft.Size = new System.Drawing.Size(202, 15);
            this.LabelTimeLeft.TabIndex = 3;
            this.LabelTimeLeft.Text = "status";
            // 
            // RichTextboxDescription
            // 
            this.RichTextboxDescription.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.RichTextboxDescription.Dock = System.Windows.Forms.DockStyle.Fill;
            this.RichTextboxDescription.Location = new System.Drawing.Point(3, 17);
            this.RichTextboxDescription.Name = "RichTextboxDescription";
            this.RichTextboxDescription.ReadOnly = true;
            this.RichTextboxDescription.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.Vertical;
            this.RichTextboxDescription.Size = new System.Drawing.Size(440, 123);
            this.RichTextboxDescription.TabIndex = 5;
            this.RichTextboxDescription.TabStop = false;
            this.RichTextboxDescription.Text = "";
            this.RichTextboxDescription.LinkClicked += new System.Windows.Forms.LinkClickedEventHandler(this.RichTextboxDescription_LinkClicked);
            // 
            // ButtonClose
            // 
            this.ButtonClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.ButtonClose.Location = new System.Drawing.Point(374, 202);
            this.ButtonClose.Name = "ButtonClose";
            this.ButtonClose.Size = new System.Drawing.Size(75, 23);
            this.ButtonClose.TabIndex = 6;
            this.ButtonClose.Text = "Close";
            this.ButtonClose.UseVisualStyleBackColor = true;
            this.ButtonClose.Click += new System.EventHandler(this.ButtonClose_Click);
            // 
            // ButtonSetupAlarm
            // 
            this.ButtonSetupAlarm.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.ButtonSetupAlarm.Enabled = false;
            this.ButtonSetupAlarm.Image = ((System.Drawing.Image)(resources.GetObject("ButtonSetupAlarm.Image")));
            this.ButtonSetupAlarm.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.ButtonSetupAlarm.Location = new System.Drawing.Point(278, 202);
            this.ButtonSetupAlarm.Name = "ButtonSetupAlarm";
            this.ButtonSetupAlarm.Size = new System.Drawing.Size(90, 23);
            this.ButtonSetupAlarm.TabIndex = 7;
            this.ButtonSetupAlarm.Text = "Setup Alarm";
            this.ButtonSetupAlarm.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.ButtonSetupAlarm.UseVisualStyleBackColor = true;
            this.ButtonSetupAlarm.Click += new System.EventHandler(this.ButtonSetupAlarm_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.RichTextboxDescription);
            this.groupBox1.Location = new System.Drawing.Point(3, 53);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(446, 143);
            this.groupBox1.TabIndex = 8;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Details";
            // 
            // LabelAlarm
            // 
            this.LabelAlarm.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.LabelAlarm.Location = new System.Drawing.Point(21, 207);
            this.LabelAlarm.Name = "LabelAlarm";
            this.LabelAlarm.Size = new System.Drawing.Size(225, 16);
            this.LabelAlarm.TabIndex = 9;
            this.LabelAlarm.Text = "No alarm is set for event.";
            this.LabelAlarm.Visible = false;
            // 
            // LabelLocalTime
            // 
            this.LabelLocalTime.Location = new System.Drawing.Point(20, 31);
            this.LabelLocalTime.Name = "LabelLocalTime";
            this.LabelLocalTime.Size = new System.Drawing.Size(110, 15);
            this.LabelLocalTime.TabIndex = 10;
            this.LabelLocalTime.Text = "local-time";
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(3, 31);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(21, 16);
            this.pictureBox1.TabIndex = 11;
            this.pictureBox1.TabStop = false;
            // 
            // pictureBox2
            // 
            this.pictureBox2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.pictureBox2.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox2.Image")));
            this.pictureBox2.Location = new System.Drawing.Point(225, 31);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(21, 16);
            this.pictureBox2.TabIndex = 12;
            this.pictureBox2.TabStop = false;
            // 
            // PictureAlarmIcon
            // 
            this.PictureAlarmIcon.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.PictureAlarmIcon.Image = ((System.Drawing.Image)(resources.GetObject("PictureAlarmIcon.Image")));
            this.PictureAlarmIcon.Location = new System.Drawing.Point(3, 207);
            this.PictureAlarmIcon.Name = "PictureAlarmIcon";
            this.PictureAlarmIcon.Size = new System.Drawing.Size(21, 16);
            this.PictureAlarmIcon.TabIndex = 13;
            this.PictureAlarmIcon.TabStop = false;
            this.PictureAlarmIcon.Visible = false;
            // 
            // frmEventViewer
            // 
            this.AcceptButton = this.ButtonClose;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(451, 230);
            this.Controls.Add(this.LabelAlarm);
            this.Controls.Add(this.LabelTimeLeft);
            this.Controls.Add(this.LabelLocalTime);
            this.Controls.Add(this.PictureAlarmIcon);
            this.Controls.Add(this.pictureBox2);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.ButtonSetupAlarm);
            this.Controls.Add(this.ButtonClose);
            this.Controls.Add(this.LabelFullTitle);
            this.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmEventViewer";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Event Viewer";
            this.Load += new System.EventHandler(this.frmEventViewer_Load);
            this.ResizeEnd += new System.EventHandler(this.frmEventViewer_ResizeEnd);
            this.groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.PictureAlarmIcon)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label LabelFullTitle;
        private System.Windows.Forms.Label LabelTimeLeft;
        private System.Windows.Forms.RichTextBox RichTextboxDescription;
        private System.Windows.Forms.Button ButtonClose;
        private System.Windows.Forms.Button ButtonSetupAlarm;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label LabelAlarm;
        private System.Windows.Forms.Label LabelLocalTime;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.PictureBox PictureAlarmIcon;

    }
}