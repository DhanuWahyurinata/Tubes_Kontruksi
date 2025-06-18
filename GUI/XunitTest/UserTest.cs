using TugasManager.Models;
using Xunit;

namespace TugasManager.Tests
{
    public class UserTests
    {
        [Fact]
        public void CanCreateUser_WithAllProperties()
        {
            var user = new User
            {
                Username = "siswa123",
                Password = "secret",
                Nama = "Andi",
                Role = "Siswa"
            };

            Assert.Equal("siswa123", user.Username);
            Assert.Equal("secret", user.Password);
            Assert.Equal("Andi", user.Nama);
            Assert.Equal("Siswa", user.Role);
        }

        [Fact]
        public void CanChangeUserProperties()
        {
            var user = new User();

            user.Username = "guru456";
            user.Password = "12345";
            user.Nama = "Bu Sari";
            user.Role = "Guru";

            Assert.Equal("guru456", user.Username);
            Assert.Equal("12345", user.Password);
            Assert.Equal("Bu Sari", user.Nama);
            Assert.Equal("Guru", user.Role);
        }

        [Fact]
        public void DefaultUser_ShouldHaveNullProperties()
        {
            var user = new User();

            Assert.Null(user.Username);
            Assert.Null(user.Password);
            Assert.Null(user.Nama);
            Assert.Null(user.Role);
        }
    }
}
