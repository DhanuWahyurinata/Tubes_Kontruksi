namespace TugasManager.Forms
{
    partial class TeacherDashboardForm
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.Label lblWelcome;
        private System.Windows.Forms.ListBox lstTugas;
        private System.Windows.Forms.TextBox txtJudul;
        private System.Windows.Forms.TextBox txtDeskripsi;
        private System.Windows.Forms.DateTimePicker dtpDeadline;
        private System.Windows.Forms.Button btnTambah;
        private System.Windows.Forms.Label lblJudul;
        private System.Windows.Forms.Label lblDeskripsi;
        private System.Windows.Forms.Label lblDeadline;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            lblWelcome = new Label();
            lstTugas = new ListBox();
            txtJudul = new TextBox();
            txtDeskripsi = new TextBox();
            dtpDeadline = new DateTimePicker();
            btnTambah = new Button();
            lblJudul = new Label();
            lblDeskripsi = new Label();
            lblDeadline = new Label();
            button1 = new Button();
            SuspendLayout();
            // 
            // lblWelcome
            // 
            lblWelcome.AutoSize = true;
            lblWelcome.Location = new Point(20, 20);
            lblWelcome.Name = "lblWelcome";
            lblWelcome.Size = new Size(0, 20);
            lblWelcome.TabIndex = 0;
            // 
            // lstTugas
            // 
            lstTugas.FormattingEnabled = true;
            lstTugas.Location = new Point(20, 50);
            lstTugas.Name = "lstTugas";
            lstTugas.Size = new Size(400, 144);
            lstTugas.TabIndex = 1;
            // 
            // txtJudul
            // 
            txtJudul.Location = new Point(120, 220);
            txtJudul.Name = "txtJudul";
            txtJudul.Size = new Size(300, 27);
            txtJudul.TabIndex = 3;
            // 
            // txtDeskripsi
            // 
            txtDeskripsi.Location = new Point(120, 250);
            txtDeskripsi.Name = "txtDeskripsi";
            txtDeskripsi.Size = new Size(300, 27);
            txtDeskripsi.TabIndex = 5;
            // 
            // dtpDeadline
            // 
            dtpDeadline.Location = new Point(120, 280);
            dtpDeadline.Name = "dtpDeadline";
            dtpDeadline.Size = new Size(200, 27);
            dtpDeadline.TabIndex = 7;
            // 
            // btnTambah
            // 
            btnTambah.Location = new Point(270, 378);
            btnTambah.Name = "btnTambah";
            btnTambah.Size = new Size(150, 30);
            btnTambah.TabIndex = 8;
            btnTambah.Text = "Tambah Tugas";
            btnTambah.Click += btnTambah_Click;
            // 
            // lblJudul
            // 
            lblJudul.Location = new Point(20, 220);
            lblJudul.Name = "lblJudul";
            lblJudul.Size = new Size(100, 20);
            lblJudul.TabIndex = 2;
            lblJudul.Text = "Judul Tugas:";
            // 
            // lblDeskripsi
            // 
            lblDeskripsi.Location = new Point(20, 250);
            lblDeskripsi.Name = "lblDeskripsi";
            lblDeskripsi.Size = new Size(100, 20);
            lblDeskripsi.TabIndex = 4;
            lblDeskripsi.Text = "Deskripsi:";
            // 
            // lblDeadline
            // 
            lblDeadline.Location = new Point(20, 280);
            lblDeadline.Name = "lblDeadline";
            lblDeadline.Size = new Size(100, 20);
            lblDeadline.TabIndex = 6;
            lblDeadline.Text = "Deadline:";
            // 
            // button1
            // 
            button1.Location = new Point(20, 378);
            button1.Name = "button1";
            button1.Size = new Size(86, 30);
            button1.TabIndex = 9;
            button1.Text = "Logout";
            button1.Click += button1_Click;
            // 
            // TeacherDashboardForm
            // 
            ClientSize = new Size(450, 440);
            Controls.Add(button1);
            Controls.Add(lblWelcome);
            Controls.Add(lstTugas);
            Controls.Add(lblJudul);
            Controls.Add(txtJudul);
            Controls.Add(lblDeskripsi);
            Controls.Add(txtDeskripsi);
            Controls.Add(lblDeadline);
            Controls.Add(dtpDeadline);
            Controls.Add(btnTambah);
            Name = "TeacherDashboardForm";
            Text = "Dashboard Guru - Pengumpulan Tugas";
            ResumeLayout(false);
            PerformLayout();
        }

        private Button button1;
    }
}
