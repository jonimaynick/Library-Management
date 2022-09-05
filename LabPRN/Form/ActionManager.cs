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
    public partial class ActionManager : Form
    {
        private static string ACTIVE = "Active";
        private static string INACTIVE = "Inactive";
        private BookDTO book = null;
        private BookManager MainForm = null;

        public ActionManager()
        {
            InitializeComponent();
        }
        public void AddAction()
        {
            ReadyComboBox();
            SetStatus(true);
            this.Controls.Remove(this.btnStatus);
        }

        public void GetBook(BookDTO dto, BookManager MainForm)
        {
            this.MainForm = MainForm;
            this.book = dto;
            if (dto.BookID == null)
            {
                MessageBox.Show("Error", "Error");
                this.Close();
                return;
            }
            else
            {
                this.txtID.Text = dto.BookID;
                this.txtPrice.Text = dto.Price.ToString();
                this.txtPublisher.Text = dto.Publisher;
                this.txtTitle.Text = dto.Title;
                this.ReadyComboBox();

                cbCategory.SelectedItem = dto.Category;
                
                cbAuthor.SelectedItem = dto.AuthorName;

                dateYear.Value = dto.YearProduction;
                numQuantity.Value = dto.Quantity;
                txtDescribe.Text = dto.Describe;
                txtImage.Text = dto.Image;
                this.imgBook.ImageLocation = txtImage.Text;
                this.imgBook.SizeMode = PictureBoxSizeMode.StretchImage;
                SetStatus(dto.Status);
            }
        }

        private void ReadyComboBox()
        {
            List<string> authorList = BookDAO.GetListAuthorCategory("author");
            ReadyComboBox(cbAuthor, authorList);
            List<string> categoryList = BookDAO.GetListAuthorCategory("category");
            ReadyComboBox(cbCategory, categoryList);
        }

        private void SetStatus(bool status)
        {
            if (status)
            {
                this.lbStatus.Text = ACTIVE;
                this.lbStatus.ForeColor = Color.Green;
                btnStatus.Text = INACTIVE;
            }
            else
            {
                this.lbStatus.Text = INACTIVE;
                this.lbStatus.ForeColor = Color.Red;
                btnStatus.Text = ACTIVE;
            }
        }

        private void ActionManager_Load(object sender, EventArgs e)
        {

        }

        private void ReadyComboBox(ComboBox box, List<string> list)
        {
            foreach (string item in list)
            {
                box.Items.Add(item);
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            
            this.Close();
        }

        private void btnCheckImage_Click(object sender, EventArgs e)
        {
            this.imgBook.ImageLocation = txtImage.Text;
            this.imgBook.SizeMode = PictureBoxSizeMode.StretchImage;
        }

        private void btnStatus_Click(object sender, EventArgs e)
        {
            string action = btnStatus.Text;
            if (action.Equals(ACTIVE))
            {
                SetStatus(true);
            }
            else
            {
                SetStatus(false);
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            bool check = Checker();
            if (check)
            {
                if (txtID.Text.Equals(""))
                {
                    this.Add();
                }
                else
                {
                    this.Update();
                }
            }
        }

        private void Add()
        {
            BookDTO book = GetBookAtForm();
            bool check = BookDAO.addBook(book);
            if (check)
            {
                MessageBox.Show("Update success !", "Success");
                this.MainForm.LoadForm();
            }
            else
            {
                MessageBox.Show("Errorr !", "Error");
            }
        }

        private void Update()
        {
            BookDTO book = GetBookAtForm();
            if(book.Status != this.book.Status && this.book.Status)
            {
                var result = MessageBox.Show($"Do you want to {INACTIVE} the book name {book.Title} !", "Confirm Delete", MessageBoxButtons.YesNoCancel);
                if (result == DialogResult.No)
                {
                    return;
                }
            }
            bool check = BookDAO.updateBook(book);
            if (check)
            {
                MessageBox.Show("Update success !", "Success");
                this.book = book;
                this.MainForm.LoadForm();
            }
            else
            {
                MessageBox.Show("Errorr !", "Error");
            }
        }

        private BookDTO GetBookAtForm()
        {
            bool status = false;
            if (lbStatus.Text.Equals(ACTIVE)) {
                status = true;
            }
            return new BookDTO(this.txtID.Text,this.txtTitle.Text,Convert.ToDouble(this.txtPrice.Text),
                this.txtImage.Text, this.txtDescribe.Text, this.cbAuthor.SelectedItem.ToString(),this.cbCategory.SelectedItem.ToString(),
                Convert.ToInt32(this.numQuantity.Value),this.txtPublisher.Text ,this.dateYear.Value, status);
        }

        private bool Checker()
        {
            bool rs = true;
            if(this.txtTitle.TextLength < 5 || this.txtTitle.TextLength > 150)
            {
                rs= false;
                MessageBox.Show("Title length must be 5 to 150!","Error");
            }
            if(this.txtPublisher.TextLength < 5 || this.txtPublisher.TextLength > 100)
            {
                rs = false;
                MessageBox.Show("Publisher length must be 5 to 100!", "Error");
            }
            double price;
            bool isDouble = Double.TryParse(this.txtPrice.Text, out price);
            if (!isDouble)
            {
                rs = false;
                MessageBox.Show("Price must be douple", "Error");
            }
            if(this.txtDescribe.TextLength < 50 || this.txtDescribe.TextLength > 500)
            {
                rs = false;
                MessageBox.Show("Describe length must be 50 to 500!", "Error");
            }
            if (!checkAuthor())
            {
                rs = false;
            }
            return rs;
        }

        private bool checkAuthor()
        {
            bool rs = true;
            DateTime date = BookDAO.GetAuthorBirth(cbAuthor.SelectedItem.ToString());
            if(date > this.dateYear.Value)
            {
                MessageBox.Show("Year Product must less author birth!", "Error");
                rs = false;
            }
            return rs;
        }


    }
}
