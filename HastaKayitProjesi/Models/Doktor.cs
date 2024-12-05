using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HastaKayitProjesi.Models
{
    public class DoktorViewModel // Doktorları listelemek için kullanılan görünüm modeli.
    {
        public int Id { get; set; } // Doktor kimliği.
        public string AdSoyad { get; set; } // Doktorun adı ve soyadı.
        public string BolumAd { get; set; } // Doktorun bağlı olduğu bölümün adı.
    }

    public class Doktor // Doktorların veritabanındaki yapısını temsil eden sınıf.
    {
        [Key] // Bu alanın birincil anahtar olduğunu belirtir.
        public int Id { get; set; } // Doktor kimliği.

        [Required(ErrorMessage = "Ad Soyad zorunludur.")] // Ad soyad alanı boş bırakılamaz.
        public string AdSoyad { get; set; } // Doktorun adı ve soyadı.

        [Required(ErrorMessage = "Bolum Id zorunludur.")] // Bölüm kimliği boş bırakılamaz.
        public int BolumId { get; set; } // Doktorun bağlı olduğu bölümün kimliği.

        [ForeignKey("BolumId")] // Bu alanın BolumId'ye yabancı anahtar olduğunu belirtir.
        public Bolum Bolum { get; set; } // Doktorun bağlı olduğu bölüm.

        public virtual List<RandevuKayit> Randevular { get; set; } // Doktorun aldığı randevuların listesi.
    }
}
