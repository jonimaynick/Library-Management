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
    public partial class frmUserManagerment : Form
    {
        UserDB userdb = new UserDB();
        DataTable tblUser;
        public frmUserManagerment()
        {
            InitializeComponent();
        }
        private void getallUser()
        {
            tblUser = userdb.getUser();

            txtUserID.DataBindings.Clear();
            txtUserPassword.DataBindings.Clear();
            txtRole.DataBindings.Clear();
            txtUserName.DataBindings.Clear();
            mskBirthday.DataBindings.Clear();
            txtAddress.DataBindings.Clear();
            txtPhone.DataBindings.Clear();
            txtEmail.DataBindings.Clear();
            txtStatus.DataBindings.Clear();

            txtUserID.DataBindings.Add("Text", tblUser, "UserID");
            txtUserPassword.DataBindings.Add("Text", tblUser, "UserPassword");
            txtRole.DataBindings.Add("Text", tblUser, "RoleName");
            txtUserName.DataBindings.Add("Text", tblUser, "UserName");
            mskBirthday.DataBindings.Add("Text", tblUser, "UserBirthday");
            txtAddress.DataBindings.Add("Text", tblUser, "Address");
            txtPhone.DataBindings.Add("Text", tblUser, "Phone");
            txtEmail.DataBindings.Add("Text", tblUser, "Email");
            txtStatus.DataBindings.Add("Text", tblUser, "Status");


            dgvUserManagerment.DataSource = tblUser;

        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnLoad_Click(object sender, EventArgs e)
        {
            btnSearch.Enabled = true;
            getallUser();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            frmAddNewUser frmAddNew = new frmAddNewUser();
            this.Visible = false;
            frmAddNew.ShowDialog();
            this.Visible = true;
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            frmUpdateUser frmUpdate = new frmUpdateUser();
            this.Visible = false;
            frmUpdate.ShowDialog();
            this.Visible = true;
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            string id = txtUserID.Text;
            bool result = userdb.deleteUser(id);
            string s = (result == true ? "Success" : "Fail");
            MessageBox.Show("Delete " + s);
            getallUser();
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            string id = txtSearchValue.Text;
            DataView dv = tblUser.DefaultView;
            string querry = "UserID = " + id;
            dv.RowFilter = querry;
            if(dv.Count == 0) {
                MessageBox.Show("Cannot find ID for that user! ");
                getallUser();
            }
        }

        private void frmUserManagerment_Load(object sender, EventArgs e)
        {
            btnSearch.Enabled = false;
        }
    }
}
