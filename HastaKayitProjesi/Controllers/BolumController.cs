using Microsoft.AspNetCore.Mvc;
using HastaKayitProjesi.Models;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace HastaKayitProjesi.Controllers
{
    public class BolumController : Controller
    {
        private readonly ApplicationDbContext _veritabani; // Veritabanı bağlantısını temsil ediyor.

        public BolumController(ApplicationDbContext veritabani) // Constructor: Veritabanını denetleyiciye enjekte ediyoruz.
        {
            _veritabani = veritabani; // Enjekte edilen veritabanı nesnesini sınıf seviyesindeki değişkene atıyoruz.
        }

        public IActionResult Listele() // Bölümleri listeleyen metod.
        {
            var bolumler = _veritabani.Bolumler.OrderBy(b => b.BolumAdi).ToList(); // Veritabanından bölümleri alfabetik sırayla alıyoruz.
            return View(bolumler); // Bölüm listesi ile View'e dönüyoruz.
        }

        public IActionResult Ekle() // Yeni bir bölüm ekleme formunu görüntüleyen metod.
        {
            return View(); // View'i döndür.
        }

        [HttpPost] // HTTP POST istekleri için bu metodu kullan.
        [ValidateAntiForgeryToken] // CSRF saldırılarına karşı koruma sağlar.
        public IActionResult Ekle(Bolum bolum) // Yeni bir bölüm ekleyen metod.
        {
            if (!ModelState.IsValid) // Model doğrulaması başarısızsa.
            {
                return View(bolum); // Hataları göstermek için aynı formu döndür.
            }

            bool bolumVarMi = _veritabani.Bolumler.Any(a => a.BolumAdi == bolum.BolumAdi); // Veritabanında aynı ada sahip bölüm var mı kontrolü.

            if (bolumVarMi) // Eğer aynı ada sahip bölüm varsa.
            {
                ViewBag.Mesaj = "Eklemek istediğiniz bölüm hastanemizde zaten mevcut."; // Kullanıcıya uyarı mesajı göster.
                return View(); // Aynı formu tekrar döndür.
            }

            _veritabani.Bolumler.Add(bolum); // Yeni bölümü veritabanına ekle.
            _veritabani.SaveChanges(); // Değişiklikleri kaydet.
            return RedirectToAction(nameof(Listele)); // Listeleme sayfasına yönlendir.
        }
    }
}
