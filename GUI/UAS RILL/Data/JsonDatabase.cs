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

        // Tambahkan field ini untuk testing
        private static JsonDatabase _testInstance;
        private static bool _isTesting = false;

        public class DatabaseData
        {
            public List<User> Users { get; set; } = new();
            public List<Tugas> DaftarTugas { get; set; } = new();
            public List<Submission> KumpulanTugas { get; set; } = new();
        }

        private DatabaseData _data;
        private readonly string _dataFilePath;

        // Constructor utama tetap private
        private JsonDatabase() : this(DataFile)
        {
        }

        // Constructor tambahan untuk testing (tetap private)
        private JsonDatabase(string dataFilePath)
        {
            _dataFilePath = dataFilePath;

            if (File.Exists(_dataFilePath))
            {
                var json = File.ReadAllText(_dataFilePath);
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

        // Properti Instance dengan dukungan testing
        public static JsonDatabase Instance
        {
            get
            {
                if (_isTesting)
                {
                    return _testInstance;
                }

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

        // Method untuk testing (tidak mempengaruhi logika bisnis utama)
        public static void SetTestInstance(JsonDatabase testInstance)
        {
            _testInstance = testInstance;
            _isTesting = true;
        }

        // Method untuk testing (tidak mempengaruhi logika bisnis utama)
        public static void ResetInstance()
        {
            _isTesting = false;
            _testInstance = null;
        }

        // Method utama tetap sama
        public void Save()
        {
            var json = JsonSerializer.Serialize(_data, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(_dataFilePath, json);
        }

        // Properti dan method lainnya tetap sama
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