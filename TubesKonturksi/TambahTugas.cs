using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

public static class TambahTugas
{
    public static Tugas BuatTugas(List<Tugas> daftar)
    {
        // Precondition: pastikan daftar tidak null
        Debug.Assert(daftar != null, "List tugas tidak boleh null");

        Console.Write("Masukkan deskripsi tugas: ");
        string? input = Console.ReadLine()?.Trim();

        // Validasi input menggunakan automata
        while (!IsValidDeskripsi(input))
        {
            Console.WriteLine("Deskripsi hanya boleh berisi huruf, angka, spasi dan minimal 3 karakter.");
            Console.Write("Masukkan deskripsi tugas: ");
            input = Console.ReadLine()?.Trim();
        }

        // Tentukan ID baru berdasarkan daftar yang ada
        int idBaru = daftar.Count > 0 ? daftar.Max(t => t.Id) + 1 : 1;

        // Buat objek tugas baru dan validasi
        Tugas tugasBaru = new Tugas { Id = idBaru, Deskripsi = input };
        tugasBaru.Validasi();

        // Tambahkan tugas baru ke daftar
        daftar.Add(tugasBaru);

        // Postcondition: pastikan tugas baru sudah ditambahkan ke daftar
        Debug.Assert(daftar.Contains(tugasBaru), "Tugas harus berhasil ditambahkan");

        return tugasBaru;
    }

    // Automata-based validation untuk deskripsi tugas
    private enum State { Start, Valid, Invalid }

    private static bool IsValidDeskripsi(string? input)
    {
        if (string.IsNullOrWhiteSpace(input))
            return false;

        State currentState = State.Start;
        int validCharCount = 0;

        foreach (char c in input)
        {
            bool isValidChar = char.IsLetterOrDigit(c) || c == ' ';

            switch (currentState)
            {
                case State.Start:
                    if (isValidChar)
                    {
                        validCharCount++;
                        if (validCharCount >= 3)
                            currentState = State.Valid;
                        // tetap di Start jika kurang dari 3 karakter valid
                    }
                    else
                    {
                        currentState = State.Invalid;
                    }
                    break;

                case State.Valid:
                    if (!isValidChar)
                    {
                        currentState = State.Invalid;
                    }
                    // tetap di Valid jika karakter valid
                    break;

                case State.Invalid:
                    // Sudah invalid, tetap invalid
                    break;
            }

            if (currentState == State.Invalid)
                break; // keluar loop langsung jika invalid
        }

        // Terima jika state Valid di akhir
        return currentState == State.Valid;
    }
}

