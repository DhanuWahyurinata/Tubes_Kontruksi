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
            // Buat file JSON sementara
            _tempFile = Path.Combine(Path.GetTempPath(), $"test_users_{Guid.NewGuid()}.json");

            // Buat data dummy kosong
            var dummyData = new JsonDatabase.DatabaseData
            {
                Users = new List<User>(),
                DaftarTugas = new List<Tugas>(),
                KumpulanTugas = new List<Submission>()
            };

            File.WriteAllText(_tempFile, JsonSerializer.Serialize(dummyData, new JsonSerializerOptions { WriteIndented = true }));

            _testDb = new JsonDatabase(_tempFile);
            _form = new AdminDashboardForm(_testDb);

            // Paksa inisialisasi ComboBox item agar bisa dipilih
            var cmb = _form.Controls.Find("cmbRole", true).FirstOrDefault() as ComboBox;
            cmb?.Items.AddRange(new[] { "Admin", "Siswa" });
        }

        [TestCleanup]
        public void Cleanup()
        {
            try { if (File.Exists(_tempFile)) File.Delete(_tempFile); } catch { /* ignore */ }
        }

        [TestMethod]
        public void TambahUser_ValidInput_BerhasilDitambahkan()
        {
            IsiForm("Test User", "testuser", "1234", "Admin");

            // Akses private method btnTambah_Click
            CallPrivateMethod("btnTambah_Click");

            var users = _testDb.Users;
            Assert.AreEqual(1, users.Count);
            Assert.AreEqual("testuser", users[0].Username);
        }

        [TestMethod]
        public void EditUser_ValidEdit_DataBerubah()
        {
            // Tambah user awal
            _testDb.AddUser(new User
            {
                Nama = "Old User",
                Username = "olduser",
                Password = "pass",
                Role = "Admin"
            });

            _form = new AdminDashboardForm(_testDb); // refresh form

            PilihUserDiList("olduser");

            IsiForm("Updated Name", "olduser", "newpass", "Siswa");

            CallPrivateMethod("btnEdit_Click");

            var index = GetUserIndexByUsername("olduser");
            Assert.AreNotEqual(-1, index);

            var updated = _testDb.Users[index];
            Assert.AreEqual("Updated Name", updated.Nama);
            Assert.AreEqual("newpass", updated.Password);
            Assert.AreEqual("Siswa", updated.Role);
        }
        [TestMethod]
        public void HapusUser_BerhasilDihapus()
        {
            // Tambahkan user
            var user = new User
            {
                Nama = "Hapus Saya",
                Username = "hapususer",
                Password = "pass",
                Role = "Siswa"
            };

            _testDb.AddUser(user);

            // Dapatkan index user
            var index = _testDb.Users.FindIndex(u => u.Username == "hapususer");
            Assert.AreNotEqual(-1, index, "User tidak ditemukan dalam database.");

            // Refresh ulang form agar data terbaru dimuat
            _form = new AdminDashboardForm(_testDb);

            // Pilih user di ListView
            PilihUserDiList("hapususer");

            //// Simulasi MessageBox.Show agar otomatis "Yes"
            //MessageBoxInterceptor.OverrideResult(DialogResult.Yes);

            // Simulasikan konfirmasi Yes (tanpa UI MessageBox)
            MessageBoxManager.SimulateYes();

            // Panggil tombol hapus
            CallPrivateMethod("btnHapus_Click");

            // Validasi user sudah tidak ada di database
            var deleted = _testDb.GetUserByUsername("hapususer");
            Assert.IsNull(deleted);
        }


        //[TestMethod]
        //public void HapusUser_BerhasilDihapus()
        //{
        //    _testDb.AddUser(new User
        //    {
        //        Nama = "Hapus Saya",
        //        Username = "hapususer",
        //        Password = "pass",
        //        Role = "Siswa"
        //    });

        //    _form = new AdminDashboardForm(_testDb);

        //    PilihUserDiList("hapususer");

        //    // Simulasikan konfirmasi Yes (tanpa UI MessageBox)
        //    MessageBoxManager.SimulateYes();

        //    CallPrivateMethod("btnHapus_Click");

        //    var cek = _testDb.GetUserByUsername("hapususer");
        //    Assert.IsNull(cek);
        //}

        // --- Utilitas bantu untuk testing ---

        private void IsiForm(string nama, string username, string password, string role)
        {
            (_form.Controls.Find("txtNama", true).FirstOrDefault() as TextBox)!.Text = nama;
            (_form.Controls.Find("txtUsername", true).FirstOrDefault() as TextBox)!.Text = username;
            (_form.Controls.Find("txtPassword", true).FirstOrDefault() as TextBox)!.Text = password;

            var cmb = _form.Controls.Find("cmbRole", true).FirstOrDefault() as ComboBox;
            cmb!.SelectedItem = role;
        }

        private void CallPrivateMethod(string methodName)
        {
            var method = _form.GetType()
                .GetMethod(methodName, BindingFlags.NonPublic | BindingFlags.Instance);
            method?.Invoke(_form, new object[] { null, EventArgs.Empty });
        }

        private void PilihUserDiList(string username)
        {
            var list = _form.Controls.Find("lstUsers", true).FirstOrDefault() as ListView;
            if (list == null) return;

            foreach (ListViewItem item in list.Items)
            {
                if (item.SubItems[1].Text == username)
                {
                    list.SelectedItems.Clear(); // pastikan hanya satu yang dipilih
                    item.Selected = true;
                    item.Focused = true;
                    item.EnsureVisible();
                    break;
                }
            }

            // Trigger perubahan selection agar form mengisi input field
            var method = _form.GetType()
                .GetMethod("lstUsers_SelectedIndexChanged", BindingFlags.NonPublic | BindingFlags.Instance);
            method?.Invoke(_form, new object[] { null, EventArgs.Empty });
        }

        /// <summary>
        /// Mendapatkan index user di list database berdasarkan username.
        /// </summary>
        private int GetUserIndexByUsername(string username)
        {
            return _testDb.Users.FindIndex(u => u.Username == username);
        }

    }

    /// <summary>
    /// Mensimulasikan klik "Yes" pada MessageBox
    /// </summary>
    public static class MessageBoxManager
    {
        public static void SimulateYes()
        {
            // Kode ini bisa diganti dengan mocking MessageBox untuk test otomatis
            // Jika tidak bisa dihindari, tambahkan library WinForms automation atau gunakan abstraction
            // Untuk sementara: override MessageBox.Show di form dengan delegate jika perlu
        }
    }
}
