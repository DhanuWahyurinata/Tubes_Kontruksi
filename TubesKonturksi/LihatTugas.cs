using System.Diagnostics;

public static class LihatTugas
{
    public static void Tampilkan(List<Tugas> daftar)
    {
        // Precondition
        Debug.Assert(daftar != null, "List tugas tidak boleh null");

        if (daftar.Count == 0)
        {
            Console.WriteLine("Daftar tugas kosong.");
            return;
        }

        foreach (var tugas in daftar)
        {
            tugas.Validasi(); // invariant
            string status = tugas.Selesai ? "[Selesai]" : "[Belum]";
            Console.WriteLine($"{tugas.Id}. {status} {tugas.Deskripsi}");
        }

        // Postcondition (opsional: bisa tambahkan output count jika perlu)
    }
}
