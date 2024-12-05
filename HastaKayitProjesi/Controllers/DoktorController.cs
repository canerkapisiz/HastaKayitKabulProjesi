using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using HastaKayitProjesi.Models;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace HastaKayitProjesi.Controllers
{
    public class DoktorController : Controller
    {
        private readonly ApplicationDbContext _veritabani; // Veritabanı bağlantısını temsil eder.

        public DoktorController(ApplicationDbContext veritabani) // Constructor: Veritabanı nesnesini denetleyiciye enjekte eder.
        {
            _veritabani = veritabani; // Veritabanı bağlantısını sınıf seviyesinde saklıyoruz.
        }

        public IActionResult Listele() // Doktorları listeleyen metod.
        {
            var doktorlar = _veritabani.Doktorlar // Veritabanındaki doktorları al.
                .Include(d => d.Bolum) // Her doktorun bölüm bilgisi ile birleştir.
                .Select(d => new DoktorViewModel // Doktor bilgilerini bir görünüm modeliyle eşleştir.
                {
                    AdSoyad = d.AdSoyad, // Doktor adı ve soyadı.
                    BolumAd = d.Bolum != null ? d.Bolum.BolumAdi : "Bölüm Yok" // Eğer bölüm bilgisi varsa ekle, yoksa "Bölüm Yok" yaz.
                }).ToList();
            return View(doktorlar); // Doktor listesiyle View döndür.
        }

        public IActionResult Ekle() // Yeni doktor ekleme formunu gösteren metod.
        {
            ViewBag.BolumListesi = BolumSecimListesiOlustur(); // Bölüm listesi ViewBag'e aktarılır.
            return View(); // View döndür.
        }

        [HttpPost] // Bu metod HTTP POST istekleri için kullanılır.
        [ValidateAntiForgeryToken] // CSRF saldırılarına karşı koruma sağlar.
        public IActionResult Ekle(Doktor doktor) // Yeni bir doktor ekleyen metod.
        {
            doktor.Bolum = _veritabani.Bolumler // Doktorun bağlı olduğu bölümü bul.
                .FirstOrDefault(b => b.Id == doktor.BolumId);

            _veritabani.Doktorlar.Add(doktor); // Yeni doktoru veritabanına ekle.
            _veritabani.SaveChanges(); // Değişiklikleri kaydet.
            return RedirectToAction(nameof(Listele)); // Listeleme sayfasına yönlendir.
        }

        private SelectList BolumSecimListesiOlustur() // Drop-down list için bölüm listesini oluşturan metod.
        {
            var bolumler = _veritabani.Bolumler // Veritabanından tüm bölümleri al.
                .OrderBy(b => b.BolumAdi) // Bölümleri alfabetik sıraya göre sırala.
                .ToList();
            return new SelectList(bolumler, "Id", "BolumAdi"); // Bölüm listesini SelectList olarak döndür.
        }
    }
}
