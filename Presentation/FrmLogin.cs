using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using BUS;

namespace Presentation
{
    public partial class FrmLogin : Form
    {
        public FrmLogin()
        {
            InitializeComponent();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            if (txtUserName.Text.Trim().Equals(""))
            {
                MessageBox.Show("Vui lòng nhập tên đăng nhập!", "Thông báo");
            }
            else if (txtPassword.Text.Trim().Equals(""))
            {
                MessageBox.Show("Vui lòng nhập mật khẩu!", "Thông báo");
            }
            else
            {
                DataTable dt = (new BUSUser()).login(txtUserName.Text, txtPassword.Text);
                if (dt.Rows.Count>0)
                {
                    this.Hide();
                    FrmCSDMarking frm = new FrmCSDMarking(txtUserName.Text);
                    frm.Show();
                }
                else
                {
                    MessageBox.Show("Tên đăng nhập hoặc mật khẩu sai!", "Thông báo");
                }
            }
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            txtUserName.Text = "";
            txtPassword.Text = "";
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Bạn muốn thoát?", "Thông báo", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                Application.Exit();
            }
        }
    }
}
