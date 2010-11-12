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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.numericUpDownMinutesBefore = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.checkBoxAllowEventNotifications = new System.Windows.Forms.CheckBox();
            this.checkBoxAllowNotificationOfEventsInProgress = new System.Windows.Forms.CheckBox();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownMinutesBefore)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.checkBoxAllowNotificationOfEventsInProgress);
            this.groupBox1.Controls.Add(this.numericUpDownMinutesBefore);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.checkBoxAllowEventNotifications);
            this.groupBox1.Location = new System.Drawing.Point(3, 2);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(444, 87);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Event Notifications";
            // 
            // numericUpDownMinutesBefore
            // 
            this.numericUpDownMinutesBefore.Location = new System.Drawing.Point(177, 60);
            this.numericUpDownMinutesBefore.Name = "numericUpDownMinutesBefore";
            this.numericUpDownMinutesBefore.Size = new System.Drawing.Size(39, 20);
            this.numericUpDownMinutesBefore.TabIndex = 2;
            this.numericUpDownMinutesBefore.Value = new decimal(new int[] {
            15,
            0,
            0,
            0});
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 62);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(165, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Minutes to notify before an event:";
            // 
            // checkBoxAllowEventNotifications
            // 
            this.checkBoxAllowEventNotifications.AutoSize = true;
            this.checkBoxAllowEventNotifications.Location = new System.Drawing.Point(9, 19);
            this.checkBoxAllowEventNotifications.Name = "checkBoxAllowEventNotifications";
            this.checkBoxAllowEventNotifications.Size = new System.Drawing.Size(143, 17);
            this.checkBoxAllowEventNotifications.TabIndex = 0;
            this.checkBoxAllowEventNotifications.Text = "Allow event notifications.";
            this.checkBoxAllowEventNotifications.UseVisualStyleBackColor = true;
            // 
            // checkBoxAllowNotificationOfEventsInProgress
            // 
            this.checkBoxAllowNotificationOfEventsInProgress.AutoSize = true;
            this.checkBoxAllowNotificationOfEventsInProgress.Location = new System.Drawing.Point(9, 42);
            this.checkBoxAllowNotificationOfEventsInProgress.Name = "checkBoxAllowNotificationOfEventsInProgress";
            this.checkBoxAllowNotificationOfEventsInProgress.Size = new System.Drawing.Size(209, 17);
            this.checkBoxAllowNotificationOfEventsInProgress.TabIndex = 3;
            this.checkBoxAllowNotificationOfEventsInProgress.Text = "Allow notification of events in-progress.";
            this.checkBoxAllowNotificationOfEventsInProgress.UseVisualStyleBackColor = true;
            // 
            // frmSettings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(451, 269);
            this.Controls.Add(this.groupBox1);
            this.Name = "frmSettings";
            this.Text = "frmSettings";
            this.Load += new System.EventHandler(this.frmSettings_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownMinutesBefore)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.CheckBox checkBoxAllowEventNotifications;
        private System.Windows.Forms.NumericUpDown numericUpDownMinutesBefore;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.CheckBox checkBoxAllowNotificationOfEventsInProgress;

    }
}