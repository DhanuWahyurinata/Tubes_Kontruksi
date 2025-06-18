using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using TugasManager.Models;

namespace TugasManager.Data
{
    public sealed class JsonDatabase
    {
        private const string DataFile = "data.json";
        private static JsonDatabase _instance;
        private static readonly object _lock = new object();

        public class DatabaseData
        {
            public List<User> Users { get; set; } = new();
            public List<Tugas> DaftarTugas { get; set; } = new();
            public List<Submission> KumpulanTugas { get; set; } = new();
        }

        private DatabaseData _data;

        // Private constructor untuk mencegah instantiasi langsung
        private JsonDatabase()
        {
            if (File.Exists(DataFile))
            {
                var json = File.ReadAllText(DataFile);
                _data = JsonSerializer.Deserialize<DatabaseData>(json) ?? new DatabaseData();
            }
            else
            {
                _data = new DatabaseData();
                _data.Users.Add(new User { Username = "guru1", Password = "1234", Nama = "Bu Guru", Role = "Guru" });
                _data.Users.Add(new User { Username = "siswa1", Password = "1234", Nama = "Andi", Role = "Siswa" });
                Save();
            }
        }

        // Property untuk mengakses instance singleton
        public static JsonDatabase Instance
        {
            get
            {
                if (_instance == null)
                {
                    lock (_lock)
                    {
                        if (_instance == null)
                        {
                            _instance = new JsonDatabase();
                        }
                    }
                }
                return _instance;
            }
        }

        public void Save()
        {
            var json = JsonSerializer.Serialize(_data, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(DataFile, json);
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