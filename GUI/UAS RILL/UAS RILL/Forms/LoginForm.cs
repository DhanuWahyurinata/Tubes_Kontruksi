using System;
using System.Linq;
using System.Windows.Forms;
using TugasManager.Data;
using TugasManager.Models;

namespace TugasManager.Forms
{
    public partial class LoginForm : Form
    {
        public LoginForm()
        {
            InitializeComponent();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            string username = txtUsername.Text.Trim();
            string password = txtPassword.Text;

            // ✅ Selalu ambil data terbaru dari file JSON
            var db = new JsonDatabase();

            var user = db.Users.FirstOrDefault(u => u.Username == username && u.Password == password);
            if (user == null)
            {
                MessageBox.Show("Username atau password salah.");
                return;
            }

            this.Hide(); // Sembunyikan LoginForm

            Form dashboard = null;

            if (user.Role == "Admin")
                dashboard = new AdminDashboardForm();
            else if (user.Role == "Guru")
                dashboard = new TeacherDashboardForm(user);
            else if (user.Role == "Siswa")
                dashboard = new StudentDashboardForm(user);
            else
            {
                MessageBox.Show("Role tidak dikenali.");
                this.Show();
                return;
            }

            dashboard.ShowDialog(); // Modal form dashboard

            this.Show(); // Setelah logout, tampilkan kembali form login
        }
    }
}

//using System;
//using System.Linq;
//using System.Windows.Forms;
//using TugasManager.Data;
//using TugasManager.Models;

//namespace TugasManager.Forms
//{
//    public partial class LoginForm : Form
//    {
//        private readonly JsonDatabase _db;

//        public LoginForm()
//        {
//            InitializeComponent();
//            _db = new JsonDatabase();
//        }

//        private void btnLogin_Click(object sender, EventArgs e)
//        {
//            string username = txtUsername.Text.Trim();
//            string password = txtPassword.Text;

//            var user = _db.Users.FirstOrDefault(u => u.Username == username && u.Password == password);
//            if (user == null)
//            {
//                MessageBox.Show("Username atau password salah.");
//                return;
//            }

//            if (user.Role == "Guru")
//            {
//                Hide();
//                new TeacherDashboardForm(user).ShowDialog();
//                Show(); 
//            }
//            else if (user.Role == "Siswa")
//            {
//                Hide();
//                new StudentDashboardForm(user).ShowDialog();
//                Show(); 
//            }
//            else if (user.Role == "Admin")
//            {
//                Hide();
//                new AdminDashboardForm().ShowDialog();
//                Show(); 
//            }
//            else
//            {
//                MessageBox.Show("Role tidak dikenali.");
//            }
//        }
//    }
//}

