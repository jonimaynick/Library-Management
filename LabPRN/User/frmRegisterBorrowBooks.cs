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
    public partial class frmRegisterBorrowBooks : Form
    {
        Book book;
        UserDTO userAccount;
        string ConnectionString;
        SmartLibrayDAO Dao;

        public frmRegisterBorrowBooks(Book b, UserDTO user)
        {
            InitializeComponent();
            ConnectionString = GetConnectionString();
            Dao = new SmartLibrayDAO(ConnectionString);
            book = b;
            userAccount = user;
        }

        private string GetConnectionString()
        {
            return ConfigurationManager.ConnectionStrings["DBConnect"].ConnectionString;
        }

        private void loadData()
        {
            txtBookID.Text = book.BookID.ToString();
            txtTitle.Text = book.BookName.ToString();
            txtCategory.Text = Dao.GetCategoryNameByID(book.CategoryID).ToString();
            txtAuthor.Text = Dao.GetAuthorNameByID(book.AuthorID).ToString();
            txtDescribe.Text = book.Describe.ToString();
            txtPublicYear.Text = book.YearProduction.ToString();
            txtPublisher.Text = book.Publisher.ToString();
            txtQuantity.Text = book.TotalQuantity.ToString();

            ptBook.ImageLocation = book.BookImage;
            dtpDateStart.Value = DateTime.Now;
            dtpDateStart.Enabled = false;

        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmRegisterBorrowBooks_Load(object sender, EventArgs e)
        {
            loadData();
            lbHello.Text = "Hello " + userAccount.name + " | " + DateTime.Now.DayOfWeek + " " + DateTime.Now.Day + "-" + DateTime.Now.Month + "-" + DateTime.Now.Year;
        }

        private bool CheckCondition()
        {
            bool result = false;
            int totalQuantity = int.Parse(txtQuantity.Text);
            decimal borrowQuantity = nudQuantity.Value;
            if (book.status == false)
            {
                MessageBox.Show("Sorry! This book is not available!", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
            else if (totalQuantity == 0 || totalQuantity - borrowQuantity < 0)
            {
                MessageBox.Show("Oop!, the quantity of this book do not enough for Borrowing!", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
            else
            {
                DateTime startDate = DateTime.Now;
                DateTime dueDate = DateTime.Parse(dtpDueDate.Text.ToString());
                TimeSpan t = dueDate - startDate;
                double diff = t.TotalDays;
                if (diff < 0)
                {
                    MessageBox.Show("Error Input Due Date for Borrowing!", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);

                }
                else if (diff > 90)
                {
                    MessageBox.Show("Can not borrow book more than three month", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);

                }
                else
                {
                    result = true;
                }
            }
            return result;
        }

        private void btnBorrow_Click(object sender, EventArgs e)
        {
            DateTime startDate = DateTime.Now;
            DateTime dueDate = DateTime.Parse(dtpDueDate.Text.ToString());

            if (CheckCondition())
            {
                string BookID = book.BookID;
                string UserID = userAccount.userID;
                decimal Quantity = nudQuantity.Value;

                bool flag = Dao.addnewBorrowing(BookID, UserID, Quantity, startDate, dueDate);
                if (flag)
                {
                    MessageBox.Show("Register borrow book succesfully");
                    if (Dao.UpdateQuantityBook(book.BookID, nudQuantity.Value) == false)
                    {
                        MessageBox.Show("Dear Master! something went wrong!", "Notification", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Fail!");
                    this.Close();
                }

            }
        }
    }
}
