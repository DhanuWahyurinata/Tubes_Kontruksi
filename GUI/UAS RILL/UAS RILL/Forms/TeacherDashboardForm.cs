using System;
using System.Windows.Forms;
using TugasManager.Models;
using TugasManager.Data;
using System.Linq;

namespace TugasManager.Forms
{
    public partial class TeacherDashboardForm : Form
    {
        private readonly User _currentUser;
        private readonly JsonDatabase _db;
        public TeacherDashboardForm(User user) : this(user, new JsonDatabase()) { }

        public TeacherDashboardForm(User user, JsonDatabase database)
        {
            InitializeComponent();
            _currentUser = user;
            _db = new JsonDatabase();
            lblWelcome.Text = $"Selamat datang, {_currentUser.Nama} (Guru)";
            LoadTugas();
        }

        private void LoadTugas()
        {
            lstTugas.Items.Clear();
            foreach (var tugas in _db.DaftarTugas.OrderByDescending(t => t.TanggalDeadline))
            {
                lstTugas.Items.Add($"{tugas.Judul} - Deadline: {tugas.TanggalDeadline:dd/MM/yyyy}");
            }
        }

        private void btnTambah_Click(object sender, EventArgs e)
        {
            var judul = txtJudul.Text.Trim();
            var deskripsi = txtDeskripsi.Text.Trim();
            var deadline = dtpDeadline.Value;

            if (string.IsNullOrWhiteSpace(judul))
            {
                MessageBox.Show("Judul tugas tidak boleh kosong.");
                return;
            }

            var tugas = new Tugas
            {
                Judul = judul,
                Deskripsi = deskripsi,
                TanggalDeadline = deadline
            };

            _db.DaftarTugas.Add(tugas);
            _db.Save();

            MessageBox.Show("Tugas berhasil ditambahkan.");
            txtJudul.Clear();
            txtDeskripsi.Clear();
            LoadTugas();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Apakah Anda yakin ingin logout?", "Konfirmasi Logout", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                this.Close(); // Akan kembali ke LoginForm jika belum di-close
            }
        }
    }
}
