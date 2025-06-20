using System;
using System.Linq;
using System.Windows.Forms;
using TugasManager.Data;
using TugasManager.Models;

namespace TugasManager.Forms
{
    public partial class LoginForm : Form
    {
        private readonly JsonDatabase _db;

        public LoginForm()
        {
            InitializeComponent();
            // Menggunakan instance singleton dari JsonDatabase
            _db = JsonDatabase.Instance;
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            string username = txtUsername.Text.Trim();
            string password = txtPassword.Text;

            var user = _db.Users.FirstOrDefault(u => u.Username == username && u.Password == password);
            if (user == null)
            {
                MessageBox.Show("Username atau password salah.");
                return;
            }

            switch (user.Role)
            {
                case "Guru":
                    Hide();
                    new TeacherDashboardForm(user).ShowDialog();
                    Show();
                    break;
                case "Siswa":
                    Hide();
                    new StudentDashboardForm(user).ShowDialog();
                    Show();
                    break;
                case "Admin":
                    Hide();
                    new AdminDashboardForm().ShowDialog();
                    Show();
                    break;
                default:
                    MessageBox.Show("Role tidak dikenali.");
                    break;
            }
        }
    }
}