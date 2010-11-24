namespace LibEvents
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
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.numericUpDownNumberOfDaysToShowEventsOnMainWindow = new System.Windows.Forms.NumericUpDown();
            this.label2 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.checkBoxAllowEventNotifications = new System.Windows.Forms.CheckBox();
            this.checkBoxAllowNotificationOfEventsInProgress = new System.Windows.Forms.CheckBox();
            this.label1 = new System.Windows.Forms.Label();
            this.numericUpDownMinutesBefore = new System.Windows.Forms.NumericUpDown();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownNumberOfDaysToShowEventsOnMainWindow)).BeginInit();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownMinutesBefore)).BeginInit();
            this.tabControl1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.groupBox2);
            this.tabPage1.Controls.Add(this.groupBox1);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(439, 236);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "General";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox2.Controls.Add(this.numericUpDownNumberOfDaysToShowEventsOnMainWindow);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Location = new System.Drawing.Point(8, 6);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(423, 44);
            this.groupBox2.TabIndex = 9;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Events";
            // 
            // numericUpDownNumberOfDaysToShowEventsOnMainWindow
            // 
            this.numericUpDownNumberOfDaysToShowEventsOnMainWindow.Location = new System.Drawing.Point(250, 14);
            this.numericUpDownNumberOfDaysToShowEventsOnMainWindow.Maximum = new decimal(new int[] {
            30,
            0,
            0,
            0});
            this.numericUpDownNumberOfDaysToShowEventsOnMainWindow.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDownNumberOfDaysToShowEventsOnMainWindow.Name = "numericUpDownNumberOfDaysToShowEventsOnMainWindow";
            this.numericUpDownNumberOfDaysToShowEventsOnMainWindow.Size = new System.Drawing.Size(41, 20);
            this.numericUpDownNumberOfDaysToShowEventsOnMainWindow.TabIndex = 1;
            this.numericUpDownNumberOfDaysToShowEventsOnMainWindow.Value = new decimal(new int[] {
            7,
            0,
            0,
            0});
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 16);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(238, 13);
            this.label2.TabIndex = 0;
            this.label2.Text = "Number of days to show events on main window:";
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.checkBoxAllowEventNotifications);
            this.groupBox1.Controls.Add(this.checkBoxAllowNotificationOfEventsInProgress);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.numericUpDownMinutesBefore);
            this.groupBox1.Location = new System.Drawing.Point(8, 56);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(423, 88);
            this.groupBox1.TabIndex = 8;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Notifications";
            // 
            // checkBoxAllowEventNotifications
            // 
            this.checkBoxAllowEventNotifications.AutoSize = true;
            this.checkBoxAllowEventNotifications.Location = new System.Drawing.Point(6, 19);
            this.checkBoxAllowEventNotifications.Name = "checkBoxAllowEventNotifications";
            this.checkBoxAllowEventNotifications.Size = new System.Drawing.Size(143, 17);
            this.checkBoxAllowEventNotifications.TabIndex = 4;
            this.checkBoxAllowEventNotifications.Text = "Allow event notifications.";
            this.checkBoxAllowEventNotifications.UseVisualStyleBackColor = true;
            // 
            // checkBoxAllowNotificationOfEventsInProgress
            // 
            this.checkBoxAllowNotificationOfEventsInProgress.AutoSize = true;
            this.checkBoxAllowNotificationOfEventsInProgress.Location = new System.Drawing.Point(6, 42);
            this.checkBoxAllowNotificationOfEventsInProgress.Name = "checkBoxAllowNotificationOfEventsInProgress";
            this.checkBoxAllowNotificationOfEventsInProgress.Size = new System.Drawing.Size(209, 17);
            this.checkBoxAllowNotificationOfEventsInProgress.TabIndex = 7;
            this.checkBoxAllowNotificationOfEventsInProgress.Text = "Allow notification of events in-progress.";
            this.checkBoxAllowNotificationOfEventsInProgress.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 62);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(165, 13);
            this.label1.TabIndex = 5;
            this.label1.Text = "Minutes to notify before an event:";
            // 
            // numericUpDownMinutesBefore
            // 
            this.numericUpDownMinutesBefore.Location = new System.Drawing.Point(177, 60);
            this.numericUpDownMinutesBefore.Maximum = new decimal(new int[] {
            1440,
            0,
            0,
            0});
            this.numericUpDownMinutesBefore.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDownMinutesBefore.Name = "numericUpDownMinutesBefore";
            this.numericUpDownMinutesBefore.Size = new System.Drawing.Size(50, 20);
            this.numericUpDownMinutesBefore.TabIndex = 6;
            this.numericUpDownMinutesBefore.Value = new decimal(new int[] {
            15,
            0,
            0,
            0});
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(447, 262);
            this.tabControl1.TabIndex = 0;
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
            this.tabPage1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownNumberOfDaysToShowEventsOnMainWindow)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownMinutesBefore)).EndInit();
            this.tabControl1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.CheckBox checkBoxAllowEventNotifications;
        private System.Windows.Forms.CheckBox checkBoxAllowNotificationOfEventsInProgress;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.NumericUpDown numericUpDownMinutesBefore;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.NumericUpDown numericUpDownNumberOfDaysToShowEventsOnMainWindow;



    }
}