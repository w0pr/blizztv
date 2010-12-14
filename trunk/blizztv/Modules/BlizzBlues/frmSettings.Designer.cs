﻿namespace BlizzTV.Modules.BlizzBlues
{
    partial class frmSettings
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmSettings));
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.label1 = new System.Windows.Forms.Label();
            this.numericUpDownUpdateBlizzBluesEveryXMinutes = new System.Windows.Forms.NumericUpDown();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.checkBoxTrackWorldofWarcraft = new System.Windows.Forms.CheckBox();
            this.checkBoxTrackStarcraft = new System.Windows.Forms.CheckBox();
            this.tabPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownUpdateBlizzBluesEveryXMinutes)).BeginInit();
            this.tabControl1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.groupBox2);
            this.tabPage1.Controls.Add(this.groupBox1);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(439, 236);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Options";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(173, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Update BlizzBlues every X minutes:";
            // 
            // numericUpDownUpdateBlizzBluesEveryXMinutes
            // 
            this.numericUpDownUpdateBlizzBluesEveryXMinutes.Location = new System.Drawing.Point(185, 14);
            this.numericUpDownUpdateBlizzBluesEveryXMinutes.Maximum = new decimal(new int[] {
            1440,
            0,
            0,
            0});
            this.numericUpDownUpdateBlizzBluesEveryXMinutes.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDownUpdateBlizzBluesEveryXMinutes.Name = "numericUpDownUpdateBlizzBluesEveryXMinutes";
            this.numericUpDownUpdateBlizzBluesEveryXMinutes.Size = new System.Drawing.Size(54, 20);
            this.numericUpDownUpdateBlizzBluesEveryXMinutes.TabIndex = 1;
            this.numericUpDownUpdateBlizzBluesEveryXMinutes.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(447, 262);
            this.tabControl1.TabIndex = 1;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.checkBoxTrackStarcraft);
            this.groupBox1.Controls.Add(this.checkBoxTrackWorldofWarcraft);
            this.groupBox1.Location = new System.Drawing.Point(8, 7);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(423, 65);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Games to track";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Controls.Add(this.numericUpDownUpdateBlizzBluesEveryXMinutes);
            this.groupBox2.Location = new System.Drawing.Point(8, 78);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(423, 45);
            this.groupBox2.TabIndex = 3;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Updates";
            // 
            // checkBoxTrackWorldofWarcraft
            // 
            this.checkBoxTrackWorldofWarcraft.AutoSize = true;
            this.checkBoxTrackWorldofWarcraft.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.checkBoxTrackWorldofWarcraft.Image = ((System.Drawing.Image)(resources.GetObject("checkBoxTrackWorldofWarcraft.Image")));
            this.checkBoxTrackWorldofWarcraft.Location = new System.Drawing.Point(9, 19);
            this.checkBoxTrackWorldofWarcraft.Name = "checkBoxTrackWorldofWarcraft";
            this.checkBoxTrackWorldofWarcraft.Size = new System.Drawing.Size(123, 17);
            this.checkBoxTrackWorldofWarcraft.TabIndex = 0;
            this.checkBoxTrackWorldofWarcraft.Text = "World of Warcraft";
            this.checkBoxTrackWorldofWarcraft.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.checkBoxTrackWorldofWarcraft.UseVisualStyleBackColor = true;
            // 
            // checkBoxTrackStarcraft
            // 
            this.checkBoxTrackStarcraft.AutoSize = true;
            this.checkBoxTrackStarcraft.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.checkBoxTrackStarcraft.Image = ((System.Drawing.Image)(resources.GetObject("checkBoxTrackStarcraft.Image")));
            this.checkBoxTrackStarcraft.Location = new System.Drawing.Point(9, 42);
            this.checkBoxTrackStarcraft.Name = "checkBoxTrackStarcraft";
            this.checkBoxTrackStarcraft.Size = new System.Drawing.Size(79, 17);
            this.checkBoxTrackStarcraft.TabIndex = 1;
            this.checkBoxTrackStarcraft.Text = "Starcraft";
            this.checkBoxTrackStarcraft.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.checkBoxTrackStarcraft.UseVisualStyleBackColor = true;
            // 
            // frmSettings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(447, 262);
            this.Controls.Add(this.tabControl1);
            this.Name = "frmSettings";
            this.Text = "frmSettings";
            this.Load += new System.EventHandler(this.frmSettings_Load);
            this.tabPage1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownUpdateBlizzBluesEveryXMinutes)).EndInit();
            this.tabControl1.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.NumericUpDown numericUpDownUpdateBlizzBluesEveryXMinutes;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.CheckBox checkBoxTrackStarcraft;
        private System.Windows.Forms.CheckBox checkBoxTrackWorldofWarcraft;
    }
}