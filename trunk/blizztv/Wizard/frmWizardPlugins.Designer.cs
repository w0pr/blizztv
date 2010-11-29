namespace BlizzTV.Wizard
{
    partial class frmWizardPlugins
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmWizardPlugins));
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.label1 = new System.Windows.Forms.Label();
            this.CheckboxFeeds = new System.Windows.Forms.CheckBox();
            this.CheckboxStreams = new System.Windows.Forms.CheckBox();
            this.CheckboxVideos = new System.Windows.Forms.CheckBox();
            this.CheckboxEvents = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(1, 0);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(32, 32);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 10;
            this.pictureBox1.TabStop = false;
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(49, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(401, 32);
            this.label1.TabIndex = 9;
            this.label1.Text = "BlizzTV comes with a bunch of plugins. Please select the plugins you want to enab" +
                "le and use.";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // CheckboxFeeds
            // 
            this.CheckboxFeeds.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.CheckboxFeeds.Image = ((System.Drawing.Image)(resources.GetObject("CheckboxFeeds.Image")));
            this.CheckboxFeeds.ImageAlign = System.Drawing.ContentAlignment.TopLeft;
            this.CheckboxFeeds.Location = new System.Drawing.Point(52, 45);
            this.CheckboxFeeds.Name = "CheckboxFeeds";
            this.CheckboxFeeds.Size = new System.Drawing.Size(386, 19);
            this.CheckboxFeeds.TabIndex = 12;
            this.CheckboxFeeds.Text = "Feeds (Aggregates feeds from your favorite sources)";
            this.CheckboxFeeds.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
            this.CheckboxFeeds.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.CheckboxFeeds.UseVisualStyleBackColor = true;
            // 
            // CheckboxStreams
            // 
            this.CheckboxStreams.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.CheckboxStreams.Image = ((System.Drawing.Image)(resources.GetObject("CheckboxStreams.Image")));
            this.CheckboxStreams.ImageAlign = System.Drawing.ContentAlignment.TopLeft;
            this.CheckboxStreams.Location = new System.Drawing.Point(52, 70);
            this.CheckboxStreams.Name = "CheckboxStreams";
            this.CheckboxStreams.Size = new System.Drawing.Size(386, 19);
            this.CheckboxStreams.TabIndex = 13;
            this.CheckboxStreams.Text = "Streams (Let\'s you view your favorite streams.)";
            this.CheckboxStreams.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
            this.CheckboxStreams.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.CheckboxStreams.UseVisualStyleBackColor = true;
            // 
            // CheckboxVideos
            // 
            this.CheckboxVideos.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.CheckboxVideos.Image = ((System.Drawing.Image)(resources.GetObject("CheckboxVideos.Image")));
            this.CheckboxVideos.ImageAlign = System.Drawing.ContentAlignment.TopLeft;
            this.CheckboxVideos.Location = new System.Drawing.Point(52, 95);
            this.CheckboxVideos.Name = "CheckboxVideos";
            this.CheckboxVideos.Size = new System.Drawing.Size(386, 19);
            this.CheckboxVideos.TabIndex = 14;
            this.CheckboxVideos.Text = "Videos (Let\'s you view latest videos from your favorite channels)";
            this.CheckboxVideos.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
            this.CheckboxVideos.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.CheckboxVideos.UseVisualStyleBackColor = true;
            // 
            // CheckboxEvents
            // 
            this.CheckboxEvents.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.CheckboxEvents.Image = ((System.Drawing.Image)(resources.GetObject("CheckboxEvents.Image")));
            this.CheckboxEvents.ImageAlign = System.Drawing.ContentAlignment.TopLeft;
            this.CheckboxEvents.Location = new System.Drawing.Point(52, 120);
            this.CheckboxEvents.Name = "CheckboxEvents";
            this.CheckboxEvents.Size = new System.Drawing.Size(386, 19);
            this.CheckboxEvents.TabIndex = 15;
            this.CheckboxEvents.Text = "Events (Notifies you about related events\r\n)";
            this.CheckboxEvents.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
            this.CheckboxEvents.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.CheckboxEvents.UseVisualStyleBackColor = true;
            // 
            // frmWizardPlugins
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(450, 250);
            this.Controls.Add(this.CheckboxEvents);
            this.Controls.Add(this.CheckboxVideos);
            this.Controls.Add(this.CheckboxStreams);
            this.Controls.Add(this.CheckboxFeeds);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "frmWizardPlugins";
            this.Text = "Select Plugins";
            this.Load += new System.EventHandler(this.frmWizardPlugins_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.CheckBox CheckboxFeeds;
        private System.Windows.Forms.CheckBox CheckboxStreams;
        private System.Windows.Forms.CheckBox CheckboxVideos;
        private System.Windows.Forms.CheckBox CheckboxEvents;
    }
}