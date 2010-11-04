namespace BlizzTV
{
    partial class frmMain
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMain));
            this.List = new System.Windows.Forms.ListView();
            this.game = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.name = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.text = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.GameIcons = new System.Windows.Forms.ImageList(this.components);
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.blizzTVToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.ListIcons = new System.Windows.Forms.ImageList(this.components);
            this.icon = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // List
            // 
            this.List.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.game,
            this.icon,
            this.name,
            this.text});
            this.List.Dock = System.Windows.Forms.DockStyle.Fill;
            this.List.FullRowSelect = true;
            this.List.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.None;
            this.List.Location = new System.Drawing.Point(0, 24);
            this.List.Name = "List";
            this.List.OwnerDraw = true;
            this.List.Size = new System.Drawing.Size(206, 266);
            this.List.TabIndex = 0;
            this.List.UseCompatibleStateImageBehavior = false;
            this.List.View = System.Windows.Forms.View.Details;
            this.List.DrawSubItem += new System.Windows.Forms.DrawListViewSubItemEventHandler(this.List_DrawSubItem);
            this.List.DoubleClick += new System.EventHandler(this.List_DoubleClick);
            // 
            // game
            // 
            this.game.Text = "Game";
            this.game.Width = 16;
            // 
            // name
            // 
            this.name.Text = "Name";
            this.name.Width = 54;
            // 
            // text
            // 
            this.text.Text = "Text";
            this.text.Width = 115;
            // 
            // GameIcons
            // 
            this.GameIcons.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("GameIcons.ImageStream")));
            this.GameIcons.TransparentColor = System.Drawing.Color.Transparent;
            this.GameIcons.Images.SetKeyName(0, "sc2_16.png");
            this.GameIcons.Images.SetKeyName(1, "wow_16.png");
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.blizzTVToolStripMenuItem,
            this.aboutToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(206, 24);
            this.menuStrip1.TabIndex = 1;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // blizzTVToolStripMenuItem
            // 
            this.blizzTVToolStripMenuItem.Name = "blizzTVToolStripMenuItem";
            this.blizzTVToolStripMenuItem.Size = new System.Drawing.Size(56, 20);
            this.blizzTVToolStripMenuItem.Text = "BlizzTV";
            // 
            // aboutToolStripMenuItem
            // 
            this.aboutToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.aboutToolStripMenuItem1});
            this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            this.aboutToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
            this.aboutToolStripMenuItem.Text = "Help";
            // 
            // aboutToolStripMenuItem1
            // 
            this.aboutToolStripMenuItem1.Name = "aboutToolStripMenuItem1";
            this.aboutToolStripMenuItem1.Size = new System.Drawing.Size(107, 22);
            this.aboutToolStripMenuItem1.Text = "About";
            this.aboutToolStripMenuItem1.Click += new System.EventHandler(this.aboutToolStripMenuItem1_Click);
            // 
            // ListIcons
            // 
            this.ListIcons.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("ListIcons.ImageStream")));
            this.ListIcons.TransparentColor = System.Drawing.Color.Transparent;
            this.ListIcons.Images.SetKeyName(0, "satellite_16.png");
            // 
            // icon
            // 
            this.icon.Text = "Icon";
            this.icon.Width = 17;
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(206, 290);
            this.Controls.Add(this.List);
            this.Controls.Add(this.menuStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.MaximizeBox = false;
            this.Name = "frmMain";
            this.Text = "BlizzTV";
            this.Load += new System.EventHandler(this.frmMain_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListView List;
        private System.Windows.Forms.ColumnHeader game;
        private System.Windows.Forms.ColumnHeader name;
        private System.Windows.Forms.ColumnHeader text;
        private System.Windows.Forms.ImageList GameIcons;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem blizzTVToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem1;
        private System.Windows.Forms.ImageList ListIcons;
        private System.Windows.Forms.ColumnHeader icon;
    }
}

