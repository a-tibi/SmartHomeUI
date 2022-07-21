using System;

using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Ground_Floor
{
    public partial class FrmAbout : Form
    {
        private const string EXIT_PASSWORD = "2844";

        public FrmAbout()
        {
            InitializeComponent();
            
        }        

        private void btnExit_Click(object sender, EventArgs e)
        {
            inputPanel1.Enabled = false;
            string sPassword = txtPassword.Text;
            if (sPassword == EXIT_PASSWORD)
            {
                Application.Exit();
            }
            else
            {
                MessageBox.Show("Wrong password, please try again!");
            }
        } 

        private void txtPassword_GotFocus(object sender, EventArgs e)
        {
            inputPanel1.Enabled = true;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnExtend_Click(object sender, EventArgs e)
        {
            if (btnExtend.Tag.ToString() == "shrink")
            {
                pnlPrivate.Visible = true;
                this.Height = 333;
                btnExtend.Text = "<<";
                btnExtend.Tag = "extend";
            }
            else
            {
                pnlPrivate.Visible = false;
                this.Height = 286;
                btnExtend.Text = ">>";
                btnExtend.Tag = "shrink";
            }
        }

        private void inputPanel1_EnabledChanged(object sender, EventArgs e)
        {
            inputPanel1.Enabled = false;
        }
    }
}