namespace WindowsFormsApp2
{
    partial class Form1
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
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel2 = new System.Windows.Forms.ToolStripStatusLabel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.uploadPDFToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.processDocuments = new System.Windows.Forms.ToolStripMenuItem();
            this.templateToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.newToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.resetMarkingsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.databaseToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.updateEmployeeRegisterToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.clearTemplatesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.statusStrip1.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel1,
            this.toolStripStatusLabel2});
            this.statusStrip1.Location = new System.Drawing.Point(0, 680);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(1319, 22);
            this.statusStrip1.TabIndex = 2;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(118, 17);
            this.toolStripStatusLabel1.Text = "toolStripStatusLabel1";
            // 
            // toolStripStatusLabel2
            // 
            this.toolStripStatusLabel2.Name = "toolStripStatusLabel2";
            this.toolStripStatusLabel2.Size = new System.Drawing.Size(118, 17);
            this.toolStripStatusLabel2.Text = "toolStripStatusLabel2";
            // 
            // panel2
            // 
            this.panel2.AutoScroll = true;
            this.panel2.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel2.Controls.Add(this.pictureBox1);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1319, 680);
            this.panel2.TabIndex = 3;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Location = new System.Drawing.Point(6, 27);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(277, 265);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.PictureBox1_MouseDown);
            this.pictureBox1.MouseMove += new System.Windows.Forms.MouseEventHandler(this.PictureBox1_MouseMove);
            this.pictureBox1.MouseUp += new System.Windows.Forms.MouseEventHandler(this.PictureBox1_MouseUp);
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.templateToolStripMenuItem1,
            this.databaseToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.menuStrip1.Size = new System.Drawing.Size(1319, 24);
            this.menuStrip1.TabIndex = 4;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.uploadPDFToolStripMenuItem,
            this.processDocuments});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // uploadPDFToolStripMenuItem
            // 
            this.uploadPDFToolStripMenuItem.Name = "uploadPDFToolStripMenuItem";
            this.uploadPDFToolStripMenuItem.Size = new System.Drawing.Size(178, 22);
            this.uploadPDFToolStripMenuItem.Text = "Upload PDF";
            this.uploadPDFToolStripMenuItem.Click += new System.EventHandler(this.UploadPDFToolStripMenuItem_Click);
            // 
            // processDocuments
            // 
            this.processDocuments.Name = "processDocuments";
            this.processDocuments.Size = new System.Drawing.Size(178, 22);
            this.processDocuments.Text = "Process Documents";
            this.processDocuments.Click += new System.EventHandler(this.processDocumentsToolStripMenuItem_Click);
            // 
            // templateToolStripMenuItem1
            // 
            this.templateToolStripMenuItem1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.newToolStripMenuItem,
            this.saveToolStripMenuItem,
            this.resetMarkingsToolStripMenuItem});
            this.templateToolStripMenuItem1.Name = "templateToolStripMenuItem1";
            this.templateToolStripMenuItem1.Size = new System.Drawing.Size(68, 20);
            this.templateToolStripMenuItem1.Text = "Template";
            // 
            // newToolStripMenuItem
            // 
            this.newToolStripMenuItem.Name = "newToolStripMenuItem";
            this.newToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.newToolStripMenuItem.Text = "New";
            this.newToolStripMenuItem.Click += new System.EventHandler(this.templateNew_Click);
            // 
            // saveToolStripMenuItem
            // 
            this.saveToolStripMenuItem.Name = "saveToolStripMenuItem";
            this.saveToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.saveToolStripMenuItem.Text = "Save";
            this.saveToolStripMenuItem.Click += new System.EventHandler(this.templateSave_Click);
            // 
            // resetMarkingsToolStripMenuItem
            // 
            this.resetMarkingsToolStripMenuItem.Name = "resetMarkingsToolStripMenuItem";
            this.resetMarkingsToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.resetMarkingsToolStripMenuItem.Text = "Reset Markings";
            this.resetMarkingsToolStripMenuItem.Click += new System.EventHandler(this.ResetIdentificationToolStripMenuItem1_Click);
            // 
            // databaseToolStripMenuItem
            // 
            this.databaseToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.updateEmployeeRegisterToolStripMenuItem,
            this.clearTemplatesToolStripMenuItem});
            this.databaseToolStripMenuItem.Name = "databaseToolStripMenuItem";
            this.databaseToolStripMenuItem.Size = new System.Drawing.Size(67, 20);
            this.databaseToolStripMenuItem.Text = "Database";
            // 
            // updateEmployeeRegisterToolStripMenuItem
            // 
            this.updateEmployeeRegisterToolStripMenuItem.Name = "updateEmployeeRegisterToolStripMenuItem";
            this.updateEmployeeRegisterToolStripMenuItem.Size = new System.Drawing.Size(212, 22);
            this.updateEmployeeRegisterToolStripMenuItem.Text = "Update Employee Register";
            this.updateEmployeeRegisterToolStripMenuItem.Click += new System.EventHandler(this.EmployeeRegisterToolStripMenuItem_Click);
            // 
            // clearTemplatesToolStripMenuItem
            // 
            this.clearTemplatesToolStripMenuItem.Name = "clearTemplatesToolStripMenuItem";
            this.clearTemplatesToolStripMenuItem.Size = new System.Drawing.Size(212, 22);
            this.clearTemplatesToolStripMenuItem.Text = "Clear Templates";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(1319, 702);
            this.Controls.Add(this.menuStrip1);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.statusStrip1);
            this.Name = "Form1";
            this.Text = "OCR Template Manager";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel2;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem uploadPDFToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem processDocuments;
        private System.Windows.Forms.ToolStripMenuItem templateToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem newToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem resetMarkingsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem databaseToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem updateEmployeeRegisterToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem clearTemplatesToolStripMenuItem;
    }
}

