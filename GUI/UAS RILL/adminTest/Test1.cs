using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.IO;
using System.Text.Json;
using System.Windows.Forms;
using System.Collections.Generic;
using TugasManager.Data;
using TugasManager.Forms;
using TugasManager.Models;
using System.Linq;
using System.Reflection;

namespace TugasManager.Tests
{
    [TestClass]
    public class AdminDashboardFormTests
    {
        private string _tempFile;
        private AdminDashboardForm _form;
        private JsonDatabase _testDb;

        [TestInitialize]
        public void Setup()
        {
            _tempFile = Path.Combine(Path.GetTempPath(), $"test_users_{Guid.NewGuid()}.json");
            var dummyData = new JsonDatabase.DatabaseData { /* ... */ };
            File.WriteAllText(_tempFile, JsonSerializer.Serialize(dummyData, new JsonSerializerOptions { WriteIndented = true }));
            _testDb = new JsonDatabase(_tempFile);
            _form = new AdminDashboardForm(_testDb);

            var cmb = _form.Controls.Find("cmbRole", true).FirstOrDefault() as ComboBox;
            cmb?.Items.AddRange(new[] { "Admin", "Guru", "Siswa" });
        }

        [TestCleanup]
        public void Cleanup()
        {
            try { if (File.Exists(_tempFile)) File.Delete(_tempFile); } catch { /* ignore */ }
        }

        // Test TambahUser tidak berubah dan seharusnya sudah berjalan dengan baik.
        [TestMethod]
        public void TambahUser_ValidInput_BerhasilDitambahkan()
        {
            // Arrange
            IsiForm("Test User", "testuser", "1234", "Admin");

            // Act
            CallPrivateMethod("btnTambah_Click");

            // Assert
            var users = _testDb.Users;
            Assert.AreEqual(1, users.Count);
            Assert.AreEqual("testuser", users[0].Username);
        }


        [TestMethod]
        public void TambahUser_DenganUsernameKosong_Gagal()
        {
            // Arrange
            IsiForm("Nama User", "", "password", "Admin");

            // Act: Memanggil method akan menampilkan MessageBox yang akan menghentikan test runner.
            // Kita hanya perlu memverifikasi bahwa tidak ada user yang ditambahkan.
            CallPrivateMethod("btnTambah_Click");

            // Assert
            Assert.AreEqual(0, _testDb.Users.Count, "User seharusnya tidak ditambahkan jika username kosong.");
        }

        [TestMethod]
        public void EditUser_ValidEdit_DataBerubah()
        {
            // Arrange
            var userToEdit = new User { Nama = "Old User", Username = "olduser", Password = "pass", Role = "Admin" };
            _testDb.AddUser(userToEdit);
            _form = new AdminDashboardForm(_testDb);

            // Act:
            // 1. Isi form dengan data yang sudah di-load (opsional, tapi praktik yang baik)
            PilihDanIsiForm("olduser");
            // 2. Ubah data di form
            IsiForm("Updated Name", "olduser", "newpass", "Siswa");
            // 3. Panggil method edit
            CallPrivateMethod("btnEdit_Click");

            // Assert
            var updatedUser = _testDb.GetUserByUsername("olduser");
            Assert.IsNotNull(updatedUser);
            Assert.AreEqual("Updated Name", updatedUser.Nama);
            Assert.AreEqual("Siswa", updatedUser.Role);
        }

        [TestMethod]
        public void HapusUser_BerhasilDihapus()
        {
            // Arrange
            var userToDelete = new User { Nama = "Hapus Saya", Username = "hapususer", Password = "pass", Role = "Siswa" };
            _testDb.AddUser(userToDelete);
            _form = new AdminDashboardForm(_testDb);

            // Act:
            // Langsung atur state user yang dipilih di form
            PilihDanIsiForm("hapususer");

            // Panggil method hapus (Anda harus klik "Yes" secara manual pada dialog)
            CallPrivateMethod("btnHapus_Click");

            // Assert
            var user = _testDb.GetUserByUsername("hapususer");
            Assert.IsNull(user, "User seharusnya sudah terhapus.");
        }


        // --- UTILITIES ---

        private void IsiForm(string nama, string username, string password, string role)
        {
            (_form.Controls.Find("txtNama", true).FirstOrDefault() as TextBox)!.Text = nama;
            (_form.Controls.Find("txtUsername", true).FirstOrDefault() as TextBox)!.Text = username;
            (_form.Controls.Find("txtPassword", true).FirstOrDefault() as TextBox)!.Text = password;
            (_form.Controls.Find("cmbRole", true).FirstOrDefault() as ComboBox)!.SelectedItem = role;
        }

        private void CallPrivateMethod(string methodName)
        {
            var method = _form.GetType().GetMethod(methodName, BindingFlags.NonPublic | BindingFlags.Instance);
            method?.Invoke(_form, new object[] { null, EventArgs.Empty });
        }

        /// <summary>
        /// Helper baru untuk langsung mengatur state internal form dan mengisi field-fieldnya.
        /// Ini adalah cara yang lebih andal untuk testing.
        /// </summary>
        private void PilihDanIsiForm(string username)
        {
            // 1. Dapatkan data user dari database
            var user = _testDb.GetUserByUsername(username);
            Assert.IsNotNull(user, $"User test '{username}' tidak ditemukan di database.");

            // 2. Gunakan Reflection untuk mengatur field private '_selectedUsername' di form
            var field = typeof(AdminDashboardForm).GetField("_selectedUsername", BindingFlags.NonPublic | BindingFlags.Instance);
            Assert.IsNotNull(field, "Field _selectedUsername tidak ditemukan di AdminDashboardForm.");
            field.SetValue(_form, username);

            // 3. Isi kontrol-kontrol UI di form sesuai data user
            IsiForm(user.Nama, user.Username, user.Password, user.Role);
        }
    }
}