namespace LibVideos
{
    partial class frmSettings
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
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.buttonRemove = new System.Windows.Forms.Button();
            this.buttonAdd = new System.Windows.Forms.Button();
            this.ListviewSubscriptions = new System.Windows.Forms.ListView();
            this.colName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colProvider = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colSlug = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.numericUpDownUpdateFeedsEveryXMinutes = new System.Windows.Forms.NumericUpDown();
            this.numericUpDownNumberOfVideosToQueryChannelFor = new System.Windows.Forms.NumericUpDown();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.tabControl1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.tabPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownUpdateFeedsEveryXMinutes)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownNumberOfVideosToQueryChannelFor)).BeginInit();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(447, 262);
            this.tabControl1.TabIndex = 0;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.buttonRemove);
            this.tabPage2.Controls.Add(this.buttonAdd);
            this.tabPage2.Controls.Add(this.ListviewSubscriptions);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(439, 236);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Subscriptions";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // buttonRemove
            // 
            this.buttonRemove.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonRemove.Location = new System.Drawing.Point(355, 206);
            this.buttonRemove.Name = "buttonRemove";
            this.buttonRemove.Size = new System.Drawing.Size(75, 23);
            this.buttonRemove.TabIndex = 7;
            this.buttonRemove.Text = "Remove";
            this.buttonRemove.UseVisualStyleBackColor = true;
            this.buttonRemove.Click += new System.EventHandler(this.buttonRemove_Click);
            // 
            // buttonAdd
            // 
            this.buttonAdd.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonAdd.Location = new System.Drawing.Point(277, 206);
            this.buttonAdd.Name = "buttonAdd";
            this.buttonAdd.Size = new System.Drawing.Size(72, 23);
            this.buttonAdd.TabIndex = 6;
            this.buttonAdd.Text = "Add";
            this.buttonAdd.UseVisualStyleBackColor = true;
            this.buttonAdd.Click += new System.EventHandler(this.buttonAdd_Click);
            // 
            // ListviewSubscriptions
            // 
            this.ListviewSubscriptions.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.ListviewSubscriptions.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.colName,
            this.colProvider,
            this.colSlug});
            this.ListviewSubscriptions.Location = new System.Drawing.Point(3, 8);
            this.ListviewSubscriptions.Name = "ListviewSubscriptions";
            this.ListviewSubscriptions.Size = new System.Drawing.Size(432, 192);
            this.ListviewSubscriptions.TabIndex = 5;
            this.ListviewSubscriptions.UseCompatibleStateImageBehavior = false;
            this.ListviewSubscriptions.View = System.Windows.Forms.View.Details;
            // 
            // colName
            // 
            this.colName.Text = "Name";
            this.colName.Width = 127;
            // 
            // colProvider
            // 
            this.colProvider.Text = "Provider";
            this.colProvider.Width = 120;
            // 
            // colSlug
            // 
            this.colSlug.Text = "Slug";
            this.colSlug.Width = 180;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.numericUpDownUpdateFeedsEveryXMinutes);
            this.tabPage1.Controls.Add(this.numericUpDownNumberOfVideosToQueryChannelFor);
            this.tabPage1.Controls.Add(this.label2);
            this.tabPage1.Controls.Add(this.label1);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(439, 236);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Updates";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // numericUpDownUpdateFeedsEveryXMinutes
            // 
            this.numericUpDownUpdateFeedsEveryXMinutes.Location = new System.Drawing.Point(166, 11);
            this.numericUpDownUpdateFeedsEveryXMinutes.Maximum = new decimal(new int[] {
            1440,
            0,
            0,
            0});
            this.numericUpDownUpdateFeedsEveryXMinutes.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDownUpdateFeedsEveryXMinutes.Name = "numericUpDownUpdateFeedsEveryXMinutes";
            this.numericUpDownUpdateFeedsEveryXMinutes.Size = new System.Drawing.Size(54, 20);
            this.numericUpDownUpdateFeedsEveryXMinutes.TabIndex = 3;
            this.numericUpDownUpdateFeedsEveryXMinutes.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // numericUpDownNumberOfVideosToQueryChannelFor
            // 
            this.numericUpDownNumberOfVideosToQueryChannelFor.Location = new System.Drawing.Point(232, 32);
            this.numericUpDownNumberOfVideosToQueryChannelFor.Name = "numericUpDownNumberOfVideosToQueryChannelFor";
            this.numericUpDownNumberOfVideosToQueryChannelFor.Size = new System.Drawing.Size(53, 20);
            this.numericUpDownNumberOfVideosToQueryChannelFor.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(8, 13);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(169, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Update channels every X minutes:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(8, 34);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(218, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Number of latest videos to query channel for:";
            // 
            // frmSettings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(447, 262);
            this.Controls.Add(this.tabControl1);
            this.Name = "frmSettings";
            this.Text = "frmSettings";
            this.Load += new System.EventHandler(this.frmSettings_Load);
            this.tabControl1.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownUpdateFeedsEveryXMinutes)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownNumberOfVideosToQueryChannelFor)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.NumericUpDown numericUpDownNumberOfVideosToQueryChannelFor;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.NumericUpDown numericUpDownUpdateFeedsEveryXMinutes;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.Button buttonRemove;
        private System.Windows.Forms.Button buttonAdd;
        private System.Windows.Forms.ListView ListviewSubscriptions;
        private System.Windows.Forms.ColumnHeader colName;
        private System.Windows.Forms.ColumnHeader colProvider;
        private System.Windows.Forms.ColumnHeader colSlug;
    }
}