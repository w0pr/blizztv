namespace BlizzTV.CommonLib.Notifications
{
    partial class frmQueuedNotifications
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmQueuedNotifications));
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.label1 = new System.Windows.Forms.Label();
            this.listViewNotifications = new System.Windows.Forms.ListView();
            this.colTitle = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.ButtonClose = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(2, 2);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(24, 20);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.Location = new System.Drawing.Point(32, 2);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(451, 20);
            this.label1.TabIndex = 1;
            this.label1.Text = "Here is the notifications you may be interested. ";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // listViewNotifications
            // 
            this.listViewNotifications.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.listViewNotifications.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.colTitle});
            this.listViewNotifications.FullRowSelect = true;
            this.listViewNotifications.Location = new System.Drawing.Point(2, 25);
            this.listViewNotifications.MultiSelect = false;
            this.listViewNotifications.Name = "listViewNotifications";
            this.listViewNotifications.Size = new System.Drawing.Size(481, 229);
            this.listViewNotifications.TabIndex = 2;
            this.listViewNotifications.UseCompatibleStateImageBehavior = false;
            this.listViewNotifications.View = System.Windows.Forms.View.Details;
            this.listViewNotifications.DoubleClick += new System.EventHandler(this.listViewNotifications_DoubleClick);
            // 
            // colTitle
            // 
            this.colTitle.Text = "Title";
            this.colTitle.Width = 460;
            // 
            // ButtonClose
            // 
            this.ButtonClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.ButtonClose.Location = new System.Drawing.Point(398, 221);
            this.ButtonClose.Name = "ButtonClose";
            this.ButtonClose.Size = new System.Drawing.Size(75, 23);
            this.ButtonClose.TabIndex = 3;
            this.ButtonClose.Text = "Close";
            this.ButtonClose.UseVisualStyleBackColor = true;
            this.ButtonClose.Click += new System.EventHandler(this.ButtonClose_Click);
            // 
            // frmQueuedNotifications
            // 
            this.AcceptButton = this.ButtonClose;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.ButtonClose;
            this.ClientSize = new System.Drawing.Size(485, 256);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.listViewNotifications);
            this.Controls.Add(this.ButtonClose);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmQueuedNotifications";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Notifications";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmQueuedNotifications_FormClosing);
            this.Load += new System.EventHandler(this.frmQueuedNotifications_Load);
            this.Resize += new System.EventHandler(this.frmQueuedNotifications_Resize);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ListView listViewNotifications;
        private System.Windows.Forms.ColumnHeader colTitle;
        private System.Windows.Forms.Button ButtonClose;
    }
}