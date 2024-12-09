using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using HastaKayitProjesi.Models;
using Microsoft.EntityFrameworkCore;


namespace HastaKayitProjesi.Controllers
{
    public class HastaController : Controller
    {
        private readonly ApplicationDbContext _database;

        // Constructor: Veritabanı bağlamını kullanıma hazırlar
        public HastaController(ApplicationDbContext database)
        {
            _database = database;
        }

        // Listele metodu: Tüm hastaları listelemek için kullanılır
        public IActionResult Index()
        {
            var hastalar = _database.Hastalar.ToList(); // Tüm hastaları veritabanından getir
            return View(hastalar); // Hastaları listeleme görünümüne gönder
        }

        // Ekle metodu (GET): Yeni hasta formunu döner
        public IActionResult Create()
        {
            // Doktorların listesini formda seçim yapmak için hazırlar
            ViewBag.Doktorlar = new SelectList(_database.Doktorlar, "Id", "AdSoyad");
            return View(); // Hasta ekleme formu görünümünü döner
        }

        // Ekle metodu (POST): Yeni hasta kaydını veritabanına ekler
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Hasta hasta)
        {
            // Girilen TC Kimlik Numarasıyla bir hasta var mı kontrol et
            bool tcNumarasiVarMi = _database.Hastalar.Any(h => h.TcKimlikNo == hasta.TcKimlikNo);

            if (tcNumarasiVarMi)
            {
                // Eğer varsa hata mesajını ekler
                ModelState.AddModelError("TcKimlikNo", "Bu TC Kimlik Numarası ile kayıtlı bir hasta zaten var.");
            }

            // Model doğrulama başarılıysa hasta kaydını ekle
            if (ModelState.IsValid)
            {
                _database.Hastalar.Add(hasta); // Yeni hastayı ekle
                _database.SaveChanges(); // Veritabanında değişiklikleri kaydet
                return RedirectToAction(nameof(Index)); // Hasta listesini göster
            }

            // Hata durumunda formu tekrar doldurmak için doktor listesini hazırla
            ViewBag.Doktorlar = new SelectList(_database.Doktorlar, "Id", "AdSoyad");
            return View(hasta); // Form görünümüne dön ve hataları göster
        }
    }
}
