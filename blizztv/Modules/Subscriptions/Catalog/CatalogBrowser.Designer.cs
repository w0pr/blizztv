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
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.browser = new System.Windows.Forms.WebBrowser();
            this.buttonBack = new System.Windows.Forms.ToolStripButton();
            this.buttonForward = new System.Windows.Forms.ToolStripButton();
            this.toolStripLabel1 = new System.Windows.Forms.ToolStripLabel();
            this.comboCatalog = new System.Windows.Forms.ToolStripComboBox();
            this.loadingAnimation = new BlizzTV.Controls.Animations.LoadingAnimationToolStripMenuItem();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.buttonBack,
            this.buttonForward,
            this.toolStripLabel1,
            this.comboCatalog,
            this.loadingAnimation});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(1035, 25);
            this.toolStrip1.TabIndex = 0;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // browser
            // 
            this.browser.Dock = System.Windows.Forms.DockStyle.Fill;
            this.browser.IsWebBrowserContextMenuEnabled = false;
            this.browser.Location = new System.Drawing.Point(0, 25);
            this.browser.MinimumSize = new System.Drawing.Size(20, 20);
            this.browser.Name = "browser";
            this.browser.ScriptErrorsSuppressed = true;
            this.browser.Size = new System.Drawing.Size(1035, 501);
            this.browser.TabIndex = 1;
            this.browser.DocumentCompleted += new System.Windows.Forms.WebBrowserDocumentCompletedEventHandler(this.browser_DocumentCompleted);
            this.browser.Navigating += new System.Windows.Forms.WebBrowserNavigatingEventHandler(this.browser_Navigating);
            // 
            // buttonBack
            // 
            this.buttonBack.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.buttonBack.Enabled = false;
            this.buttonBack.Image = ((System.Drawing.Image)(resources.GetObject("buttonBack.Image")));
            this.buttonBack.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.buttonBack.Name = "buttonBack";
            this.buttonBack.Size = new System.Drawing.Size(23, 22);
            this.buttonBack.Text = "toolStripButton1";
            // 
            // buttonForward
            // 
            this.buttonForward.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.buttonForward.Enabled = false;
            this.buttonForward.Image = ((System.Drawing.Image)(resources.GetObject("buttonForward.Image")));
            this.buttonForward.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.buttonForward.Name = "buttonForward";
            this.buttonForward.Size = new System.Drawing.Size(23, 22);
            this.buttonForward.Text = "toolStripButton2";
            // 
            // toolStripLabel1
            // 
            this.toolStripLabel1.Name = "toolStripLabel1";
            this.toolStripLabel1.Size = new System.Drawing.Size(51, 22);
            this.toolStripLabel1.Text = "Catalog:";
            // 
            // comboCatalog
            // 
            this.comboCatalog.Items.AddRange(new object[] {
            "Feeds",
            "Podcasts",
            "Streams",
            "Video Channels"});
            this.comboCatalog.Name = "comboCatalog";
            this.comboCatalog.Size = new System.Drawing.Size(121, 25);
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
            this.loadingAnimation.LoadingCircleControl.Location = new System.Drawing.Point(1006, 1);
            this.loadingAnimation.LoadingCircleControl.Name = "loadingAnimation";
            this.loadingAnimation.LoadingCircleControl.NumberSpoke = 24;
            this.loadingAnimation.LoadingCircleControl.OuterCircleRadius = 7;
            this.loadingAnimation.LoadingCircleControl.RotationSpeed = 50;
            this.loadingAnimation.LoadingCircleControl.Size = new System.Drawing.Size(28, 22);
            this.loadingAnimation.LoadingCircleControl.SpokeThickness = 3;
            this.loadingAnimation.LoadingCircleControl.StylePreset = BlizzTV.Controls.Animations.LoadingAnimation.StylePresets.IE7;
            this.loadingAnimation.LoadingCircleControl.TabIndex = 2;
            this.loadingAnimation.LoadingCircleControl.Text = "loadingAnimationToolStripMenuItem1";
            this.loadingAnimation.Name = "loadingAnimation";
            this.loadingAnimation.Size = new System.Drawing.Size(28, 22);
            this.loadingAnimation.Text = "loadingAnimationToolStripMenuItem1";
            this.loadingAnimation.Visible = false;
            // 
            // Catalog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1035, 526);
            this.Controls.Add(this.browser);
            this.Controls.Add(this.toolStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Catalog";
            this.Text = "Catalog Browser";
            this.Load += new System.EventHandler(this.Catalog_Load);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.WebBrowser browser;
        private System.Windows.Forms.ToolStripButton buttonBack;
        private System.Windows.Forms.ToolStripButton buttonForward;
        private System.Windows.Forms.ToolStripLabel toolStripLabel1;
        private System.Windows.Forms.ToolStripComboBox comboCatalog;
        private Controls.Animations.LoadingAnimationToolStripMenuItem loadingAnimation;
    }
}