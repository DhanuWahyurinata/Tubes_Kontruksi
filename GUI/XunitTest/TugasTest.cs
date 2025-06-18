using System;
using TugasManager.Models;
using Xunit;

namespace TugasManager.Tests
{
    public class TugasTests
    {
        [Fact]
        public void CanCreateTugas_WithAllProperties()
        {
            var deadline = new DateTime(2025, 7, 1);
            var tugas = new Tugas
            {
                Judul = "Tugas Matematika",
                Deskripsi = "Kerjakan soal dari halaman 20-30",
                TanggalDeadline = deadline
            };

            Assert.Equal("Tugas Matematika", tugas.Judul);
            Assert.Equal("Kerjakan soal dari halaman 20-30", tugas.Deskripsi);
            Assert.Equal(deadline, tugas.TanggalDeadline);
            Assert.False(string.IsNullOrWhiteSpace(tugas.Id));
        }

        [Fact]
        public void TugasId_ShouldBeUnique()
        {
            var tugas1 = new Tugas();
            var tugas2 = new Tugas();

            Assert.NotEqual(tugas1.Id, tugas2.Id);
            Assert.True(Guid.TryParse(tugas1.Id, out _));
            Assert.True(Guid.TryParse(tugas2.Id, out _));
        }

        [Fact]
        public void DefaultTanggalDeadline_ShouldBeMinValue()
        {
            var tugas = new Tugas();

            Assert.Equal(default(DateTime), tugas.TanggalDeadline);
        }
    }
}
