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
    public partial class Manager : Form
    {


        private DataGridView dgv;
        private Label lbID;
        private Label lbName;
        private Label lbBirth;
        private Label lbPlace;
        private TextBox txtID;
        private TextBox txtName;
        private TextBox txtPlace;
        private DateTimePicker dateBirth;
        private Button btnQuit;
        private Button btnAdd;
        private Button btnSave;
        private Label lbTitle;

        private static string AUTHORTITLE = "Author Manager";
        private static string CATEGORYTITLE = "Category Manager";
        private List<AuthorDTO> listAu = null;
        private List<CategoryDTO> listCate = null;
        private bool isAuthor = false;

        public Manager()
        {
            InitializeComponent();
        }

        private DateTime ChangeYear(DateTime now, int year)
        {
            return new DateTime(year, now.Month, now.Day);
        }

        public void SetupForm(bool isAuthor)
        {
            this.isAuthor = isAuthor;
            if (isAuthor)
            {
                this.lbTitle.Text = AUTHORTITLE;
                listAu = BookDAO.GetAllAuthor();
                this.dgv.DataSource = listAu;
            }
            else
            {
                this.lbTitle.Text = CATEGORYTITLE;
                this.Controls.Remove(this.lbBirth);
                this.Controls.Remove(this.lbPlace);
                this.Controls.Remove(this.txtPlace);
                this.Controls.Remove(this.dateBirth);
                this.lbID.Text = "Category ID:";
                this.lbName.Text = "Category Name:";
                listCate = BookDAO.GetAllCategory();
                this.dgv.DataSource = listCate;
            }
        }

        private void InitializeComponent()
        {
            this.dgv = new System.Windows.Forms.DataGridView();
            this.lbTitle = new System.Windows.Forms.Label();
            this.lbID = new System.Windows.Forms.Label();
            this.lbName = new System.Windows.Forms.Label();
            this.lbBirth = new System.Windows.Forms.Label();
            this.lbPlace = new System.Windows.Forms.Label();
            this.txtID = new System.Windows.Forms.TextBox();
            this.txtName = new System.Windows.Forms.TextBox();
            this.txtPlace = new System.Windows.Forms.TextBox();
            this.dateBirth = new System.Windows.Forms.DateTimePicker();
            this.btnQuit = new System.Windows.Forms.Button();
            this.btnAdd = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgv)).BeginInit();
            this.SuspendLayout();
            // 
            // dgv
            // 
            this.dgv.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv.Location = new System.Drawing.Point(472, 28);
            this.dgv.Name = "dgv";
            this.dgv.Size = new System.Drawing.Size(429, 521);
            this.dgv.TabIndex = 0;
            this.dgv.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgv_CellClick);
            // 
            // lbTitle
            // 
            this.lbTitle.AutoSize = true;
            this.lbTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbTitle.Location = new System.Drawing.Point(192, 28);
            this.lbTitle.Name = "lbTitle";
            this.lbTitle.Size = new System.Drawing.Size(81, 25);
            this.lbTitle.TabIndex = 1;
            this.lbTitle.Text = "Author";
            // 
            // lbID
            // 
            this.lbID.AutoSize = true;
            this.lbID.Location = new System.Drawing.Point(115, 121);
            this.lbID.Name = "lbID";
            this.lbID.Size = new System.Drawing.Size(55, 13);
            this.lbID.TabIndex = 2;
            this.lbID.Text = "Author ID:";
            // 
            // lbName
            // 
            this.lbName.AutoSize = true;
            this.lbName.Location = new System.Drawing.Point(98, 172);
            this.lbName.Name = "lbName";
            this.lbName.Size = new System.Drawing.Size(72, 13);
            this.lbName.TabIndex = 3;
            this.lbName.Text = "Author Name:";
            // 
            // lbBirth
            // 
            this.lbBirth.AutoSize = true;
            this.lbBirth.Location = new System.Drawing.Point(105, 217);
            this.lbBirth.Name = "lbBirth";
            this.lbBirth.Size = new System.Drawing.Size(65, 13);
            this.lbBirth.TabIndex = 4;
            this.lbBirth.Text = "Author Birth:";
            // 
            // lbPlace
            // 
            this.lbPlace.AutoSize = true;
            this.lbPlace.Location = new System.Drawing.Point(109, 264);
            this.lbPlace.Name = "lbPlace";
            this.lbPlace.Size = new System.Drawing.Size(61, 13);
            this.lbPlace.TabIndex = 5;
            this.lbPlace.Text = "Birth Place:";
            // 
            // txtID
            // 
            this.txtID.Enabled = false;
            this.txtID.Location = new System.Drawing.Point(203, 118);
            this.txtID.Name = "txtID";
            this.txtID.ReadOnly = true;
            this.txtID.Size = new System.Drawing.Size(200, 20);
            this.txtID.TabIndex = 6;
            // 
            // txtName
            // 
            this.txtName.Location = new System.Drawing.Point(203, 169);
            this.txtName.Name = "txtName";
            this.txtName.Size = new System.Drawing.Size(200, 20);
            this.txtName.TabIndex = 7;
            // 
            // txtPlace
            // 
            this.txtPlace.Location = new System.Drawing.Point(203, 261);
            this.txtPlace.Name = "txtPlace";
            this.txtPlace.Size = new System.Drawing.Size(200, 20);
            this.txtPlace.TabIndex = 8;
            // 
            // dateBirth
            // 
            this.dateBirth.Location = new System.Drawing.Point(203, 211);
            this.dateBirth.Name = "dateBirth";
            this.dateBirth.Size = new System.Drawing.Size(200, 20);
            this.dateBirth.TabIndex = 9;
            // 
            // btnQuit
            // 
            this.btnQuit.Location = new System.Drawing.Point(24, 542);
            this.btnQuit.Name = "btnQuit";
            this.btnQuit.Size = new System.Drawing.Size(75, 23);
            this.btnQuit.TabIndex = 10;
            this.btnQuit.Text = "Close";
            this.btnQuit.UseVisualStyleBackColor = true;
            this.btnQuit.Click += new System.EventHandler(this.btnQuit_Click);
            // 
            // btnAdd
            // 
            this.btnAdd.Location = new System.Drawing.Point(108, 394);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(75, 23);
            this.btnAdd.TabIndex = 11;
            this.btnAdd.Text = "Add New";
            this.btnAdd.UseVisualStyleBackColor = true;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(291, 394);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 23);
            this.btnSave.TabIndex = 12;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // Manager
            // 
            this.ClientSize = new System.Drawing.Size(932, 577);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.btnAdd);
            this.Controls.Add(this.btnQuit);
            this.Controls.Add(this.dateBirth);
            this.Controls.Add(this.txtPlace);
            this.Controls.Add(this.txtName);
            this.Controls.Add(this.txtID);
            this.Controls.Add(this.lbPlace);
            this.Controls.Add(this.lbBirth);
            this.Controls.Add(this.lbName);
            this.Controls.Add(this.lbID);
            this.Controls.Add(this.lbTitle);
            this.Controls.Add(this.dgv);
            this.Name = "Manager";
            ((System.ComponentModel.ISupportInitialize)(this.dgv)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        private void btnQuit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (this.lbTitle.Text.Equals(AUTHORTITLE))
            {
                this.txtID.Text = "";
                this.txtName.Text = "";
                this.txtPlace.Text = "";
                
            }
            else
            {
                this.txtID.Text = "";
                this.txtName.Text = "";
            }
            btnAdd.Enabled = false;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            bool check = Checker();
            if (!check)
            {
                return;
            }
            if (this.txtID.Text.Equals(""))
            {
                this.Add();
            }
            else
            {
                this.Update();
            }
        }

        private bool Checker()
        {
            bool rs = true;
            if (txtName.Text.Equals(""))
            {
                rs = false;
                MessageBox.Show("Name can not blank!", "Error");
            }
            if(txtName.TextLength < 3 || txtName.TextLength > 50)
            {
                rs =false;
                MessageBox.Show("Name length must be 2 to 50", "Error");
            }
            if (this.lbTitle.Text.Equals(AUTHORTITLE))
            {
                DateTime now = DateTime.Now;
                now = ChangeYear(now, now.Year - 5);
                if(now < this.dateBirth.Value)
                {
                    rs = false;
                    MessageBox.Show("Author age cannot less 5 age","Error");
                }
                if (txtPlace.Text.Equals(""))
                {
                    rs = false;
                    MessageBox.Show("Place can not blank!", "Error");
                }
                if (txtPlace.TextLength <5 || txtPlace.TextLength>100)
                {
                    rs = false;
                    MessageBox.Show("Place length must be 5 to 100", "Error");

                }
            }
            return rs;
        }

        private void Update()
        {
            string id = this.txtID.Text;
            string name = this.txtName.Text;
            bool check = false;
            if (this.lbTitle.Text.Equals(AUTHORTITLE))
            {
                DateTime date = this.dateBirth.Value;
                string place = this.txtPlace.Text;
                check = BookDAO.updateAuthor(new AuthorDTO(id, name, date, place));
            }
            else
            {
                check = BookDAO.updateCategory(new CategoryDTO(id, name));
            }
            
            if (!check)
            {
                MessageBox.Show("Error!", "Error");
            }
            else
            {
                MessageBox.Show("Update success", "Success");
                SetupForm(this.isAuthor);
            }
        }

        private void Add()
        {
            string id = this.txtID.Text;
            string name = this.txtName.Text; 
            bool check = false;
            if (this.lbTitle.Text.Equals(AUTHORTITLE))
            {
                DateTime date = this.dateBirth.Value;
                string place = this.txtPlace.Text;
                check = BookDAO.addAuthor(new AuthorDTO(id, name, date, place));
            }
            else
            {
                check = BookDAO.addCategory(new CategoryDTO(id, name));
            }
            

            if (!check)
            {
                MessageBox.Show("Error!", "Error");
            }
            else
            {
                MessageBox.Show("Add success", "Success");
                SetupForm(this.isAuthor);
            }
        }

        private void dgv_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int index = this.dgv.CurrentRow.Index;
            if (this.lbTitle.Text.Equals(AUTHORTITLE))
            {
                this.txtID.Text = this.listAu[index].id;
                this.txtName.Text = this.listAu[index].name;
                this.txtPlace.Text = this.listAu[index].place;
                this.dateBirth.Value = this.listAu[index].birth;
            }
            else
            {
                this.txtID.Text = this.listCate[index].ID;
                this.txtName.Text = this.listCate[index].Name;
            }
            btnAdd.Enabled = true;
        }
    }
}
