using System;

namespace TugasManager.Models
{
    public class Submission
    {
        public string TugasId { get; set; }
        public string UsernameSiswa { get; set; }
        public string FilePath { get; set; }
        public DateTime TanggalKumpul { get; set; }
        public string Status { get; set; }
    }
}
