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
            this.StageList = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.SuspendLayout();
            // 
            // StageList
            // 
            this.StageList.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1});
            this.StageList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.StageList.Location = new System.Drawing.Point(0, 0);
            this.StageList.Name = "StageList";
            this.StageList.Size = new System.Drawing.Size(206, 290);
            this.StageList.TabIndex = 0;
            this.StageList.UseCompatibleStateImageBehavior = false;
            this.StageList.View = System.Windows.Forms.View.Details;
            this.StageList.DoubleClick += new System.EventHandler(this.StageList_DoubleClick);
            // 
            // columnHeader1
            // 
            this.columnHeader1.Width = 196;
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(206, 290);
            this.Controls.Add(this.StageList);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
            this.Name = "frmMain";
            this.Text = "BlizzTV";
            this.Load += new System.EventHandler(this.frmMain_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListView StageList;
        private System.Windows.Forms.ColumnHeader columnHeader1;
    }
}

