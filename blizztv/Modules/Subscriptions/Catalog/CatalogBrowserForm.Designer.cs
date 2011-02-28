namespace BlizzTV.Modules.Subscriptions.Catalog
{
    partial class CatalogBrowserForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CatalogBrowserForm));
            this.Browser = new System.Windows.Forms.WebBrowser();
            this.statusStrip = new System.Windows.Forms.StatusStrip();
            this.LoadingAnimation = new BlizzTV.Controls.Animations.LoadingAnimationToolStripMenuItem();
            this.ProgressBar = new System.Windows.Forms.ToolStripProgressBar();
            this.statusStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // Browser
            // 
            this.Browser.AllowWebBrowserDrop = false;
            this.Browser.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Browser.IsWebBrowserContextMenuEnabled = false;
            this.Browser.Location = new System.Drawing.Point(0, 0);
            this.Browser.MinimumSize = new System.Drawing.Size(20, 20);
            this.Browser.Name = "Browser";
            this.Browser.ScriptErrorsSuppressed = true;
            this.Browser.Size = new System.Drawing.Size(1008, 730);
            this.Browser.TabIndex = 0;
            this.Browser.DocumentCompleted += new System.Windows.Forms.WebBrowserDocumentCompletedEventHandler(this.Browser_DocumentCompleted);
            this.Browser.Navigating += new System.Windows.Forms.WebBrowserNavigatingEventHandler(this.Browser_Navigating);
            this.Browser.ProgressChanged += new System.Windows.Forms.WebBrowserProgressChangedEventHandler(this.Browser_ProgressChanged);
            // 
            // statusStrip
            // 
            this.statusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.LoadingAnimation,
            this.ProgressBar});
            this.statusStrip.Location = new System.Drawing.Point(0, 708);
            this.statusStrip.Name = "statusStrip";
            this.statusStrip.Size = new System.Drawing.Size(1008, 22);
            this.statusStrip.TabIndex = 1;
            this.statusStrip.Text = "statusStrip1";
            // 
            // LoadingAnimation
            // 
            // 
            // LoadingAnimation
            // 
            this.LoadingAnimation.LoadingCircleControl.AccessibleName = "LoadingAnimation";
            this.LoadingAnimation.LoadingCircleControl.Active = false;
            this.LoadingAnimation.LoadingCircleControl.Color = System.Drawing.Color.DarkGray;
            this.LoadingAnimation.LoadingCircleControl.InnerCircleRadius = 6;
            this.LoadingAnimation.LoadingCircleControl.Location = new System.Drawing.Point(1, 2);
            this.LoadingAnimation.LoadingCircleControl.Name = "LoadingAnimation";
            this.LoadingAnimation.LoadingCircleControl.NumberSpoke = 24;
            this.LoadingAnimation.LoadingCircleControl.OuterCircleRadius = 7;
            this.LoadingAnimation.LoadingCircleControl.RotationSpeed = 50;
            this.LoadingAnimation.LoadingCircleControl.Size = new System.Drawing.Size(26, 20);
            this.LoadingAnimation.LoadingCircleControl.SpokeThickness = 3;
            this.LoadingAnimation.LoadingCircleControl.StylePreset = BlizzTV.Controls.Animations.LoadingAnimation.StylePresets.IE7;
            this.LoadingAnimation.LoadingCircleControl.TabIndex = 1;
            this.LoadingAnimation.LoadingCircleControl.Text = "loadingAnimationToolStripMenuItem1";
            this.LoadingAnimation.Name = "LoadingAnimation";
            this.LoadingAnimation.Size = new System.Drawing.Size(26, 20);
            this.LoadingAnimation.Text = "loadingAnimationToolStripMenuItem1";
            // 
            // ProgressBar
            // 
            this.ProgressBar.Name = "ProgressBar";
            this.ProgressBar.Size = new System.Drawing.Size(100, 16);
            // 
            // CatalogBrowserForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1008, 730);
            this.Controls.Add(this.statusStrip);
            this.Controls.Add(this.Browser);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "CatalogBrowserForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Loading Catalog..";
            this.Load += new System.EventHandler(this.frmCatalogBrowser_Load);
            this.statusStrip.ResumeLayout(false);
            this.statusStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.WebBrowser Browser;
        private System.Windows.Forms.StatusStrip statusStrip;
        private Controls.Animations.LoadingAnimationToolStripMenuItem LoadingAnimation;
        private System.Windows.Forms.ToolStripProgressBar ProgressBar;
    }
}