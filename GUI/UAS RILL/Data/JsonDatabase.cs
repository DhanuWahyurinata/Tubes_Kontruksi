using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
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

            [ContractInvariantMethod]
            private void ObjectInvariant()
            {
                Contract.Invariant(Users != null);
                Contract.Invariant(DaftarTugas != null);
                Contract.Invariant(KumpulanTugas != null);
            }
        }

        private DatabaseData _data;

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

            Contract.Assert(_data.Users != null && _data.DaftarTugas != null && _data.KumpulanTugas != null,
                "Database gagal diinisialisasi.");
        }

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
            Contract.Requires(_data != null, "Data tidak boleh null sebelum disimpan.");

            var json = JsonSerializer.Serialize(_data, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(DataFile, json);
        }

        public List<User> Users
        {
            get
            {
                Contract.Ensures(Contract.Result<List<User>>() != null);
                return _data.Users;
            }
        }

        public List<Tugas> DaftarTugas
        {
            get
            {
                Contract.Ensures(Contract.Result<List<Tugas>>() != null);
                return _data.DaftarTugas;
            }
        }

        public List<Submission> KumpulanTugas
        {
            get
            {
                Contract.Ensures(Contract.Result<List<Submission>>() != null);
                return _data.KumpulanTugas;
            }
        }

        public void AddUser(User user)
        {
            Contract.Requires(user != null, "User tidak boleh null.");
            Contract.Requires(!string.IsNullOrWhiteSpace(user.Username), "Username tidak boleh kosong.");
            Contract.Ensures(UsernameExists(user.Username), "User gagal ditambahkan.");

            _data.Users.Add(user);
            Save();
        }

        public void UpdateUser(string oldUsername, User updatedUser)
        {
            Contract.Requires(!string.IsNullOrWhiteSpace(oldUsername), "Username lama tidak boleh kosong.");
            Contract.Requires(updatedUser != null, "Updated user tidak boleh null.");
            Contract.Ensures(UsernameExists(updatedUser.Username), "User gagal diperbarui.");

            var index = _data.Users.FindIndex(u => u.Username == oldUsername);
            if (index != -1)
            {
                _data.Users[index] = updatedUser;
                Save();
            }
        }

        public void DeleteUser(string username)
        {
            Contract.Requires(!string.IsNullOrWhiteSpace(username), "Username tidak boleh kosong.");
            Contract.Ensures(!UsernameExists(username), "User gagal dihapus.");

            var userToRemove = _data.Users.FirstOrDefault(u => u.Username == username);
            if (userToRemove != null)
            {
                _data.Users.Remove(userToRemove);
                Save();
            }
        }

        public User GetUserByUsername(string username)
        {
            Contract.Requires(!string.IsNullOrWhiteSpace(username), "Username tidak boleh kosong.");

            return _data.Users.FirstOrDefault(u => u.Username == username);
        }

        public bool UsernameExists(string username)
        {
            Contract.Requires(!string.IsNullOrWhiteSpace(username), "Username tidak boleh kosong.");
            Contract.Ensures(!Contract.Result<bool>() || _data.Users.Any(u => u.Username == username),
                "Data tidak konsisten: username seharusnya ada.");

            return _data.Users.Any(u => u.Username == username);
        }

        [ContractInvariantMethod]
        private void ObjectInvariant()
        {
            Contract.Invariant(_data != null);
            
            Contract.Invariant(_data.Users != null);

            Contract.Invariant(_data.DaftarTugas != null);
            Contract.Invariant(_data.KumpulanTugas != null);
        }
    }
}
