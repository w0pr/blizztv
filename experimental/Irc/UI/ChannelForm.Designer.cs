namespace BlizzTV.EmbeddedModules.Irc.UI
{
    partial class ChannelForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ChannelForm));
            this.channelText = new System.Windows.Forms.RichTextBox();
            this.inputBox = new System.Windows.Forms.TextBox();
            this.splitContainer = new System.Windows.Forms.SplitContainer();
            this.usersList = new System.Windows.Forms.ListBox();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer)).BeginInit();
            this.splitContainer.Panel1.SuspendLayout();
            this.splitContainer.Panel2.SuspendLayout();
            this.splitContainer.SuspendLayout();
            this.SuspendLayout();
            // 
            // channelText
            // 
            this.channelText.BackColor = System.Drawing.Color.White;
            this.channelText.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.channelText.Dock = System.Windows.Forms.DockStyle.Fill;
            this.channelText.Font = new System.Drawing.Font("Lucida Console", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.channelText.Location = new System.Drawing.Point(0, 0);
            this.channelText.Name = "channelText";
            this.channelText.ReadOnly = true;
            this.channelText.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.Vertical;
            this.channelText.Size = new System.Drawing.Size(493, 239);
            this.channelText.TabIndex = 1;
            this.channelText.TabStop = false;
            this.channelText.Text = "";
            // 
            // inputBox
            // 
            this.inputBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.inputBox.Font = new System.Drawing.Font("Lucida Console", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.inputBox.Location = new System.Drawing.Point(0, 241);
            this.inputBox.Name = "inputBox";
            this.inputBox.Size = new System.Drawing.Size(624, 20);
            this.inputBox.TabIndex = 2;
            // 
            // splitContainer
            // 
            this.splitContainer.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.splitContainer.Location = new System.Drawing.Point(0, 0);
            this.splitContainer.Name = "splitContainer";
            // 
            // splitContainer.Panel1
            // 
            this.splitContainer.Panel1.Controls.Add(this.channelText);
            // 
            // splitContainer.Panel2
            // 
            this.splitContainer.Panel2.Controls.Add(this.usersList);
            this.splitContainer.Size = new System.Drawing.Size(624, 239);
            this.splitContainer.SplitterDistance = 493;
            this.splitContainer.TabIndex = 3;
            // 
            // usersList
            // 
            this.usersList.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.usersList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.usersList.FormattingEnabled = true;
            this.usersList.Location = new System.Drawing.Point(0, 0);
            this.usersList.Name = "usersList";
            this.usersList.Size = new System.Drawing.Size(127, 239);
            this.usersList.TabIndex = 0;
            // 
            // ChannelForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(624, 262);
            this.Controls.Add(this.splitContainer);
            this.Controls.Add(this.inputBox);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "ChannelForm";
            this.Text = "ChannelForm";
            this.Load += new System.EventHandler(this.ChannelForm_Load);
            this.splitContainer.Panel1.ResumeLayout(false);
            this.splitContainer.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer)).EndInit();
            this.splitContainer.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.RichTextBox channelText;
        private System.Windows.Forms.TextBox inputBox;
        private System.Windows.Forms.SplitContainer splitContainer;
        private System.Windows.Forms.ListBox usersList;
    }
}