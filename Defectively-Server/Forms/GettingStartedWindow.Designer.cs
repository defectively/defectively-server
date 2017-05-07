namespace DefectivelyServer.Forms
{
    partial class GettingStartedWindow
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing) {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(GettingStartedWindow));
            this.lblTitle = new System.Windows.Forms.Label();
            this.btnContinue = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.btnCreateGroup = new System.Windows.Forms.Button();
            this.lblStep1 = new System.Windows.Forms.Label();
            this.lblStep2 = new System.Windows.Forms.Label();
            this.btnCreateAccount = new System.Windows.Forms.Button();
            this.lblStep3 = new System.Windows.Forms.Label();
            this.btnSpecifyPassword = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // lblTitle
            // 
            this.lblTitle.AccessibleName = "";
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.lblTitle.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.lblTitle.Location = new System.Drawing.Point(12, 12);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(177, 21);
            this.lblTitle.TabIndex = 6;
            this.lblTitle.Text = "Welcome to Defectively!";
            // 
            // btnContinue
            // 
            this.btnContinue.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnContinue.Enabled = false;
            this.btnContinue.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.btnContinue.Location = new System.Drawing.Point(261, 389);
            this.btnContinue.Name = "btnContinue";
            this.btnContinue.Size = new System.Drawing.Size(100, 25);
            this.btnContinue.TabIndex = 7;
            this.btnContinue.Text = "Continue";
            this.btnContinue.UseVisualStyleBackColor = true;
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.btnCancel.Location = new System.Drawing.Point(367, 389);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(100, 25);
            this.btnCancel.TabIndex = 8;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.label1.Location = new System.Drawing.Point(13, 43);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(359, 15);
            this.label1.TabIndex = 9;
            this.label1.Text = "In order to use Defectively, you must complete the following steps.";
            // 
            // btnCreateGroup
            // 
            this.btnCreateGroup.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnCreateGroup.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.btnCreateGroup.Location = new System.Drawing.Point(367, 93);
            this.btnCreateGroup.Name = "btnCreateGroup";
            this.btnCreateGroup.Size = new System.Drawing.Size(100, 25);
            this.btnCreateGroup.TabIndex = 10;
            this.btnCreateGroup.Text = "Create";
            this.btnCreateGroup.UseVisualStyleBackColor = true;
            // 
            // lblStep1
            // 
            this.lblStep1.AccessibleName = "";
            this.lblStep1.AutoSize = true;
            this.lblStep1.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.lblStep1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(252)))), ((int)(((byte)(53)))), ((int)(((byte)(57)))));
            this.lblStep1.Location = new System.Drawing.Point(12, 93);
            this.lblStep1.Name = "lblStep1";
            this.lblStep1.Size = new System.Drawing.Size(243, 21);
            this.lblStep1.TabIndex = 11;
            this.lblStep1.Text = "1.  Create an administrator group.";
            // 
            // lblStep2
            // 
            this.lblStep2.AccessibleName = "";
            this.lblStep2.AutoSize = true;
            this.lblStep2.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.lblStep2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(252)))), ((int)(((byte)(53)))), ((int)(((byte)(57)))));
            this.lblStep2.Location = new System.Drawing.Point(12, 190);
            this.lblStep2.Name = "lblStep2";
            this.lblStep2.Size = new System.Drawing.Size(255, 21);
            this.lblStep2.TabIndex = 13;
            this.lblStep2.Text = "2.  Create an administrator account.";
            // 
            // btnCreateAccount
            // 
            this.btnCreateAccount.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnCreateAccount.Enabled = false;
            this.btnCreateAccount.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.btnCreateAccount.Location = new System.Drawing.Point(367, 190);
            this.btnCreateAccount.Name = "btnCreateAccount";
            this.btnCreateAccount.Size = new System.Drawing.Size(100, 25);
            this.btnCreateAccount.TabIndex = 12;
            this.btnCreateAccount.Text = "Create";
            this.btnCreateAccount.UseVisualStyleBackColor = true;
            // 
            // lblStep3
            // 
            this.lblStep3.AccessibleName = "";
            this.lblStep3.AutoSize = true;
            this.lblStep3.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.lblStep3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(252)))), ((int)(((byte)(53)))), ((int)(((byte)(57)))));
            this.lblStep3.Location = new System.Drawing.Point(12, 287);
            this.lblStep3.Name = "lblStep3";
            this.lblStep3.Size = new System.Drawing.Size(213, 21);
            this.lblStep3.TabIndex = 15;
            this.lblStep3.Text = "3.  Specify a server password.";
            // 
            // btnSpecifyPassword
            // 
            this.btnSpecifyPassword.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnSpecifyPassword.Enabled = false;
            this.btnSpecifyPassword.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.btnSpecifyPassword.Location = new System.Drawing.Point(367, 287);
            this.btnSpecifyPassword.Name = "btnSpecifyPassword";
            this.btnSpecifyPassword.Size = new System.Drawing.Size(100, 25);
            this.btnSpecifyPassword.TabIndex = 14;
            this.btnSpecifyPassword.Text = "Specify";
            this.btnSpecifyPassword.UseVisualStyleBackColor = true;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.label5.Location = new System.Drawing.Point(34, 122);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(281, 15);
            this.label5.TabIndex = 16;
            this.label5.Text = "This group will have the Luva! value \"luva.wildcard\".";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.label6.Location = new System.Drawing.Point(34, 219);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(228, 15);
            this.label6.TabIndex = 17;
            this.label6.Text = "This will be the root account of the server.";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.label7.Location = new System.Drawing.Point(34, 316);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(186, 30);
            this.label7.TabIndex = 18;
            this.label7.Text = "This password is required to make\r\nsignificant changes to the server.";
            // 
            // GettingStartedWindow
            // 
            this.AcceptButton = this.btnContinue;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(484, 431);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.lblStep3);
            this.Controls.Add(this.btnSpecifyPassword);
            this.Controls.Add(this.lblStep2);
            this.Controls.Add(this.btnCreateAccount);
            this.Controls.Add(this.lblStep1);
            this.Controls.Add(this.btnCreateGroup);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnContinue);
            this.Controls.Add(this.lblTitle);
            this.Font = new System.Drawing.Font("Segoe UI", 8.25F);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "GettingStartedWindow";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Getting Started";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        internal System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Button btnContinue;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnCreateGroup;
        internal System.Windows.Forms.Label lblStep1;
        internal System.Windows.Forms.Label lblStep2;
        private System.Windows.Forms.Button btnCreateAccount;
        internal System.Windows.Forms.Label lblStep3;
        private System.Windows.Forms.Button btnSpecifyPassword;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
    }
}