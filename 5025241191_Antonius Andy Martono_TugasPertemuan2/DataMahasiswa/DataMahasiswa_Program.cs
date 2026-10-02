using System;
using System.Collections.Generic;
using System.Text;

namespace DataMahasiswa
{
    class Mahasiswa
    {
        public string NIM { get; set; }
        public string Nama { get; set; }
        public string Prodi { get; set; }
        public double IPK { get; set; }

        public Mahasiswa(string nim, string nama, string prodi, double ipk)
        {
            NIM = nim;
            Nama = nama;
            Prodi = prodi;
            IPK = ipk;
        }
    }

    class Program
    {
        // ── Warna tema ──────────────────────────────────────────────────────
        const ConsoleColor WarnaBingkai = ConsoleColor.Cyan;
        const ConsoleColor WarnaTambah  = ConsoleColor.Green;
        const ConsoleColor WarnaDaftar  = ConsoleColor.Blue;
        const ConsoleColor WarnaCari    = ConsoleColor.Yellow;
        const ConsoleColor WarnaHapus   = ConsoleColor.Red;
        const ConsoleColor WarnaKeluar  = ConsoleColor.Gray;

        static List<Mahasiswa> daftarMahasiswa = new List<Mahasiswa>();

        // ── Entry point ──────────────────────────────────────────────────────
        static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;

            int pilihan = 0;
            try
            {
                do
                {
                    TampilkanMenu();
                    string input = BacaInput("Pilihan", WarnaBingkai);

                    if (input == null) break;

                    if (!int.TryParse(input, out pilihan))
                        pilihan = 0;

                    Console.WriteLine();

                    switch (pilihan)
                    {
                        case 1: TambahMahasiswa();    break;
                        case 2: TampilkanMahasiswa(); break;
                        case 3: CariMahasiswa();      break;
                        case 4: HapusMahasiswa();     break;
                        case 5:
                            TulisBarisWarna("  Sampai jumpa!", WarnaKeluar);
                            break;
                        default:
                            TulisBarisWarna("  [!] Pilihan tidak tersedia.", ConsoleColor.Red);
                            break;
                    }

                    if (pilihan != 5)
                    {
                        Console.WriteLine();
                        TulisWarna("  Tekan ", ConsoleColor.DarkGray);
                        TulisWarna("ENTER", ConsoleColor.White);
                        TulisBarisWarna(" untuk kembali ke menu...", ConsoleColor.DarkGray);
                        Console.ReadLine();
                    }

                } while (pilihan != 5);
            }
            finally
            {
                Console.ResetColor();
            }
        }

        // ── Menu utama ───────────────────────────────────────────────────────
        static void TampilkanMenu()
        {
            Console.Clear();
            TulisBatasPanel("╔", "═", "╗", WarnaBingkai);
            TulisBarisPanel("  SISTEM DATA MAHASISWA", WarnaBingkai, ConsoleColor.White);
            TulisBarisPanel("  Antonius Andy Martono · 5025241191", WarnaBingkai, ConsoleColor.DarkGray);
            TulisBatasPanel("╠", "═", "╣", WarnaBingkai);

            TulisOpsiMenu("1", "Tambah Mahasiswa",    WarnaTambah);
            TulisOpsiMenu("2", "Tampilkan Mahasiswa", WarnaDaftar);
            TulisOpsiMenu("3", "Cari Mahasiswa",      WarnaCari);
            TulisOpsiMenu("4", "Hapus Mahasiswa",     WarnaHapus);
            TulisOpsiMenu("5", "Keluar",              WarnaKeluar);

            TulisBatasPanel("╚", "═", "╝", WarnaBingkai);
            Console.WriteLine();
        }

        // ── Operasi 1: Tambah ────────────────────────────────────────────────
        static void TambahMahasiswa()
        {
            TulisJudul("[ TAMBAH MAHASISWA ]", WarnaTambah);

            string nim  = BacaInput("NIM",          WarnaTambah); if (nim  == null) return;
            string nama = BacaInput("Nama",         WarnaTambah); if (nama == null) return;
            string prodi= BacaInput("Program Studi",WarnaTambah); if (prodi== null) return;

            double ipk;
            while (true)
            {
                string inputIpk = BacaInput("IPK (0 - 4)", WarnaTambah);
                if (inputIpk == null) return;

                if (double.TryParse(inputIpk, System.Globalization.NumberStyles.Any,
                        System.Globalization.CultureInfo.InvariantCulture, out ipk)
                    && ipk >= 0 && ipk <= 4)
                    break;

                TulisBarisWarna("  [!] IPK harus berupa angka 0 – 4.", ConsoleColor.Red);
            }

            Mahasiswa mahasiswa = new Mahasiswa(nim, nama, prodi, ipk);
            daftarMahasiswa.Add(mahasiswa);

            Console.WriteLine();
            TulisBarisWarna("  [OK] Data mahasiswa berhasil ditambahkan.", ConsoleColor.Green);
        }

