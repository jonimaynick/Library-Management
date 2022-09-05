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
    public partial class RentalManager : Form
    {
        List<HistoryDTO> list = null;
        public RentalManager()
        {
            InitializeComponent();
            cbStatus.SelectedIndex = 0;
        }

        private void RentalManager_Load(object sender, EventArgs e)
        {
            this.list = HistoryDAO.SearchBooks(txtUserID.Text, false, false);
            dgv.DataSource = list;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void dgv_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int index = dgv.CurrentRow.Index;
            ConfirmReturn confirm = new ConfirmReturn();
            confirm.LoadBook(list[index]);
            this.Visible = false;
            confirm.ShowDialog();
            this.Visible = true;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (cbStatus.SelectedIndex == 0)
            {
                this.list = HistoryDAO.SearchBooks(txtUserID.Text, false, false);
            }
            else if (cbStatus.SelectedIndex == 1)
            {
                this.list = HistoryDAO.SearchBooks(txtUserID.Text, true, true);
            }
            else
            {
                this.list = HistoryDAO.SearchBooks(txtUserID.Text, true, false);
            }
            dgv.DataSource = this.list;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            var list = HistoryDAO.SearchBooks(txtUserID.Text, false, false);
            foreach (var item in list)
            {
                if (item.Id.ToString().Equals(txtHistoryID.Text))
                {
                    list = new List<HistoryDTO>();
                    list.Add(item);
                    break;
                }
            }
            this.list = list;
            dgv.DataSource = list;
        }
    }
}
