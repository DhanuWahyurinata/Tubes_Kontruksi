using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using FileHandlerLibraries;
using Models;
using TubesKonturksi;

[TestClass]
public class FileHandlerTests
{
    private List<string> tempFiles = new();

    private string GetTempFilePath()
    {
        var path = Path.Combine(Path.GetTempPath(), $"test_{Guid.NewGuid()}.json");
        tempFiles.Add(path);
        return path;
    }

    [TestCleanup]
    public void Cleanup()
    {
        foreach (var file in tempFiles)
        {
            try { if (File.Exists(file)) File.Delete(file); } catch { /* ignore */ }
        }
        tempFiles.Clear();
    }

    [TestMethod]
    public void Simpan_MembuatFileDenganDataValid()
    {
        var path = GetTempFilePath();
        var tugasList = new List<Tugas>
        {
            new Tugas { Id = 1, Deskripsi = "Tugas A", Selesai = false },
            new Tugas { Id = 2, Deskripsi = "Tugas B", Selesai = true }
        };

        FileHandler.Simpan(path, tugasList);

        Assert.IsTrue(File.Exists(path));
        string json = File.ReadAllText(path);
        var hasil = JsonSerializer.Deserialize<List<Tugas>>(json);

        Assert.IsNotNull(hasil);
        Assert.AreEqual(2, hasil.Count);
        Assert.AreEqual("Tugas A", hasil[0].Deskripsi);
        Assert.AreEqual("Tugas B", hasil[1].Deskripsi);
    }

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
        var path = GetTempFilePath();
        var tugasList = new List<Tugas>
        {
            new Tugas { Id = 1, Deskripsi = "Tugas X", Selesai = false },
            new Tugas { Id = 2, Deskripsi = "Tugas Y", Selesai = true }
        };
        FileHandler.Simpan(path, tugasList);

        var hasil = FileHandler.Muat(path);

        Assert.AreEqual(2, hasil.Count);
        Assert.AreEqual(1, hasil[0].Id);
        Assert.AreEqual("Tugas X", hasil[0].Deskripsi);
        Assert.IsFalse(hasil[0].Selesai);
    }

    [TestMethod]
    [ExpectedException(typeof(FileNotFoundException))]
    public void Muat_FileTidakAda_ThrowFileNotFoundException()
    {
        var path = GetTempFilePath();
        FileHandler.Muat(path); // File belum dibuat
    }

    [TestMethod]
    public void Muat_KembalikanListKosongJikaJsonNull()
    {
        var path = GetTempFilePath();
        File.WriteAllText(path, "null");

        var hasil = FileHandler.Muat(path);

        Assert.IsNotNull(hasil);
        Assert.AreEqual(0, hasil.Count);
    }

    [TestMethod]
    [ExpectedException(typeof(InvalidOperationException))]
    public void Muat_ValidasiTugasGagalKarenaDeskripsiKosong()
    {
        var path = GetTempFilePath();
        var tugasList = new List<Tugas> { new Tugas { Id = 1, Deskripsi = "" } };
        FileHandler.Simpan(path, tugasList);
        FileHandler.Muat(path);
    }

    [TestMethod]
    [ExpectedException(typeof(InvalidOperationException))]
    public void Validasi_IdTidakValid_ThrowInvalidOperationException()
    {
        var path = GetTempFilePath();
        var tugasList = new List<Tugas> { new Tugas { Id = 0, Deskripsi = "Test" } };
        FileHandler.Simpan(path, tugasList);
        FileHandler.Muat(path);
    }
}
