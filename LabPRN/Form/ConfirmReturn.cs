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
    public partial class ConfirmReturn : Form
    {
        HistoryDTO htr = new HistoryDTO();
        public ConfirmReturn()
        {
            InitializeComponent();
        }

        public void LoadBook(HistoryDTO htr)
        {
            this.htr = htr;
            txtID.Text = htr.Id.ToString();
            txtBookName.Text = htr.Title;
            txtUserName.Text = htr.userName;
            txtQuantity.Text = htr.quantity.ToString();
            dateDue.Value = htr.DueDate;
            dateRelease.Value = htr.releaseDate;
            lbStatus.Text = htr.Status;
            if (htr.Status.Equals("Paid"))
            {
                lbStatus.ForeColor = Color.Green;
                btnConfirm.Enabled = false;
            }
            else
            {
                lbStatus.ForeColor = Color.Red;
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnConfirm_Click(object sender, EventArgs e)
        {
            bool rs = HistoryDAO.confirmReturn(this.htr);
            if (rs)
            {
                htr.Status = "Paid";
                this.LoadBook(this.htr);
                MessageBox.Show("Success", "Note");
                btnConfirm.Enabled = false;
            }
        }
    }
}
