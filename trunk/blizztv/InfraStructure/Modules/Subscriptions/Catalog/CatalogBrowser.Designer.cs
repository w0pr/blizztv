namespace BlizzTV.InfraStructure.Modules.Subscriptions.Catalog
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CatalogBrowser));
            this.toolStrip = new System.Windows.Forms.ToolStrip();
            this.buttonBack = new System.Windows.Forms.ToolStripButton();
            this.buttonForward = new System.Windows.Forms.ToolStripButton();
            this.loadingAnimation = new BlizzTV.Controls.Animations.LoadingAnimationToolStripMenuItem();
            this.imageList = new System.Windows.Forms.ImageList(this.components);
            this.browser = new System.Windows.Forms.WebBrowser();
            this.notificationBar = new BlizzTV.Controls.NotificationBar.NotificationBar();
            this.toolStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStrip
            // 
            this.toolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.buttonBack,
            this.buttonForward,
            this.loadingAnimation});
            this.toolStrip.Location = new System.Drawing.Point(0, 0);
            this.toolStrip.Name = "toolStrip";
            this.toolStrip.Size = new System.Drawing.Size(632, 25);
            this.toolStrip.TabIndex = 0;
            this.toolStrip.Text = "toolStrip1";
            // 
            // buttonBack
            // 
            this.buttonBack.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.buttonBack.Enabled = false;
            this.buttonBack.Image = ((System.Drawing.Image)(resources.GetObject("buttonBack.Image")));
            this.buttonBack.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.buttonBack.Name = "buttonBack";
            this.buttonBack.Size = new System.Drawing.Size(23, 22);
            this.buttonBack.Text = "Back";
            this.buttonBack.Click += new System.EventHandler(this.buttonBack_Click);
            // 
            // buttonForward
            // 
            this.buttonForward.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.buttonForward.Enabled = false;
            this.buttonForward.Image = ((System.Drawing.Image)(resources.GetObject("buttonForward.Image")));
            this.buttonForward.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.buttonForward.Name = "buttonForward";
            this.buttonForward.Size = new System.Drawing.Size(23, 22);
            this.buttonForward.Text = "Forward";
            this.buttonForward.Click += new System.EventHandler(this.buttonForward_Click);
            // 
            // loadingAnimation
            // 
            this.loadingAnimation.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            // 
            // loadingAnimation
            // 
            this.loadingAnimation.LoadingAnimationControl.AccessibleName = "loadingAnimation";
            this.loadingAnimation.LoadingAnimationControl.Active = false;
            this.loadingAnimation.LoadingAnimationControl.Color = System.Drawing.Color.DarkGray;
            this.loadingAnimation.LoadingAnimationControl.InnerCircleRadius = 6;
            this.loadingAnimation.LoadingAnimationControl.Location = new System.Drawing.Point(611, 1);
            this.loadingAnimation.LoadingAnimationControl.Name = "loadingAnimation";
            this.loadingAnimation.LoadingAnimationControl.NumberSpoke = 24;
            this.loadingAnimation.LoadingAnimationControl.OuterCircleRadius = 7;
            this.loadingAnimation.LoadingAnimationControl.RotationSpeed = 100;
            this.loadingAnimation.LoadingAnimationControl.Size = new System.Drawing.Size(20, 22);
            this.loadingAnimation.LoadingAnimationControl.SpokeThickness = 3;
            this.loadingAnimation.LoadingAnimationControl.StylePreset = BlizzTV.Controls.Animations.LoadingAnimation.StylePresets.IE7;
            this.loadingAnimation.LoadingAnimationControl.TabIndex = 1;
            this.loadingAnimation.LoadingAnimationControl.Text = "loadingAnimationToolStripMenuItem1";
            this.loadingAnimation.Name = "loadingAnimation";
            this.loadingAnimation.Size = new System.Drawing.Size(20, 22);
            this.loadingAnimation.Text = "loadingAnimationToolStripMenuItem1";
            // 
            // imageList
            // 
            this.imageList.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList.ImageStream")));
            this.imageList.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList.Images.SetKeyName(0, "information.png");
            // 
            // browser
            // 
            this.browser.Dock = System.Windows.Forms.DockStyle.Fill;
            this.browser.IsWebBrowserContextMenuEnabled = false;
            this.browser.Location = new System.Drawing.Point(0, 25);
            this.browser.MinimumSize = new System.Drawing.Size(20, 20);
            this.browser.Name = "browser";
            this.browser.ScriptErrorsSuppressed = true;
            this.browser.Size = new System.Drawing.Size(632, 282);
            this.browser.TabIndex = 3;
            this.browser.DocumentCompleted += new System.Windows.Forms.WebBrowserDocumentCompletedEventHandler(this.browser_DocumentCompleted);
            this.browser.Navigating += new System.Windows.Forms.WebBrowserNavigatingEventHandler(this.browser_Navigating);
            // 
            // notificationBar
            // 
            this.notificationBar.BackColor = System.Drawing.SystemColors.Info;
            this.notificationBar.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.notificationBar.Location = new System.Drawing.Point(0, 307);
            this.notificationBar.Name = "notificationBar";
            this.notificationBar.Size = new System.Drawing.Size(632, 25);
            this.notificationBar.SmallImageList = this.imageList;
            this.notificationBar.TabIndex = 2;
            this.notificationBar.Text = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Nam non ipsum magna, nec" +
    " pharetra eros.";
            // 
            // CatalogBrowser
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(632, 332);
            this.Controls.Add(this.browser);
            this.Controls.Add(this.notificationBar);
            this.Controls.Add(this.toolStrip);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "CatalogBrowser";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Catalog Browser";
            this.Load += new System.EventHandler(this.Catalog_Load);
            this.toolStrip.ResumeLayout(false);
            this.toolStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStrip;
        private Controls.NotificationBar.NotificationBar notificationBar;
        private System.Windows.Forms.WebBrowser browser;
        private System.Windows.Forms.ImageList imageList;
        private System.Windows.Forms.ToolStripButton buttonBack;
        private System.Windows.Forms.ToolStripButton buttonForward;
        private Controls.Animations.LoadingAnimationToolStripMenuItem loadingAnimation;
    }
}