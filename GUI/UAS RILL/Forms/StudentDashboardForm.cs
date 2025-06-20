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
        private readonly JsonDatabase _db = JsonDatabase.Instance;

        public StudentDashboardForm(User user)
        {
            InitializeComponent();
            _currentUser = user;

            lblWelcome.Text = $"Halo, {_currentUser.Nama} (Siswa)";

            // Configure ListView
            lstTugas.View = View.Details;
            lstTugas.FullRowSelect = true;
            lstTugas.Columns.Clear();
            lstTugas.Columns.Add("Judul", 150);
            lstTugas.Columns.Add("Deskripsi", 250);
            lstTugas.Columns.Add("Deadline", 100);
            lstTugas.Columns.Add("Status", 150);

            LoadTugas();
        }

        public void LoadTugas()
        {
            lstTugas.Items.Clear();

            foreach (var tugas in _db.DaftarTugas.OrderByDescending(t => t.TanggalDeadline))
            {
                var submission = _db.KumpulanTugas
                    .FirstOrDefault(s => s.TugasId == tugas.Id && s.UsernameSiswa == _currentUser.Username);

                string status = submission == null
                    ? "Belum Dikumpulkan"
                    : submission.TanggalKumpul > tugas.TanggalDeadline
                        ? "Dikumpulkan (Terlambat)"
                        : "Dikumpulkan";

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
                MessageBox.Show("Pilih salah satu tugas terlebih dahulu.", "Peringatan",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var tugas = (Tugas)lstTugas.SelectedItems[0].Tag;

            using (OpenFileDialog ofd = new OpenFileDialog())
            {
                ofd.Title = "Pilih file tugas yang akan dikumpulkan";
                ofd.Filter = "All Files|*.*";

                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        bool isLate = DateTime.Now > tugas.TanggalDeadline;
                        string status = isLate ? "Dikumpulkan (Terlambat)" : "Dikumpulkan";

                        var submission = new Submission
                        {
                            TugasId = tugas.Id,
                            UsernameSiswa = _currentUser.Username,
                            FilePath = ofd.FileName,
                            TanggalKumpul = DateTime.Now,
                            Status = status
                        };

                        // Remove existing submission if any
                        var existing = _db.KumpulanTugas
                            .FirstOrDefault(s => s.TugasId == tugas.Id && s.UsernameSiswa == _currentUser.Username);
                        if (existing != null)
                        {
                            _db.KumpulanTugas.Remove(existing);
                        }

                        _db.KumpulanTugas.Add(submission);
                        _db.Save();

                        MessageBox.Show(
                            $"Tugas berhasil dikumpulkan{(isLate ? " (Terlambat)" : "")}!",
                            "Sukses",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Information);

                        LoadTugas();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Gagal mengumpulkan tugas: {ex.Message}", "Error",
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Application.Restart();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            LoadTugas();
        }
    }
}