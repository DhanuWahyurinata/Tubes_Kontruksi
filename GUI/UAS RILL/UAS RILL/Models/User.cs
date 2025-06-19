namespace TugasManager.Models
{
    public class User
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string Nama { get; set; }
        public string Role { get; set; } // "Guru" atau "Siswa"
    }
}
