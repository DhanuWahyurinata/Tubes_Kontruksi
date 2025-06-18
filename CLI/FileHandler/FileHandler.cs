using System.Diagnostics;
using System.Text.Json;
using Models;

namespace FileHandlerLibraries
{
    public static class FileHandler
    {
        public static void Simpan(string path, List<Tugas> daftar)
        {
            //Debug.Assert(!string.IsNullOrWhiteSpace(path), "Path tidak boleh kosong");
            if (string.IsNullOrWhiteSpace(path))
                throw new ArgumentException("Path tidak boleh kosong");

            string json = JsonSerializer.Serialize(daftar, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(path, json);
            //Debug.Assert(File.Exists(path), "File harus berhasil dibuat");
            if (!File.Exists(path))
                throw new IOException("File gagal dibuat");
        }

        public static List<Tugas> Muat(string path)
        {
            //Debug.Assert(File.Exists(path), "File tidak ditemukan");
            if (!File.Exists(path))
                throw new FileNotFoundException("File tidak ditemukan", path);

            string json = File.ReadAllText(path);
            var daftar = JsonSerializer.Deserialize<List<Tugas>>(json) ?? new List<Tugas>();

            // Invariant check
            foreach (var t in daftar)
            {
                t.Validasi();
            }

            return daftar;
        }
    }
}
