using System;
using System.Collections.Generic;
using System.IO;
using Xunit;

namespace TambahTugasTests
{
    public class TambahTugasUnitTest
    {
        [Fact]
        public void BuatTugas_InputValid_TugasBerhasilDitambahkan()
        {
            // Arrange
            var daftarTugas = new List<Tugas>();
            var inputDeskripsi = "Mengerjakan PR";
            var input = new StringReader(inputDeskripsi + Environment.NewLine);
            Console.SetIn(input);

            // Act
            var tugas = TambahTugas.BuatTugas(daftarTugas);

            // Assert
            Assert.NotNull(tugas);
            Assert.Equal(1, tugas.Id);
            Assert.Equal("Mengerjakan PR", tugas.Deskripsi);
            Assert.Contains(tugas, daftarTugas);
        }

        [Fact]
        public void BuatTugas_InputKosong_DimintaUlangSampaiValid()
        {
            // Arrange
            var daftarTugas = new List<Tugas>();
            var input = new StringReader("\n \n\nBelajar xUnit Lho Ini\n");
            Console.SetIn(input);

            // Act
            var tugas = TambahTugas.BuatTugas(daftarTugas);

            // Assert
            Assert.Equal("Belajar xUnit Lho Ini", tugas.Deskripsi);
            Assert.Single(daftarTugas);
        }
    }
}
