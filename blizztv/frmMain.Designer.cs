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
            this.menuStrip = new System.Windows.Forms.MenuStrip();
            this.blizzTVToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.preferencesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuPlugins = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.MenuExit = new System.Windows.Forms.ToolStripMenuItem();
            this.HelpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuDonate = new System.Windows.Forms.ToolStripMenuItem();
            this.blizzTVcomToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.userGuideToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.checkUpdatesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.bugReportsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.AboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.StatusStrip = new System.Windows.Forms.StatusStrip();
            this.ProgressBar = new System.Windows.Forms.ToolStripProgressBar();
            this.TrayIcon = new System.Windows.Forms.NotifyIcon(this.components);
            this.TrayContextMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.ContextAboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.TrayIconMenuExit = new System.Windows.Forms.ToolStripMenuItem();
            this.TreeView = new System.Windows.Forms.TreeView();
            this.NodeIcons = new System.Windows.Forms.ImageList(this.components);
            this.menuStrip.SuspendLayout();
            this.StatusStrip.SuspendLayout();
            this.TrayContextMenu.SuspendLayout();
            this.SuspendLayout();
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
            this.preferencesToolStripMenuItem,
            this.MenuPlugins,
            this.toolStripSeparator1,
            this.MenuExit});
            this.blizzTVToolStripMenuItem.Name = "blizzTVToolStripMenuItem";
            this.blizzTVToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.blizzTVToolStripMenuItem.Text = "File";
            // 
            // preferencesToolStripMenuItem
            // 
            this.preferencesToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("preferencesToolStripMenuItem.Image")));
            this.preferencesToolStripMenuItem.Name = "preferencesToolStripMenuItem";
            this.preferencesToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.preferencesToolStripMenuItem.Text = "Preferences";
            this.preferencesToolStripMenuItem.Click += new System.EventHandler(this.preferencesToolStripMenuItem_Click);
            // 
            // MenuPlugins
            // 
            this.MenuPlugins.Image = ((System.Drawing.Image)(resources.GetObject("MenuPlugins.Image")));
            this.MenuPlugins.Name = "MenuPlugins";
            this.MenuPlugins.Size = new System.Drawing.Size(152, 22);
            this.MenuPlugins.Text = "Plugins";
            this.MenuPlugins.Click += new System.EventHandler(this.MenuPlugins_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(149, 6);
            // 
            // MenuExit
            // 
            this.MenuExit.Name = "MenuExit";
            this.MenuExit.Size = new System.Drawing.Size(152, 22);
            this.MenuExit.Text = "Exit";
            this.MenuExit.Click += new System.EventHandler(this.MenuExit_Click);
            // 
            // HelpToolStripMenuItem
            // 
            this.HelpToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.MenuDonate,
            this.blizzTVcomToolStripMenuItem,
            this.userGuideToolStripMenuItem,
            this.toolStripSeparator2,
            this.checkUpdatesToolStripMenuItem,
            this.bugReportsToolStripMenuItem,
            this.AboutToolStripMenuItem});
            this.HelpToolStripMenuItem.Name = "HelpToolStripMenuItem";
            this.HelpToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
            this.HelpToolStripMenuItem.Text = "Help";
            // 
            // MenuDonate
            // 
            this.MenuDonate.Image = ((System.Drawing.Image)(resources.GetObject("MenuDonate.Image")));
            this.MenuDonate.Name = "MenuDonate";
            this.MenuDonate.Size = new System.Drawing.Size(153, 22);
            this.MenuDonate.Text = "Donate";
            this.MenuDonate.Click += new System.EventHandler(this.MenuDonate_Click);
            // 
            // blizzTVcomToolStripMenuItem
            // 
            this.blizzTVcomToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("blizzTVcomToolStripMenuItem.Image")));
            this.blizzTVcomToolStripMenuItem.Name = "blizzTVcomToolStripMenuItem";
            this.blizzTVcomToolStripMenuItem.Size = new System.Drawing.Size(153, 22);
            this.blizzTVcomToolStripMenuItem.Text = "BlizzTV.com";
            this.blizzTVcomToolStripMenuItem.Click += new System.EventHandler(this.blizzTVcomToolStripMenuItem_Click);
            // 
            // userGuideToolStripMenuItem
            // 
            this.userGuideToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("userGuideToolStripMenuItem.Image")));
            this.userGuideToolStripMenuItem.Name = "userGuideToolStripMenuItem";
            this.userGuideToolStripMenuItem.Size = new System.Drawing.Size(153, 22);
            this.userGuideToolStripMenuItem.Text = "User Guide";
            this.userGuideToolStripMenuItem.Click += new System.EventHandler(this.userGuideToolStripMenuItem_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(150, 6);
            // 
            // checkUpdatesToolStripMenuItem
            // 
            this.checkUpdatesToolStripMenuItem.Name = "checkUpdatesToolStripMenuItem";
            this.checkUpdatesToolStripMenuItem.Size = new System.Drawing.Size(153, 22);
            this.checkUpdatesToolStripMenuItem.Text = "Check Updates";
            this.checkUpdatesToolStripMenuItem.Click += new System.EventHandler(this.checkUpdatesToolStripMenuItem_Click);
            // 
            // bugReportsToolStripMenuItem
            // 
            this.bugReportsToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("bugReportsToolStripMenuItem.Image")));
            this.bugReportsToolStripMenuItem.Name = "bugReportsToolStripMenuItem";
            this.bugReportsToolStripMenuItem.Size = new System.Drawing.Size(153, 22);
            this.bugReportsToolStripMenuItem.Text = "Bug Reports";
            this.bugReportsToolStripMenuItem.Click += new System.EventHandler(this.bugReportsToolStripMenuItem_Click);
            // 
            // AboutToolStripMenuItem
            // 
            this.AboutToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("AboutToolStripMenuItem.Image")));
            this.AboutToolStripMenuItem.Name = "AboutToolStripMenuItem";
            this.AboutToolStripMenuItem.Size = new System.Drawing.Size(153, 22);
            this.AboutToolStripMenuItem.Text = "About";
            this.AboutToolStripMenuItem.Click += new System.EventHandler(this.AboutToolStripMenuItem_Click);
            // 
            // StatusStrip
            // 
            this.StatusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ProgressBar});
            this.StatusStrip.Location = new System.Drawing.Point(0, 293);
            this.StatusStrip.Name = "StatusStrip";
            this.StatusStrip.Size = new System.Drawing.Size(256, 22);
            this.StatusStrip.TabIndex = 2;
            this.StatusStrip.Text = "statusStrip1";
            // 
            // ProgressBar
            // 
            this.ProgressBar.Name = "ProgressBar";
            this.ProgressBar.Size = new System.Drawing.Size(100, 16);
            this.ProgressBar.Visible = false;
            // 
            // TrayIcon
            // 
            this.TrayIcon.ContextMenuStrip = this.TrayContextMenu;
            this.TrayIcon.Icon = ((System.Drawing.Icon)(resources.GetObject("TrayIcon.Icon")));
            this.TrayIcon.Text = "BlizzTV";
            this.TrayIcon.Visible = true;
            this.TrayIcon.BalloonTipClicked += new System.EventHandler(this.TrayIcon_BalloonTipClicked);
            this.TrayIcon.DoubleClick += new System.EventHandler(this.TrayIcon_DoubleClick);
            // 
            // TrayContextMenu
            // 
            this.TrayContextMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ContextAboutToolStripMenuItem,
            this.toolStripSeparator3,
            this.TrayIconMenuExit});
            this.TrayContextMenu.Name = "TrayContextMenu";
            this.TrayContextMenu.Size = new System.Drawing.Size(108, 54);
            // 
            // ContextAboutToolStripMenuItem
            // 
            this.ContextAboutToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("ContextAboutToolStripMenuItem.Image")));
            this.ContextAboutToolStripMenuItem.Name = "ContextAboutToolStripMenuItem";
            this.ContextAboutToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.ContextAboutToolStripMenuItem.Text = "About";
            this.ContextAboutToolStripMenuItem.Click += new System.EventHandler(this.AboutToolStripMenuItem_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(149, 6);
            // 
            // TrayIconMenuExit
            // 
            this.TrayIconMenuExit.Name = "TrayIconMenuExit";
            this.TrayIconMenuExit.Size = new System.Drawing.Size(152, 22);
            this.TrayIconMenuExit.Text = "Exit";
            this.TrayIconMenuExit.Click += new System.EventHandler(this.TrayIconMenuExit_Click);
            // 
            // TreeView
            // 
            this.TreeView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.TreeView.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.TreeView.FullRowSelect = true;
            this.TreeView.ImageIndex = 0;
            this.TreeView.ImageList = this.NodeIcons;
            this.TreeView.Location = new System.Drawing.Point(0, 24);
            this.TreeView.Name = "TreeView";
            this.TreeView.SelectedImageIndex = 0;
            this.TreeView.ShowNodeToolTips = true;
            this.TreeView.ShowRootLines = false;
            this.TreeView.Size = new System.Drawing.Size(256, 269);
            this.TreeView.TabIndex = 3;
            this.TreeView.NodeMouseClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.TreeView_NodeMouseClick);
            this.TreeView.NodeMouseDoubleClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.TreeView_NodeMouseDoubleClick);
            // 
            // NodeIcons
            // 
            this.NodeIcons.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("NodeIcons.ImageStream")));
            this.NodeIcons.TransparentColor = System.Drawing.Color.Transparent;
            this.NodeIcons.Images.SetKeyName(0, "current.gif");
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(256, 315);
            this.Controls.Add(this.TreeView);
            this.Controls.Add(this.StatusStrip);
            this.Controls.Add(this.menuStrip);
            this.DoubleBuffered = true;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip;
            this.MaximizeBox = false;
            this.Name = "frmMain";
            this.Text = "BlizzTV";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmMain_FormClosing);
            this.Load += new System.EventHandler(this.frmMain_Load);
            this.SizeChanged += new System.EventHandler(this.frmMain_SizeChanged);
            this.menuStrip.ResumeLayout(false);
            this.menuStrip.PerformLayout();
            this.StatusStrip.ResumeLayout(false);
            this.StatusStrip.PerformLayout();
            this.TrayContextMenu.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip;
        private System.Windows.Forms.ToolStripMenuItem blizzTVToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem HelpToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem AboutToolStripMenuItem;
        private System.Windows.Forms.StatusStrip StatusStrip;
        private System.Windows.Forms.ToolStripProgressBar ProgressBar;
        internal System.Windows.Forms.NotifyIcon TrayIcon;
        private System.Windows.Forms.ContextMenuStrip TrayContextMenu;
        private System.Windows.Forms.ToolStripMenuItem ContextAboutToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem userGuideToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem bugReportsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem blizzTVcomToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem preferencesToolStripMenuItem;
        private System.Windows.Forms.TreeView TreeView;
        private System.Windows.Forms.ImageList NodeIcons;
        private System.Windows.Forms.ToolStripMenuItem MenuPlugins;
        private System.Windows.Forms.ToolStripMenuItem MenuDonate;
        private System.Windows.Forms.ToolStripMenuItem MenuExit;
        private System.Windows.Forms.ToolStripMenuItem TrayIconMenuExit;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripMenuItem checkUpdatesToolStripMenuItem;
    }
}

