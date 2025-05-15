using System.Diagnostics;

public static class TambahTugas
{
    public static Tugas BuatTugas(List<Tugas> daftar)
    {
        Console.Write("Masukkan deskripsi tugas: ");
        string? input = Console.ReadLine()?.Trim();

        // Precondition
        Debug.Assert(daftar != null, "List tugas tidak boleh null");
        while (string.IsNullOrWhiteSpace(input))
        {
            Console.Write("Deskripsi tidak boleh kosong. Coba lagi: ");
            input = Console.ReadLine()?.Trim();
        }

        int idBaru = daftar.Count > 0 ? daftar.Max(t => t.Id) + 1 : 1;

        Tugas tugasBaru = new Tugas { Id = idBaru, Deskripsi = input };
        tugasBaru.Validasi();

        daftar.Add(tugasBaru);

        // Postcondition
        Debug.Assert(daftar.Contains(tugasBaru), "Tugas harus berhasil ditambahkan");

        return tugasBaru;
    }
}
