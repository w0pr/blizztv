namespace BlizzTV.Events
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
            this.checkBoxEnableNotifications = new System.Windows.Forms.CheckBox();
            this.checkBoxEnableInProgressEventNotifications = new System.Windows.Forms.CheckBox();
            this.label1 = new System.Windows.Forms.Label();
            this.numericUpDownMinutesToNotifyBeforeEvent = new System.Windows.Forms.NumericUpDown();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownNumberOfDaysToShowEventsOnMainWindow)).BeginInit();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownMinutesToNotifyBeforeEvent)).BeginInit();
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
            this.tabPage1.Text = "Options";
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
            this.groupBox2.TabIndex = 0;
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
            this.groupBox1.Controls.Add(this.checkBoxEnableNotifications);
            this.groupBox1.Controls.Add(this.checkBoxEnableInProgressEventNotifications);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.numericUpDownMinutesToNotifyBeforeEvent);
            this.groupBox1.Location = new System.Drawing.Point(8, 56);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(423, 88);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Notifications";
            // 
            // checkBoxEnableNotifications
            // 
            this.checkBoxEnableNotifications.AutoSize = true;
            this.checkBoxEnableNotifications.Location = new System.Drawing.Point(6, 19);
            this.checkBoxEnableNotifications.Name = "checkBoxEnableNotifications";
            this.checkBoxEnableNotifications.Size = new System.Drawing.Size(186, 17);
            this.checkBoxEnableNotifications.TabIndex = 2;
            this.checkBoxEnableNotifications.Text = "Enable notification of new events.";
            this.checkBoxEnableNotifications.UseVisualStyleBackColor = true;
            this.checkBoxEnableNotifications.CheckedChanged += new System.EventHandler(this.checkBoxEnableNotifications_CheckedChanged);
            // 
            // checkBoxEnableInProgressEventNotifications
            // 
            this.checkBoxEnableInProgressEventNotifications.AutoSize = true;
            this.checkBoxEnableInProgressEventNotifications.Location = new System.Drawing.Point(6, 42);
            this.checkBoxEnableInProgressEventNotifications.Name = "checkBoxEnableInProgressEventNotifications";
            this.checkBoxEnableInProgressEventNotifications.Size = new System.Drawing.Size(240, 17);
            this.checkBoxEnableInProgressEventNotifications.TabIndex = 3;
            this.checkBoxEnableInProgressEventNotifications.Text = "Enable notification for In-Progress events too.";
            this.checkBoxEnableInProgressEventNotifications.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 62);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(165, 13);
            this.label1.TabIndex = 5;
            this.label1.Text = "Minutes to notify before an event:";
            // 
            // numericUpDownMinutesToNotifyBeforeEvent
            // 
            this.numericUpDownMinutesToNotifyBeforeEvent.Location = new System.Drawing.Point(174, 60);
            this.numericUpDownMinutesToNotifyBeforeEvent.Maximum = new decimal(new int[] {
            1440,
            0,
            0,
            0});
            this.numericUpDownMinutesToNotifyBeforeEvent.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDownMinutesToNotifyBeforeEvent.Name = "numericUpDownMinutesToNotifyBeforeEvent";
            this.numericUpDownMinutesToNotifyBeforeEvent.Size = new System.Drawing.Size(50, 20);
            this.numericUpDownMinutesToNotifyBeforeEvent.TabIndex = 4;
            this.numericUpDownMinutesToNotifyBeforeEvent.Value = new decimal(new int[] {
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
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownMinutesToNotifyBeforeEvent)).EndInit();
            this.tabControl1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.CheckBox checkBoxEnableNotifications;
        private System.Windows.Forms.CheckBox checkBoxEnableInProgressEventNotifications;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.NumericUpDown numericUpDownMinutesToNotifyBeforeEvent;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.NumericUpDown numericUpDownNumberOfDaysToShowEventsOnMainWindow;



    }
}