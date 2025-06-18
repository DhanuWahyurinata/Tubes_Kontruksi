using System;
using System.Collections.Generic;
using Models;
public static class LihatTugas
{
    // Table untuk mapping status ke teks
    private static readonly Dictionary<bool, string> StatusMapping = new()
    {
        { false, "[ ] Belum Selesai" },
        { true,  "[X] Selesai" }
    };

    public static void Tampilkan(List<Tugas> daftar)
    {
        // Defensive programming: handle list null atau kosong
        if (daftar == null || daftar.Count == 0)
        {
            Console.WriteLine("Daftar tugas kosong.");
            return;
        }

        Console.WriteLine("Daftar Tugas:");
        foreach (var tugas in daftar)
        {
            string status = StatusMapping[tugas.Selesai]; // Table-driven mapping
            Console.WriteLine($"{tugas.Id}. {status} - {tugas.Deskripsi}");
        }
    }
}