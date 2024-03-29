﻿using BlizzTV.Controls.Animations;

namespace BlizzTV.UI
{
    partial class MainForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.menuStrip = new System.Windows.Forms.MenuStrip();
            this.menuFile = new System.Windows.Forms.ToolStripMenuItem();
            this.menuSleepMode = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
            this.menuPreferences = new System.Windows.Forms.ToolStripMenuItem();
            this.menuModules = new System.Windows.Forms.ToolStripMenuItem();
            this.menuRefresh = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator6 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.menuExit = new System.Windows.Forms.ToolStripMenuItem();
            this.menuHelp = new System.Windows.Forms.ToolStripMenuItem();
            this.menuBlizztvcom = new System.Windows.Forms.ToolStripMenuItem();
            this.menuUserGuide = new System.Windows.Forms.ToolStripMenuItem();
            this.videoGuidesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuVideoGuideStreamSubscription = new System.Windows.Forms.ToolStripMenuItem();
            this.menuVideoGuideVideoSubscription = new System.Windows.Forms.ToolStripMenuItem();
            this.menuVideoGuidePodcastSubscription = new System.Windows.Forms.ToolStripMenuItem();
            this.menuVideoGuideFeedSubscription = new System.Windows.Forms.ToolStripMenuItem();
            this.menuFAQ = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.menuBugReports = new System.Windows.Forms.ToolStripMenuItem();
            this.menuCheckUpdates = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.menuDonate = new System.Windows.Forms.ToolStripMenuItem();
            this.menuAbout = new System.Windows.Forms.ToolStripMenuItem();
            this.StatusStrip = new System.Windows.Forms.StatusStrip();
            this.LoadingAnimation = new BlizzTV.Controls.Animations.LoadingAnimationToolStripMenuItem();
            this.ProgressBar = new System.Windows.Forms.ToolStripProgressBar();
            this.toolStripSpacer = new System.Windows.Forms.ToolStripStatusLabel();
            this.SleepIcon = new System.Windows.Forms.ToolStripStatusLabel();
            this.NotificationIcon = new System.Windows.Forms.ToolStripStatusLabel();
            this.TrayIcon = new System.Windows.Forms.NotifyIcon(this.components);
            this.TrayContextMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.ContextMenuSleepMode = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.TrayIconMenuExit = new System.Windows.Forms.ToolStripMenuItem();
            this.TreeView = new System.Windows.Forms.TreeView();
            this.NodeIcons = new System.Windows.Forms.ImageList(this.components);
            this.ItemContextMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.TreeviewContextMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.menuContextRefreshAll = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip.SuspendLayout();
            this.StatusStrip.SuspendLayout();
            this.TrayContextMenu.SuspendLayout();
            this.TreeviewContextMenu.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip
            // 
            this.menuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuFile,
            this.menuHelp});
            this.menuStrip.Location = new System.Drawing.Point(0, 0);
            this.menuStrip.Name = "menuStrip";
            this.menuStrip.Size = new System.Drawing.Size(259, 24);
            this.menuStrip.TabIndex = 1;
            this.menuStrip.Text = "menuStrip1";
            // 
            // menuFile
            // 
            this.menuFile.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuSleepMode,
            this.toolStripSeparator5,
            this.menuPreferences,
            this.menuModules,
            this.toolStripSeparator1,
            this.menuExit});
            this.menuFile.Name = "menuFile";
            this.menuFile.Size = new System.Drawing.Size(37, 20);
            this.menuFile.Text = "File";
            // 
            // menuSleepMode
            // 
            this.menuSleepMode.Image = ((System.Drawing.Image)(resources.GetObject("menuSleepMode.Image")));
            this.menuSleepMode.Name = "menuSleepMode";
            this.menuSleepMode.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.S)));
            this.menuSleepMode.Size = new System.Drawing.Size(176, 22);
            this.menuSleepMode.Text = "Sleep Mode";
            this.menuSleepMode.Click += new System.EventHandler(this.MenuSleepMode_Click);
            // 
            // toolStripSeparator5
            // 
            this.toolStripSeparator5.Name = "toolStripSeparator5";
            this.toolStripSeparator5.Size = new System.Drawing.Size(173, 6);
            // 
            // menuPreferences
            // 
            this.menuPreferences.Image = ((System.Drawing.Image)(resources.GetObject("menuPreferences.Image")));
            this.menuPreferences.Name = "menuPreferences";
            this.menuPreferences.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.P)));
            this.menuPreferences.Size = new System.Drawing.Size(176, 22);
            this.menuPreferences.Text = "Preferences";
            this.menuPreferences.Click += new System.EventHandler(this.MenuPreferences_Click);
            // 
            // menuModules
            // 
            this.menuModules.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuRefresh,
            this.toolStripSeparator6});
            this.menuModules.Image = ((System.Drawing.Image)(resources.GetObject("menuModules.Image")));
            this.menuModules.Name = "menuModules";
            this.menuModules.Size = new System.Drawing.Size(176, 22);
            this.menuModules.Text = "Modules";
            // 
            // menuRefresh
            // 
            this.menuRefresh.Image = ((System.Drawing.Image)(resources.GetObject("menuRefresh.Image")));
            this.menuRefresh.Name = "menuRefresh";
            this.menuRefresh.Size = new System.Drawing.Size(130, 22);
            this.menuRefresh.Text = "Refresh All";
            this.menuRefresh.Click += new System.EventHandler(this.menuRefresh_Click);
            // 
            // toolStripSeparator6
            // 
            this.toolStripSeparator6.Name = "toolStripSeparator6";
            this.toolStripSeparator6.Size = new System.Drawing.Size(127, 6);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(173, 6);
            // 
            // menuExit
            // 
            this.menuExit.Image = ((System.Drawing.Image)(resources.GetObject("menuExit.Image")));
            this.menuExit.Name = "menuExit";
            this.menuExit.Size = new System.Drawing.Size(176, 22);
            this.menuExit.Text = "Exit";
            this.menuExit.Click += new System.EventHandler(this.MenuExit_Click);
            // 
            // menuHelp
            // 
            this.menuHelp.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuBlizztvcom,
            this.menuUserGuide,
            this.videoGuidesToolStripMenuItem,
            this.menuFAQ,
            this.toolStripSeparator2,
            this.menuBugReports,
            this.menuCheckUpdates,
            this.toolStripSeparator4,
            this.menuDonate,
            this.menuAbout});
            this.menuHelp.Name = "menuHelp";
            this.menuHelp.Size = new System.Drawing.Size(44, 20);
            this.menuHelp.Text = "Help";
            // 
            // menuBlizztvcom
            // 
            this.menuBlizztvcom.Image = ((System.Drawing.Image)(resources.GetObject("menuBlizztvcom.Image")));
            this.menuBlizztvcom.Name = "menuBlizztvcom";
            this.menuBlizztvcom.Size = new System.Drawing.Size(153, 22);
            this.menuBlizztvcom.Text = "BlizzTV.com";
            this.menuBlizztvcom.Click += new System.EventHandler(this.MenuBlizzTVCom_Click);
            // 
            // menuUserGuide
            // 
            this.menuUserGuide.Image = ((System.Drawing.Image)(resources.GetObject("menuUserGuide.Image")));
            this.menuUserGuide.Name = "menuUserGuide";
            this.menuUserGuide.Size = new System.Drawing.Size(153, 22);
            this.menuUserGuide.Text = "User Guide";
            this.menuUserGuide.Click += new System.EventHandler(this.MenuUserGuide_Click);
            // 
            // videoGuidesToolStripMenuItem
            // 
            this.videoGuidesToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuVideoGuideStreamSubscription,
            this.menuVideoGuideVideoSubscription,
            this.menuVideoGuidePodcastSubscription,
            this.menuVideoGuideFeedSubscription});
            this.videoGuidesToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("videoGuidesToolStripMenuItem.Image")));
            this.videoGuidesToolStripMenuItem.Name = "videoGuidesToolStripMenuItem";
            this.videoGuidesToolStripMenuItem.Size = new System.Drawing.Size(153, 22);
            this.videoGuidesToolStripMenuItem.Text = "Video Guides";
            // 
            // menuVideoGuideStreamSubscription
            // 
            this.menuVideoGuideStreamSubscription.Image = ((System.Drawing.Image)(resources.GetObject("menuVideoGuideStreamSubscription.Image")));
            this.menuVideoGuideStreamSubscription.Name = "menuVideoGuideStreamSubscription";
            this.menuVideoGuideStreamSubscription.Size = new System.Drawing.Size(291, 22);
            this.menuVideoGuideStreamSubscription.Text = "How to add stream subscriptions?";
            this.menuVideoGuideStreamSubscription.Click += new System.EventHandler(this.menuVideoGuideStreamSubscription_Click);
            // 
            // menuVideoGuideVideoSubscription
            // 
            this.menuVideoGuideVideoSubscription.Image = ((System.Drawing.Image)(resources.GetObject("menuVideoGuideVideoSubscription.Image")));
            this.menuVideoGuideVideoSubscription.Name = "menuVideoGuideVideoSubscription";
            this.menuVideoGuideVideoSubscription.Size = new System.Drawing.Size(291, 22);
            this.menuVideoGuideVideoSubscription.Text = "How to add video channel subscriptions?";
            this.menuVideoGuideVideoSubscription.Click += new System.EventHandler(this.menuVideoGuideVideoSubscription_Click);
            // 
            // menuVideoGuidePodcastSubscription
            // 
            this.menuVideoGuidePodcastSubscription.Image = ((System.Drawing.Image)(resources.GetObject("menuVideoGuidePodcastSubscription.Image")));
            this.menuVideoGuidePodcastSubscription.Name = "menuVideoGuidePodcastSubscription";
            this.menuVideoGuidePodcastSubscription.Size = new System.Drawing.Size(291, 22);
            this.menuVideoGuidePodcastSubscription.Text = "How to add podcast subscriptions?";
            this.menuVideoGuidePodcastSubscription.Click += new System.EventHandler(this.menuVideoGuidePodcastSubscription_Click);
            // 
            // menuVideoGuideFeedSubscription
            // 
            this.menuVideoGuideFeedSubscription.Image = ((System.Drawing.Image)(resources.GetObject("menuVideoGuideFeedSubscription.Image")));
            this.menuVideoGuideFeedSubscription.Name = "menuVideoGuideFeedSubscription";
            this.menuVideoGuideFeedSubscription.Size = new System.Drawing.Size(291, 22);
            this.menuVideoGuideFeedSubscription.Text = "How to add feed subscriptions?";
            this.menuVideoGuideFeedSubscription.Click += new System.EventHandler(this.menuVideoGuideFeedSubscription_Click);
            // 
            // menuFAQ
            // 
            this.menuFAQ.Image = ((System.Drawing.Image)(resources.GetObject("menuFAQ.Image")));
            this.menuFAQ.Name = "menuFAQ";
            this.menuFAQ.Size = new System.Drawing.Size(153, 22);
            this.menuFAQ.Text = "FAQ";
            this.menuFAQ.Click += new System.EventHandler(this.MenuFAQ_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(150, 6);
            // 
            // menuBugReports
            // 
            this.menuBugReports.Image = ((System.Drawing.Image)(resources.GetObject("menuBugReports.Image")));
            this.menuBugReports.Name = "menuBugReports";
            this.menuBugReports.Size = new System.Drawing.Size(153, 22);
            this.menuBugReports.Text = "Bug Reports";
            this.menuBugReports.Click += new System.EventHandler(this.MenuBugReports_Click);
            // 
            // menuCheckUpdates
            // 
            this.menuCheckUpdates.Image = ((System.Drawing.Image)(resources.GetObject("menuCheckUpdates.Image")));
            this.menuCheckUpdates.Name = "menuCheckUpdates";
            this.menuCheckUpdates.Size = new System.Drawing.Size(153, 22);
            this.menuCheckUpdates.Text = "Check Updates";
            this.menuCheckUpdates.Click += new System.EventHandler(this.MenuCheckUpdates);
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(150, 6);
            // 
            // menuDonate
            // 
            this.menuDonate.Image = ((System.Drawing.Image)(resources.GetObject("menuDonate.Image")));
            this.menuDonate.Name = "menuDonate";
            this.menuDonate.Size = new System.Drawing.Size(153, 22);
            this.menuDonate.Text = "Donate";
            this.menuDonate.Click += new System.EventHandler(this.MenuDonate_Click);
            // 
            // menuAbout
            // 
            this.menuAbout.Image = ((System.Drawing.Image)(resources.GetObject("menuAbout.Image")));
            this.menuAbout.Name = "menuAbout";
            this.menuAbout.Size = new System.Drawing.Size(153, 22);
            this.menuAbout.Text = "About";
            this.menuAbout.Click += new System.EventHandler(this.MenuAbout_Click);
            // 
            // StatusStrip
            // 
            this.StatusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.LoadingAnimation,
            this.ProgressBar,
            this.toolStripSpacer,
            this.SleepIcon,
            this.NotificationIcon});
            this.StatusStrip.Location = new System.Drawing.Point(0, 290);
            this.StatusStrip.Name = "StatusStrip";
            this.StatusStrip.Size = new System.Drawing.Size(259, 22);
            this.StatusStrip.TabIndex = 2;
            this.StatusStrip.Text = "statusStrip1";
            // 
            // LoadingAnimation
            // 
            // 
            // LoadingAnimation
            // 
            this.LoadingAnimation.LoadingAnimationControl.AccessibleName = "LoadingAnimation";
            this.LoadingAnimation.LoadingAnimationControl.Active = false;
            this.LoadingAnimation.LoadingAnimationControl.Color = System.Drawing.Color.DarkGray;
            this.LoadingAnimation.LoadingAnimationControl.InnerCircleRadius = 6;
            this.LoadingAnimation.LoadingAnimationControl.Location = new System.Drawing.Point(0, 0);
            this.LoadingAnimation.LoadingAnimationControl.Name = "LoadingCircle";
            this.LoadingAnimation.LoadingAnimationControl.NumberSpoke = 24;
            this.LoadingAnimation.LoadingAnimationControl.OuterCircleRadius = 7;
            this.LoadingAnimation.LoadingAnimationControl.RotationSpeed = 50;
            this.LoadingAnimation.LoadingAnimationControl.Size = new System.Drawing.Size(20, 20);
            this.LoadingAnimation.LoadingAnimationControl.SpokeThickness = 3;
            this.LoadingAnimation.LoadingAnimationControl.StylePreset = BlizzTV.Controls.Animations.LoadingAnimation.StylePresets.IE7;
            this.LoadingAnimation.LoadingAnimationControl.TabIndex = 3;
            this.LoadingAnimation.LoadingAnimationControl.Visible = false;
            this.LoadingAnimation.Name = "LoadingAnimation";
            this.LoadingAnimation.Size = new System.Drawing.Size(20, 20);
            // 
            // ProgressBar
            // 
            this.ProgressBar.Name = "ProgressBar";
            this.ProgressBar.Size = new System.Drawing.Size(100, 16);
            this.ProgressBar.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
            this.ProgressBar.Visible = false;
            // 
            // toolStripSpacer
            // 
            this.toolStripSpacer.Name = "toolStripSpacer";
            this.toolStripSpacer.Size = new System.Drawing.Size(244, 17);
            this.toolStripSpacer.Spring = true;
            // 
            // SleepIcon
            // 
            this.SleepIcon.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.SleepIcon.Image = ((System.Drawing.Image)(resources.GetObject("SleepIcon.Image")));
            this.SleepIcon.Name = "SleepIcon";
            this.SleepIcon.Size = new System.Drawing.Size(16, 17);
            this.SleepIcon.Visible = false;
            // 
            // NotificationIcon
            // 
            this.NotificationIcon.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.NotificationIcon.Image = ((System.Drawing.Image)(resources.GetObject("NotificationIcon.Image")));
            this.NotificationIcon.Name = "NotificationIcon";
            this.NotificationIcon.Size = new System.Drawing.Size(16, 17);
            this.NotificationIcon.Visible = false;
            this.NotificationIcon.MouseUp += new System.Windows.Forms.MouseEventHandler(this.NotificationIcon_MouseUp);
            // 
            // TrayIcon
            // 
            this.TrayIcon.ContextMenuStrip = this.TrayContextMenu;
            this.TrayIcon.Icon = ((System.Drawing.Icon)(resources.GetObject("TrayIcon.Icon")));
            this.TrayIcon.Text = "BlizzTV";
            this.TrayIcon.Visible = true;
            this.TrayIcon.DoubleClick += new System.EventHandler(this.TrayIcon_DoubleClick);
            // 
            // TrayContextMenu
            // 
            this.TrayContextMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ContextMenuSleepMode,
            this.toolStripSeparator3,
            this.TrayIconMenuExit});
            this.TrayContextMenu.Name = "TrayContextMenu";
            this.TrayContextMenu.Size = new System.Drawing.Size(137, 54);
            // 
            // ContextMenuSleepMode
            // 
            this.ContextMenuSleepMode.Image = ((System.Drawing.Image)(resources.GetObject("ContextMenuSleepMode.Image")));
            this.ContextMenuSleepMode.Name = "ContextMenuSleepMode";
            this.ContextMenuSleepMode.Size = new System.Drawing.Size(136, 22);
            this.ContextMenuSleepMode.Text = "Sleep Mode";
            this.ContextMenuSleepMode.Click += new System.EventHandler(this.MenuSleepMode_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(133, 6);
            // 
            // TrayIconMenuExit
            // 
            this.TrayIconMenuExit.Image = ((System.Drawing.Image)(resources.GetObject("TrayIconMenuExit.Image")));
            this.TrayIconMenuExit.Name = "TrayIconMenuExit";
            this.TrayIconMenuExit.Size = new System.Drawing.Size(136, 22);
            this.TrayIconMenuExit.Text = "Exit";
            this.TrayIconMenuExit.Click += new System.EventHandler(this.TrayIconMenuExit_Click);
            // 
            // TreeView
            // 
            this.TreeView.AllowDrop = true;
            this.TreeView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.TreeView.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.TreeView.FullRowSelect = true;
            this.TreeView.ImageIndex = 0;
            this.TreeView.ImageList = this.NodeIcons;
            this.TreeView.Location = new System.Drawing.Point(0, 24);
            this.TreeView.Name = "TreeView";
            this.TreeView.SelectedImageIndex = 0;
            this.TreeView.ShowNodeToolTips = true;
            this.TreeView.ShowRootLines = false;
            this.TreeView.Size = new System.Drawing.Size(259, 266);
            this.TreeView.TabIndex = 0;
            this.TreeView.NodeMouseDoubleClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.TreeView_NodeMouseDoubleClick);
            this.TreeView.DragDrop += new System.Windows.Forms.DragEventHandler(this.TreeView_DragDrop);
            this.TreeView.DragEnter += new System.Windows.Forms.DragEventHandler(this.TreeView_DragEnter);
            this.TreeView.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TreeView_KeyPress);
            this.TreeView.MouseUp += new System.Windows.Forms.MouseEventHandler(this.TreeView_MouseUp);
            // 
            // NodeIcons
            // 
            this.NodeIcons.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("NodeIcons.ImageStream")));
            this.NodeIcons.TransparentColor = System.Drawing.Color.Transparent;
            this.NodeIcons.Images.SetKeyName(0, "current.gif");
            // 
            // ItemContextMenu
            // 
            this.ItemContextMenu.Name = "TreeviewContextMenu";
            this.ItemContextMenu.Size = new System.Drawing.Size(61, 4);
            // 
            // TreeviewContextMenu
            // 
            this.TreeviewContextMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuContextRefreshAll});
            this.TreeviewContextMenu.Name = "TreeviewContextMenu";
            this.TreeviewContextMenu.Size = new System.Drawing.Size(153, 48);
            // 
            // menuContextRefreshAll
            // 
            this.menuContextRefreshAll.Image = ((System.Drawing.Image)(resources.GetObject("menuContextRefreshAll.Image")));
            this.menuContextRefreshAll.Name = "menuContextRefreshAll";
            this.menuContextRefreshAll.Size = new System.Drawing.Size(152, 22);
            this.menuContextRefreshAll.Text = "Refresh All";
            this.menuContextRefreshAll.Click += new System.EventHandler(this.menuRefresh_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(259, 312);
            this.Controls.Add(this.TreeView);
            this.Controls.Add(this.StatusStrip);
            this.Controls.Add(this.menuStrip);
            this.DoubleBuffered = true;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip;
            this.MaximizeBox = false;
            this.Name = "MainForm";
            this.Text = "BlizzTV";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.ResizeEnd += new System.EventHandler(this.MainForm_ResizeEnd);
            this.menuStrip.ResumeLayout(false);
            this.menuStrip.PerformLayout();
            this.StatusStrip.ResumeLayout(false);
            this.StatusStrip.PerformLayout();
            this.TrayContextMenu.ResumeLayout(false);
            this.TreeviewContextMenu.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip;
        private System.Windows.Forms.ToolStripMenuItem menuFile;
        private System.Windows.Forms.ToolStripMenuItem menuHelp;
        private System.Windows.Forms.ToolStripMenuItem menuAbout;
        private System.Windows.Forms.StatusStrip StatusStrip;
        private System.Windows.Forms.ToolStripProgressBar ProgressBar;
        internal System.Windows.Forms.NotifyIcon TrayIcon;
        private System.Windows.Forms.ContextMenuStrip TrayContextMenu;
        private System.Windows.Forms.ToolStripMenuItem menuUserGuide;
        private System.Windows.Forms.ToolStripMenuItem menuBugReports;
        private System.Windows.Forms.ToolStripMenuItem menuBlizztvcom;
        private System.Windows.Forms.ToolStripMenuItem menuPreferences;
        private System.Windows.Forms.TreeView TreeView;
        private System.Windows.Forms.ImageList NodeIcons;
        private System.Windows.Forms.ToolStripMenuItem menuModules;
        private System.Windows.Forms.ToolStripMenuItem menuDonate;
        private System.Windows.Forms.ToolStripMenuItem menuExit;
        private System.Windows.Forms.ToolStripMenuItem TrayIconMenuExit;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripMenuItem menuCheckUpdates;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.ContextMenuStrip ItemContextMenu;
        private System.Windows.Forms.ToolStripMenuItem menuSleepMode;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator5;
        private System.Windows.Forms.ToolStripMenuItem ContextMenuSleepMode;
        private System.Windows.Forms.ToolStripMenuItem menuFAQ;
        private System.Windows.Forms.ToolStripStatusLabel NotificationIcon;
        private System.Windows.Forms.ToolStripStatusLabel toolStripSpacer;
        private System.Windows.Forms.ToolStripStatusLabel SleepIcon;
        private LoadingAnimationToolStripMenuItem LoadingAnimation;
        private System.Windows.Forms.ContextMenuStrip TreeviewContextMenu;
        private System.Windows.Forms.ToolStripMenuItem menuContextRefreshAll;
        private System.Windows.Forms.ToolStripMenuItem menuRefresh;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator6;
        private System.Windows.Forms.ToolStripMenuItem videoGuidesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem menuVideoGuideStreamSubscription;
        private System.Windows.Forms.ToolStripMenuItem menuVideoGuideVideoSubscription;
        private System.Windows.Forms.ToolStripMenuItem menuVideoGuidePodcastSubscription;
        private System.Windows.Forms.ToolStripMenuItem menuVideoGuideFeedSubscription;
    }
}

