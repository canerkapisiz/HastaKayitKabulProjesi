using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HastaKayitProjesi.Models
{
    public class RandevuKayit
    {
        // Randevu kaydının birincil anahtar (ID) alanı
        [Key]
        public int Id { get; set; }

        // T.C. Kimlik Numarası alanı, bu alanda veri doğrulama kuralları belirlenmiştir
        [Required(ErrorMessage = "T.C. Kimlik Numarası zorunludur.")]
        [StringLength(11, MinimumLength = 11, ErrorMessage = "T.C. Kimlik Numarası 11 haneli olmalıdır.")]
        public string TcKimlikNo { get; set; }

        // Bölüm ID'si, bir randevu kaydının hangi bölüme ait olduğunu belirtir
        [Required(ErrorMessage = "Bölüm seçilmelidir.")]
        public int BolumId { get; set; }

        // Doktor ID'si, bir randevunun hangi doktora ait olduğunu belirtir
        [Required(ErrorMessage = "Doktor seçilmelidir.")]
        public int DoktorId { get; set; }

        // Randevu tarihi, randevunun yapılacağı tarihi belirtir
        [Required(ErrorMessage = "Randevu tarihi zorunludur.")]
        public DateTime RandevuTarihi { get; set; }

        // Navigation properties (Yönlendirme özellikleri)
        // Hasta modeline bağlanır. Hasta'nın T.C. Kimlik Numarası ile ilişkilendirilir
        [ForeignKey("TcKimlikNo")]
        public virtual Hasta? Hasta { get; set; }

        // Doktor modeline bağlanır. Doktor ID'si ile ilişkilendirilir
        [ForeignKey("DoktorId")]
        public virtual Doktor? Doktor { get; set; }

        // Bölüm modeline bağlanır. Bölüm ID'si ile ilişkilendirilir
        [ForeignKey("BolumId")]
        public virtual Bolum? Bolum { get; set; }
    }
}
