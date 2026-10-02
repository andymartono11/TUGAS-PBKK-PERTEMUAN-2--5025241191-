# TUGAS-PBKK-PERTEMUAN-2--5025241191-

PBKK (C) › Pertemuan 2

# Pertemuan 2

Pengenalan .NET dan Pemrograman Console dengan C#

NamaAntonius Andy Martono

NRP5025241191

Mata KuliahPemrograman Berbasis Kerangka Kerja (PBKK) (C)

Pertemuan2 — .NET Console & C#

Pada pertemuan ini, saya mempelajari dasar C# melalui dua program console. Latihan pertama adalah **Hello World** untuk mengenali struktur program dan menampilkan teks. Latihan kedua adalah **Sistem Data Mahasiswa** yang menggunakan class, objek, list, percabangan, dan perulangan. Program dikerjakan menggunakan VS Code di Windows dengan target .NET 10.

## 0. Struktur Folder Proyek

Seluruh kode disimpan dalam satu folder tugas dengan struktur sebagai berikut:

Struktur Direktori

```
5025241191_Antonius Andy Martono_TugasPertemuan2\
├── Hello\
│   ├── Program.cs
│   └── Hello.csproj
├── DataMahasiswa\
│   ├── Program.cs
│   └── DataMahasiswa.csproj
└── .gitignore
```

## 1. Hello World

### 1.1 Kode Program

C# · Hello\\Program.cs

```
using System;

class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("Hello Dunia");
        Console.WriteLine("Nama  : Antonius Andy Martono");
        Console.WriteLine("NRP   : 5025241191");
    }
}
```

### 1.2 Output Program

Hello Dunia\
Nama : Antonius Andy Martono\
NRP : 5025241191

### 1.3 Cara Menjalankan

PowerShell / Terminal

```
cd "C:\KULIAH ITS\SEMESTER 5\PBKK\5025241191_Antonius Andy Martono_TugasPertemuan2\Hello"
dotnet run
```

### 1.4 Penjelasan Kode

`using System;` memungkinkan kelas dari namespace `System`, seperti `Console`, dipanggil langsung. `class Program` adalah wadah untuk method `Main`, yaitu titik masuk yang pertama dijalankan.

| Bagian | Penjelasan |
| --- | --- |
| `static` | Method dapat dipanggil tanpa membuat objek Program terlebih dahulu. |
| `void` | Method tidak mengembalikan nilai. |
| `Main` | Nama method yang menjadi titik masuk program. |
| `string[] args` | Menampung argumen dari command line. Pada latihan ini belum digunakan. |
| `Console.WriteLine()` | Menampilkan teks lalu berpindah ke baris baru. |

## 2. Sistem Data Mahasiswa

### 2.1 Gambaran Program

Program ini mengelola data mahasiswa melalui terminal. Setiap mahasiswa memiliki NIM, nama, program studi, dan IPK. Data tersimpan selama program berjalan menggunakan `List<Mahasiswa>`.

| Pilihan | Menu | Fungsi |
| --- | --- | --- |
| 1 | Tambah Mahasiswa | Memasukkan data mahasiswa ke dalam list. |
| 2 | Tampilkan Mahasiswa | Melihat semua data yang tersimpan. |
| 3 | Cari Mahasiswa | Mencari data berdasarkan NIM. |
| 4 | Hapus Mahasiswa | Menghapus data berdasarkan NIM. |
| 5 | Keluar | Mengakhiri program. |

### 2.2 Output Tampilan Menu

╔═══════════════════════════════════════════════════════════════╗\
║ SISTEM DATA MAHASISWA ║\
║ Antonius Andy Martono · 5025241191 ║\
╠═══════════════════════════════════════════════════════════════╣\
║ 1 Tambah Mahasiswa ║\
║ 2 Tampilkan Mahasiswa ║\
║ 3 Cari Mahasiswa ║\
║ 4 Hapus Mahasiswa ║\
║ 5 Keluar ║\
╚═══════════════════════════════════════════════════════════════╝

### 2.3 Cara Menjalankan

PowerShell / Terminal

```
cd "C:\KULIAH ITS\SEMESTER 5\PBKK\5025241191_Antonius Andy Martono_TugasPertemuan2\DataMahasiswa"
dotnet run
```

