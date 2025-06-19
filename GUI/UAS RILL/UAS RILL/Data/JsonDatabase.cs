using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using TugasManager.Models;

namespace TugasManager.Data
{
    public class JsonDatabase
    {
        private readonly string _filePath;
        private DatabaseData _data;

        public class DatabaseData
        {
            public List<User> Users { get; set; } = new();
            public List<Tugas> DaftarTugas { get; set; } = new();
            public List<Submission> KumpulanTugas { get; set; } = new();
        }

        public JsonDatabase(string filePath = "data.json")
        {
            _filePath = filePath;

            if (File.Exists(_filePath))
            {
                var json = File.ReadAllText(_filePath);
                _data = JsonSerializer.Deserialize<DatabaseData>(json) ?? new DatabaseData();
            }
            else
            {
                _data = new DatabaseData();
                _data.Users.Add(new User { Username = "guru1", Password = "1234", Nama = "Bu Guru", Role = "Admin" });
                _data.Users.Add(new User { Username = "siswa1", Password = "1234", Nama = "Andi", Role = "Siswa" });
                Save();
            }
        }

        public void Save()
        {
            var json = JsonSerializer.Serialize(_data, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(_filePath, json);
        }

        public List<User> Users => _data.Users;
        public List<Tugas> DaftarTugas => _data.DaftarTugas;
        public List<Submission> KumpulanTugas => _data.KumpulanTugas;

        public void AddUser(User user)
        {
            _data.Users.Add(user);
            Save();
        }

        public void UpdateUser(string oldUsername, User updatedUser)
        {
            var index = _data.Users.FindIndex(u => u.Username == oldUsername);
            if (index != -1)
            {
                _data.Users[index] = updatedUser;
                Save();
            }
        }

        public void DeleteUser(string username)
        {
            var userToRemove = _data.Users.FirstOrDefault(u => u.Username == username);
            if (userToRemove != null)
            {
                _data.Users.Remove(userToRemove);
                Save();
            }
        }

        public User GetUserByUsername(string username)
        {
            return _data.Users.FirstOrDefault(u => u.Username == username);
        }

        public bool UsernameExists(string username)
        {
            return _data.Users.Any(u => u.Username == username);
        }
    }

}
