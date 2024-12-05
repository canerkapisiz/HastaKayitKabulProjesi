using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace HastaKayitProjesi.Models
{
    public class Bolum
    {
        [Key] // Bu alanın birincil anahtar olduğunu belirtir.
        public int Id { get; set; } // Bölümün benzersiz kimliği.

        [Required(ErrorMessage = "Bölüm adı gereklidir.")] // Bölüm adı boş bırakılamaz.
        [StringLength(100, ErrorMessage = "Bölüm adı en fazla 100 karakter olabilir.")] // Maksimum karakter sınırı.
        public string BolumAdi { get; set; } // Bölümün adı.

        public virtual List<Doktor>? Doktorlar { get; set; } // Bölümde çalışan doktorların listesi (isteğe bağlı).
    }
}
