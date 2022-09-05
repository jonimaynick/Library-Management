namespace LabPRN
{
    partial class AdminForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.label1 = new System.Windows.Forms.Label();
            this.btnBookManager = new System.Windows.Forms.Button();
            this.btnRentalManager = new System.Windows.Forms.Button();
            this.btnUserManager = new System.Windows.Forms.Button();
            this.btnLogout = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(143, 33);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(186, 25);
            this.label1.TabIndex = 0;
            this.label1.Text = "Wellcome Admin";
            // 
            // btnBookManager
            // 
            this.btnBookManager.Location = new System.Drawing.Point(42, 108);
            this.btnBookManager.Name = "btnBookManager";
            this.btnBookManager.Size = new System.Drawing.Size(128, 26);
            this.btnBookManager.TabIndex = 1;
            this.btnBookManager.Text = "Book Manager";
            this.btnBookManager.UseVisualStyleBackColor = true;
            this.btnBookManager.Click += new System.EventHandler(this.btnBookManager_Click);
            // 
            // btnRentalManager
            // 
            this.btnRentalManager.Location = new System.Drawing.Point(42, 187);
            this.btnRentalManager.Name = "btnRentalManager";
            this.btnRentalManager.Size = new System.Drawing.Size(128, 27);
            this.btnRentalManager.TabIndex = 2;
            this.btnRentalManager.Text = "Rental Manager";
            this.btnRentalManager.UseVisualStyleBackColor = true;
            this.btnRentalManager.Click += new System.EventHandler(this.btnRentalManager_Click);
            // 
            // btnUserManager
            // 
            this.btnUserManager.Location = new System.Drawing.Point(289, 108);
            this.btnUserManager.Name = "btnUserManager";
            this.btnUserManager.Size = new System.Drawing.Size(128, 26);
            this.btnUserManager.TabIndex = 3;
            this.btnUserManager.Text = "User Manager";
            this.btnUserManager.UseVisualStyleBackColor = true;
            this.btnUserManager.Click += new System.EventHandler(this.btnUserManager_Click);
            // 
            // btnLogout
            // 
            this.btnLogout.Location = new System.Drawing.Point(289, 187);
            this.btnLogout.Name = "btnLogout";
            this.btnLogout.Size = new System.Drawing.Size(128, 27);
            this.btnLogout.TabIndex = 4;
            this.btnLogout.Text = "Logout";
            this.btnLogout.UseVisualStyleBackColor = true;
            this.btnLogout.Click += new System.EventHandler(this.btnLogout_Click);
            // 
            // AdminForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(479, 279);
            this.Controls.Add(this.btnLogout);
            this.Controls.Add(this.btnUserManager);
            this.Controls.Add(this.btnRentalManager);
            this.Controls.Add(this.btnBookManager);
            this.Controls.Add(this.label1);
            this.Name = "AdminForm";
            this.Text = "Formm";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnBookManager;
        private System.Windows.Forms.Button btnRentalManager;
        private System.Windows.Forms.Button btnUserManager;
        private System.Windows.Forms.Button btnLogout;
    }
}