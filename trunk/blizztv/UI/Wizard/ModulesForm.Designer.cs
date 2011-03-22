namespace BlizzTV.UI.Wizard
{
    partial class ModulesForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ModulesForm));
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.label1 = new System.Windows.Forms.Label();
            this.listviewModules = new System.Windows.Forms.ListView();
            this.ColEnable = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.ColName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.ColDescription = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.imageList = new System.Windows.Forms.ImageList(this.components);
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
            this.label1.Size = new System.Drawing.Size(401, 50);
            this.label1.TabIndex = 9;
            this.label1.Text = resources.GetString("label1.Text");
            // 
            // listviewModules
            // 
            this.listviewModules.CheckBoxes = true;
            this.listviewModules.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.ColEnable,
            this.ColName,
            this.ColDescription});
            this.listviewModules.FullRowSelect = true;
            this.listviewModules.Location = new System.Drawing.Point(1, 53);
            this.listviewModules.Name = "listviewModules";
            this.listviewModules.ShowGroups = false;
            this.listviewModules.Size = new System.Drawing.Size(449, 196);
            this.listviewModules.SmallImageList = this.imageList;
            this.listviewModules.TabIndex = 11;
            this.listviewModules.UseCompatibleStateImageBehavior = false;
            this.listviewModules.View = System.Windows.Forms.View.Details;
            // 
            // ColEnable
            // 
            this.ColEnable.Text = "Enable";
            this.ColEnable.Width = 56;
            // 
            // ColName
            // 
            this.ColName.Text = "Name";
            this.ColName.Width = 96;
            // 
            // ColDescription
            // 
            this.ColDescription.Text = "Description";
            this.ColDescription.Width = 292;
            // 
            // imageList
            // 
            this.imageList.ColorDepth = System.Windows.Forms.ColorDepth.Depth8Bit;
            this.imageList.ImageSize = new System.Drawing.Size(16, 16);
            this.imageList.TransparentColor = System.Drawing.Color.Transparent;
            // 
            // ModulesForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(450, 250);
            this.Controls.Add(this.listviewModules);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "ModulesForm";
            this.Text = "Modules";
            this.Load += new System.EventHandler(this.frmWizardPlugins_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ListView listviewModules;
        private System.Windows.Forms.ColumnHeader ColEnable;
        private System.Windows.Forms.ColumnHeader ColName;
        private System.Windows.Forms.ColumnHeader ColDescription;
        private System.Windows.Forms.ImageList imageList;
    }
}