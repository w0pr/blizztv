namespace BlizzTV
{
    partial class frmAbout
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmAbout));
            this.label1 = new System.Windows.Forms.Label();
            this.LinkLabelProjectPage = new System.Windows.Forms.LinkLabel();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.ListviewModules = new System.Windows.Forms.ListView();
            this.ColIcon = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.ColName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.ColVersion = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.ColDescription = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.buttonOK = new System.Windows.Forms.Button();
            this.LinkLabelBlizzTV = new System.Windows.Forms.LinkLabel();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Tahoma", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.label1.Location = new System.Drawing.Point(334, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(99, 33);
            this.label1.TabIndex = 5;
            this.label1.Text = "BlizzTV";
            // 
            // LinkLabelProjectPage
            // 
            this.LinkLabelProjectPage.AutoSize = true;
            this.LinkLabelProjectPage.Location = new System.Drawing.Point(248, 113);
            this.LinkLabelProjectPage.Name = "LinkLabelProjectPage";
            this.LinkLabelProjectPage.Size = new System.Drawing.Size(170, 13);
            this.LinkLabelProjectPage.TabIndex = 6;
            this.LinkLabelProjectPage.TabStop = true;
            this.LinkLabelProjectPage.Text = "http://code.google.com/p/blizztv/";
            this.LinkLabelProjectPage.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.LinkLabelProjectPage_LinkClicked);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(305, 7);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(32, 32);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 7;
            this.pictureBox1.TabStop = false;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.textBox1);
            this.groupBox1.Controls.Add(this.LinkLabelProjectPage);
            this.groupBox1.Location = new System.Drawing.Point(12, 63);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(421, 131);
            this.groupBox1.TabIndex = 8;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Copyleft 2010, BlizzTV project";
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(6, 16);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.ReadOnly = true;
            this.textBox1.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textBox1.Size = new System.Drawing.Size(409, 94);
            this.textBox1.TabIndex = 2;
            this.textBox1.Text = resources.GetString("textBox1.Text");
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.ListviewModules);
            this.groupBox2.Location = new System.Drawing.Point(12, 200);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(421, 140);
            this.groupBox2.TabIndex = 9;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Plugins";
            // 
            // ListviewModules
            // 
            this.ListviewModules.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.ColIcon,
            this.ColName,
            this.ColVersion,
            this.ColDescription});
            this.ListviewModules.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ListviewModules.FullRowSelect = true;
            this.ListviewModules.Location = new System.Drawing.Point(3, 16);
            this.ListviewModules.Name = "ListviewModules";
            this.ListviewModules.Size = new System.Drawing.Size(415, 121);
            this.ListviewModules.TabIndex = 0;
            this.ListviewModules.UseCompatibleStateImageBehavior = false;
            this.ListviewModules.View = System.Windows.Forms.View.Details;
            // 
            // ColIcon
            // 
            this.ColIcon.Text = "";
            this.ColIcon.Width = 16;
            // 
            // ColName
            // 
            this.ColName.Text = "Name";
            this.ColName.Width = 100;
            // 
            // ColVersion
            // 
            this.ColVersion.Text = "Version";
            this.ColVersion.Width = 74;
            // 
            // ColDescription
            // 
            this.ColDescription.Text = "Description";
            this.ColDescription.Width = 215;
            // 
            // buttonOK
            // 
            this.buttonOK.Location = new System.Drawing.Point(358, 346);
            this.buttonOK.Name = "buttonOK";
            this.buttonOK.Size = new System.Drawing.Size(75, 23);
            this.buttonOK.TabIndex = 0;
            this.buttonOK.Text = "OK";
            this.buttonOK.UseVisualStyleBackColor = true;
            this.buttonOK.Click += new System.EventHandler(this.buttonOK_Click);
            // 
            // LinkLabelBlizzTV
            // 
            this.LinkLabelBlizzTV.AutoSize = true;
            this.LinkLabelBlizzTV.Location = new System.Drawing.Point(316, 47);
            this.LinkLabelBlizzTV.Name = "LinkLabelBlizzTV";
            this.LinkLabelBlizzTV.Size = new System.Drawing.Size(117, 13);
            this.LinkLabelBlizzTV.TabIndex = 11;
            this.LinkLabelBlizzTV.TabStop = true;
            this.LinkLabelBlizzTV.Text = "http://www.blizztv.com";
            this.LinkLabelBlizzTV.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.LinkLabelBlizzTV_LinkClicked);
            // 
            // frmAbout
            // 
            this.AcceptButton = this.buttonOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(445, 376);
            this.Controls.Add(this.LinkLabelBlizzTV);
            this.Controls.Add(this.buttonOK);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmAbout";
            this.Text = "About BlizzTV";
            this.Load += new System.EventHandler(this.frmAbout_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.LinkLabel LinkLabelProjectPage;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.ListView ListviewModules;
        private System.Windows.Forms.ColumnHeader ColName;
        private System.Windows.Forms.ColumnHeader ColVersion;
        private System.Windows.Forms.ColumnHeader ColDescription;
        private System.Windows.Forms.Button buttonOK;
        private System.Windows.Forms.LinkLabel LinkLabelBlizzTV;
        private System.Windows.Forms.ColumnHeader ColIcon;
    }
}