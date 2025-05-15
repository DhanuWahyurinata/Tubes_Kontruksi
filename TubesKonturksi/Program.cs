using System;
using System.Collections.Generic;

class Program
{
    static List<Tugas> daftarTugas = new List<Tugas>();
    const string filePath = "tugas.json";

    static void Main(string[] args)
    {

        if (File.Exists(filePath))
        {
            daftarTugas = FileHandler.Muat(filePath);
            Console.WriteLine("Data tugas berhasil dimuat.");
        }

        while (true)
        {
            TampilkanMenu();
            string pilihan = Console.ReadLine()?.Trim() ?? "";

            switch (pilihan)
            {
                case "1":
                    TambahTugas.BuatTugas(daftarTugas);
                    break;
                case "2":
                    LihatTugas.Tampilkan(daftarTugas);
                    break;
                case "3":
                    Console.Write("Masukkan ID tugas yang ingin ditandai selesai: ");
                    string? inputSelesai = Console.ReadLine();
                    TandaiSelesai.UbahStatus(daftarTugas, inputSelesai);
                    break;
                case "4":
                    HapusTugas.HapusDenganPilihID(daftarTugas);
                    break;
                case "5":
                    FileHandler.Simpan(filePath, daftarTugas);
                    Console.WriteLine("Data berhasil disimpan.");
                    break;
                case "6":
                    Console.WriteLine("Keluar program.");
                    return;
                default:
                    Console.WriteLine("Pilihan tidak dikenali.");
                    break;
            }

            Console.WriteLine("\nTekan Enter untuk kembali ke menu...");
            Console.ReadLine();
            Console.Clear();
        }
    }

    static void TampilkanMenu()
    {
        Console.WriteLine("==================== MANAJEMEN  TUGAS ===============");
        Console.WriteLine("1. Tambah Tugas");
        Console.WriteLine("2. Lihat Daftar Tugas");
        Console.WriteLine("3. Tandai Tugas Selesai");
        Console.WriteLine("4. Hapus Tugas");
        Console.WriteLine("5. Simpan Tugas");
        Console.WriteLine("6. Keluar");
        Console.Write("Pilih menu : ");
    }
}