### 2.4 Namespace yang Digunakan

C#

```
using System;
using System.Collections.Generic;
using System.Text;

namespace DataMahasiswa { }
```

`System` untuk `Console`, `System.Collections.Generic` menyediakan `List<T>`, dan `System.Text` untuk mengatur encoding UTF-8 agar karakter bingkai menu bisa ditampilkan dengan benar di Windows.

### 2.5 Class Mahasiswa dan Constructor

C#

```
class Mahasiswa
{
    public string NIM  { get; set; }
    public string Nama { get; set; }
    public string Prodi{ get; set; }
    public double IPK  { get; set; }

    public Mahasiswa(string nim, string nama, string prodi, double ipk)
    {
        NIM = nim; Nama = nama; Prodi = prodi; IPK = ipk;
    }
}
```

NIM disimpan sebagai `string` karena tidak digunakan untuk perhitungan. IPK memakai `double` karena nilainya desimal. Constructor mengisi nilai awal saat objek dibuat dengan `new Mahasiswa(...)`.

### 2.6 Menyimpan Data dengan List

C#

```
static List<Mahasiswa> daftarMahasiswa = new List<Mahasiswa>();
```

`List<Mahasiswa>` menyimpan banyak objek mahasiswa sekaligus, bisa bertambah lewat `.Add()` dan berkurang lewat `.Remove()`. Diberi `static` agar bisa diakses oleh semua method di class `Program`.

### 2.7 Alur Menu Utama

C#

```
do
{
    TampilkanMenu();
    string input = BacaInput("Pilihan", WarnaBingkai);
    if (input == null) break;
    if (!int.TryParse(input, out pilihan)) pilihan = 0;

    switch (pilihan)
    {
        case 1: TambahMahasiswa();    break;
        case 2: TampilkanMahasiswa(); break;
        case 3: CariMahasiswa();      break;
        case 4: HapusMahasiswa();     break;
    }
} while (pilihan != 5);
```

Perulangan `do…while` membuat menu tampil terus sampai pengguna memilih keluar. `TryParse` mengamankan input: jika bukan angka, pilihan diisi `0`.

### 2.8 Validasi IPK

C#

```
while (true)
{
    string inputIpk = BacaInput("IPK (0 - 4)", WarnaTambah);
    if (inputIpk == null) return;
    if (double.TryParse(inputIpk, ..., out ipk) && ipk >= 0 && ipk <= 4)
        break;
    TulisBarisWarna("  [!] IPK harus berupa angka 0 – 4.", ConsoleColor.Red);
}
```

Perulangan terus meminta IPK hingga nilainya valid (0.00–4.00). Input seperti `abc` atau `5` ditolak dengan pesan merah.

### 2.9 Pencarian dan Penghapusan

C#

```
Mahasiswa mahasiswaDitemukan = null;
foreach (Mahasiswa m in daftarMahasiswa)
{
    if (m.NIM.Equals(nimCari, StringComparison.OrdinalIgnoreCase))
    { mahasiswaDitemukan = m; break; }
}
daftarMahasiswa.Remove(mahasiswaDitemukan); // untuk hapus
```

`OrdinalIgnoreCase` membuat perbandingan tidak membedakan huruf besar/kecil. `break` menghentikan pencarian segera setelah NIM cocok ditemukan.

### 2.10 Skema Warna

Bingkai menu utama

Tambah & pesan berhasil

Tampilkan data

Cari & tidak ditemukan

Hapus & input tidak valid

Keluar

💡 Hijau selalu menandakan operasi berhasil, merah untuk pesan error — terlepas dari warna menu yang aktif.


## 3. Pengisian Form Absensi

| Field | Isi |
| --- | --- |
| Tahun Kuliah | 2026 |
| Nama | Antonius Andy Martono |
| NRP | 5025241191 |
| Kelas | Pemrograman Berbasis Kerangka Kerja C - 2026 |
| Pertemuan | Pertemuan 2 |
| Deskripsi Latihan | Membuat program Hello World dan Sistem Data Mahasiswa berbasis console menggunakan C# (.NET 10) di Windows, mencakup class, objek, List\<T>, percabangan, perulangan, dan pewarnaan output terminal. |

---

Antonius Andy Martono · 5025241191 · PBKK (C) · Pertemuan 2
