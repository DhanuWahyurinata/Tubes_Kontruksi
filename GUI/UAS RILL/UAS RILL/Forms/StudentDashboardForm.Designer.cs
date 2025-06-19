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
            this.lblWelcome = new System.Windows.Forms.Label();
            this.lstTugas = new System.Windows.Forms.ListView();
            this.colJudul = new System.Windows.Forms.ColumnHeader();
            this.colDeskripsi = new System.Windows.Forms.ColumnHeader();
            this.colDeadline = new System.Windows.Forms.ColumnHeader();
            this.colStatus = new System.Windows.Forms.ColumnHeader(); // ✅ Kolom status
            this.btnKumpul = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // lblWelcome
            // 
            this.lblWelcome.AutoSize = true;
            this.lblWelcome.Location = new System.Drawing.Point(20, 20);
            this.lblWelcome.Name = "lblWelcome";
            this.lblWelcome.Size = new System.Drawing.Size(0, 20);
            this.lblWelcome.TabIndex = 0;
            // 
            // lstTugas
            // 
            this.lstTugas.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
                this.colJudul,
                this.colDeskripsi,
                this.colDeadline,
                this.colStatus}); // ✅ Kolom status ditambahkan
            this.lstTugas.FullRowSelect = true;
            this.lstTugas.Location = new System.Drawing.Point(20, 50);
            this.lstTugas.Name = "lstTugas";
            this.lstTugas.Size = new System.Drawing.Size(600, 200); // ✅ Ukuran disesuaikan
            this.lstTugas.TabIndex = 1;
            this.lstTugas.UseCompatibleStateImageBehavior = false;
            this.lstTugas.View = System.Windows.Forms.View.Details;
            // 
            // colJudul
            // 
            this.colJudul.Text = "Judul";
            this.colJudul.Width = 150;
            // 
            // colDeskripsi
            // 
            this.colDeskripsi.Text = "Deskripsi";
            this.colDeskripsi.Width = 200;
            // 
            // colDeadline
            // 
            this.colDeadline.Text = "Deadline";
            this.colDeadline.Width = 100;
            // 
            // colStatus
            // 
            this.colStatus.Text = "Status"; // ✅ Nama kolom status
            this.colStatus.Width = 120; // ✅ Ukuran kolom
            // 
            // btnKumpul
            // 
            this.btnKumpul.Location = new System.Drawing.Point(20, 270);
            this.btnKumpul.Name = "btnKumpul";
            this.btnKumpul.Size = new System.Drawing.Size(200, 30);
            this.btnKumpul.TabIndex = 2;
            this.btnKumpul.Text = "Kumpulkan Tugas";
            this.btnKumpul.UseVisualStyleBackColor = true;
            this.btnKumpul.Click += new System.EventHandler(this.btnKumpul_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(520, 270); // ✅ Disesuaikan agar tidak ketumpuk
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(100, 30);
            this.button1.TabIndex = 3;
            this.button1.Text = "Logout";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // StudentDashboardForm
            // 
            this.ClientSize = new System.Drawing.Size(650, 330); // ✅ Lebarkan form agar lebih luas
            this.Controls.Add(this.button1);
            this.Controls.Add(this.lblWelcome);
            this.Controls.Add(this.lstTugas);
            this.Controls.Add(this.btnKumpul);
            this.Name = "StudentDashboardForm";
            this.Text = "Dashboard Siswa - Pengumpulan Tugas";
            this.ResumeLayout(false);
            this.PerformLayout();
        }
    }
}
