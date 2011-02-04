namespace BlizzTV.Notifications
{
    partial class frmArchivedNotifications
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
            this.listViewNotifications = new System.Windows.Forms.ListView();
            this.colModule = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colTitle = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.ImageList = new System.Windows.Forms.ImageList(this.components);
            this.ButtonClose = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // listViewNotifications
            // 
            this.listViewNotifications.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.colModule,
            this.colTitle});
            this.listViewNotifications.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listViewNotifications.FullRowSelect = true;
            this.listViewNotifications.GridLines = true;
            this.listViewNotifications.Location = new System.Drawing.Point(0, 0);
            this.listViewNotifications.MultiSelect = false;
            this.listViewNotifications.Name = "listViewNotifications";
            this.listViewNotifications.ShowItemToolTips = true;
            this.listViewNotifications.Size = new System.Drawing.Size(279, 319);
            this.listViewNotifications.SmallImageList = this.ImageList;
            this.listViewNotifications.TabIndex = 2;
            this.listViewNotifications.UseCompatibleStateImageBehavior = false;
            this.listViewNotifications.View = System.Windows.Forms.View.Details;
            this.listViewNotifications.DoubleClick += new System.EventHandler(this.listViewNotifications_DoubleClick);
            // 
            // colModule
            // 
            this.colModule.Text = "";
            this.colModule.Width = 24;
            // 
            // colTitle
            // 
            this.colTitle.Text = "Title";
            this.colTitle.Width = 210;
            // 
            // ImageList
            // 
            this.ImageList.ColorDepth = System.Windows.Forms.ColorDepth.Depth32Bit;
            this.ImageList.ImageSize = new System.Drawing.Size(16, 16);
            this.ImageList.TransparentColor = System.Drawing.Color.Transparent;
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
            // frmArchivedNotifications
            // 
            this.AcceptButton = this.ButtonClose;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.ButtonClose;
            this.ClientSize = new System.Drawing.Size(279, 319);
            this.Controls.Add(this.listViewNotifications);
            this.Controls.Add(this.ButtonClose);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmArchivedNotifications";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "Notifications";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmQueuedNotifications_FormClosing);
            this.Load += new System.EventHandler(this.frmQueuedNotifications_Load);
            this.ResizeEnd += new System.EventHandler(this.frmArchivedNotifications_ResizeEnd);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListView listViewNotifications;
        private System.Windows.Forms.ColumnHeader colTitle;
        private System.Windows.Forms.Button ButtonClose;
        private System.Windows.Forms.ColumnHeader colModule;
        private System.Windows.Forms.ImageList ImageList;
    }
}