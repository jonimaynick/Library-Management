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
    public partial class frmUpdateUser : Form
    {
        UserDB userDB = new UserDB();
        public frmUpdateUser()
        {
            InitializeComponent();
        }
        public void ShowRoleComboBox()
        {
            DataTable tblRole = userDB.ShowRoleComboBox();
            if (tblRole != null)
            {
                cbRole.DataSource = tblRole;
                cbRole.ValueMember = "RoleName";
            }
        }

        public void ShowStatusComboBox()
        {
            DataTable tblStatue = userDB.ShowStatusComboBox();
            if (tblStatue != null)
            {
                cbStatus.DataSource = tblStatue;
                cbStatus.ValueMember = "Status";
            }
        }

        private void frmUpdateUser_Load(object sender, EventArgs e)
        {
            ShowRoleComboBox();
            //ShowStatusComboBox();
            cbStatus.Items.Insert(0, "Active");
            cbStatus.Items.Insert(1, "DeActivated");
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            string id = txtUserID.Text;
            string password = txtUserPassword.Text;
            string role = cbRole.Text;
            string name = txtUserName.Text;
            DateTime birthday = DateTime.Parse(mskBirthday.Text);
            string address = txtAddress.Text;
            string phone = txtPhone.Text;
            string email = txtEmail.Text;
            bool status;
            if (cbStatus.SelectedIndex == 0)
            {
                status = true;
            }
            else
            {
                status = false;
            }

            UserDTO user = new UserDTO
            {
                userID = id,
                password = password,
                roleName = role,
                name = name,
                birthday = birthday,
                address = address,
                phone = phone,
                email = email,
                status = status
            };
            bool result = userDB.updateUser(user);
            string s = (result == true ? "Success" : "Fail");
            MessageBox.Show("Update " + s);
        }
    }
}
