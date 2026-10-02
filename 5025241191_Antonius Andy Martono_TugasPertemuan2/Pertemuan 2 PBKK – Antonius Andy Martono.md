PBKK (C) › Pertemuan 2

Tugas Terselesaikan

# Pertemuan 2

Pengenalan .NET dan Pemrograman Console dengan C#

Nama Antonius Andy Martono

NRP 5025241191

Mata Kuliah Pemrograman Berbasis Kerangka Kerja (PBKK) (C)

Pertemuan 2 — .NET Console & C#

Pada pertemuan ini, saya mempelajari dasar C# melalui dua program console. Latihan pertama adalah **Hello World** untuk mengenali struktur program dan menampilkan teks. Latihan kedua adalah **Sistem Data Mahasiswa** yang menggunakan class, objek, list, percabangan, dan perulangan. Program dikerjakan menggunakan VS Code di Ubuntu dengan target .NET 10.

## 1. Hello World

### 1.1 Kode Program

C# · Hello/Program.cs

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

### 1.2 Penjelasan Kode

`using System;` memungkinkan kelas dari namespace `System`, seperti `Console`, dipanggil langsung tanpa prefix penuh. `class Program` adalah wadah untuk method `Main`, yaitu titik masuk yang pertama kali dijalankan saat aplikasi dieksekusi.

| Bagian | Penjelasan |
| --- | --- |
| `static` | Method dapat dipanggil tanpa membuat objek `Program` terlebih dahulu. |
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

### 2.2 Namespace yang Digunakan

C#

```
using System;
using System.Collections.Generic;
using System.Text;

namespace DataMahasiswa { }
```

`System` untuk `Console`, `System.Collections.Generic` menyediakan `List<T>`, dan `System.Text` untuk mengatur encoding UTF-8 agar karakter bingkai menu bisa ditampilkan.

### 2.3 Class Mahasiswa dan Constructor

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
        NIM   = nim;
        Nama  = nama;
        Prodi = prodi;
        IPK   = ipk;
    }
}
```

NIM, nama, dan prodi memakai `string`. NIM tetap disimpan sebagai teks karena tidak digunakan untuk perhitungan. IPK memakai `double` karena nilainya bisa berupa bilangan desimal. Constructor mengisi nilai awal saat objek dibuat dengan `new Mahasiswa(...)`.

### 2.4 Menyimpan Data dengan List

C#

```
static List<Mahasiswa> daftarMahasiswa = new List<Mahasiswa>();
```

`List<Mahasiswa>` menyimpan banyak objek mahasiswa sekaligus. Jumlah isinya fleksibel—bisa bertambah lewat `.Add()` dan berkurang lewat `.Remove()`. Variabel ini diberi `static` agar bisa digunakan oleh semua method statis di class `Program`.

### 2.5 Alur Menu Utama

C#

```
do
{
    TampilkanMenu();
    string input = BacaInput("Pilihan", WarnaBingkai);

    if (input == null) break;          // Ctrl+D
    if (!int.TryParse(input, out pilihan))
        pilihan = 0;

    switch (pilihan)
    {
        case 1: TambahMahasiswa();    break;
        case 2: TampilkanMahasiswa(); break;
        case 3: CariMahasiswa();      break;
        case 4: HapusMahasiswa();     break;
    }
} while (pilihan != 5);
```

Perulangan `do…while` membuat menu tampil terus sampai pengguna memilih keluar. `TryParse` mengamankan input: jika bukan angka, pilihan diisi `0` dan masuk ke default handler.

### 2.6 Validasi IPK

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

Perulangan terus meminta IPK hingga nilainya valid (angka `0.00`–`4.00`). Input seperti `abc` atau `5` akan ditolak dengan pesan merah.

### 2.7 Pencarian dan Penghapusan

C#

```
Mahasiswa mahasiswaDitemukan = null;
foreach (Mahasiswa m in daftarMahasiswa)
{
    if (m.NIM.Equals(nimCari, StringComparison.OrdinalIgnoreCase))
    {
        mahasiswaDitemukan = m;
        break;
    }
}
// Untuk hapus:
daftarMahasiswa.Remove(mahasiswaDitemukan);
```

`OrdinalIgnoreCase` membuat perbandingan tidak membedakan huruf besar/kecil. `break` menghentikan pencarian segera setelah cocok ditemukan, tanpa menelusuri sisa list.

### 2.8 Skema Warna

Bingkai menu utama

Tambah & pesan berhasil

Tampilkan data

Cari & tidak ditemukan

Hapus & input tidak valid

Keluar

💡 Hijau selalu menandakan operasi berhasil, merah untuk pesan error — terlepas dari warna menu yang aktif.

## 3. Cara Menjalankan Program

1

Install .NET 10 SDK

Unduh dari `dot.net` atau jalankan script instalasi Ubuntu. Cek dengan `dotnet --version`.

2

Jalankan Hello World

`cd PBKK/Pertemuan2/Hello` lalu `dotnet run`

3

Jalankan Sistem Data Mahasiswa

`cd PBKK/Pertemuan2/DataMahasiswa` lalu `dotnet run`

4

Rekam video demo

Gunakan `kazam` atau OBS di Ubuntu. Rekam terminal dan tunjukkan semua menu: tambah, tampilkan, cari, hapus, keluar.

## 4. Upload ke GitHub

#### 🗂️ Struktur folder yang diupload

PBKK/\
├── .gitignore\
└── Pertemuan2/\
├── Hello/\
│ ├── Hello.csproj\
│ └── Program.cs\
└── DataMahasiswa/\
├── DataMahasiswa.csproj\
└── Program.cs

#### ⌨️ Perintah Git (pertama kali)

1\. Inisialisasi repo lokal dan push ke GitHub

cd \~/PBKK\
git init\
git add .\
git commit -m "Pertemuan 2: Hello World dan Sistem Data Mahasiswa"\
git branch -M main\
git remote add origin https://github.com/USERNAME/PBKK.git\
git push -u origin main

2\. Jika repo sudah ada (pertemuan berikutnya)

git add .\
git commit -m "Tambah Pertemuan 2"\
git push

⚠️ Ganti **USERNAME** dengan username GitHub kamu. Buat repo baru di `github.com/new` dengan nama `PBKK` sebelum push. Folder `bin/` dan `obj/` sudah diabaikan lewat `.gitignore`.

## 5. Pengisian Form Absensi

| Field | Isi |
| --- | --- |
| Tahun Kuliah | 2026 |
| Nama | Antonius Andy Martono |
| NRP | 5025241191 |
| Kelas | Pemrograman Berbasis Kerangka Kerja C - 2026 |
| Pertemuan | Pertemuan 2 |
| Deskripsi Latihan | Membuat program Hello World dan Sistem Data Mahasiswa berbasis console menggunakan C# (.NET 10), mencakup class, objek, list, percabangan, perulangan, dan pewarnaan output terminal. |
| Link Latihan | URL blog/Notion dokumentasi kamu |

---

Antonius Andy Martono · 5025241191 · PBKK (C) · Pertemuan 2