        // ── Operasi 2: Tampilkan ─────────────────────────────────────────────
        static void TampilkanMahasiswa()
        {
            TulisJudul("[ DAFTAR MAHASISWA ]", WarnaDaftar);

            if (daftarMahasiswa.Count == 0)
            {
                TulisBarisWarna("  [!] Belum ada data mahasiswa.", ConsoleColor.Yellow);
                return;
            }

            // Header tabel
            Console.Write("  ");
            TulisWarna(Kolom("NIM",   12) + " ", ConsoleColor.DarkCyan);
            TulisWarna(Kolom("Nama",  20) + " ", ConsoleColor.DarkGray);
            TulisWarna(Kolom("Prodi", 20) + " ", ConsoleColor.DarkGray);
            TulisBarisWarna("IPK".PadLeft(5),     ConsoleColor.DarkCyan);

            Console.Write("  ");
            TulisBarisWarna(new string('─', 61), ConsoleColor.DarkGray);

            foreach (Mahasiswa m in daftarMahasiswa)
            {
                Console.Write("  ");
                TulisWarna(Kolom(m.NIM,   12) + " ", ConsoleColor.Cyan);
                TulisWarna(Kolom(m.Nama,  20) + " ", ConsoleColor.White);
                TulisWarna(Kolom(m.Prodi, 20) + " ", ConsoleColor.Gray);
                TulisBarisWarna(m.IPK.ToString("F2").PadLeft(5), ConsoleColor.Cyan);
            }

            Console.WriteLine();
            TulisBarisWarna($"  Total: {daftarMahasiswa.Count} mahasiswa.", ConsoleColor.DarkGray);
        }

        // ── Operasi 3: Cari ──────────────────────────────────────────────────
        static void CariMahasiswa()
        {
            TulisJudul("[ CARI MAHASISWA ]", WarnaCari);

            string nimCari = BacaInput("Masukkan NIM", WarnaCari);
            if (nimCari == null) return;

            Mahasiswa mahasiswaDitemukan = null;
            foreach (Mahasiswa m in daftarMahasiswa)
            {
                if (m.NIM.Equals(nimCari, StringComparison.OrdinalIgnoreCase))
                {
                    mahasiswaDitemukan = m;
                    break;
                }
            }

            Console.WriteLine();
            if (mahasiswaDitemukan != null)
            {
                TulisBarisWarna("  [OK] Data ditemukan:", ConsoleColor.Green);
                TulisFieldHasil("NIM",          mahasiswaDitemukan.NIM);
                TulisFieldHasil("Nama",         mahasiswaDitemukan.Nama);
                TulisFieldHasil("Program Studi",mahasiswaDitemukan.Prodi);
                TulisFieldHasil("IPK",          mahasiswaDitemukan.IPK.ToString("F2"));
            }
            else
            {
                TulisBarisWarna("  [!] Mahasiswa dengan NIM tersebut tidak ditemukan.", ConsoleColor.Yellow);
            }
        }

        // ── Operasi 4: Hapus ─────────────────────────────────────────────────
        static void HapusMahasiswa()
        {
            TulisJudul("[ HAPUS MAHASISWA ]", WarnaHapus);

            string nimHapus = BacaInput("Masukkan NIM", WarnaHapus);
            if (nimHapus == null) return;

            Mahasiswa mahasiswaDitemukan = null;
            foreach (Mahasiswa m in daftarMahasiswa)
            {
                if (m.NIM.Equals(nimHapus, StringComparison.OrdinalIgnoreCase))
                {
                    mahasiswaDitemukan = m;
                    break;
                }
            }

            Console.WriteLine();
            if (mahasiswaDitemukan != null)
            {
                daftarMahasiswa.Remove(mahasiswaDitemukan);
                TulisBarisWarna("  [OK] Data mahasiswa berhasil dihapus.", ConsoleColor.Green);
            }
            else
            {
                TulisBarisWarna("  [!] Mahasiswa dengan NIM tersebut tidak ditemukan.", ConsoleColor.Yellow);
            }
        }

        // ── Helper: UI ───────────────────────────────────────────────────────
        static void TulisWarna(string teks, ConsoleColor warna)
        {
            ConsoleColor sebelumnya = Console.ForegroundColor;
            Console.ForegroundColor = warna;
            Console.Write(teks);
            Console.ForegroundColor = sebelumnya;
        }

        static void TulisBarisWarna(string teks, ConsoleColor warna)
        {
            TulisWarna(teks, warna);
            Console.WriteLine();
        }

        static void TulisBatasPanel(string kiri, string isi, string kanan, ConsoleColor warna)
        {
            TulisBarisWarna("  " + kiri + new string(isi[0], 63) + kanan, warna);
        }

        static void TulisBarisPanel(string isi, ConsoleColor warnaPanel, ConsoleColor warnaTeks)
        {
            TulisWarna("  ║ ", warnaPanel);
            TulisWarna(isi.PadRight(62), warnaTeks);
            TulisBarisWarna("║", warnaPanel);
        }

        static void TulisOpsiMenu(string nomor, string label, ConsoleColor warna)
        {
            TulisWarna("  ║  ", WarnaBingkai);
            ConsoleColor bg = Console.BackgroundColor;
            Console.BackgroundColor = warna;
            TulisWarna($" {nomor} ", ConsoleColor.Black);
            Console.BackgroundColor = bg;
            TulisWarna("  ", ConsoleColor.White);
            TulisWarna(label.PadRight(56), warna);
            TulisBarisWarna("║", WarnaBingkai);
        }

        static void TulisJudul(string judul, ConsoleColor warna)
        {
            TulisBarisWarna("  " + judul, warna);
            TulisBarisWarna("  " + new string('─', judul.Length), ConsoleColor.DarkGray);
        }

        static void TulisFieldHasil(string label, string nilai)
        {
            TulisWarna($"    {label,-15}: ", ConsoleColor.DarkGray);
            TulisBarisWarna(nilai, ConsoleColor.White);
        }

        static string BacaInput(string label, ConsoleColor warna)
        {
            TulisWarna($"  {label}: ", warna);
            return Console.ReadLine();
        }

        static string Kolom(string teks, int lebar)
        {
            if (teks.Length <= lebar) return teks.PadRight(lebar);
            return teks.Substring(0, lebar - 3) + "...";
        }
    }
}
