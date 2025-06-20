using System;
using System.Linq;
using System.Windows.Forms;
using TugasManager.Data;
using TugasManager.Models;

namespace TugasManager.Forms
{
    public partial class AdminDashboardForm : Form
    {
        private readonly JsonDatabase _db = JsonDatabase.Instance;

        public AdminDashboardForm()
        {
            InitializeComponent();
            LoadUsersToListView();
        }

        private void LoadUsersToListView()
        {
            lstUsers.Items.Clear();
            foreach (var user in _db.Users)
            {
                var item = new ListViewItem(user.Nama);
                item.SubItems.Add(user.Username);
                item.SubItems.Add(user.Role);
                lstUsers.Items.Add(item);
            }
        }

        private void btnTambah_Click(object sender, EventArgs e)
        {
            var nama = txtNama.Text.Trim();
            var username = txtUsername.Text.Trim();
            var password = txtPassword.Text.Trim();
            var role = cmbRole.Text;

            if (string.IsNullOrWhiteSpace(nama) || string.IsNullOrWhiteSpace(username) ||
                string.IsNullOrWhiteSpace(password) || string.IsNullOrWhiteSpace(role))
            {
                MessageBox.Show("Semua field harus diisi.");
                return;
            }

            if (_db.UsernameExists(username))
            {
                MessageBox.Show("Username sudah ada.");
                return;
            }

            var newUser = new User
            {
                Nama = nama,
                Username = username,
                Password = password,
                Role = role
            };

            _db.AddUser(newUser);
            LoadUsersToListView();
            ClearForm();
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (lstUsers.SelectedItems.Count == 0)
            {
                MessageBox.Show("Pilih user yang ingin diedit.");
                return;
            }

            var selectedItem = lstUsers.SelectedItems[0];
            var oldUsername = selectedItem.SubItems[1].Text;

            var nama = txtNama.Text.Trim();
            var username = txtUsername.Text.Trim();
            var password = txtPassword.Text.Trim();
            var role = cmbRole.Text;

            if (string.IsNullOrWhiteSpace(nama) || string.IsNullOrWhiteSpace(username) ||
                string.IsNullOrWhiteSpace(password) || string.IsNullOrWhiteSpace(role))
            {
                MessageBox.Show("Semua field harus diisi.");
                return;
            }

            var updatedUser = new User
            {
                Nama = nama,
                Username = username,
                Password = password,
                Role = role
            };

            _db.UpdateUser(oldUsername, updatedUser);
            LoadUsersToListView();
            ClearForm();
        }

        private void btnHapus_Click(object sender, EventArgs e)
        {
            if (lstUsers.SelectedItems.Count == 0)
            {
                MessageBox.Show("Pilih user yang ingin dihapus.");
                return;
            }

            var username = lstUsers.SelectedItems[0].SubItems[1].Text;

            var confirm = MessageBox.Show($"Hapus user '{username}'?", "Konfirmasi", MessageBoxButtons.YesNo);
            if (confirm == DialogResult.Yes)
            {
                _db.DeleteUser(username);
                LoadUsersToListView();
                ClearForm();
            }
        }

        private void lstUsers_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lstUsers.SelectedItems.Count == 0) return;

            var selected = lstUsers.SelectedItems[0];
            txtNama.Text = selected.SubItems[0].Text;
            txtUsername.Text = selected.SubItems[1].Text;
            cmbRole.SelectedItem = selected.SubItems[2].Text;

            var user = _db.GetUserByUsername(selected.SubItems[1].Text);
            if (user != null)
            {
                txtPassword.Text = user.Password;
            }
        }

        private void ClearForm()
        {
            txtNama.Text = "";
            txtUsername.Text = "";
            txtPassword.Text = "";
            cmbRole.SelectedIndex = -1;
            lstUsers.SelectedItems.Clear();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Application.Restart();
        }
    }
}