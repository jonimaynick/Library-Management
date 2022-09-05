using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Configuration;

namespace LabPRN
{
    public partial class frmBorrowNewBook : Form
    {
        UserDTO userAccount;
        SmartLibrayDAO Dao;
        string ConnectionString;
        DataTable dtBooks;
        public frmBorrowNewBook(UserDTO user)
        {
            InitializeComponent();
            userAccount = user;
            ConnectionString = getConnectionString();
            Dao = new SmartLibrayDAO(ConnectionString);
            dgvBooks.ReadOnly = true;
        }

        private string getConnectionString()
        {

            return ConfigurationManager.
                ConnectionStrings["DBConnect"].ConnectionString;
        }

        private void loadData()
        {
            dtBooks = Dao.GetBooks();
            dgvBooks.DataSource = dtBooks;
            dgvBooks.Columns[2].Visible = false;
            dgvBooks.Columns[3].Visible = false;
            dgvBooks.Columns[4].Visible = false;
            //dgvBooks.Columns[8].Visible = false;

            txtBookID.DataBindings.Clear();
            txtTitle.DataBindings.Clear();
            txtCategory.DataBindings.Clear();
            txtDescribe.DataBindings.Clear();
            txtAuthor.DataBindings.Clear();
            txtQuantity.DataBindings.Clear();
            txtPublicYear.DataBindings.Clear();
            txtPublisher.DataBindings.Clear();

            txtBookID.DataBindings.Add("Text", dtBooks, "BookID");
            txtTitle.DataBindings.Add("Text", dtBooks, "Title");
            txtAuthor.DataBindings.Add("Text", dtBooks, "AuthorName");
            txtCategory.DataBindings.Add("Text", dtBooks, "CategoryName");
            txtDescribe.DataBindings.Add("Text", dtBooks, "BookDescribe");
            txtQuantity.DataBindings.Add("Text", dtBooks, "TotalQuantity");
            txtPublisher.DataBindings.Add("Text", dtBooks, "Publisher");
            txtPublicYear.DataBindings.Add("Text", dtBooks, "YearProduction");

            loadColorCellBaseOnStatus();
            LoadCombobox();
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

        private void loadColorCellBaseOnStatus()
        {
            foreach (DataGridViewRow row in dgvBooks.Rows)
            {
                Book currentBook = Dao.getBookByID(Convert.ToString(row.Cells[0].Value));
                if (currentBook != null && currentBook.status == false)
                {
                    row.DefaultCellStyle.BackColor = Color.OrangeRed;
                }
            }
        }

        private void LoadCombobox()
        {
            List<string> listCB = Dao.GetListCategory("--Category--");
            if (listCB.Count > 0)
            {
                cbCategory.DataSource = listCB;
            }
        }

        private void btnRefesh_Click(object sender, EventArgs e)
        {
            loadData();
            txtSearchName.Text = String.Empty;
        }

        private void dgvBooks_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (txtBookID.Text != null || txtBookID.Text.Length != 0)
            {
                loadPicture(Dao.getStringImageResource(txtBookID.Text));
            }
        }

        private void frmBorrowNewBook_Load(object sender, EventArgs e)
        {
            loadData();
            lbHello.Text = "Hello " + userAccount.name + " | " + DateTime.Now.DayOfWeek + " " + DateTime.Now.Day + "-" + DateTime.Now.Month + "-" + DateTime.Now.Year;
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

        private void btnSearch_Click(object sender, EventArgs e)
        {
            string CharSearch = txtSearchName.Text;
            DataTable dtSearch = Dao.SearchByName(CharSearch);
            dgvBooks.DataSource = dtSearch;
            dgvBooks.Columns[2].Visible = false;
            dgvBooks.Columns[3].Visible = false;
            dgvBooks.Columns[4].Visible = false;
            //dgvBooks.Columns[8].Visible = false;

            txtBookID.DataBindings.Clear();
            txtTitle.DataBindings.Clear();
            txtCategory.DataBindings.Clear();
            txtDescribe.DataBindings.Clear();
            txtAuthor.DataBindings.Clear();
            txtQuantity.DataBindings.Clear();
            txtPublicYear.DataBindings.Clear();
            txtPublisher.DataBindings.Clear();

            txtBookID.DataBindings.Add("Text", dtSearch, "BookID");
            txtTitle.DataBindings.Add("Text", dtSearch, "Title");
            txtAuthor.DataBindings.Add("Text", dtSearch, "AuthorName");
            txtCategory.DataBindings.Add("Text", dtSearch, "CategoryName");
            txtDescribe.DataBindings.Add("Text", dtSearch, "BookDescribe");
            txtQuantity.DataBindings.Add("Text", dtSearch, "TotalQuantity");
            txtPublisher.DataBindings.Add("Text", dtSearch, "Publisher");
            txtPublicYear.DataBindings.Add("Text", dtSearch, "YearProduction");

            //LoadCombobox();
            loadColorCellBaseOnStatus();
        }

        private void cbCategory_SelectedIndexChanged(object sender, EventArgs e)
        {
            DataTable dt;
            if (cbCategory.SelectedIndex != 0)
            {
                dt = Dao.GetListBookByCategory(cbCategory.SelectedItem.ToString());
                dgvBooks.DataSource = dt;
                dgvBooks.Columns[2].Visible = false;
                dgvBooks.Columns[3].Visible = false;
                dgvBooks.Columns[4].Visible = false;
                //dgvBooks.Columns[8].Visible = false;

                txtBookID.DataBindings.Clear();
                txtTitle.DataBindings.Clear();
                txtCategory.DataBindings.Clear();
                txtDescribe.DataBindings.Clear();
                txtAuthor.DataBindings.Clear();
                txtQuantity.DataBindings.Clear();
                txtPublicYear.DataBindings.Clear();
                txtPublisher.DataBindings.Clear();

                txtBookID.DataBindings.Add("Text", dt, "BookID");
                txtTitle.DataBindings.Add("Text", dt, "Title");
                txtAuthor.DataBindings.Add("Text", dt, "AuthorName");
                txtCategory.DataBindings.Add("Text", dt, "CategoryName");
                txtDescribe.DataBindings.Add("Text", dt, "BookDescribe");
                txtQuantity.DataBindings.Add("Text", dt, "TotalQuantity");
                txtPublisher.DataBindings.Add("Text", dt, "Publisher");
                txtPublicYear.DataBindings.Add("Text", dt, "YearProduction");

                loadColorCellBaseOnStatus();
            }
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnBorrow_Click(object sender, EventArgs e)
        {
            Book currentBook = Dao.getBookByID(txtBookID.Text);
            if (!currentBook.status)
            {
                MessageBox.Show("this book is not available, choose the new one!", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                frmRegisterBorrowBooks frmrbb = new frmRegisterBorrowBooks(currentBook, userAccount);
                frmrbb.ShowDialog();
            }
        }
    }
}
