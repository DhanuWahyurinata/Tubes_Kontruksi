using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Diagnostics;
using TubesKonturksi;

[TestClass]
public class FileHandlerTests
{
    private string testFilePath = "test_tugas.json";

    [TestCleanup]
    public void Cleanup() //untuk membersihkan atau mereset file json
    {
        if (File.Exists(testFilePath))
            File.Delete(testFilePath);
    }

    [TestMethod]
    public void Simpan_MembuatFileDenganDataValid()
    {
        // Arrange
        var tugasList = new List<Tugas>
        {
            new Tugas { Id = 1, Deskripsi = "Tugas A", Selesai = false },
            new Tugas { Id = 2, Deskripsi = "Tugas B", Selesai = true }
        };

        // Act
        FileHandler.Simpan(testFilePath, tugasList);

        // Assert
        Assert.IsTrue(File.Exists(testFilePath));
        string json = File.ReadAllText(testFilePath);
        var hasil = JsonSerializer.Deserialize<List<Tugas>>(json);

        Assert.IsNotNull(hasil);
        Assert.AreEqual(2, hasil.Count);
        Assert.AreEqual("Tugas A", hasil[0].Deskripsi);
        Assert.AreEqual("Tugas B", hasil[1].Deskripsi);
    }

    //[TestMethod]
    //[ExpectedException(typeof(AssertionException))]
    //public void Simpan_PathKosong_AssertFailure()
    //{
    //    var tugasList = new List<Tugas> { new Tugas { Id = 1, Deskripsi = "Tes" } };
    //    FileHandler.Simpan("", tugasList);
    //}

    [TestMethod]
    [ExpectedException(typeof(ArgumentException))]
    public void Simpan_PathKosong_ThrowArgumentException()
    {
        var tugasList = new List<Tugas> { new Tugas { Id = 1, Deskripsi = "Tes" } };
        FileHandler.Simpan("", tugasList);
    }

    [TestMethod]
    public void Muat_MengembalikanListTugasValid()
    {
        // Arrange
        var tugasList = new List<Tugas>
        {
            new Tugas { Id = 1, Deskripsi = "Tugas X", Selesai = false },
            new Tugas { Id = 2, Deskripsi = "Tugas Y", Selesai = true }
        };
        FileHandler.Simpan(testFilePath, tugasList);

        // Act
        var hasil = FileHandler.Muat(testFilePath);

        // Assert
        Assert.AreEqual(2, hasil.Count);
        Assert.AreEqual(1, hasil[0].Id);
        Assert.AreEqual("Tugas X", hasil[0].Deskripsi);
        Assert.IsFalse(hasil[0].Selesai);
    }

    //[TestMethod]
    //[ExpectedException(typeof(AssertionException))]
    //public void Muat_FileTidakAda_AssertFailure()
    //{
    //    FileHandler.Muat("file_tidak_ada.json");
    //}

    [TestMethod]
    [ExpectedException(typeof(FileNotFoundException))]
    public void Muat_FileTidakAda_ThrowFileNotFoundException()
    {
        FileHandler.Muat("tidak_ada.json");
    }

    [TestMethod]
    public void Muat_KembalikanListKosongJikaJsonNull()
    {
        // Arrange
        File.WriteAllText(testFilePath, "null");

        // Act
        var hasil = FileHandler.Muat(testFilePath);

        // Assert
        Assert.IsNotNull(hasil);
        Assert.AreEqual(0, hasil.Count);
    }

    //[TestMethod]
    //[ExpectedException(typeof(AssertionException))]
    //public void Muat_ValidasiTugasGagalKarenaDeskripsiKosong()
    //{
    //    // Arrange
    //    var tugasList = new List<Tugas> { new Tugas { Id = 1, Deskripsi = "" } };
    //    FileHandler.Simpan(testFilePath, tugasList);

    //    // Act
    //    FileHandler.Muat(testFilePath); // Akan panggil Validasi() dan gagal
    //}

    [TestMethod]
    [ExpectedException(typeof(InvalidOperationException))]
    public void Muat_ValidasiTugasGagalKarenaDeskripsiKosong()
    {
        var tugasList = new List<Tugas> { new Tugas { Id = 1, Deskripsi = "" } };
        FileHandler.Simpan(testFilePath, tugasList);

        FileHandler.Muat(testFilePath);
    }

    //[TestMethod]
    //[ExpectedException(typeof(AssertionException))]
    //public void Muat_ValidasiTugasGagalKarenaIdNegatif()
    //{
    //    var tugasList = new List<Tugas> { new Tugas { Id = -1, Deskripsi = "Validasi ID" } };
    //    FileHandler.Simpan(testFilePath, tugasList);

    //    FileHandler.Muat(testFilePath); // Akan trigger Debug.Assert di Validasi
    //}

    [TestMethod]
    [ExpectedException(typeof(InvalidOperationException))]
    public void Validasi_IdTidakValid_ThrowInvalidOperationException()
    {
        var tugasList = new List<Tugas> { new Tugas { Id = 0, Deskripsi = "Test" } };
        FileHandler.Simpan(testFilePath, tugasList);
        FileHandler.Muat(testFilePath);
    }
}
