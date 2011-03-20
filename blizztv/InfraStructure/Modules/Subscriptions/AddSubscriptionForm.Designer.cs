namespace BlizzTV.InfraStructure.Modules.Subscriptions
{
    partial class AddSubscriptionForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AddSubscriptionForm));
            this.labelIntroduction = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.panelHost = new System.Windows.Forms.Panel();
            this.buttonAdd = new System.Windows.Forms.Button();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.loadingAnimation1 = new BlizzTV.Controls.Animations.LoadingAnimation();
            this.labelStatus = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // labelIntroduction
            // 
            this.labelIntroduction.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.labelIntroduction.Location = new System.Drawing.Point(66, 9);
            this.labelIntroduction.Name = "labelIntroduction";
            this.labelIntroduction.Size = new System.Drawing.Size(412, 52);
            this.labelIntroduction.TabIndex = 0;
            this.labelIntroduction.Text = resources.GetString("labelIntroduction.Text");
            this.labelIntroduction.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(12, 12);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(48, 48);
            this.pictureBox1.TabIndex = 1;
            this.pictureBox1.TabStop = false;
            // 
            // panelHost
            // 
            this.panelHost.Location = new System.Drawing.Point(12, 77);
            this.panelHost.Name = "panelHost";
            this.panelHost.Size = new System.Drawing.Size(468, 126);
            this.panelHost.TabIndex = 2;
            // 
            // buttonAdd
            // 
            this.buttonAdd.Location = new System.Drawing.Point(322, 218);
            this.buttonAdd.Name = "buttonAdd";
            this.buttonAdd.Size = new System.Drawing.Size(75, 23);
            this.buttonAdd.TabIndex = 3;
            this.buttonAdd.Text = "Add";
            this.buttonAdd.UseVisualStyleBackColor = true;
            // 
            // buttonCancel
            // 
            this.buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonCancel.Location = new System.Drawing.Point(403, 218);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(75, 23);
            this.buttonCancel.TabIndex = 4;
            this.buttonCancel.Text = "Cancel";
            this.buttonCancel.UseVisualStyleBackColor = true;
            // 
            // loadingAnimation1
            // 
            this.loadingAnimation1.Active = false;
            this.loadingAnimation1.Color = System.Drawing.Color.DarkGray;
            this.loadingAnimation1.InnerCircleRadius = 6;
            this.loadingAnimation1.Location = new System.Drawing.Point(12, 218);
            this.loadingAnimation1.Name = "loadingAnimation1";
            this.loadingAnimation1.NumberSpoke = 24;
            this.loadingAnimation1.OuterCircleRadius = 7;
            this.loadingAnimation1.RotationSpeed = 100;
            this.loadingAnimation1.Size = new System.Drawing.Size(23, 23);
            this.loadingAnimation1.SpokeThickness = 3;
            this.loadingAnimation1.StylePreset = BlizzTV.Controls.Animations.LoadingAnimation.StylePresets.IE7;
            this.loadingAnimation1.TabIndex = 5;
            this.loadingAnimation1.Text = "loadingAnimation1";
            // 
            // labelStatus
            // 
            this.labelStatus.Location = new System.Drawing.Point(35, 218);
            this.labelStatus.Name = "labelStatus";
            this.labelStatus.Size = new System.Drawing.Size(173, 23);
            this.labelStatus.TabIndex = 6;
            this.labelStatus.Text = "Parsing resource.";
            this.labelStatus.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // AddSubscriptionForm
            // 
            this.AcceptButton = this.buttonAdd;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.buttonCancel;
            this.ClientSize = new System.Drawing.Size(490, 252);
            this.Controls.Add(this.labelStatus);
            this.Controls.Add(this.loadingAnimation1);
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(this.buttonAdd);
            this.Controls.Add(this.labelIntroduction);
            this.Controls.Add(this.panelHost);
            this.Controls.Add(this.pictureBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "AddSubscriptionForm";
            this.Text = "AddSubscriptionForm";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label labelIntroduction;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Panel panelHost;
        private System.Windows.Forms.Button buttonAdd;
        private System.Windows.Forms.Button buttonCancel;
        private Controls.Animations.LoadingAnimation loadingAnimation1;
        private System.Windows.Forms.Label labelStatus;
    }
}