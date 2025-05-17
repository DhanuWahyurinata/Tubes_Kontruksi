using System.Diagnostics;
using Models;
public static class TandaiSelesai
{
    public static bool UbahStatus<T>(List<Tugas> daftar, T id)
    {
        Debug.Assert(id != null, "ID tidak boleh null");

        int parsedId = Convert.ToInt32(id);

        var tugas = daftar.FirstOrDefault(t => t.Id == parsedId);
        if (tugas == null)
        {
            Console.WriteLine("Tugas tidak ditemukan.");
            return false;
        }

        tugas.Selesai = true;

        // Postcondition
        Debug.Assert(tugas.Selesai == true, "Tugas harus ditandai selesai");

        Console.WriteLine("Tugas telah ditandai selesai.");
        return true;
    }
}
