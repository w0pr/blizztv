using BlizzTV.Media.Controls.Flash;

namespace BlizzTV.UI
{
    partial class AboutForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AboutForm));
            System.Windows.Forms.ListViewItem listViewItem1 = new System.Windows.Forms.ListViewItem(new string[] {
            "Hüseyin Uslu",
            "Developer."}, -1);
            System.Windows.Forms.ListViewItem listViewItem2 = new System.Windows.Forms.ListViewItem(new string[] {
            "Donors",
            "Thanks for supporting the project by donations!"}, -1);
            System.Windows.Forms.ListViewItem listViewItem3 = new System.Windows.Forms.ListViewItem(new string[] {
            "TeamLiquid.net",
            "Thanks for calendar API and all suggestions from TL users."}, -1);
            System.Windows.Forms.ListViewItem listViewItem4 = new System.Windows.Forms.ListViewItem(new string[] {
            "Mark James",
            "The shiny icons."}, -1);
            System.Windows.Forms.ListViewItem listViewItem5 = new System.Windows.Forms.ListViewItem(new string[] {
            "Brent R. Matzelle",
            "Lovely Nini."}, -1);
            System.Windows.Forms.ListViewItem listViewItem6 = new System.Windows.Forms.ListViewItem(new string[] {
            "DotNetZip",
            "http://dotnetzip.codeplex.com"}, -1);
            System.Windows.Forms.ListViewItem listViewItem7 = new System.Windows.Forms.ListViewItem(new string[] {
            "SaveTheMurlocs.org",
            "No murlocs were harmed during making of this program."}, -1);
            this.label1 = new System.Windows.Forms.Label();
            this.picBlizzTV = new System.Windows.Forms.PictureBox();
            this.imageList = new System.Windows.Forms.ImageList(this.components);
            this.buttonOK = new System.Windows.Forms.Button();
            this.LinkBlizzTV = new System.Windows.Forms.LinkLabel();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.ListviewCredits = new System.Windows.Forms.ListView();
            this.columnName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnAbout = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.LinkFlattr = new System.Windows.Forms.PictureBox();
            this.LinkPaypal = new System.Windows.Forms.PictureBox();
            this.picMurloc = new System.Windows.Forms.PictureBox();
            this.LabelVersion = new System.Windows.Forms.Label();
            this.buttonChangelog = new System.Windows.Forms.Button();
            this.picTwitter = new System.Windows.Forms.PictureBox();
            this.picFacebook = new System.Windows.Forms.PictureBox();
            this.Player = new FlashPlayer();
            this.toolTip = new System.Windows.Forms.ToolTip(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.picBlizzTV)).BeginInit();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.LinkFlattr)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.LinkPaypal)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picMurloc)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picTwitter)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picFacebook)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Player)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Tahoma", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.label1.Location = new System.Drawing.Point(80, 12);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(99, 33);
            this.label1.TabIndex = 5;
            this.label1.Text = "BlizzTV";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // picBlizzTV
            // 
            this.picBlizzTV.Image = ((System.Drawing.Image)(resources.GetObject("picBlizzTV.Image")));
            this.picBlizzTV.Location = new System.Drawing.Point(10, 16);
            this.picBlizzTV.Name = "picBlizzTV";
            this.picBlizzTV.Size = new System.Drawing.Size(64, 64);
            this.picBlizzTV.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.picBlizzTV.TabIndex = 7;
            this.picBlizzTV.TabStop = false;
            // 
            // imageList
            // 
            this.imageList.ColorDepth = System.Windows.Forms.ColorDepth.Depth8Bit;
            this.imageList.ImageSize = new System.Drawing.Size(16, 16);
            this.imageList.TransparentColor = System.Drawing.Color.Transparent;
            // 
            // buttonOK
            // 
            this.buttonOK.Location = new System.Drawing.Point(403, 270);
            this.buttonOK.Name = "buttonOK";
            this.buttonOK.Size = new System.Drawing.Size(74, 23);
            this.buttonOK.TabIndex = 0;
            this.buttonOK.Text = "OK";
            this.buttonOK.UseVisualStyleBackColor = true;
            this.buttonOK.Click += new System.EventHandler(this.buttonOK_Click);
            // 
            // LinkBlizzTV
            // 
            this.LinkBlizzTV.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.LinkBlizzTV.Location = new System.Drawing.Point(83, 63);
            this.LinkBlizzTV.Name = "LinkBlizzTV";
            this.LinkBlizzTV.Size = new System.Drawing.Size(125, 13);
            this.LinkBlizzTV.TabIndex = 11;
            this.LinkBlizzTV.TabStop = true;
            this.LinkBlizzTV.Text = "http://www.blizztv.com";
            this.LinkBlizzTV.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.LinkBlizzTV.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.LinkBlizzTV_Clicked);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.ListviewCredits);
            this.groupBox1.Location = new System.Drawing.Point(7, 90);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(470, 171);
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
            listViewItem1,
            listViewItem2,
            listViewItem3,
            listViewItem4,
            listViewItem5,
            listViewItem6,
            listViewItem7});
            this.ListviewCredits.Location = new System.Drawing.Point(3, 16);
            this.ListviewCredits.Name = "ListviewCredits";
            this.ListviewCredits.ShowGroups = false;
            this.ListviewCredits.ShowItemToolTips = true;
            this.ListviewCredits.Size = new System.Drawing.Size(464, 152);
            this.ListviewCredits.TabIndex = 1;
            this.ListviewCredits.UseCompatibleStateImageBehavior = false;
            this.ListviewCredits.View = System.Windows.Forms.View.Details;
            this.ListviewCredits.DoubleClick += new System.EventHandler(this.ListviewCredits_DoubleClick);
            // 
            // columnName
            // 
            this.columnName.Text = "Name";
            this.columnName.Width = 127;
            // 
            // columnAbout
            // 
            this.columnAbout.Text = "About";
            this.columnAbout.Width = 332;
            // 
            // LinkFlattr
            // 
            this.LinkFlattr.Image = ((System.Drawing.Image)(resources.GetObject("LinkFlattr.Image")));
            this.LinkFlattr.Location = new System.Drawing.Point(224, 271);
            this.LinkFlattr.Name = "LinkFlattr";
            this.LinkFlattr.Size = new System.Drawing.Size(93, 20);
            this.LinkFlattr.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.LinkFlattr.TabIndex = 13;
            this.LinkFlattr.TabStop = false;
            this.LinkFlattr.Click += new System.EventHandler(this.LinkFlattr_Click);
            // 
            // LinkPaypal
            // 
            this.LinkPaypal.Image = ((System.Drawing.Image)(resources.GetObject("LinkPaypal.Image")));
            this.LinkPaypal.Location = new System.Drawing.Point(144, 271);
            this.LinkPaypal.Name = "LinkPaypal";
            this.LinkPaypal.Size = new System.Drawing.Size(74, 21);
            this.LinkPaypal.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.LinkPaypal.TabIndex = 14;
            this.LinkPaypal.TabStop = false;
            this.LinkPaypal.Click += new System.EventHandler(this.LinkPaypal_Click);
            // 
            // picMurloc
            // 
            this.picMurloc.Image = ((System.Drawing.Image)(resources.GetObject("picMurloc.Image")));
            this.picMurloc.Location = new System.Drawing.Point(12, 269);
            this.picMurloc.Name = "picMurloc";
            this.picMurloc.Size = new System.Drawing.Size(24, 24);
            this.picMurloc.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.picMurloc.TabIndex = 15;
            this.picMurloc.TabStop = false;
            this.picMurloc.Click += new System.EventHandler(this.EvenMoreDots);
            // 
            // LabelVersion
            // 
            this.LabelVersion.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.LabelVersion.Location = new System.Drawing.Point(83, 45);
            this.LabelVersion.Name = "LabelVersion";
            this.LabelVersion.Size = new System.Drawing.Size(100, 12);
            this.LabelVersion.TabIndex = 17;
            this.LabelVersion.Text = "version";
            this.LabelVersion.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // buttonChangelog
            // 
            this.buttonChangelog.Location = new System.Drawing.Point(323, 270);
            this.buttonChangelog.Name = "buttonChangelog";
            this.buttonChangelog.Size = new System.Drawing.Size(74, 23);
            this.buttonChangelog.TabIndex = 18;
            this.buttonChangelog.Text = "Changelog";
            this.buttonChangelog.UseVisualStyleBackColor = true;
            this.buttonChangelog.Click += new System.EventHandler(this.buttonChangelog_Click);
            // 
            // picTwitter
            // 
            this.picTwitter.Image = ((System.Drawing.Image)(resources.GetObject("picTwitter.Image")));
            this.picTwitter.Location = new System.Drawing.Point(42, 269);
            this.picTwitter.Name = "picTwitter";
            this.picTwitter.Size = new System.Drawing.Size(24, 24);
            this.picTwitter.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.picTwitter.TabIndex = 20;
            this.picTwitter.TabStop = false;
            this.picTwitter.Click += new System.EventHandler(this.picTwitter_Click);
            // 
            // picFacebook
            // 
            this.picFacebook.Image = ((System.Drawing.Image)(resources.GetObject("picFacebook.Image")));
            this.picFacebook.Location = new System.Drawing.Point(72, 269);
            this.picFacebook.Name = "picFacebook";
            this.picFacebook.Size = new System.Drawing.Size(24, 24);
            this.picFacebook.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.picFacebook.TabIndex = 21;
            this.picFacebook.TabStop = false;
            this.picFacebook.Click += new System.EventHandler(this.picFacebook_Click);
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
            // AboutForm
            // 
            this.AcceptButton = this.buttonOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(483, 301);
            this.Controls.Add(this.Player);
            this.Controls.Add(this.picMurloc);
            this.Controls.Add(this.LinkPaypal);
            this.Controls.Add(this.LinkFlattr);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.LinkBlizzTV);
            this.Controls.Add(this.buttonOK);
            this.Controls.Add(this.picBlizzTV);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.LabelVersion);
            this.Controls.Add(this.buttonChangelog);
            this.Controls.Add(this.picTwitter);
            this.Controls.Add(this.picFacebook);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "AboutForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "About BlizzTV";
            this.Load += new System.EventHandler(this.AboutForm_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.MoreDots);
            ((System.ComponentModel.ISupportInitialize)(this.picBlizzTV)).EndInit();
            this.groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.LinkFlattr)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.LinkPaypal)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picMurloc)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picTwitter)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picFacebook)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Player)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.PictureBox picBlizzTV;
        private System.Windows.Forms.Button buttonOK;
        private System.Windows.Forms.LinkLabel LinkBlizzTV;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ListView ListviewCredits;
        private System.Windows.Forms.ColumnHeader columnName;
        private System.Windows.Forms.ColumnHeader columnAbout;
        private System.Windows.Forms.PictureBox LinkFlattr;
        private System.Windows.Forms.PictureBox LinkPaypal;
        private System.Windows.Forms.PictureBox picMurloc;
        private FlashPlayer Player;
        private System.Windows.Forms.ImageList imageList;
        private System.Windows.Forms.Label LabelVersion;
        private System.Windows.Forms.Button buttonChangelog;
        private System.Windows.Forms.PictureBox picTwitter;
        private System.Windows.Forms.PictureBox picFacebook;
        private System.Windows.Forms.ToolTip toolTip;
    }
}