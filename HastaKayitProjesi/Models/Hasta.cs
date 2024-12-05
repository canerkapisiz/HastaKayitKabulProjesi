using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HastaKayitProjesi.Models
{
    public class Hasta
    {
        [Key] // Birincil anahtar
        [Required(ErrorMessage = "T.C. Kimlik Numarası zorunludur.")] // Zorunlu alan
        [StringLength(11, MinimumLength = 11, ErrorMessage = "T.C. Kimlik Numarası 11 haneli olmalıdır.")] // 11 haneli olma kısıtlaması
        public string TcKimlikNo { get; set; }

        [Required(ErrorMessage = "İsim soyisim zorunludur.")] // Zorunlu alan
        public string AdSoyad { get; set; } // Hastanın adı ve soyadı

        public string Telefon { get; set; } // Telefon numarası

        public string Adres { get; set; } // Adres bilgisi

        public virtual List<RandevuKayit>? Randevular { get; set; } // İlişkili randevular listesi
    }
}
