namespace BlizzTV.Modules.Subscriptions.Catalog
{
    partial class CatalogBrowser
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CatalogBrowser));
            this.toolStrip = new System.Windows.Forms.ToolStrip();
            this.browserBackButton = new System.Windows.Forms.ToolStripButton();
            this.browserForwardButton = new System.Windows.Forms.ToolStripButton();
            this.toolStripLabel1 = new System.Windows.Forms.ToolStripLabel();
            this.comboBoxCatalog = new System.Windows.Forms.ToolStripComboBox();
            this.loadingAnimation = new BlizzTV.Controls.Animations.LoadingAnimationToolStripMenuItem();
            this.browser = new System.Windows.Forms.WebBrowser();
            this.SuspendLayout();
            // 
            // toolStrip
            // 
            this.toolStrip.Location = new System.Drawing.Point(0, 0);
            this.toolStrip.Name = "toolStrip";
            this.toolStrip.Size = new System.Drawing.Size(1038, 25);
            this.toolStrip.TabIndex = 0;
            // 
            // browserBackButton
            // 
            this.browserBackButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.browserBackButton.Image = ((System.Drawing.Image)(resources.GetObject("browserBackButton.Image")));
            this.browserBackButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.browserBackButton.Name = "browserBackButton";
            this.browserBackButton.Size = new System.Drawing.Size(23, 22);
            this.browserBackButton.Text = "toolStripButton1";
            // 
            // browserForwardButton
            // 
            this.browserForwardButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.browserForwardButton.Image = ((System.Drawing.Image)(resources.GetObject("browserForwardButton.Image")));
            this.browserForwardButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.browserForwardButton.Name = "browserForwardButton";
            this.browserForwardButton.Size = new System.Drawing.Size(23, 22);
            this.browserForwardButton.Text = "toolStripButton2";
            // 
            // toolStripLabel1
            // 
            this.toolStripLabel1.Name = "toolStripLabel1";
            this.toolStripLabel1.Size = new System.Drawing.Size(51, 22);
            this.toolStripLabel1.Text = "Catalog:";
            // 
            // comboBoxCatalog
            // 
            this.comboBoxCatalog.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxCatalog.Items.AddRange(new object[] {
            "Feeds",
            "Podcasts",
            "Streams",
            "Video Channels"});
            this.comboBoxCatalog.Name = "comboBoxCatalog";
            this.comboBoxCatalog.Size = new System.Drawing.Size(121, 23);
            // 
            // loadingAnimation
            // 
            this.loadingAnimation.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            // 
            // loadingAnimation
            // 
            this.loadingAnimation.LoadingCircleControl.AccessibleName = "loadingAnimation";
            this.loadingAnimation.LoadingCircleControl.Active = false;
            this.loadingAnimation.LoadingCircleControl.Color = System.Drawing.Color.DarkGray;
            this.loadingAnimation.LoadingCircleControl.InnerCircleRadius = 6;
            this.loadingAnimation.LoadingCircleControl.Location = new System.Drawing.Point(0, 0);
            this.loadingAnimation.LoadingCircleControl.Name = "loadingAnimation";
            this.loadingAnimation.LoadingCircleControl.NumberSpoke = 24;
            this.loadingAnimation.LoadingCircleControl.OuterCircleRadius = 7;
            this.loadingAnimation.LoadingCircleControl.RotationSpeed = 50;
            this.loadingAnimation.LoadingCircleControl.Size = new System.Drawing.Size(20, 22);
            this.loadingAnimation.LoadingCircleControl.SpokeThickness = 3;
            this.loadingAnimation.LoadingCircleControl.StylePreset = BlizzTV.Controls.Animations.LoadingAnimation.StylePresets.IE7;
            this.loadingAnimation.LoadingCircleControl.TabIndex = 2;
            this.loadingAnimation.LoadingCircleControl.Text = "loadingAnimationToolStripMenuItem1";
            this.loadingAnimation.Name = "loadingAnimation";
            this.loadingAnimation.Size = new System.Drawing.Size(20, 22);
            this.loadingAnimation.Text = "loadingAnimationToolStripMenuItem1";
            // 
            // browser
            // 
            this.browser.Dock = System.Windows.Forms.DockStyle.Fill;
            this.browser.IsWebBrowserContextMenuEnabled = false;
            this.browser.Location = new System.Drawing.Point(0, 25);
            this.browser.MinimumSize = new System.Drawing.Size(20, 20);
            this.browser.Name = "browser";
            this.browser.ScriptErrorsSuppressed = true;
            this.browser.Size = new System.Drawing.Size(1038, 557);
            this.browser.TabIndex = 1;
            this.browser.DocumentCompleted += new System.Windows.Forms.WebBrowserDocumentCompletedEventHandler(this.WebBrowser_DocumentCompleted);
            this.browser.Navigating += new System.Windows.Forms.WebBrowserNavigatingEventHandler(this.WebBrowser_Navigating);
            // 
            // CatalogBrowser
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1038, 582);
            this.Controls.Add(this.browser);
            this.Controls.Add(this.toolStrip);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "CatalogBrowser";
            this.Text = "Catalog Browser";
            this.Load += new System.EventHandler(this.Catalog_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStrip;
        private System.Windows.Forms.ToolStripButton browserBackButton;
        private System.Windows.Forms.ToolStripButton browserForwardButton;
        private System.Windows.Forms.ToolStripLabel toolStripLabel1;
        private System.Windows.Forms.ToolStripComboBox comboBoxCatalog;
        private Controls.Animations.LoadingAnimationToolStripMenuItem loadingAnimation;
        private System.Windows.Forms.WebBrowser browser;
    }
}