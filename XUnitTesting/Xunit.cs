using System;
using Xunit;
using ManajemenTugas;

public class TugasTests
{
    [Fact]
    public void Validasi_ProperTugas_ShouldPass()
    {
        // Arrange
        var tugas = new Tugas
        {
            Id = 1,
            Deskripsi = "Mengerjakan laporan",
            Selesai = false
        };

        // Act & Assert
        var exception = Record.Exception(() => tugas.Validasi());

        // Assert: Harus tidak melempar exception (berarti valid)
        Assert.Null(exception); // ✅ PASS
    }

    [Fact]
    public void Validasi_NegativeId_ShouldFail()
    {
        // Arrange
        var tugas = new Tugas
        {
            Id = -1,
            Deskripsi = "Tugas salah",
            Selesai = false
        };

        // Act & Assert
        var exception = Record.Exception(() => tugas.Validasi());

        // Assert: Harus melempar exception karena ID negatif
        Assert.NotNull(exception); // ❌ FAIL (expected to fail)
    }

    [Fact]
    public void Validasi_EmptyDescription_ShouldFail()
    {
        // Arrange
        var tugas = new Tugas
        {
            Id = 5,
            Deskripsi = "", // kosong
            Selesai = false
        };

        // Act & Assert
        var exception = Record.Exception(() => tugas.Validasi());

        // Assert: Harus melempar exception karena deskripsi kosong
        Assert.NotNull(exception); // ❌ FAIL (expected to fail)
    }
}
