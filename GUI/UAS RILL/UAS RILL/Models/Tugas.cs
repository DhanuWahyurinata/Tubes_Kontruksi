using System;

namespace TugasManager.Models
{
    public class Tugas
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public string Judul { get; set; }
        public string Deskripsi { get; set; }
        public DateTime TanggalDeadline { get; set; }
    }
}
