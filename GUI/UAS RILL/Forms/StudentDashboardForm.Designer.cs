namespace TugasManager.Forms
{
    partial class StudentDashboardForm
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.Label lblWelcome;
        private System.Windows.Forms.ListView lstTugas;
        private System.Windows.Forms.ColumnHeader colJudul;
        private System.Windows.Forms.ColumnHeader colDeskripsi;
        private System.Windows.Forms.ColumnHeader colDeadline;
        private System.Windows.Forms.ColumnHeader colStatus; // ✅ Tambahan kolom status
        private System.Windows.Forms.Button btnKumpul;
        private System.Windows.Forms.Button button1;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            lblWelcome = new Label();
            lstTugas = new ListView();
            colJudul = new ColumnHeader();
            colDeskripsi = new ColumnHeader();
            colDeadline = new ColumnHeader();
            colStatus = new ColumnHeader();
            btnKumpul = new Button();
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
            lstTugas.Columns.AddRange(new ColumnHeader[] { colJudul, colDeskripsi, colDeadline, colStatus });
            lstTugas.FullRowSelect = true;
            lstTugas.Location = new Point(20, 50);
            lstTugas.Name = "lstTugas";
            lstTugas.Size = new Size(600, 200);
            lstTugas.TabIndex = 1;
            lstTugas.UseCompatibleStateImageBehavior = false;
            lstTugas.View = View.Details;
            // 
            // colJudul
            // 
            colJudul.Text = "Judul";
            colJudul.Width = 150;
            // 
            // colDeskripsi
            // 
            colDeskripsi.Text = "Deskripsi";
            colDeskripsi.Width = 200;
            // 
            // colDeadline
            // 
            colDeadline.Text = "Deadline";
            colDeadline.Width = 100;
            // 
            // colStatus
            // 
            colStatus.Text = "Status";
            colStatus.Width = 120;
            // 
            // btnKumpul
            // 
            btnKumpul.Location = new Point(20, 270);
            btnKumpul.Name = "btnKumpul";
            btnKumpul.Size = new Size(200, 30);
            btnKumpul.TabIndex = 2;
            btnKumpul.Text = "Kumpulkan Tugas";
            btnKumpul.UseVisualStyleBackColor = true;
            btnKumpul.Click += btnKumpul_Click;
            // 
            // button1
            // 
            button1.Location = new Point(520, 270);
            button1.Name = "button1";
            button1.Size = new Size(100, 30);
            button1.TabIndex = 3;
            button1.Text = "Logout";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // StudentDashboardForm
            // 
            ClientSize = new Size(650, 330);
            Controls.Add(button1);
            Controls.Add(lblWelcome);
            Controls.Add(lstTugas);
            Controls.Add(btnKumpul);
            Name = "StudentDashboardForm";
            Text = "Dashboard Siswa - Pengumpulan Tugas";
            ResumeLayout(false);
            PerformLayout();
        }
    }
}