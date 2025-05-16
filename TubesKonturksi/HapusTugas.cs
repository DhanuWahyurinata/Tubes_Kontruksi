using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using TubesKonturksi;

public static class HapusTugas
{
    public static void HapusDenganPilihID(List<Tugas> daftar)
    {
        // Precondition
        Debug.Assert(daftar != null, "Daftar tugas tidak boleh null.");

        if (!daftar.Any())
        {
            Console.WriteLine("Daftar tugas kosong.");
            return;
        }

        Console.WriteLine("Daftar tugas:");
        foreach (var t in daftar)
        {
            Console.WriteLine($"[{t.Id}] {t.Deskripsi} ({(t.Selesai ? "Selesai" : "Belum")})");
        }

        Console.Write("\nMasukkan ID tugas yang ingin dihapus (contoh: 1 2 3): ");
        string? input = Console.ReadLine();

        if (string.IsNullOrWhiteSpace(input))
        {
            Console.WriteLine("Tidak ada ID yang dimasukkan.");
            return;
        }

        // Parsing dan validasi ID
        var ids = input.Split(' ', StringSplitOptions.RemoveEmptyEntries)
                       .Select(s => int.TryParse(s, out var id) ? id : (int?)null)
                       .Where(id => id.HasValue)
                       .Select(id => id!.Value)
                       .Distinct()
                       .ToList();

        var tugasDihapus = daftar.Where(t => ids.Contains(t.Id)).ToList();
        var idTidakDitemukan = ids.Except(tugasDihapus.Select(t => t.Id));

        if (!tugasDihapus.Any())
        {
            Console.WriteLine("Tidak ada ID yang cocok.");
            return;
        }

        if (idTidakDitemukan.Any())
        {
            Console.WriteLine("ID yang tidak ditemukan: " + string.Join(", ", idTidakDitemukan));
        }

        Console.WriteLine("\nTugas berikut akan dihapus:");
        foreach (var t in tugasDihapus)
        {
            Console.WriteLine($"[{t.Id}] {t.Deskripsi}");
        }

        Console.Write("Yakin ingin menghapus semuanya? (y/n): ");
        string? konfirmasi = Console.ReadLine()?.ToLower();

        if (konfirmasi == "y")
        {
            daftar.RemoveAll(t => ids.Contains(t.Id));
            Console.WriteLine($"{tugasDihapus.Count} tugas berhasil dihapus.");
        }
        else
        {
            Console.WriteLine("Penghapusan dibatalkan.");
        }

        // Postcondition
        Debug.Assert(daftar.Count >= 0, "Jumlah tugas tidak boleh negatif.");
    }
}
