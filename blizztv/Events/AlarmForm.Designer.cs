namespace BlizzTV.Events
{
    partial class AlarmForm
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
            this.LabelEvent = new System.Windows.Forms.Label();
            this.LabelStatus = new System.Windows.Forms.Label();
            this.ButtonOkay = new System.Windows.Forms.Button();
            this.ButtonView = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // LabelEvent
            // 
            this.LabelEvent.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.LabelEvent.Location = new System.Drawing.Point(12, 9);
            this.LabelEvent.Name = "LabelEvent";
            this.LabelEvent.Size = new System.Drawing.Size(320, 18);
            this.LabelEvent.TabIndex = 0;
            this.LabelEvent.Text = "Event";
            // 
            // LabelStatus
            // 
            this.LabelStatus.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.LabelStatus.Location = new System.Drawing.Point(12, 27);
            this.LabelStatus.Name = "LabelStatus";
            this.LabelStatus.Size = new System.Drawing.Size(320, 18);
            this.LabelStatus.TabIndex = 0;
            this.LabelStatus.Text = "is about to start in x minutes";
            // 
            // ButtonOkay
            // 
            this.ButtonOkay.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.ButtonOkay.Location = new System.Drawing.Point(257, 48);
            this.ButtonOkay.Name = "ButtonOkay";
            this.ButtonOkay.Size = new System.Drawing.Size(75, 23);
            this.ButtonOkay.TabIndex = 1;
            this.ButtonOkay.Text = "OK";
            this.ButtonOkay.UseVisualStyleBackColor = false;
            this.ButtonOkay.Click += new System.EventHandler(this.ButtonOkay_Click);
            // 
            // ButtonView
            // 
            this.ButtonView.Location = new System.Drawing.Point(176, 48);
            this.ButtonView.Name = "ButtonView";
            this.ButtonView.Size = new System.Drawing.Size(75, 23);
            this.ButtonView.TabIndex = 0;
            this.ButtonView.Text = "View";
            this.ButtonView.UseVisualStyleBackColor = true;
            this.ButtonView.Click += new System.EventHandler(this.ButtonView_Click);
            // 
            // AlarmForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.ButtonOkay;
            this.ClientSize = new System.Drawing.Size(339, 79);
            this.Controls.Add(this.ButtonView);
            this.Controls.Add(this.ButtonOkay);
            this.Controls.Add(this.LabelStatus);
            this.Controls.Add(this.LabelEvent);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "AlarmForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Alarm:";
            this.TopMost = true;
            this.Load += new System.EventHandler(this.AlarmForm_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label LabelEvent;
        private System.Windows.Forms.Label LabelStatus;
        private System.Windows.Forms.Button ButtonOkay;
        private System.Windows.Forms.Button ButtonView;
    }
}