namespace BlizzTV.ModuleLib.Subscriptions
{
    partial class frmCatalog
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmCatalog));
            this.linkLabelSuggest = new System.Windows.Forms.LinkLabel();
            this.ButtonClose = new System.Windows.Forms.Button();
            this.buttonAdd = new System.Windows.Forms.Button();
            this.listViewCatalog = new System.Windows.Forms.ListView();
            this.colCategory = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colDescription = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.SuspendLayout();
            // 
            // linkLabelSuggest
            // 
            this.linkLabelSuggest.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.linkLabelSuggest.AutoSize = true;
            this.linkLabelSuggest.Location = new System.Drawing.Point(3, 455);
            this.linkLabelSuggest.Name = "linkLabelSuggest";
            this.linkLabelSuggest.Size = new System.Drawing.Size(104, 13);
            this.linkLabelSuggest.TabIndex = 25;
            this.linkLabelSuggest.TabStop = true;
            this.linkLabelSuggest.Text = "Suggest a new entry";
            this.linkLabelSuggest.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabelSuggest_LinkClicked);
            // 
            // ButtonClose
            // 
            this.ButtonClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.ButtonClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.ButtonClose.Location = new System.Drawing.Point(675, 450);
            this.ButtonClose.Name = "ButtonClose";
            this.ButtonClose.Size = new System.Drawing.Size(75, 23);
            this.ButtonClose.TabIndex = 26;
            this.ButtonClose.Text = "Close";
            this.ButtonClose.UseVisualStyleBackColor = true;
            this.ButtonClose.Click += new System.EventHandler(this.ButtonClose_Click);
            // 
            // buttonAdd
            // 
            this.buttonAdd.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonAdd.Location = new System.Drawing.Point(594, 450);
            this.buttonAdd.Name = "buttonAdd";
            this.buttonAdd.Size = new System.Drawing.Size(75, 23);
            this.buttonAdd.TabIndex = 28;
            this.buttonAdd.Text = "Add";
            this.buttonAdd.UseVisualStyleBackColor = true;
            this.buttonAdd.Click += new System.EventHandler(this.ButtonAdd_Click);
            // 
            // listViewCatalog
            // 
            this.listViewCatalog.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.listViewCatalog.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.colCategory,
            this.colName,
            this.colDescription});
            this.listViewCatalog.FullRowSelect = true;
            this.listViewCatalog.GridLines = true;
            this.listViewCatalog.Location = new System.Drawing.Point(-1, -1);
            this.listViewCatalog.MultiSelect = false;
            this.listViewCatalog.Name = "listViewCatalog";
            this.listViewCatalog.ShowGroups = false;
            this.listViewCatalog.ShowItemToolTips = true;
            this.listViewCatalog.Size = new System.Drawing.Size(756, 445);
            this.listViewCatalog.TabIndex = 29;
            this.listViewCatalog.UseCompatibleStateImageBehavior = false;
            this.listViewCatalog.View = System.Windows.Forms.View.Details;
            this.listViewCatalog.ColumnClick += new System.Windows.Forms.ColumnClickEventHandler(this.listViewCatalog_ColumnClick);
            this.listViewCatalog.DoubleClick += new System.EventHandler(this.listViewCatalog_DoubleClick);
            // 
            // colCategory
            // 
            this.colCategory.Text = "Category";
            this.colCategory.Width = 126;
            // 
            // colName
            // 
            this.colName.Text = "Name";
            this.colName.Width = 120;
            // 
            // colDescription
            // 
            this.colDescription.Text = "Description";
            this.colDescription.Width = 322;
            // 
            // frmCatalog
            // 
            this.AcceptButton = this.ButtonClose;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.ButtonClose;
            this.ClientSize = new System.Drawing.Size(755, 479);
            this.Controls.Add(this.listViewCatalog);
            this.Controls.Add(this.buttonAdd);
            this.Controls.Add(this.ButtonClose);
            this.Controls.Add(this.linkLabelSuggest);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmCatalog";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Catalog";
            this.Load += new System.EventHandler(this.frmCatalog_Load);
            this.ResizeEnd += new System.EventHandler(this.frmCatalog_ResizeEnd);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.LinkLabel linkLabelSuggest;
        private System.Windows.Forms.Button ButtonClose;
        private System.Windows.Forms.Button buttonAdd;
        private System.Windows.Forms.ListView listViewCatalog;
        private System.Windows.Forms.ColumnHeader colCategory;
        private System.Windows.Forms.ColumnHeader colName;
        private System.Windows.Forms.ColumnHeader colDescription;
    }
}