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
    public partial class BookManager : Form
    {
        ActionManager action = null;
        Manager mng = null;
        List<BookDTO> list = new List<BookDTO>();
        List<BookDTO> currentTable = null;
        public BookManager()
        {
            InitializeComponent();
        }

        public void LoadForm()
        {
            LoadComboBox();
            filterNameAndStatus();
        }


        private void LoadComboBox() {
            List<string> authorList = BookDAO.GetListAuthorCategory("author");            
            ReadyComboBox(cbAuthor, authorList);
            List<string> categoryList = BookDAO.GetListAuthorCategory("category");
            ReadyComboBox(cbCategory, categoryList);
            List<string> status = new List<string>();
            status.Add("True");
            status.Add("False");
            ReadyComboBox(cbStatus, status);
        }

        private void ReadyComboBox(ComboBox box, List<string> list)
        {
            box.Items.Clear();
            box.Items.Add("All");
            foreach (string item in list)
            {
                box.Items.Add(item);
            }
            box.SelectedIndex = 0;
        }

        private void BookManager_Load(object sender, EventArgs e)
        {
            this.LoadForm();   
        }

        private void btnQuit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void dgv_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int selec = dgv.CurrentRow.Index;
            if(this.action == null)
            {
                action = new ActionManager();
            }
            else
            {
                action.Close();
                action = new ActionManager();
            }

            action.GetBook(this.currentTable[selec], this);
            this.Visible = false;
            action.ShowDialog();
            this.Visible = true;
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (this.action == null)
            {
                action = new ActionManager();
            }
            else
            {
                action.Close();
                action = new ActionManager();
            }
            action.AddAction();
            this.Visible = false;
            action.ShowDialog();
            this.Visible = true;
        }

        private void btnAuMa_Click(object sender, EventArgs e)
        {
            if(this.mng == null)
            {
                mng = new Manager();
            }
            else
            {
                mng.Close();
                mng = new Manager();
            }
            mng.SetupForm(true);
            mng.Show();
        }

        private void btnCaMa_Click(object sender, EventArgs e)
        {
            if (this.mng == null)
            {
                mng = new Manager();
            }
            else
            {
                mng.Close();
                mng = new Manager();
            }
            mng.SetupForm(false);
            this.Visible = false;
            mng.ShowDialog();
            this.Visible = true;
        }

        private void txtName_TextChanged(object sender, EventArgs e)
        {
            filterNameAndStatus();
        }

        private void cbCategory_SelectedValueChanged(object sender, EventArgs e)
        {
            if(currentTable != null)
            {
                currentTable = filterCategoryAndAuthor();
                dgv.DataSource = currentTable;
            }
        }

        private void cbAuthor_SelectedValueChanged(object sender, EventArgs e)
        {
            if(currentTable != null)
            {
                currentTable = filterCategoryAndAuthor();
                dgv.DataSource = currentTable;
            }
        }

        private void cbStatus_SelectedValueChanged(object sender, EventArgs e)
        {
            filterNameAndStatus();
        }


        private void filterNameAndStatus()
        {
            if(cbStatus.SelectedIndex == 0)
            {
                this.list = BookDAO.SearchBooks(txtName.Text,false,false);
            }else if (cbStatus.SelectedItem.ToString().Equals("True"))
            {
                this.list = BookDAO.SearchBooks(txtName.Text,true,true);
            }
            else
            {
                this.list = BookDAO.SearchBooks(txtName.Text,true,false);
            }
            currentTable = this.list;
            currentTable = filterAuthor(this.list);
            list = filterCategory(list);
            currentTable = list;
            dgv.DataSource=currentTable;

        }
        private List<BookDTO> filterCategory(List<BookDTO> list)
        {
            if(cbCategory.SelectedIndex != 0)
            {
                var newList = new List<BookDTO>();
                foreach (BookDTO item in list)
                {                   
                    if (cbCategory.SelectedItem.ToString().Equals(item.Category))
                    {
                        newList.Add(item);
                    }
                }
                list = newList;
            }
            return list;
        }
        private List<BookDTO> filterAuthor(List<BookDTO> list)
        {
            if (cbAuthor.SelectedIndex != 0)
            {
                var newList = new List<BookDTO>();
                foreach (BookDTO item in list)
                {
                    if (cbAuthor.SelectedItem.ToString().Equals(item.AuthorName))
                    {
                        newList.Add(item);
                    }
                }
                list = newList;
            }
            return list;
        }

        private List<BookDTO> filterCategoryAndAuthor()
        {
            currentTable = filterAuthor(this.list);
            currentTable = filterCategory(currentTable);
            return currentTable;

        }
    }
}
