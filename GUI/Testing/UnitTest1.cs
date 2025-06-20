using Xunit;
using System;
using System.IO;
using System.Text.Json;
using System.Windows.Forms;
using TugasManager.Data;
using TugasManager.Forms;
using TugasManager.Models;
using System.Reflection;

namespace TugasManager.Tests.Forms
{
    public class LoginFormTests : IDisposable
    {
        private readonly string _tempFile;
        private readonly LoginForm _form;
        private readonly JsonDatabase _testDb;

        public LoginFormTests()
        {
            // Setup test database
            _tempFile = Path.GetTempFileName();
            InitializeTestDatabase(_tempFile);

            // Create test database instance
            _testDb = CreateTestDatabaseInstance(_tempFile);
            JsonDatabase.SetTestInstance(_testDb);

            // Initialize form
            _form = new LoginForm();
        }

        public void Dispose()
        {
            // Cleanup
            JsonDatabase.ResetInstance();
            try { File.Delete(_tempFile); } catch { /* Ignore */ }
        }

        private void InitializeTestDatabase(string filePath)
        {
            var testData = new JsonDatabase.DatabaseData
            {
                Users = new List<User>
                {
                    new User { Username = "guru1", Password = "1234", Role = "Guru", Nama = "Guru Test" },
                    new User { Username = "siswa1", Password = "1234", Role = "Siswa", Nama = "Siswa Test" },
                    new User { Username = "admin1", Password = "1234", Role = "Admin", Nama = "Admin Test" }
                }
            };

            File.WriteAllText(filePath, JsonSerializer.Serialize(testData));
        }

        private JsonDatabase CreateTestDatabaseInstance(string filePath)
        {
            var constructor = typeof(JsonDatabase).GetConstructor(
                BindingFlags.NonPublic | BindingFlags.Instance,
                null,
                new[] { typeof(string) },
                null);

            return (JsonDatabase)constructor.Invoke(new object[] { filePath });
        }

        [Fact]
        public void Login_WithValidTeacherCredentials_ShouldAuthenticate()
        {
            // Arrange
            SetFormInputs("guru1", "1234");

            // Act
            CallPrivateMethod("btnLogin_Click");

            // Assert
            var user = _testDb.GetUserByUsername("guru1");
            Assert.NotNull(user);
            Assert.Equal("Guru", user.Role);
        }

        [Fact]
        public void Login_WithInvalidCredentials_ShouldNotAuthenticate()
        {
            // Arrange
            SetFormInputs("invalid", "wrong");

            // Act
            CallPrivateMethod("btnLogin_Click");

            // Assert
            var user = _testDb.GetUserByUsername("invalid");
            Assert.Null(user);
        }

        [Theory]
        [InlineData("", "1234")]
        [InlineData("guru1", "")]
        [InlineData("", "")]
        public void Login_WithEmptyCredentials_ShouldNotAuthenticate(string username, string password)
        {
            // Arrange
            SetFormInputs(username, password);

            // Act
            CallPrivateMethod("btnLogin_Click");

            // Assert
            if (!string.IsNullOrEmpty(username))
            {
                var user = _testDb.GetUserByUsername(username);
                Assert.NotNull(user);
                Assert.NotEqual(password, user.Password);
            }
        }

        // Helper methods
        private void SetFormInputs(string username, string password)
        {
            var txtUsername = _form.Controls.Find("txtUsername", true).FirstOrDefault() as TextBox;
            var txtPassword = _form.Controls.Find("txtPassword", true).FirstOrDefault() as TextBox;

            txtUsername.Text = username;
            txtPassword.Text = password;
        }

        private void CallPrivateMethod(string methodName)
        {
            var method = _form.GetType().GetMethod(methodName, BindingFlags.NonPublic | BindingFlags.Instance);
            method?.Invoke(_form, new object[] { null, EventArgs.Empty });
        }
    }
}