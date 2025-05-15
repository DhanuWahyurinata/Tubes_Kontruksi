using TubesKonstruksi;

public static class HapusTugas
{
    public static void HapusDenganPilihID(List<Tugas> daftar)
    {
        // Precondition
        Debug.Assert(daftar != null, "Daftar tugas tidak boleh null.");

        if (daftar.Count == 0)
        {
            Console.WriteLine("Daftar tugas kosong.");
            return;
        }

        Console.WriteLine("Daftar tugas:");
        foreach (var t in daftar)
        {
            Console.WriteLine($"[{t.Id}] {t.Deskripsi} ({(t.Selesai ? "Selesai" : "Belum")})");
        }

        Console.Write("\nMasukkan ID tugas yang ingin dihapus (pisahkan dengan spasi): ");
        string? input = Console.ReadLine();
        if (string.IsNullOrWhiteSpace(input))
        {
            Console.WriteLine("Tidak ada ID yang dimasukkan.");
            return;
        }

        var idStrings = input.Split(' ', StringSplitOptions.RemoveEmptyEntries);
        var ids = new List<int>();

        foreach (var s in idStrings)
        {
            if (int.TryParse(s, out int id))
                ids.Add(id);
        }

        var tugasDihapus = daftar.Where(t => ids.Contains(t.Id)).ToList();

        if (tugasDihapus.Count == 0)
        {
            Console.WriteLine("Tidak ada ID yang cocok.");
            return;
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
            foreach (var t in tugasDihapus)
            {
                daftar.Remove(t);
            }
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
