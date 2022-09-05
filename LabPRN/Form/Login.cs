using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LabPRN
{
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            UserDAO dao = new UserDAO();
            UserDTO user = dao.CheckLogin(txtUserID.Text, txtPassword.Text);
            if(user == null)
            {
                MessageBox.Show("Id and password is not right !","Error");
            }
            else
            {
                if (user.roleName.Equals("Admin"))
                {
                    AdminForm admin = new AdminForm();
                    this.Visible = false;
                    admin.ShowDialog();
                    this.Visible = true;
                    this.reset();
                }
                else
                {
                    frmBorrowedBookDetail userForm = new frmBorrowedBookDetail(user);
                    this.Visible = false;
                    userForm.ShowDialog();
                    this.Visible = true;
                    this.reset();
                }
            }
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            this.reset();
        }

        private void reset()
        {
            this.txtUserID.Text = "";
            this.txtPassword.Text = "";
        }

        private void btnQuit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
