namespace BlizzTV
{
    partial class frmPreferences
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmPreferences));
            this.ImageList = new System.Windows.Forms.ImageList(this.components);
            this.buttonOK = new System.Windows.Forms.Button();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.tabDebug = new System.Windows.Forms.TabPage();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.checkBoxEnableDebugLogging = new System.Windows.Forms.CheckBox();
            this.checkBoxEnableDebugConsole = new System.Windows.Forms.CheckBox();
            this.tabPlugins = new System.Windows.Forms.TabPage();
            this.ListviewPlugins = new System.Windows.Forms.ListView();
            this.ColEnabled = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colIcon = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.ColName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.ColDescription = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.tabGeneral = new System.Windows.Forms.TabPage();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txtVideoPlayerWidth = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtVideoPlayerHeight = new System.Windows.Forms.TextBox();
            this.checkBoxVideoAutoPlay = new System.Windows.Forms.CheckBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.radioButtonUseInternalViewers = new System.Windows.Forms.RadioButton();
            this.radioButtonUseDefaultWebBrowser = new System.Windows.Forms.RadioButton();
            this.tabPreferences = new System.Windows.Forms.TabControl();
            this.tabDebug.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.tabPlugins.SuspendLayout();
            this.tabGeneral.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.tabPreferences.SuspendLayout();
            this.SuspendLayout();
            // 
            // ImageList
            // 
            this.ImageList.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("ImageList.ImageStream")));
            this.ImageList.TransparentColor = System.Drawing.Color.Transparent;
            this.ImageList.Images.SetKeyName(0, "preferences.png");
            this.ImageList.Images.SetKeyName(1, "plugin.png");
            this.ImageList.Images.SetKeyName(2, "bug_16.png");
            // 
            // buttonOK
            // 
            this.buttonOK.Location = new System.Drawing.Point(397, 312);
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
            this.buttonCancel.Location = new System.Drawing.Point(316, 312);
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
            this.tabDebug.ImageIndex = 2;
            this.tabDebug.Location = new System.Drawing.Point(4, 23);
            this.tabDebug.Name = "tabDebug";
            this.tabDebug.Padding = new System.Windows.Forms.Padding(3);
            this.tabDebug.Size = new System.Drawing.Size(461, 276);
            this.tabDebug.TabIndex = 2;
            this.tabDebug.Text = "Debug";
            this.tabDebug.UseVisualStyleBackColor = true;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.checkBoxEnableDebugConsole);
            this.groupBox3.Controls.Add(this.checkBoxEnableDebugLogging);
            this.groupBox3.Location = new System.Drawing.Point(6, 6);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(449, 69);
            this.groupBox3.TabIndex = 0;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Debug Support";
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
            // tabPlugins
            // 
            this.tabPlugins.Controls.Add(this.ListviewPlugins);
            this.tabPlugins.ImageIndex = 1;
            this.tabPlugins.Location = new System.Drawing.Point(4, 23);
            this.tabPlugins.Name = "tabPlugins";
            this.tabPlugins.Padding = new System.Windows.Forms.Padding(3);
            this.tabPlugins.Size = new System.Drawing.Size(461, 276);
            this.tabPlugins.TabIndex = 1;
            this.tabPlugins.Text = "Plugins";
            this.tabPlugins.UseVisualStyleBackColor = true;
            // 
            // ListviewPlugins
            // 
            this.ListviewPlugins.CheckBoxes = true;
            this.ListviewPlugins.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.ColEnabled,
            this.colIcon,
            this.ColName,
            this.ColDescription});
            this.ListviewPlugins.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ListviewPlugins.FullRowSelect = true;
            this.ListviewPlugins.Location = new System.Drawing.Point(3, 3);
            this.ListviewPlugins.Name = "ListviewPlugins";
            this.ListviewPlugins.Size = new System.Drawing.Size(455, 270);
            this.ListviewPlugins.TabIndex = 1;
            this.ListviewPlugins.UseCompatibleStateImageBehavior = false;
            this.ListviewPlugins.View = System.Windows.Forms.View.Details;
            // 
            // ColEnabled
            // 
            this.ColEnabled.Text = "Enabled";
            this.ColEnabled.Width = 56;
            // 
            // colIcon
            // 
            this.colIcon.Text = "Icon";
            this.colIcon.Width = 33;
            // 
            // ColName
            // 
            this.ColName.Text = "Name";
            this.ColName.Width = 100;
            // 
            // ColDescription
            // 
            this.ColDescription.Text = "Description";
            this.ColDescription.Width = 262;
            // 
            // tabGeneral
            // 
            this.tabGeneral.Controls.Add(this.groupBox2);
            this.tabGeneral.Controls.Add(this.groupBox1);
            this.tabGeneral.ImageIndex = 0;
            this.tabGeneral.Location = new System.Drawing.Point(4, 23);
            this.tabGeneral.Name = "tabGeneral";
            this.tabGeneral.Padding = new System.Windows.Forms.Padding(3);
            this.tabGeneral.Size = new System.Drawing.Size(461, 276);
            this.tabGeneral.TabIndex = 0;
            this.tabGeneral.Text = "General";
            this.tabGeneral.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.checkBoxVideoAutoPlay);
            this.groupBox1.Controls.Add(this.txtVideoPlayerHeight);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.txtVideoPlayerWidth);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(9, 79);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(446, 67);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Internal Video Player Settings";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 21);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(62, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Player Size:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(74, 21);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(38, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Width:";
            // 
            // txtVideoPlayerWidth
            // 
            this.txtVideoPlayerWidth.Location = new System.Drawing.Point(118, 18);
            this.txtVideoPlayerWidth.Name = "txtVideoPlayerWidth";
            this.txtVideoPlayerWidth.Size = new System.Drawing.Size(49, 20);
            this.txtVideoPlayerWidth.TabIndex = 2;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(173, 21);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(41, 13);
            this.label3.TabIndex = 3;
            this.label3.Text = "Height:";
            // 
            // txtVideoPlayerHeight
            // 
            this.txtVideoPlayerHeight.Location = new System.Drawing.Point(220, 18);
            this.txtVideoPlayerHeight.Name = "txtVideoPlayerHeight";
            this.txtVideoPlayerHeight.Size = new System.Drawing.Size(49, 20);
            this.txtVideoPlayerHeight.TabIndex = 3;
            // 
            // checkBoxVideoAutoPlay
            // 
            this.checkBoxVideoAutoPlay.AutoSize = true;
            this.checkBoxVideoAutoPlay.Location = new System.Drawing.Point(9, 44);
            this.checkBoxVideoAutoPlay.Name = "checkBoxVideoAutoPlay";
            this.checkBoxVideoAutoPlay.Size = new System.Drawing.Size(70, 17);
            this.checkBoxVideoAutoPlay.TabIndex = 4;
            this.checkBoxVideoAutoPlay.Text = "Auto-play";
            this.checkBoxVideoAutoPlay.UseVisualStyleBackColor = true;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.radioButtonUseDefaultWebBrowser);
            this.groupBox2.Controls.Add(this.radioButtonUseInternalViewers);
            this.groupBox2.Location = new System.Drawing.Point(9, 6);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(446, 67);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "For viewing content:";
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
            // tabPreferences
            // 
            this.tabPreferences.Controls.Add(this.tabGeneral);
            this.tabPreferences.Controls.Add(this.tabPlugins);
            this.tabPreferences.Controls.Add(this.tabDebug);
            this.tabPreferences.ImageList = this.ImageList;
            this.tabPreferences.Location = new System.Drawing.Point(3, 3);
            this.tabPreferences.Name = "tabPreferences";
            this.tabPreferences.SelectedIndex = 0;
            this.tabPreferences.Size = new System.Drawing.Size(469, 303);
            this.tabPreferences.TabIndex = 0;
            // 
            // frmPreferences
            // 
            this.AcceptButton = this.buttonOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.buttonCancel;
            this.ClientSize = new System.Drawing.Size(474, 340);
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(this.buttonOK);
            this.Controls.Add(this.tabPreferences);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmPreferences";
            this.Text = "Preferences";
            this.Load += new System.EventHandler(this.frmPreferences_Load);
            this.tabDebug.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.tabPlugins.ResumeLayout(false);
            this.tabGeneral.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.tabPreferences.ResumeLayout(false);
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
        private System.Windows.Forms.TabPage tabPlugins;
        private System.Windows.Forms.ListView ListviewPlugins;
        private System.Windows.Forms.ColumnHeader ColEnabled;
        private System.Windows.Forms.ColumnHeader colIcon;
        private System.Windows.Forms.ColumnHeader ColName;
        private System.Windows.Forms.ColumnHeader ColDescription;
        private System.Windows.Forms.TabPage tabGeneral;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.RadioButton radioButtonUseDefaultWebBrowser;
        private System.Windows.Forms.RadioButton radioButtonUseInternalViewers;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.CheckBox checkBoxVideoAutoPlay;
        private System.Windows.Forms.TextBox txtVideoPlayerHeight;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtVideoPlayerWidth;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TabControl tabPreferences;
    }
}