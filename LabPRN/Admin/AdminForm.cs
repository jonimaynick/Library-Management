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
    public partial class AdminForm : Form
    {
        public AdminForm()
        {
            InitializeComponent();
        }

        private void btnBookManager_Click(object sender, EventArgs e)
        {
            BookManager book = new BookManager();
            this.Visible = false;
            book.ShowDialog();
            this.Visible = true;
        }

        private void btnLogout_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnRentalManager_Click(object sender, EventArgs e)
        {
            RentalManager rtm = new RentalManager();
            this.Visible = false;
            rtm.ShowDialog();
            this.Visible = true;            
        }

        private void btnUserManager_Click(object sender, EventArgs e)
        {
            frmUserManagerment userMng = new frmUserManagerment();
            this.Visible=false;
            userMng.ShowDialog();
            this.Visible = true;
        }
    }
}
