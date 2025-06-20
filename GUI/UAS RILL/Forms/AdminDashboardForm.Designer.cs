namespace TugasManager.Forms
{
    partial class AdminDashboardForm
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Label lblNama, lblUsername, lblPassword, lblRole;
        private System.Windows.Forms.TextBox txtNama, txtUsername, txtPassword;
        private System.Windows.Forms.ComboBox cmbRole;
        private System.Windows.Forms.Button btnTambah, btnEdit, btnHapus;
        private System.Windows.Forms.ListView lstUsers;
        private System.Windows.Forms.ColumnHeader colNama, colUsername, colRole;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            lblTitle = new Label();
            lblNama = new Label();
            lblUsername = new Label();
            lblPassword = new Label();
            lblRole = new Label();
            txtNama = new TextBox();
            txtUsername = new TextBox();
            txtPassword = new TextBox();
            cmbRole = new ComboBox();
            btnTambah = new Button();
            btnEdit = new Button();
            btnHapus = new Button();
            lstUsers = new ListView();
            colNama = new ColumnHeader();
            colUsername = new ColumnHeader();
            colRole = new ColumnHeader();
            button1 = new Button();
            SuspendLayout();
            // 
            // lblTitle
            // 
            lblTitle.AutoSize = true;
            lblTitle.Location = new Point(146, 9);
            lblTitle.Name = "lblTitle";
            lblTitle.Size = new Size(134, 20);
            lblTitle.TabIndex = 0;
            lblTitle.Text = "Dashboard Admin ";
            // 
            // lblNama
            // 
            lblNama.Location = new Point(20, 50);
            lblNama.Name = "lblNama";
            lblNama.Size = new Size(100, 23);
            lblNama.TabIndex = 1;
            lblNama.Text = "Nama:";
            // 
            // lblUsername
            // 
            lblUsername.Location = new Point(20, 80);
            lblUsername.Name = "lblUsername";
            lblUsername.Size = new Size(100, 23);
            lblUsername.TabIndex = 2;
            lblUsername.Text = "Username:";
            // 
            // lblPassword
            // 
            lblPassword.Location = new Point(20, 110);
            lblPassword.Name = "lblPassword";
            lblPassword.Size = new Size(100, 23);
            lblPassword.TabIndex = 3;
            lblPassword.Text = "Password:";
            // 
            // lblRole
            // 
            lblRole.Location = new Point(20, 140);
            lblRole.Name = "lblRole";
            lblRole.Size = new Size(100, 23);
            lblRole.TabIndex = 4;
            lblRole.Text = "Role:";
            // 
            // txtNama
            // 
            txtNama.Location = new Point(121, 46);
            txtNama.Name = "txtNama";
            txtNama.Size = new Size(181, 27);
            txtNama.TabIndex = 5;
            // 
            // txtUsername
            // 
            txtUsername.Location = new Point(121, 77);
            txtUsername.Name = "txtUsername";
            txtUsername.Size = new Size(181, 27);
            txtUsername.TabIndex = 6;
            // 
            // txtPassword
            // 
            txtPassword.Location = new Point(121, 106);
            txtPassword.Name = "txtPassword";
            txtPassword.Size = new Size(181, 27);
            txtPassword.TabIndex = 7;
            // 
            // cmbRole
            // 
            cmbRole.Items.AddRange(new object[] { "Guru", "Siswa" });
            cmbRole.Location = new Point(121, 136);
            cmbRole.Name = "cmbRole";
            cmbRole.Size = new Size(181, 28);
            cmbRole.TabIndex = 8;
            // 
            // btnTambah
            // 
            btnTambah.Location = new Point(20, 180);
            btnTambah.Name = "btnTambah";
            btnTambah.Size = new Size(90, 30);
            btnTambah.TabIndex = 9;
            btnTambah.Text = "Tambah";
            btnTambah.Click += btnTambah_Click;
            // 
            // btnEdit
            // 
            btnEdit.Location = new Point(116, 180);
            btnEdit.Name = "btnEdit";
            btnEdit.Size = new Size(90, 30);
            btnEdit.TabIndex = 10;
            btnEdit.Text = "Edit";
            btnEdit.Click += btnEdit_Click;
            // 
            // btnHapus
            // 
            btnHapus.Location = new Point(212, 180);
            btnHapus.Name = "btnHapus";
            btnHapus.Size = new Size(90, 30);
            btnHapus.TabIndex = 11;
            btnHapus.Text = "Hapus";
            btnHapus.Click += btnHapus_Click;
            // 
            // lstUsers
            // 
            lstUsers.Columns.AddRange(new ColumnHeader[] { colNama, colUsername, colRole });
            lstUsers.FullRowSelect = true;
            lstUsers.Location = new Point(20, 220);
            lstUsers.Name = "lstUsers";
            lstUsers.Size = new Size(400, 200);
            lstUsers.TabIndex = 12;
            lstUsers.UseCompatibleStateImageBehavior = false;
            lstUsers.View = View.Details;
            lstUsers.SelectedIndexChanged += lstUsers_SelectedIndexChanged;
            // 
            // colNama
            // 
            colNama.Text = "Nama";
            colNama.Width = 150;
            // 
            // colUsername
            // 
            colUsername.Text = "Username";
            colUsername.Width = 120;
            // 
            // colRole
            // 
            colRole.Text = "Role";
            colRole.Width = 100;
            // 
            // button1
            // 
            button1.Location = new Point(330, 433);
            button1.Name = "button1";
            button1.Size = new Size(90, 30);
            button1.TabIndex = 13;
            button1.Text = "Logout";
            button1.Click += button1_Click;
            // 
            // AdminDashboardForm
            // 
            ClientSize = new Size(451, 475);
            Controls.Add(button1);
            Controls.Add(lblTitle);
            Controls.Add(lblNama);
            Controls.Add(lblUsername);
            Controls.Add(lblPassword);
            Controls.Add(lblRole);
            Controls.Add(txtNama);
            Controls.Add(txtUsername);
            Controls.Add(txtPassword);
            Controls.Add(cmbRole);
            Controls.Add(btnTambah);
            Controls.Add(btnEdit);
            Controls.Add(btnHapus);
            Controls.Add(lstUsers);
            Name = "AdminDashboardForm";
            Text = "Admin Dashboard";
            ResumeLayout(false);
            PerformLayout();
        }

        private Button button1;
    }
}
