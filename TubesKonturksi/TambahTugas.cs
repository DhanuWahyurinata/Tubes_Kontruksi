using System.Diagnostics;

public static class TambahTugas
{
    public static Tugas BuatTugas(List<Tugas> daftar)
    {
        // Precondition
        Debug.Assert(daftar != null, "List tugas tidak boleh null");

        Console.Write("Masukkan deskripsi tugas: ");
        string? input = Console.ReadLine()?.Trim();

        while (!IsValidDeskripsi(input))
        {
            Console.WriteLine("Deskripsi hanya boleh berisi huruf, angka, spasi dan minimal 3 karakter.");
            Console.Write("Masukkan deskripsi tugas: ");
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

    // Automata-based validation
    private static bool IsValidDeskripsi(string? input)
    {
        if (string.IsNullOrWhiteSpace(input) || input.Length < 3)
            return false;

        // Simulasi automata: valid hanya jika semua karakter adalah huruf/angka/spasi
        foreach (char c in input)
        {
            if (!char.IsLetterOrDigit(c) && c != ' ')
                return false;
        }

        return true;
    }
}
