using System;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using TugasManager.Models;
using TugasManager.Data;

namespace TugasManager.Forms
{
    public partial class StudentDashboardForm : Form
    {
        private readonly User _currentUser;
        private readonly JsonDatabase _db;
        // ✅ Constructor default (untuk aplikasi normal)
        public StudentDashboardForm(User user) : this(user, new JsonDatabase()) { }

        // ✅ Constructor tambahan (untuk unit testing atau custom DB file)
        public StudentDashboardForm(User user, JsonDatabase database)
        {
            InitializeComponent();
            _currentUser = user;
            _db = database;

            lblWelcome.Text = $"Halo, {_currentUser.Nama} (Siswa)";

            lstTugas.View = View.Details;
            lstTugas.FullRowSelect = true;
            lstTugas.Columns.Clear();
            lstTugas.Columns.Add("Judul", 150);
            lstTugas.Columns.Add("Deskripsi", 250);
            lstTugas.Columns.Add("Deadline", 100);
            lstTugas.Columns.Add("Status", 120);

            LoadTugas();
        }

        private void LoadTugas()
        {
            lstTugas.Items.Clear();

            foreach (var tugas in _db.DaftarTugas.OrderByDescending(t => t.TanggalDeadline))
            {
                var submission = _db.KumpulanTugas
                    .FirstOrDefault(s => s.TugasId == tugas.Id && s.UsernameSiswa == _currentUser.Username);

                string status;

                if (submission == null)
                {
                    status = "Belum Dikumpulkan";
                }
                else
                {
                    status = submission.TanggalKumpul > tugas.TanggalDeadline
                        ? "Terlambat"
                        : "Dikumpulkan";
                }

                var item = new ListViewItem(new string[]
                {
                    tugas.Judul,
                    tugas.Deskripsi,
                    tugas.TanggalDeadline.ToShortDateString(),
                    status
                });

                item.Tag = tugas;
                lstTugas.Items.Add(item);
            }
        }

        private void btnKumpul_Click(object sender, EventArgs e)
        {
            if (lstTugas.SelectedItems.Count == 0)
            {
                MessageBox.Show("Pilih salah satu tugas terlebih dahulu.");
                return;
            }

            var tugas = (Tugas)lstTugas.SelectedItems[0].Tag;

            using (OpenFileDialog ofd = new OpenFileDialog())
            {
                ofd.Title = "Pilih file tugas yang akan dikumpulkan";
                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    var filePath = ofd.FileName;

                    var submission = new Submission
                    {
                        TugasId = tugas.Id,
                        UsernameSiswa = _currentUser.Username,
                        FilePath = filePath,
                        TanggalKumpul = DateTime.Now,
                        Status = DateTime.Now > tugas.TanggalDeadline
                            ? "Terlambat"
                            : "Dikumpulkan"
                    };

                    // Hapus submission sebelumnya jika ada
                    var existing = _db.KumpulanTugas
                        .FirstOrDefault(s => s.TugasId == tugas.Id && s.UsernameSiswa == _currentUser.Username);
                    if (existing != null)
                    {
                        _db.KumpulanTugas.Remove(existing);
                    }

                    _db.KumpulanTugas.Add(submission);
                    _db.Save();
                    MessageBox.Show("Tugas berhasil dikumpulkan!");
                    LoadTugas();
                }
            }
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
