namespace BlizzTV
{
    partial class frmMain
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMain));
            this.List = new System.Windows.Forms.ListView();
            this.icon = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.name = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.game = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.GameIcons = new System.Windows.Forms.ImageList(this.components);
            this.menuStrip = new System.Windows.Forms.MenuStrip();
            this.blizzTVToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.preferencesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.HelpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.userGuideToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.bugReportsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.blizzTVcomToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.AboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ListIcons = new System.Windows.Forms.ImageList(this.components);
            this.statusStrip = new System.Windows.Forms.StatusStrip();
            this.LabelStatus = new System.Windows.Forms.ToolStripStatusLabel();
            this.ProgressStatus = new System.Windows.Forms.ToolStripProgressBar();
            this.notifyIcon = new System.Windows.Forms.NotifyIcon(this.components);
            this.TrayContextMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.ContextAboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip.SuspendLayout();
            this.statusStrip.SuspendLayout();
            this.TrayContextMenu.SuspendLayout();
            this.SuspendLayout();
            // 
            // List
            // 
            this.List.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.icon,
            this.name,
            this.game});
            this.List.Dock = System.Windows.Forms.DockStyle.Fill;
            this.List.FullRowSelect = true;
            this.List.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.None;
            this.List.LabelWrap = false;
            this.List.Location = new System.Drawing.Point(0, 24);
            this.List.MultiSelect = false;
            this.List.Name = "List";
            this.List.OwnerDraw = true;
            this.List.ShowItemToolTips = true;
            this.List.Size = new System.Drawing.Size(256, 291);
            this.List.TabIndex = 0;
            this.List.UseCompatibleStateImageBehavior = false;
            this.List.View = System.Windows.Forms.View.Details;
            this.List.DrawSubItem += new System.Windows.Forms.DrawListViewSubItemEventHandler(this.List_DrawSubItem);
            this.List.DoubleClick += new System.EventHandler(this.List_DoubleClick);
            // 
            // icon
            // 
            this.icon.Text = "Icon";
            this.icon.Width = 21;
            // 
            // name
            // 
            this.name.Text = "Name";
            this.name.Width = 210;
            // 
            // game
            // 
            this.game.Text = "Game";
            this.game.Width = 16;
            // 
            // GameIcons
            // 
            this.GameIcons.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("GameIcons.ImageStream")));
            this.GameIcons.TransparentColor = System.Drawing.Color.Transparent;
            this.GameIcons.Images.SetKeyName(0, "sc2_16.png");
            this.GameIcons.Images.SetKeyName(1, "wow_16.png");
            // 
            // menuStrip
            // 
            this.menuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.blizzTVToolStripMenuItem,
            this.HelpToolStripMenuItem});
            this.menuStrip.Location = new System.Drawing.Point(0, 0);
            this.menuStrip.Name = "menuStrip";
            this.menuStrip.Size = new System.Drawing.Size(256, 24);
            this.menuStrip.TabIndex = 1;
            this.menuStrip.Text = "menuStrip1";
            // 
            // blizzTVToolStripMenuItem
            // 
            this.blizzTVToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.preferencesToolStripMenuItem});
            this.blizzTVToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("blizzTVToolStripMenuItem.Image")));
            this.blizzTVToolStripMenuItem.Name = "blizzTVToolStripMenuItem";
            this.blizzTVToolStripMenuItem.Size = new System.Drawing.Size(28, 20);
            // 
            // preferencesToolStripMenuItem
            // 
            this.preferencesToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("preferencesToolStripMenuItem.Image")));
            this.preferencesToolStripMenuItem.Name = "preferencesToolStripMenuItem";
            this.preferencesToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.preferencesToolStripMenuItem.Text = "Preferences";
            this.preferencesToolStripMenuItem.Click += new System.EventHandler(this.preferencesToolStripMenuItem_Click);
            // 
            // HelpToolStripMenuItem
            // 
            this.HelpToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.userGuideToolStripMenuItem,
            this.bugReportsToolStripMenuItem,
            this.blizzTVcomToolStripMenuItem,
            this.AboutToolStripMenuItem});
            this.HelpToolStripMenuItem.Name = "HelpToolStripMenuItem";
            this.HelpToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
            this.HelpToolStripMenuItem.Text = "Help";
            // 
            // userGuideToolStripMenuItem
            // 
            this.userGuideToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("userGuideToolStripMenuItem.Image")));
            this.userGuideToolStripMenuItem.Name = "userGuideToolStripMenuItem";
            this.userGuideToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.userGuideToolStripMenuItem.Text = "User Guide";
            this.userGuideToolStripMenuItem.Click += new System.EventHandler(this.userGuideToolStripMenuItem_Click);
            // 
            // bugReportsToolStripMenuItem
            // 
            this.bugReportsToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("bugReportsToolStripMenuItem.Image")));
            this.bugReportsToolStripMenuItem.Name = "bugReportsToolStripMenuItem";
            this.bugReportsToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.bugReportsToolStripMenuItem.Text = "Bug Reports";
            this.bugReportsToolStripMenuItem.Click += new System.EventHandler(this.bugReportsToolStripMenuItem_Click);
            // 
            // blizzTVcomToolStripMenuItem
            // 
            this.blizzTVcomToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("blizzTVcomToolStripMenuItem.Image")));
            this.blizzTVcomToolStripMenuItem.Name = "blizzTVcomToolStripMenuItem";
            this.blizzTVcomToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.blizzTVcomToolStripMenuItem.Text = "BlizzTV.com";
            this.blizzTVcomToolStripMenuItem.Click += new System.EventHandler(this.blizzTVcomToolStripMenuItem_Click);
            // 
            // AboutToolStripMenuItem
            // 
            this.AboutToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("AboutToolStripMenuItem.Image")));
            this.AboutToolStripMenuItem.Name = "AboutToolStripMenuItem";
            this.AboutToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.AboutToolStripMenuItem.Text = "About";
            this.AboutToolStripMenuItem.Click += new System.EventHandler(this.AboutToolStripMenuItem_Click);
            // 
            // ListIcons
            // 
            this.ListIcons.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("ListIcons.ImageStream")));
            this.ListIcons.TransparentColor = System.Drawing.Color.Transparent;
            this.ListIcons.Images.SetKeyName(0, "satellite_16.png");
            this.ListIcons.Images.SetKeyName(1, "video_16.png");
            this.ListIcons.Images.SetKeyName(2, "feed_16.png");
            // 
            // statusStrip
            // 
            this.statusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.LabelStatus,
            this.ProgressStatus});
            this.statusStrip.Location = new System.Drawing.Point(0, 293);
            this.statusStrip.Name = "statusStrip";
            this.statusStrip.Size = new System.Drawing.Size(256, 22);
            this.statusStrip.TabIndex = 2;
            this.statusStrip.Text = "statusStrip1";
            // 
            // LabelStatus
            // 
            this.LabelStatus.Name = "LabelStatus";
            this.LabelStatus.Size = new System.Drawing.Size(10, 17);
            this.LabelStatus.Text = " ";
            // 
            // ProgressStatus
            // 
            this.ProgressStatus.Name = "ProgressStatus";
            this.ProgressStatus.Size = new System.Drawing.Size(100, 16);
            this.ProgressStatus.Visible = false;
            // 
            // notifyIcon
            // 
            this.notifyIcon.ContextMenuStrip = this.TrayContextMenu;
            this.notifyIcon.Icon = ((System.Drawing.Icon)(resources.GetObject("notifyIcon.Icon")));
            this.notifyIcon.Text = "BlizzTV";
            this.notifyIcon.Visible = true;
            // 
            // TrayContextMenu
            // 
            this.TrayContextMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ContextAboutToolStripMenuItem});
            this.TrayContextMenu.Name = "TrayContextMenu";
            this.TrayContextMenu.Size = new System.Drawing.Size(108, 26);
            // 
            // ContextAboutToolStripMenuItem
            // 
            this.ContextAboutToolStripMenuItem.Name = "ContextAboutToolStripMenuItem";
            this.ContextAboutToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.ContextAboutToolStripMenuItem.Text = "About";
            this.ContextAboutToolStripMenuItem.Click += new System.EventHandler(this.AboutToolStripMenuItem_Click);
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(256, 315);
            this.Controls.Add(this.statusStrip);
            this.Controls.Add(this.List);
            this.Controls.Add(this.menuStrip);
            this.DoubleBuffered = true;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip;
            this.MaximizeBox = false;
            this.Name = "frmMain";
            this.Text = "BlizzTV";
            this.Load += new System.EventHandler(this.frmMain_Load);
            this.menuStrip.ResumeLayout(false);
            this.menuStrip.PerformLayout();
            this.statusStrip.ResumeLayout(false);
            this.statusStrip.PerformLayout();
            this.TrayContextMenu.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListView List;
        private System.Windows.Forms.ColumnHeader game;
        private System.Windows.Forms.ColumnHeader name;
        private System.Windows.Forms.ImageList GameIcons;
        private System.Windows.Forms.MenuStrip menuStrip;
        private System.Windows.Forms.ToolStripMenuItem blizzTVToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem HelpToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem AboutToolStripMenuItem;
        private System.Windows.Forms.ImageList ListIcons;
        private System.Windows.Forms.ColumnHeader icon;
        private System.Windows.Forms.StatusStrip statusStrip;
        private System.Windows.Forms.ToolStripStatusLabel LabelStatus;
        private System.Windows.Forms.ToolStripProgressBar ProgressStatus;
        private System.Windows.Forms.NotifyIcon notifyIcon;
        private System.Windows.Forms.ContextMenuStrip TrayContextMenu;
        private System.Windows.Forms.ToolStripMenuItem ContextAboutToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem userGuideToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem bugReportsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem blizzTVcomToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem preferencesToolStripMenuItem;
    }
}

