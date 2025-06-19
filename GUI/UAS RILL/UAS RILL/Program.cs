using System;
using System.Windows.Forms;
using TugasManager.Forms;

namespace TugasManager
{
    static class Program
    {
        /// <summary>
        /// Titik masuk utama untuk aplikasi.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new LoginForm());
        }
    }
}
