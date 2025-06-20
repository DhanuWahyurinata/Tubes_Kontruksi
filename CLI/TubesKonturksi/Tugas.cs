using System.Diagnostics;

public class Tugas
{
    public int Id { get; set; }
    public string Deskripsi { get; set; } = "";
    public bool Selesai { get; set; } = false;

    public void Validasi()
    {
        Debug.Assert(Id > 0, "ID harus positif");
        Debug.Assert(!string.IsNullOrWhiteSpace(Deskripsi), "Deskripsi tidak boleh kosong");
    }
}
