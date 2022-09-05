using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Configuration;

namespace LabPRN
{
    public partial class frmBorrowedBookDetail : Form
    {
        string ConnectionString;
        SmartLibrayDAO Dao;
        UserDTO userAccount;
        DataTable dt;
        public frmBorrowedBookDetail(UserDTO user)
        {
            InitializeComponent();
            userAccount = user;
            ConnectionString = GetConnectionString();
            Dao = new SmartLibrayDAO(ConnectionString);
            dgvListBook.ReadOnly = true;
        }

        private string GetConnectionString()
        {
            return ConfigurationManager.
                ConnectionStrings["DBConnect"].ConnectionString;
        }

        private void LoadData()
        {
            dt = Dao.GetListHistoryBorrowed(userAccount.userID);

            dgvListBook.DataSource = dt;
            txtCodeID.DataBindings.Clear();
            txtBookID.DataBindings.Clear();
            txtTitle.DataBindings.Clear();
            txtQuantity.DataBindings.Clear();
            txtStartDate.DataBindings.Clear();
            txtDueDate.DataBindings.Clear();

            txtCodeID.DataBindings.Add("Text", dt, "CodeID");
            txtBookID.DataBindings.Add("Text", dt, "BookID");
            txtTitle.DataBindings.Add("Text", dt, "Title");
            txtQuantity.DataBindings.Add("Text", dt, "BorrowedQuantity");
            txtStartDate.DataBindings.Add("Text", dt, "ReleaseDate");
            txtDueDate.DataBindings.Add("Text", dt, "DueDate");

            lbTotal.Text = dt.Rows.Count.ToString();
            UpdatelbExpiredBook();
            setColorCell();
        }

        private void frmBorrowedBookDetail_Load(object sender, EventArgs e)
        {
            LoadData();
            lbHello.Text = "Hello " + userAccount.name + " | " + DateTime.Now.DayOfWeek + " " + DateTime.Now.Day + "-" + DateTime.Now.Month + "-" + DateTime.Now.Year;
        }

        private void setColorCell()
        {
            foreach (DataGridViewRow row in dgvListBook.Rows)
            {
                if (Dao.GetStatus(Convert.ToString(row.Cells[0].Value)) == "Expired")
                {
                    row.DefaultCellStyle.BackColor = Color.OrangeRed;
                }

            }
        }

        private void UpdatelbExpiredBook()
        {
            int count = 0;
            DataTable dt = Dao.GetListHistoryBorrowed(userAccount.userID);
            if (dt != null)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    HistoryBorrowedBook h = Dao.GetHistoryBorrowedBook(dr[0].ToString());
                    if (Dao.GetStatus(h.CodeID.ToString()).Equals("Expired"))
                    {
                        count++;
                    }
                }
                if (count < 2)
                {
                    lbExpiredBook.Text = count.ToString();
                }
                else
                {
                    lbExpiredBook.Text = count.ToString();
                }

            }
            else
            {
                lbExpiredBook.Text = "0";
                lbTotal.Text = "0";
            }

        }

        private void loadPicture(string ImageResource)
        {
            if (ImageResource != null || ImageResource.Length != 0)
            {
                ptBook.ImageLocation = ImageResource;
            }
            else
            {
                ptBook.ImageLocation = null;
            }
        }

        private void txtBookID_TextChanged(object sender, EventArgs e)
        {
            string ID = txtBookID.Text;
            if (ID.Length == 0 || ID == null)
            {
                ptBook.Image = null;
            }
            loadPicture(Dao.getStringImageResource(ID));
        }

        private void btnRefesh_Click(object sender, EventArgs e)
        {
            LoadData();
        }

        private void btnLogout_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Do you want to log out?", "Notification", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
            {
                this.Close();
            }
        }

        private void btnSearchBook_Click(object sender, EventArgs e)
        {
            DataTable dtSearch = Dao.SearchHistoryByName(txtSearchBook.Text, userAccount.userID);
            dgvListBook.DataSource = dtSearch;

            txtCodeID.DataBindings.Clear();
            txtBookID.DataBindings.Clear();
            txtTitle.DataBindings.Clear();
            txtQuantity.DataBindings.Clear();
            txtStartDate.DataBindings.Clear();
            txtDueDate.DataBindings.Clear();

            txtCodeID.DataBindings.Add("Text", dtSearch, "CodeID");
            txtBookID.DataBindings.Add("Text", dtSearch, "BookID");
            txtTitle.DataBindings.Add("Text", dtSearch, "Title");
            txtQuantity.DataBindings.Add("Text", dtSearch, "BorrowedQuantity");
            txtStartDate.DataBindings.Add("Text", dtSearch, "ReleaseDate");
            txtDueDate.DataBindings.Add("Text", dtSearch, "DueDate");

            setColorCell();
        }

        private void btnBorrow_Click(object sender, EventArgs e)
        {
                frmBorrowNewBook frmBNB = new frmBorrowNewBook(userAccount);
                this.Visible = false;
                frmBNB.ShowDialog();
                this.Visible = true;
            
        }

        private void txtCodeID_TextChanged(object sender, EventArgs e)
        {
            string codeID = txtCodeID.Text;
            txtStatus.Text = Dao.GetStatus(codeID);
           
        }

        private void dgvListBook_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
