using System.IO;
using System.Linq;
using TugasManager.Data;
using TugasManager.Models;
using Xunit;

namespace TugasManager.Tests
{
    public class JsonDatabaseTests
    {
        private readonly string testDataFile = "test_data.json";

        private JsonDatabase CreateIsolatedDatabase()
        {
            // Backup original file if exists
            if (File.Exists("data.json"))
                File.Copy("data.json", "backup_data.json", true);

            // Use fresh file
            File.Delete("data.json");
            return JsonDatabase.Instance;
        }

        [Fact]
        public void AddUser_ShouldAddNewUser()
        {
            var db = CreateIsolatedDatabase();
            var user = new User
            {
                Username = "testuser",
                Password = "pass",
                Nama = "Test User",
                Role = "Siswa"
            };

            db.AddUser(user);
            var result = db.GetUserByUsername("testuser");

            Assert.NotNull(result);
            Assert.Equal("Test User", result.Nama);
        }

        [Fact]
        public void UsernameExists_ShouldReturnTrueIfExists()
        {
            var db = CreateIsolatedDatabase();
            db.AddUser(new User { Username = "existinguser", Password = "123", Nama = "Ada", Role = "Siswa" });

            bool exists = db.UsernameExists("existinguser");
            Assert.True(exists);
        }

        [Fact]
        public void DeleteUser_ShouldRemoveUser()
        {
            var db = CreateIsolatedDatabase();
            db.AddUser(new User { Username = "deleteuser", Password = "123", Nama = "Delete", Role = "Guru" });

            db.DeleteUser("deleteuser");
            var user = db.GetUserByUsername("deleteuser");

            Assert.Null(user);
        }

        [Fact]
        public void UpdateUser_ShouldUpdateUserData()
        {
            var db = CreateIsolatedDatabase();
            db.AddUser(new User { Username = "updateuser", Password = "123", Nama = "Old", Role = "Siswa" });

            var updatedUser = new User { Username = "updateuser", Password = "999", Nama = "Updated", Role = "Guru" };
            db.UpdateUser("updateuser", updatedUser);

            var result = db.GetUserByUsername("updateuser");
            Assert.Equal("Updated", result.Nama);
            Assert.Equal("999", result.Password);
            Assert.Equal("Guru", result.Role);
        }
    }
}
