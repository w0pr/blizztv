namespace BlizzTV.UI
{
    partial class PreferencesForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PreferencesForm));
            this.ImageList = new System.Windows.Forms.ImageList(this.components);
            this.buttonOK = new System.Windows.Forms.Button();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.tabDebug = new System.Windows.Forms.TabPage();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.checkBoxEnableDebugConsole = new System.Windows.Forms.CheckBox();
            this.checkBoxEnableDebugLogging = new System.Windows.Forms.CheckBox();
            this.tabModules = new System.Windows.Forms.TabPage();
            this.listviewModules = new System.Windows.Forms.ListView();
            this.ColEnabled = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.ColName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.ColDescription = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.tabGeneral = new System.Windows.Forms.TabPage();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.checkBoxStartOnSystemStartup = new System.Windows.Forms.CheckBox();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.checkBoxAllowBetaVersionNotifications = new System.Windows.Forms.CheckBox();
            this.checkBoxAllowAutomaticUpdateChecks = new System.Windows.Forms.CheckBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.radioButtonUseDefaultWebBrowser = new System.Windows.Forms.RadioButton();
            this.radioButtonUseInternalViewers = new System.Windows.Forms.RadioButton();
            this.TabControl = new System.Windows.Forms.TabControl();
            this.tabUI = new System.Windows.Forms.TabPage();
            this.checkBoxMinimimizeToSystemTray = new System.Windows.Forms.CheckBox();
            this.tabNotifications = new System.Windows.Forms.TabPage();
            this.groupBoxNotificationSounds = new System.Windows.Forms.GroupBox();
            this.label1 = new System.Windows.Forms.Label();
            this.checkBoxNotificationSoundsEnabled = new System.Windows.Forms.CheckBox();
            this.comboBoxNotificationSound = new System.Windows.Forms.ComboBox();
            this.checkBoxNotificationsEnabled = new System.Windows.Forms.CheckBox();
            this.tabPlayer = new System.Windows.Forms.TabPage();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.CheckBoxPlayerAlwaysOnTop = new System.Windows.Forms.CheckBox();
            this.label2 = new System.Windows.Forms.Label();
            this.volumeBar = new System.Windows.Forms.TrackBar();
            this.tabDebug.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.tabModules.SuspendLayout();
            this.tabGeneral.SuspendLayout();
            this.groupBox5.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.TabControl.SuspendLayout();
            this.tabUI.SuspendLayout();
            this.tabNotifications.SuspendLayout();
            this.groupBoxNotificationSounds.SuspendLayout();
            this.tabPlayer.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.volumeBar)).BeginInit();
            this.SuspendLayout();
            // 
            // ImageList
            // 
            this.ImageList.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("ImageList.ImageStream")));
            this.ImageList.TransparentColor = System.Drawing.Color.Transparent;
            this.ImageList.Images.SetKeyName(0, "settings.png");
            this.ImageList.Images.SetKeyName(1, "userinterface.png");
            this.ImageList.Images.SetKeyName(2, "notification.png");
            this.ImageList.Images.SetKeyName(3, "player.png");
            this.ImageList.Images.SetKeyName(4, "plugin.png");
            this.ImageList.Images.SetKeyName(5, "bug.png");
            // 
            // buttonOK
            // 
            this.buttonOK.Location = new System.Drawing.Point(458, 312);
            this.buttonOK.Name = "buttonOK";
            this.buttonOK.Size = new System.Drawing.Size(75, 23);
            this.buttonOK.TabIndex = 1;
            this.buttonOK.Text = "OK";
            this.buttonOK.UseVisualStyleBackColor = true;
            this.buttonOK.Click += new System.EventHandler(this.buttonOK_Click);
            // 
            // buttonCancel
            // 
            this.buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonCancel.Location = new System.Drawing.Point(377, 312);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(75, 23);
            this.buttonCancel.TabIndex = 2;
            this.buttonCancel.Text = "Cancel";
            this.buttonCancel.UseVisualStyleBackColor = true;
            this.buttonCancel.Click += new System.EventHandler(this.buttonCancel_Click);
            // 
            // tabDebug
            // 
            this.tabDebug.Controls.Add(this.groupBox3);
            this.tabDebug.ImageIndex = 5;
            this.tabDebug.Location = new System.Drawing.Point(4, 23);
            this.tabDebug.Name = "tabDebug";
            this.tabDebug.Padding = new System.Windows.Forms.Padding(3);
            this.tabDebug.Size = new System.Drawing.Size(531, 276);
            this.tabDebug.TabIndex = 2;
            this.tabDebug.Text = "Debug";
            this.tabDebug.UseVisualStyleBackColor = true;
            // 
            // groupBox3
            // 
            this.groupBox3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox3.Controls.Add(this.checkBoxEnableDebugConsole);
            this.groupBox3.Controls.Add(this.checkBoxEnableDebugLogging);
            this.groupBox3.Location = new System.Drawing.Point(6, 6);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(519, 69);
            this.groupBox3.TabIndex = 0;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Debug Support";
            // 
            // checkBoxEnableDebugConsole
            // 
            this.checkBoxEnableDebugConsole.AutoSize = true;
            this.checkBoxEnableDebugConsole.Location = new System.Drawing.Point(9, 42);
            this.checkBoxEnableDebugConsole.Name = "checkBoxEnableDebugConsole";
            this.checkBoxEnableDebugConsole.Size = new System.Drawing.Size(132, 17);
            this.checkBoxEnableDebugConsole.TabIndex = 1;
            this.checkBoxEnableDebugConsole.Text = "Enable debug-console";
            this.checkBoxEnableDebugConsole.UseVisualStyleBackColor = true;
            // 
            // checkBoxEnableDebugLogging
            // 
            this.checkBoxEnableDebugLogging.AutoSize = true;
            this.checkBoxEnableDebugLogging.Location = new System.Drawing.Point(9, 19);
            this.checkBoxEnableDebugLogging.Name = "checkBoxEnableDebugLogging";
            this.checkBoxEnableDebugLogging.Size = new System.Drawing.Size(129, 17);
            this.checkBoxEnableDebugLogging.TabIndex = 0;
            this.checkBoxEnableDebugLogging.Text = "Enable debug-logging";
            this.checkBoxEnableDebugLogging.UseVisualStyleBackColor = true;
            // 
            // tabModules
            // 
            this.tabModules.Controls.Add(this.listviewModules);
            this.tabModules.ImageIndex = 4;
            this.tabModules.Location = new System.Drawing.Point(4, 23);
            this.tabModules.Name = "tabModules";
            this.tabModules.Padding = new System.Windows.Forms.Padding(3);
            this.tabModules.Size = new System.Drawing.Size(531, 276);
            this.tabModules.TabIndex = 1;
            this.tabModules.Text = "Modules";
            this.tabModules.UseVisualStyleBackColor = true;
            // 
            // listviewModules
            // 
            this.listviewModules.CheckBoxes = true;
            this.listviewModules.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.ColEnabled,
            this.ColName,
            this.ColDescription});
            this.listviewModules.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listviewModules.FullRowSelect = true;
            this.listviewModules.Location = new System.Drawing.Point(3, 3);
            this.listviewModules.Name = "listviewModules";
            this.listviewModules.ShowGroups = false;
            this.listviewModules.Size = new System.Drawing.Size(525, 270);
            this.listviewModules.SmallImageList = this.ImageList;
            this.listviewModules.TabIndex = 1;
            this.listviewModules.UseCompatibleStateImageBehavior = false;
            this.listviewModules.View = System.Windows.Forms.View.Details;
            // 
            // ColEnabled
            // 
            this.ColEnabled.Text = "Enabled";
            this.ColEnabled.Width = 56;
            // 
            // ColName
            // 
            this.ColName.Text = "Name";
            this.ColName.Width = 100;
            // 
            // ColDescription
            // 
            this.ColDescription.Text = "Description";
            this.ColDescription.Width = 295;
            // 
            // tabGeneral
            // 
            this.tabGeneral.Controls.Add(this.groupBox5);
            this.tabGeneral.Controls.Add(this.groupBox4);
            this.tabGeneral.Controls.Add(this.groupBox2);
            this.tabGeneral.ImageIndex = 0;
            this.tabGeneral.Location = new System.Drawing.Point(4, 23);
            this.tabGeneral.Name = "tabGeneral";
            this.tabGeneral.Padding = new System.Windows.Forms.Padding(3);
            this.tabGeneral.Size = new System.Drawing.Size(531, 276);
            this.tabGeneral.TabIndex = 0;
            this.tabGeneral.Text = "General";
            this.tabGeneral.UseVisualStyleBackColor = true;
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.checkBoxStartOnSystemStartup);
            this.groupBox5.Location = new System.Drawing.Point(9, 153);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(511, 48);
            this.groupBox5.TabIndex = 3;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "Windows Integration";
            // 
            // checkBoxStartOnSystemStartup
            // 
            this.checkBoxStartOnSystemStartup.AutoSize = true;
            this.checkBoxStartOnSystemStartup.Location = new System.Drawing.Point(9, 19);
            this.checkBoxStartOnSystemStartup.Name = "checkBoxStartOnSystemStartup";
            this.checkBoxStartOnSystemStartup.Size = new System.Drawing.Size(174, 17);
            this.checkBoxStartOnSystemStartup.TabIndex = 0;
            this.checkBoxStartOnSystemStartup.Text = "Start BlizzTV on system startup.";
            this.checkBoxStartOnSystemStartup.UseVisualStyleBackColor = true;
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.checkBoxAllowBetaVersionNotifications);
            this.groupBox4.Controls.Add(this.checkBoxAllowAutomaticUpdateChecks);
            this.groupBox4.Location = new System.Drawing.Point(9, 79);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(511, 68);
            this.groupBox4.TabIndex = 2;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Updates";
            // 
            // checkBoxAllowBetaVersionNotifications
            // 
            this.checkBoxAllowBetaVersionNotifications.AutoSize = true;
            this.checkBoxAllowBetaVersionNotifications.Location = new System.Drawing.Point(9, 42);
            this.checkBoxAllowBetaVersionNotifications.Name = "checkBoxAllowBetaVersionNotifications";
            this.checkBoxAllowBetaVersionNotifications.Size = new System.Drawing.Size(174, 17);
            this.checkBoxAllowBetaVersionNotifications.TabIndex = 1;
            this.checkBoxAllowBetaVersionNotifications.Text = "Notify also about beta versions.";
            this.checkBoxAllowBetaVersionNotifications.UseVisualStyleBackColor = true;
            // 
            // checkBoxAllowAutomaticUpdateChecks
            // 
            this.checkBoxAllowAutomaticUpdateChecks.AutoSize = true;
            this.checkBoxAllowAutomaticUpdateChecks.Location = new System.Drawing.Point(9, 19);
            this.checkBoxAllowAutomaticUpdateChecks.Name = "checkBoxAllowAutomaticUpdateChecks";
            this.checkBoxAllowAutomaticUpdateChecks.Size = new System.Drawing.Size(180, 17);
            this.checkBoxAllowAutomaticUpdateChecks.TabIndex = 0;
            this.checkBoxAllowAutomaticUpdateChecks.Text = "Automatically check for updates.";
            this.checkBoxAllowAutomaticUpdateChecks.UseVisualStyleBackColor = true;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.radioButtonUseDefaultWebBrowser);
            this.groupBox2.Controls.Add(this.radioButtonUseInternalViewers);
            this.groupBox2.Location = new System.Drawing.Point(9, 6);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(511, 67);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "For viewing content:";
            // 
            // radioButtonUseDefaultWebBrowser
            // 
            this.radioButtonUseDefaultWebBrowser.AutoSize = true;
            this.radioButtonUseDefaultWebBrowser.Location = new System.Drawing.Point(9, 42);
            this.radioButtonUseDefaultWebBrowser.Name = "radioButtonUseDefaultWebBrowser";
            this.radioButtonUseDefaultWebBrowser.Size = new System.Drawing.Size(142, 17);
            this.radioButtonUseDefaultWebBrowser.TabIndex = 1;
            this.radioButtonUseDefaultWebBrowser.Text = "Use default web-browser";
            this.radioButtonUseDefaultWebBrowser.UseVisualStyleBackColor = true;
            // 
            // radioButtonUseInternalViewers
            // 
            this.radioButtonUseInternalViewers.AutoSize = true;
            this.radioButtonUseInternalViewers.Location = new System.Drawing.Point(9, 19);
            this.radioButtonUseInternalViewers.Name = "radioButtonUseInternalViewers";
            this.radioButtonUseInternalViewers.Size = new System.Drawing.Size(120, 17);
            this.radioButtonUseInternalViewers.TabIndex = 0;
            this.radioButtonUseInternalViewers.Text = "Use internal viewers";
            this.radioButtonUseInternalViewers.UseVisualStyleBackColor = true;
            // 
            // TabControl
            // 
            this.TabControl.Controls.Add(this.tabGeneral);
            this.TabControl.Controls.Add(this.tabUI);
            this.TabControl.Controls.Add(this.tabNotifications);
            this.TabControl.Controls.Add(this.tabPlayer);
            this.TabControl.Controls.Add(this.tabModules);
            this.TabControl.Controls.Add(this.tabDebug);
            this.TabControl.ImageList = this.ImageList;
            this.TabControl.Location = new System.Drawing.Point(3, 3);
            this.TabControl.Multiline = true;
            this.TabControl.Name = "TabControl";
            this.TabControl.SelectedIndex = 0;
            this.TabControl.ShowToolTips = true;
            this.TabControl.Size = new System.Drawing.Size(539, 303);
            this.TabControl.TabIndex = 0;
            // 
            // tabUI
            // 
            this.tabUI.Controls.Add(this.checkBoxMinimimizeToSystemTray);
            this.tabUI.ImageIndex = 1;
            this.tabUI.Location = new System.Drawing.Point(4, 23);
            this.tabUI.Name = "tabUI";
            this.tabUI.Padding = new System.Windows.Forms.Padding(3);
            this.tabUI.Size = new System.Drawing.Size(531, 276);
            this.tabUI.TabIndex = 3;
            this.tabUI.Text = "User Interface";
            this.tabUI.UseVisualStyleBackColor = true;
            // 
            // checkBoxMinimimizeToSystemTray
            // 
            this.checkBoxMinimimizeToSystemTray.AutoSize = true;
            this.checkBoxMinimimizeToSystemTray.Location = new System.Drawing.Point(17, 17);
            this.checkBoxMinimimizeToSystemTray.Name = "checkBoxMinimimizeToSystemTray";
            this.checkBoxMinimimizeToSystemTray.Size = new System.Drawing.Size(133, 17);
            this.checkBoxMinimimizeToSystemTray.TabIndex = 0;
            this.checkBoxMinimimizeToSystemTray.Text = "Minimize to system tray";
            this.checkBoxMinimimizeToSystemTray.UseVisualStyleBackColor = true;
            // 
            // tabNotifications
            // 
            this.tabNotifications.Controls.Add(this.groupBoxNotificationSounds);
            this.tabNotifications.Controls.Add(this.checkBoxNotificationsEnabled);
            this.tabNotifications.ImageIndex = 2;
            this.tabNotifications.Location = new System.Drawing.Point(4, 23);
            this.tabNotifications.Name = "tabNotifications";
            this.tabNotifications.Size = new System.Drawing.Size(531, 276);
            this.tabNotifications.TabIndex = 5;
            this.tabNotifications.Text = "Notifications";
            this.tabNotifications.UseVisualStyleBackColor = true;
            // 
            // groupBoxNotificationSounds
            // 
            this.groupBoxNotificationSounds.Controls.Add(this.volumeBar);
            this.groupBoxNotificationSounds.Controls.Add(this.label2);
            this.groupBoxNotificationSounds.Controls.Add(this.label1);
            this.groupBoxNotificationSounds.Controls.Add(this.checkBoxNotificationSoundsEnabled);
            this.groupBoxNotificationSounds.Controls.Add(this.comboBoxNotificationSound);
            this.groupBoxNotificationSounds.Location = new System.Drawing.Point(5, 35);
            this.groupBoxNotificationSounds.Name = "groupBoxNotificationSounds";
            this.groupBoxNotificationSounds.Size = new System.Drawing.Size(520, 98);
            this.groupBoxNotificationSounds.TabIndex = 1;
            this.groupBoxNotificationSounds.TabStop = false;
            this.groupBoxNotificationSounds.Text = "Notification Sounds";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 45);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(97, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "Notification Sound:";
            // 
            // checkBoxNotificationSoundsEnabled
            // 
            this.checkBoxNotificationSoundsEnabled.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.checkBoxNotificationSoundsEnabled.Location = new System.Drawing.Point(9, 19);
            this.checkBoxNotificationSoundsEnabled.Name = "checkBoxNotificationSoundsEnabled";
            this.checkBoxNotificationSoundsEnabled.Size = new System.Drawing.Size(505, 17);
            this.checkBoxNotificationSoundsEnabled.TabIndex = 0;
            this.checkBoxNotificationSoundsEnabled.Text = "Enable notification sounds";
            this.checkBoxNotificationSoundsEnabled.UseVisualStyleBackColor = true;
            this.checkBoxNotificationSoundsEnabled.CheckedChanged += new System.EventHandler(this.checkBoxNotificationSoundsEnabled_CheckedChanged);
            // 
            // comboBoxNotificationSound
            // 
            this.comboBoxNotificationSound.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.comboBoxNotificationSound.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxNotificationSound.FormattingEnabled = true;
            this.comboBoxNotificationSound.Location = new System.Drawing.Point(109, 42);
            this.comboBoxNotificationSound.Name = "comboBoxNotificationSound";
            this.comboBoxNotificationSound.Size = new System.Drawing.Size(142, 21);
            this.comboBoxNotificationSound.TabIndex = 1;
            this.comboBoxNotificationSound.SelectedIndexChanged += new System.EventHandler(this.comboBoxNotificationSound_SelectedIndexChanged);
            // 
            // checkBoxNotificationsEnabled
            // 
            this.checkBoxNotificationsEnabled.AutoSize = true;
            this.checkBoxNotificationsEnabled.Location = new System.Drawing.Point(11, 12);
            this.checkBoxNotificationsEnabled.Name = "checkBoxNotificationsEnabled";
            this.checkBoxNotificationsEnabled.Size = new System.Drawing.Size(118, 17);
            this.checkBoxNotificationsEnabled.TabIndex = 0;
            this.checkBoxNotificationsEnabled.Text = "Enable notifications";
            this.checkBoxNotificationsEnabled.UseVisualStyleBackColor = true;
            this.checkBoxNotificationsEnabled.CheckedChanged += new System.EventHandler(this.checkBoxNotificationsEnabled_CheckedChanged);
            // 
            // tabPlayer
            // 
            this.tabPlayer.Controls.Add(this.groupBox1);
            this.tabPlayer.ImageIndex = 3;
            this.tabPlayer.Location = new System.Drawing.Point(4, 23);
            this.tabPlayer.Name = "tabPlayer";
            this.tabPlayer.Padding = new System.Windows.Forms.Padding(3);
            this.tabPlayer.Size = new System.Drawing.Size(531, 276);
            this.tabPlayer.TabIndex = 4;
            this.tabPlayer.Text = "Player";
            this.tabPlayer.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.CheckBoxPlayerAlwaysOnTop);
            this.groupBox1.Location = new System.Drawing.Point(6, 6);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(519, 44);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Internal Video Player Settings";
            // 
            // CheckBoxPlayerAlwaysOnTop
            // 
            this.CheckBoxPlayerAlwaysOnTop.AutoSize = true;
            this.CheckBoxPlayerAlwaysOnTop.Location = new System.Drawing.Point(9, 19);
            this.CheckBoxPlayerAlwaysOnTop.Name = "CheckBoxPlayerAlwaysOnTop";
            this.CheckBoxPlayerAlwaysOnTop.Size = new System.Drawing.Size(98, 17);
            this.CheckBoxPlayerAlwaysOnTop.TabIndex = 5;
            this.CheckBoxPlayerAlwaysOnTop.Text = "Always On Top";
            this.CheckBoxPlayerAlwaysOnTop.UseVisualStyleBackColor = true;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(58, 67);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(45, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "Volume:";
            // 
            // volumeBar
            // 
            this.volumeBar.AutoSize = false;
            this.volumeBar.BackColor = System.Drawing.Color.White;
            this.volumeBar.Location = new System.Drawing.Point(109, 69);
            this.volumeBar.Name = "volumeBar";
            this.volumeBar.Size = new System.Drawing.Size(142, 23);
            this.volumeBar.TabIndex = 2;
            this.volumeBar.ValueChanged += new System.EventHandler(this.volumeBar_ValueChanged);
            // 
            // PreferencesForm
            // 
            this.AcceptButton = this.buttonOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.buttonCancel;
            this.ClientSize = new System.Drawing.Size(544, 342);
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(this.buttonOK);
            this.Controls.Add(this.TabControl);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "PreferencesForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Preferences";
            this.Load += new System.EventHandler(this.PreferencesForm_Load);
            this.tabDebug.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.tabModules.ResumeLayout(false);
            this.tabGeneral.ResumeLayout(false);
            this.groupBox5.ResumeLayout(false);
            this.groupBox5.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.TabControl.ResumeLayout(false);
            this.tabUI.ResumeLayout(false);
            this.tabUI.PerformLayout();
            this.tabNotifications.ResumeLayout(false);
            this.tabNotifications.PerformLayout();
            this.groupBoxNotificationSounds.ResumeLayout(false);
            this.groupBoxNotificationSounds.PerformLayout();
            this.tabPlayer.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.volumeBar)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button buttonOK;
        private System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.ImageList ImageList;
        private System.Windows.Forms.TabPage tabDebug;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.CheckBox checkBoxEnableDebugConsole;
        private System.Windows.Forms.CheckBox checkBoxEnableDebugLogging;
        private System.Windows.Forms.TabPage tabModules;
        private System.Windows.Forms.ListView listviewModules;
        private System.Windows.Forms.ColumnHeader ColEnabled;
        private System.Windows.Forms.ColumnHeader ColName;
        private System.Windows.Forms.ColumnHeader ColDescription;
        private System.Windows.Forms.TabPage tabGeneral;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.RadioButton radioButtonUseDefaultWebBrowser;
        private System.Windows.Forms.RadioButton radioButtonUseInternalViewers;
        private System.Windows.Forms.TabControl TabControl;
        private System.Windows.Forms.TabPage tabUI;
        private System.Windows.Forms.TabPage tabPlayer;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.CheckBox checkBoxMinimimizeToSystemTray;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.CheckBox checkBoxAllowBetaVersionNotifications;
        private System.Windows.Forms.CheckBox checkBoxAllowAutomaticUpdateChecks;
        private System.Windows.Forms.CheckBox CheckBoxPlayerAlwaysOnTop;
        private System.Windows.Forms.TabPage tabNotifications;
        private System.Windows.Forms.CheckBox checkBoxNotificationsEnabled;
        private System.Windows.Forms.GroupBox groupBoxNotificationSounds;
        private System.Windows.Forms.ComboBox comboBoxNotificationSound;
        private System.Windows.Forms.CheckBox checkBoxNotificationSoundsEnabled;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.CheckBox checkBoxStartOnSystemStartup;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TrackBar volumeBar;
    }
}