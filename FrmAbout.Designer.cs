namespace Ground_Floor
{
    partial class FrmAbout
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.MainMenu mainMenu1;

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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmAbout));
            this.mainMenu1 = new System.Windows.Forms.MainMenu();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.label1 = new System.Windows.Forms.Label();
            this.linkLabel1 = new System.Windows.Forms.LinkLabel();
            this.btnExit = new System.Windows.Forms.Button();
            this.pnlPrivate = new System.Windows.Forms.Panel();
            this.lblPrivate = new System.Windows.Forms.Label();
            this.txtPassword = new System.Windows.Forms.TextBox();
            this.inputPanel1 = new Microsoft.WindowsCE.Forms.InputPanel();
            this.btnOk = new System.Windows.Forms.Button();
            this.btnExtend = new System.Windows.Forms.Button();
            this.pnlPrivate.SuspendLayout();
            this.SuspendLayout();
            // 
            // pictureBox1
            // 
            this.pictureBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(0, 0);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(398, 133);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(15, 138);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(241, 67);
            this.label1.Text = "TJ Villa - Automation User Interface Version 1.0\r\n© 2011 Interior Automation.\r\nAl" +
                "l rights reserved.";
            // 
            // linkLabel1
            // 
            this.linkLabel1.Location = new System.Drawing.Point(15, 212);
            this.linkLabel1.Name = "linkLabel1";
            this.linkLabel1.Size = new System.Drawing.Size(199, 15);
            this.linkLabel1.TabIndex = 3;
            this.linkLabel1.Text = "www.interior-automation.com";
            // 
            // btnExit
            // 
            this.btnExit.Location = new System.Drawing.Point(215, 11);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(65, 28);
            this.btnExit.TabIndex = 5;
            this.btnExit.Text = "Exit UI";
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // pnlPrivate
            // 
            this.pnlPrivate.BackColor = System.Drawing.Color.White;
            this.pnlPrivate.Controls.Add(this.btnExit);
            this.pnlPrivate.Controls.Add(this.lblPrivate);
            this.pnlPrivate.Controls.Add(this.txtPassword);
            this.pnlPrivate.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlPrivate.Location = new System.Drawing.Point(0, 213);
            this.pnlPrivate.Name = "pnlPrivate";
            this.pnlPrivate.Size = new System.Drawing.Size(398, 48);
            this.pnlPrivate.Visible = false;            
            // 
            // lblPrivate
            // 
            this.lblPrivate.Location = new System.Drawing.Point(11, 19);
            this.lblPrivate.Name = "lblPrivate";
            this.lblPrivate.Size = new System.Drawing.Size(207, 20);
            this.lblPrivate.Text = "Enter the password to exit the UI";
            // 
            // txtPassword
            // 
            this.txtPassword.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.txtPassword.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(9)))), ((int)(((byte)(69)))), ((int)(((byte)(123)))));
            this.txtPassword.Location = new System.Drawing.Point(291, 16);
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.PasswordChar = '*';
            this.txtPassword.Size = new System.Drawing.Size(100, 23);
            this.txtPassword.TabIndex = 6;
            this.txtPassword.GotFocus += new System.EventHandler(this.txtPassword_GotFocus);
            // 
            // btnOk
            // 
            this.btnOk.Location = new System.Drawing.Point(255, 227);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(65, 28);
            this.btnOk.TabIndex = 6;
            this.btnOk.Text = "OK";
            this.btnOk.Click += new System.EventHandler(this.button1_Click);
            // 
            // btnExtend
            // 
            this.btnExtend.Location = new System.Drawing.Point(326, 227);
            this.btnExtend.Name = "btnExtend";
            this.btnExtend.Size = new System.Drawing.Size(65, 28);
            this.btnExtend.TabIndex = 7;
            this.btnExtend.Tag = "shrink";
            this.btnExtend.Text = ">>";
            this.btnExtend.Click += new System.EventHandler(this.btnExtend_Click);
            // 
            // FrmAbout
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.ClientSize = new System.Drawing.Size(398, 261);
            this.Controls.Add(this.linkLabel1);
            this.Controls.Add(this.btnExtend);
            this.Controls.Add(this.btnOk);
            this.Controls.Add(this.pnlPrivate);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.pictureBox1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmAbout";
            this.Text = "About User Interface";            
            this.pnlPrivate.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.LinkLabel linkLabel1;
        private System.Windows.Forms.Button btnExit;
        private System.Windows.Forms.Panel pnlPrivate;
        private System.Windows.Forms.TextBox txtPassword;
        private Microsoft.WindowsCE.Forms.InputPanel inputPanel1;
        private System.Windows.Forms.Button btnOk;
        private System.Windows.Forms.Button btnExtend;
        private System.Windows.Forms.Label lblPrivate;
    }
}