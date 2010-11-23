namespace LibEvents
{
    partial class frmSetupAlarm
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
            this.label1 = new System.Windows.Forms.Label();
            this.LabelEventName = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.LabelEventTime = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.ComboBoxAlertBefore = new System.Windows.Forms.ComboBox();
            this.ButtonSetup = new System.Windows.Forms.Button();
            this.ButtonCancel = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.LabelTimeLeft = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(1, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(38, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Event:";
            // 
            // LabelEventName
            // 
            this.LabelEventName.Location = new System.Drawing.Point(124, 9);
            this.LabelEventName.Name = "LabelEventName";
            this.LabelEventName.Size = new System.Drawing.Size(244, 13);
            this.LabelEventName.TabIndex = 1;
            this.LabelEventName.Text = "Event Name";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(1, 32);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(33, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Time:";
            // 
            // LabelEventTime
            // 
            this.LabelEventTime.Location = new System.Drawing.Point(124, 32);
            this.LabelEventTime.Name = "LabelEventTime";
            this.LabelEventTime.Size = new System.Drawing.Size(247, 13);
            this.LabelEventTime.TabIndex = 3;
            this.LabelEventTime.Text = "Event Time";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(1, 76);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(120, 13);
            this.label3.TabIndex = 4;
            this.label3.Text = "Minutes to alarm before:";
            // 
            // ComboBoxAlertBefore
            // 
            this.ComboBoxAlertBefore.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ComboBoxAlertBefore.FormattingEnabled = true;
            this.ComboBoxAlertBefore.Items.AddRange(new object[] {
            "5",
            "10",
            "15",
            "30",
            "60",
            "90"});
            this.ComboBoxAlertBefore.Location = new System.Drawing.Point(124, 73);
            this.ComboBoxAlertBefore.Name = "ComboBoxAlertBefore";
            this.ComboBoxAlertBefore.Size = new System.Drawing.Size(244, 21);
            this.ComboBoxAlertBefore.TabIndex = 5;
            // 
            // ButtonSetup
            // 
            this.ButtonSetup.Location = new System.Drawing.Point(293, 102);
            this.ButtonSetup.Name = "ButtonSetup";
            this.ButtonSetup.Size = new System.Drawing.Size(75, 23);
            this.ButtonSetup.TabIndex = 6;
            this.ButtonSetup.Text = "Setup";
            this.ButtonSetup.UseVisualStyleBackColor = true;
            // 
            // ButtonCancel
            // 
            this.ButtonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.ButtonCancel.Location = new System.Drawing.Point(212, 102);
            this.ButtonCancel.Name = "ButtonCancel";
            this.ButtonCancel.Size = new System.Drawing.Size(75, 23);
            this.ButtonCancel.TabIndex = 7;
            this.ButtonCancel.Text = "Cancel";
            this.ButtonCancel.UseVisualStyleBackColor = true;
            this.ButtonCancel.Click += new System.EventHandler(this.ButtonCancel_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(1, 52);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(54, 13);
            this.label4.TabIndex = 8;
            this.label4.Text = "Time Left:";
            // 
            // LabelTimeLeft
            // 
            this.LabelTimeLeft.Location = new System.Drawing.Point(124, 52);
            this.LabelTimeLeft.Name = "LabelTimeLeft";
            this.LabelTimeLeft.Size = new System.Drawing.Size(247, 13);
            this.LabelTimeLeft.TabIndex = 9;
            this.LabelTimeLeft.Text = "Time Left";
            // 
            // frmSetupAlarm
            // 
            this.AcceptButton = this.ButtonSetup;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.ButtonCancel;
            this.ClientSize = new System.Drawing.Size(375, 131);
            this.Controls.Add(this.LabelTimeLeft);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.ButtonCancel);
            this.Controls.Add(this.ButtonSetup);
            this.Controls.Add(this.ComboBoxAlertBefore);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.LabelEventTime);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.LabelEventName);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "frmSetupAlarm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Setup Alarm for Event:";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label LabelEventName;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label LabelEventTime;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox ComboBoxAlertBefore;
        private System.Windows.Forms.Button ButtonSetup;
        private System.Windows.Forms.Button ButtonCancel;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label LabelTimeLeft;
    }
}