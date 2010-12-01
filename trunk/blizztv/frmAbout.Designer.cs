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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmAbout));
            System.Windows.Forms.ListViewItem listViewItem6 = new System.Windows.Forms.ListViewItem(new string[] {
            "Hüseyin Uslu",
            "Developer."}, -1);
            System.Windows.Forms.ListViewItem listViewItem7 = new System.Windows.Forms.ListViewItem(new string[] {
            "TeamLiquid.net",
            "Thanks for calendar API and all suggestions from TL users."}, -1);
            System.Windows.Forms.ListViewItem listViewItem8 = new System.Windows.Forms.ListViewItem(new string[] {
            "Mark James",
            "The shiny icons. (http://www.famfamfam.com/)"}, -1);
            System.Windows.Forms.ListViewItem listViewItem9 = new System.Windows.Forms.ListViewItem(new string[] {
            "Brent R. Matzelle",
            "Nini - http://nini.sourceforge.net"}, -1);
            System.Windows.Forms.ListViewItem listViewItem10 = new System.Windows.Forms.ListViewItem(new string[] {
            "SaveTheMurlocs.org",
            "No murlocs were harmed during making of this program."}, -1);
            this.label1 = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.ListviewModules = new System.Windows.Forms.ListView();
            this.ColName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.ColDescription = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.imageList = new System.Windows.Forms.ImageList(this.components);
            this.buttonOK = new System.Windows.Forms.Button();
            this.LinkLabelBlizzTV = new System.Windows.Forms.LinkLabel();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.ListviewCredits = new System.Windows.Forms.ListView();
            this.columnName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnAbout = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.LinkFlattr = new System.Windows.Forms.PictureBox();
            this.LinkPaypal = new System.Windows.Forms.PictureBox();
            this.Murloc = new System.Windows.Forms.PictureBox();
            this.LabelVersion = new System.Windows.Forms.Label();
            this.Player = new BlizzTV.ModuleLib.Players.FlashPlayer();
            this.buttonChangelog = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.groupBox2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.LinkFlattr)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.LinkPaypal)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Murloc)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Player)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Tahoma", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.label1.Location = new System.Drawing.Point(346, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(99, 33);
            this.label1.TabIndex = 5;
            this.label1.Text = "BlizzTV";
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(308, 7);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(32, 32);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 7;
            this.pictureBox1.TabStop = false;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.ListviewModules);
            this.groupBox2.Location = new System.Drawing.Point(7, 192);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(438, 117);
            this.groupBox2.TabIndex = 9;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Modules";
            // 
            // ListviewModules
            // 
            this.ListviewModules.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.ColName,
            this.ColDescription});
            this.ListviewModules.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ListviewModules.FullRowSelect = true;
            this.ListviewModules.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.ListviewModules.Location = new System.Drawing.Point(3, 16);
            this.ListviewModules.Name = "ListviewModules";
            this.ListviewModules.ShowItemToolTips = true;
            this.ListviewModules.Size = new System.Drawing.Size(432, 98);
            this.ListviewModules.SmallImageList = this.imageList;
            this.ListviewModules.TabIndex = 0;
            this.ListviewModules.UseCompatibleStateImageBehavior = false;
            this.ListviewModules.View = System.Windows.Forms.View.Details;
            // 
            // ColName
            // 
            this.ColName.Text = "Name";
            this.ColName.Width = 100;
            // 
            // ColDescription
            // 
            this.ColDescription.Text = "Description";
            this.ColDescription.Width = 310;
            // 
            // imageList
            // 
            this.imageList.ColorDepth = System.Windows.Forms.ColorDepth.Depth8Bit;
            this.imageList.ImageSize = new System.Drawing.Size(16, 16);
            this.imageList.TransparentColor = System.Drawing.Color.Transparent;
            // 
            // buttonOK
            // 
            this.buttonOK.Location = new System.Drawing.Point(370, 312);
            this.buttonOK.Name = "buttonOK";
            this.buttonOK.Size = new System.Drawing.Size(75, 23);
            this.buttonOK.TabIndex = 0;
            this.buttonOK.Text = "OK";
            this.buttonOK.UseVisualStyleBackColor = true;
            this.buttonOK.Click += new System.EventHandler(this.buttonOK_Click);
            // 
            // LinkLabelBlizzTV
            // 
            this.LinkLabelBlizzTV.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.LinkLabelBlizzTV.Location = new System.Drawing.Point(320, 61);
            this.LinkLabelBlizzTV.Name = "LinkLabelBlizzTV";
            this.LinkLabelBlizzTV.Size = new System.Drawing.Size(125, 13);
            this.LinkLabelBlizzTV.TabIndex = 11;
            this.LinkLabelBlizzTV.TabStop = true;
            this.LinkLabelBlizzTV.Text = "http://www.blizztv.com";
            this.LinkLabelBlizzTV.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.LinkLabelBlizzTV.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.LinkLabelBlizzTV_LinkClicked);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.ListviewCredits);
            this.groupBox1.Location = new System.Drawing.Point(7, 77);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(438, 109);
            this.groupBox1.TabIndex = 12;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Credits";
            // 
            // ListviewCredits
            // 
            this.ListviewCredits.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnName,
            this.columnAbout});
            this.ListviewCredits.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ListviewCredits.FullRowSelect = true;
            this.ListviewCredits.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.ListviewCredits.Items.AddRange(new System.Windows.Forms.ListViewItem[] {
            listViewItem6,
            listViewItem7,
            listViewItem8,
            listViewItem9,
            listViewItem10});
            this.ListviewCredits.Location = new System.Drawing.Point(3, 16);
            this.ListviewCredits.Name = "ListviewCredits";
            this.ListviewCredits.ShowItemToolTips = true;
            this.ListviewCredits.Size = new System.Drawing.Size(432, 90);
            this.ListviewCredits.TabIndex = 1;
            this.ListviewCredits.UseCompatibleStateImageBehavior = false;
            this.ListviewCredits.View = System.Windows.Forms.View.Details;
            // 
            // columnName
            // 
            this.columnName.Text = "Name";
            this.columnName.Width = 127;
            // 
            // columnAbout
            // 
            this.columnAbout.Text = "About";
            this.columnAbout.Width = 284;
            // 
            // LinkFlattr
            // 
            this.LinkFlattr.Image = ((System.Drawing.Image)(resources.GetObject("LinkFlattr.Image")));
            this.LinkFlattr.Location = new System.Drawing.Point(10, 315);
            this.LinkFlattr.Name = "LinkFlattr";
            this.LinkFlattr.Size = new System.Drawing.Size(93, 20);
            this.LinkFlattr.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.LinkFlattr.TabIndex = 13;
            this.LinkFlattr.TabStop = false;
            this.LinkFlattr.Click += new System.EventHandler(this.LinkFlattr_Click);
            // 
            // LinkPaypal
            // 
            this.LinkPaypal.Image = ((System.Drawing.Image)(resources.GetObject("LinkPaypal.Image")));
            this.LinkPaypal.Location = new System.Drawing.Point(109, 315);
            this.LinkPaypal.Name = "LinkPaypal";
            this.LinkPaypal.Size = new System.Drawing.Size(74, 21);
            this.LinkPaypal.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.LinkPaypal.TabIndex = 14;
            this.LinkPaypal.TabStop = false;
            this.LinkPaypal.Click += new System.EventHandler(this.LinkPaypal_Click);
            // 
            // Murloc
            // 
            this.Murloc.Image = ((System.Drawing.Image)(resources.GetObject("Murloc.Image")));
            this.Murloc.Location = new System.Drawing.Point(10, 7);
            this.Murloc.Name = "Murloc";
            this.Murloc.Size = new System.Drawing.Size(56, 50);
            this.Murloc.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.Murloc.TabIndex = 15;
            this.Murloc.TabStop = false;
            this.Murloc.Click += new System.EventHandler(this.even_more_dots);
            // 
            // LabelVersion
            // 
            this.LabelVersion.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.LabelVersion.Location = new System.Drawing.Point(345, 42);
            this.LabelVersion.Name = "LabelVersion";
            this.LabelVersion.Size = new System.Drawing.Size(100, 12);
            this.LabelVersion.TabIndex = 17;
            this.LabelVersion.Text = "version";
            this.LabelVersion.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // Player
            // 
            this.Player.Enabled = true;
            this.Player.Location = new System.Drawing.Point(10, 359);
            this.Player.Name = "Player";
            this.Player.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("Player.OcxState")));
            this.Player.Size = new System.Drawing.Size(624, 347);
            this.Player.TabIndex = 16;
            // 
            // buttonChangelog
            // 
            this.buttonChangelog.Location = new System.Drawing.Point(289, 312);
            this.buttonChangelog.Name = "buttonChangelog";
            this.buttonChangelog.Size = new System.Drawing.Size(75, 23);
            this.buttonChangelog.TabIndex = 18;
            this.buttonChangelog.Text = "Changelog";
            this.buttonChangelog.UseVisualStyleBackColor = true;
            this.buttonChangelog.Click += new System.EventHandler(this.buttonChangelog_Click);
            // 
            // frmAbout
            // 
            this.AcceptButton = this.buttonOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(451, 343);
            this.Controls.Add(this.buttonChangelog);
            this.Controls.Add(this.Player);
            this.Controls.Add(this.Murloc);
            this.Controls.Add(this.LinkPaypal);
            this.Controls.Add(this.LinkFlattr);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.LinkLabelBlizzTV);
            this.Controls.Add(this.buttonOK);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.LabelVersion);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmAbout";
            this.Text = "About BlizzTV";
            this.Load += new System.EventHandler(this.frmAbout_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.more_dots);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.LinkFlattr)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.LinkPaypal)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Murloc)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Player)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.ListView ListviewModules;
        private System.Windows.Forms.ColumnHeader ColName;
        private System.Windows.Forms.ColumnHeader ColDescription;
        private System.Windows.Forms.Button buttonOK;
        private System.Windows.Forms.LinkLabel LinkLabelBlizzTV;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ListView ListviewCredits;
        private System.Windows.Forms.ColumnHeader columnName;
        private System.Windows.Forms.ColumnHeader columnAbout;
        private System.Windows.Forms.PictureBox LinkFlattr;
        private System.Windows.Forms.PictureBox LinkPaypal;
        private System.Windows.Forms.PictureBox Murloc;
        private ModuleLib.Players.FlashPlayer Player;
        private System.Windows.Forms.ImageList imageList;
        private System.Windows.Forms.Label LabelVersion;
        private System.Windows.Forms.Button buttonChangelog;
    }
}