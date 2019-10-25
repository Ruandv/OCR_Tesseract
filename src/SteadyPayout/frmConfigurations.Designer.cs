namespace WindowsFormsApp2
{
    partial class FrmConfigurations
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
            this.cmdSave = new System.Windows.Forms.Button();
            this.chkEmail = new System.Windows.Forms.CheckBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txtApplicationDirectory = new System.Windows.Forms.TextBox();
            this.txtEncryptionDirectory = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtErrorDirectory = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtHost = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtPort = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.txtApiKey = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.chkDelete = new System.Windows.Forms.CheckBox();
            this.txtEmailMessage = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.txtEmailSubject = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.txtFromEmailAddress = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.txtEmailClientName = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // cmdSave
            // 
            this.cmdSave.Location = new System.Drawing.Point(499, 349);
            this.cmdSave.Name = "cmdSave";
            this.cmdSave.Size = new System.Drawing.Size(75, 23);
            this.cmdSave.TabIndex = 0;
            this.cmdSave.Text = "Save";
            this.cmdSave.UseVisualStyleBackColor = true;
            this.cmdSave.Click += new System.EventHandler(this.CmdSave_Click);
            // 
            // chkEmail
            // 
            this.chkEmail.AutoSize = true;
            this.chkEmail.Location = new System.Drawing.Point(139, 210);
            this.chkEmail.Name = "chkEmail";
            this.chkEmail.Size = new System.Drawing.Size(73, 17);
            this.chkEmail.TabIndex = 1;
            this.chkEmail.Text = "Use Email";
            this.chkEmail.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(32, 31);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(101, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "ApplicationDirectory";
            // 
            // txtApplicationDirectory
            // 
            this.txtApplicationDirectory.Location = new System.Drawing.Point(139, 28);
            this.txtApplicationDirectory.Name = "txtApplicationDirectory";
            this.txtApplicationDirectory.Size = new System.Drawing.Size(435, 20);
            this.txtApplicationDirectory.TabIndex = 3;
            // 
            // txtEncryptionDirectory
            // 
            this.txtEncryptionDirectory.Location = new System.Drawing.Point(139, 54);
            this.txtEncryptionDirectory.Name = "txtEncryptionDirectory";
            this.txtEncryptionDirectory.Size = new System.Drawing.Size(435, 20);
            this.txtEncryptionDirectory.TabIndex = 5;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(32, 57);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(102, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "Encryption Directory";
            // 
            // txtErrorDirectory
            // 
            this.txtErrorDirectory.Location = new System.Drawing.Point(139, 80);
            this.txtErrorDirectory.Name = "txtErrorDirectory";
            this.txtErrorDirectory.Size = new System.Drawing.Size(435, 20);
            this.txtErrorDirectory.TabIndex = 7;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(32, 83);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(74, 13);
            this.label3.TabIndex = 6;
            this.label3.Text = "Error Directory";
            // 
            // txtHost
            // 
            this.txtHost.Location = new System.Drawing.Point(139, 132);
            this.txtHost.Name = "txtHost";
            this.txtHost.Size = new System.Drawing.Size(435, 20);
            this.txtHost.TabIndex = 9;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(32, 135);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(56, 13);
            this.label4.TabIndex = 8;
            this.label4.Text = "Smtp Host";
            // 
            // txtPort
            // 
            this.txtPort.Location = new System.Drawing.Point(139, 158);
            this.txtPort.Name = "txtPort";
            this.txtPort.Size = new System.Drawing.Size(73, 20);
            this.txtPort.TabIndex = 11;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(32, 161);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(53, 13);
            this.label5.TabIndex = 10;
            this.label5.Text = "Smtp Port";
            // 
            // txtApiKey
            // 
            this.txtApiKey.Location = new System.Drawing.Point(139, 184);
            this.txtApiKey.Name = "txtApiKey";
            this.txtApiKey.Size = new System.Drawing.Size(435, 20);
            this.txtApiKey.TabIndex = 13;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(32, 187);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(90, 13);
            this.label6.TabIndex = 12;
            this.label6.Text = "Send Grid ApiKey";
            // 
            // chkDelete
            // 
            this.chkDelete.AutoSize = true;
            this.chkDelete.Location = new System.Drawing.Point(139, 233);
            this.chkDelete.Name = "chkDelete";
            this.chkDelete.Size = new System.Drawing.Size(145, 17);
            this.chkDelete.TabIndex = 14;
            this.chkDelete.Text = "Delete Unencrypted Files";
            this.chkDelete.UseVisualStyleBackColor = true;
            // 
            // txtEmailMessage
            // 
            this.txtEmailMessage.Location = new System.Drawing.Point(139, 319);
            this.txtEmailMessage.Name = "txtEmailMessage";
            this.txtEmailMessage.Size = new System.Drawing.Size(435, 20);
            this.txtEmailMessage.TabIndex = 16;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(32, 322);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(78, 13);
            this.label7.TabIndex = 15;
            this.label7.Text = "Email Message";
            // 
            // txtEmailSubject
            // 
            this.txtEmailSubject.Location = new System.Drawing.Point(139, 290);
            this.txtEmailSubject.Name = "txtEmailSubject";
            this.txtEmailSubject.Size = new System.Drawing.Size(435, 20);
            this.txtEmailSubject.TabIndex = 18;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(32, 293);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(71, 13);
            this.label8.TabIndex = 17;
            this.label8.Text = "Email Subject";
            // 
            // txtFromEmailAddress
            // 
            this.txtFromEmailAddress.Location = new System.Drawing.Point(139, 256);
            this.txtFromEmailAddress.Name = "txtFromEmailAddress";
            this.txtFromEmailAddress.Size = new System.Drawing.Size(435, 20);
            this.txtFromEmailAddress.TabIndex = 20;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(32, 259);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(99, 13);
            this.label9.TabIndex = 19;
            this.label9.Text = "From Email Address";
            // 
            // txtEmailClientName
            // 
            this.txtEmailClientName.Location = new System.Drawing.Point(139, 106);
            this.txtEmailClientName.Name = "txtEmailClientName";
            this.txtEmailClientName.Size = new System.Drawing.Size(435, 20);
            this.txtEmailClientName.TabIndex = 22;
            this.txtEmailClientName.Text = "SendGrid";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(32, 109);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(60, 13);
            this.label10.TabIndex = 21;
            this.label10.Text = "Smtp Client";
            // 
            // FrmConfigurations
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(610, 359);
            this.Controls.Add(this.txtEmailClientName);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.txtFromEmailAddress);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.txtEmailSubject);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.txtEmailMessage);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.chkDelete);
            this.Controls.Add(this.txtApiKey);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.txtPort);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.txtHost);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.txtErrorDirectory);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txtEncryptionDirectory);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtApplicationDirectory);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.chkEmail);
            this.Controls.Add(this.cmdSave);
            this.Name = "FrmConfigurations";
            this.Text = "FrmConfigurations";
            this.Load += new System.EventHandler(this.FrmConfigurations_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button cmdSave;
        private System.Windows.Forms.CheckBox chkEmail;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtApplicationDirectory;
        private System.Windows.Forms.TextBox txtEncryptionDirectory;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtErrorDirectory;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtHost;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtPort;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtApiKey;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.CheckBox chkDelete;
        private System.Windows.Forms.TextBox txtEmailMessage;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox txtEmailSubject;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox txtFromEmailAddress;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox txtEmailClientName;
        private System.Windows.Forms.Label label10;
    }
}