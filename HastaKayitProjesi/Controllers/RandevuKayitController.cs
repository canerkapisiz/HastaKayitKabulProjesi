using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using HastaKayitProjesi.Models;
using System.Linq;
using System.Globalization;

namespace HastaKayitProjesi.Controllers
{
    // Randevu ile ilgili verilerin taşındığı view model sınıfı
    public class RandevuViewModel
    {
        public RandevuKayit Randevu { get; set; } // Randevu bilgilerini taşıyan özellik
        public List<Bolum> Bolumler { get; set; } // Bölüm bilgilerini taşıyan liste
    }

    // Randevu kaydı ile ilgili işlemlerin yapıldığı controller
    public class RandevuKayitController : Controller
    {
        private readonly ApplicationDbContext _database; // Veritabanı bağlamı
        DateTime bugun = DateTime.Today; // Bugünün tarihi

        // Constructor, veritabanı bağlamını alır
        public RandevuKayitController(ApplicationDbContext database)
        {
            _database = database;
        }

        // Randevu listeleme sayfası
        public IActionResult Index()
        {
            DateTime bugun = DateTime.Today; // Bugünün tarihi

            // Bugüne ait randevuları filtreleyerek getiriyoruz
            var bugununRandevulari = _database.RandevuKayitlar
                .Where(r => r.RandevuTarihi.Date == bugun) // Tarihe göre filtreleme
                .Include(r => r.Hasta) // Hasta bilgisini de dahil et
                .Include(r => r.Bolum) // Bölüm bilgisini de dahil et
                .Include(r => r.Doktor) // Doktor bilgisini de dahil et
                .ToList(); // Listeyi al

            // Bugünün randevularını view'e göndermek için ViewBag kullanıyoruz
            ViewBag.BugununRandevulari = bugununRandevulari;

            return View(); // Görünümü döndürüyoruz
        }

        public IActionResult Islemler()
        {
            ViewBag.Bolumler = _database.Bolumler.ToList();
            ViewBag.Doktorlar = _database.Doktorlar.ToList();
            return View();
        }

        [HttpPost]
        public IActionResult Islemler(int DoktorId)
        {
            // Bugünün tarihini al
            var today = DateTime.Today;

            ViewBag.Bolumler = _database.Bolumler.ToList();
            ViewBag.Doktorlar = _database.Doktorlar.ToList();

            ViewBag.Randevular = _database.RandevuKayitlar
                .Where(r => r.DoktorId == DoktorId && r.RandevuTarihi.Date == today) // Bugüne göre filtreleme
                .Include(r => r.Hasta) // Hasta bilgisi dahil
                .Include(r => r.Bolum) // Hasta bilgisi dahil
                .Include(r => r.Doktor) // Doktor bilgisi dahil
                .ToList();

            return View();
        }

        // Randevu oluşturma sayfası için GET metod
        [HttpGet]
        public IActionResult Create()
        {
            // Bölüm listesini view'e göndermek için ViewBag kullanıyoruz
            ViewBag.Bolumler = _database.Bolumler.ToList();
            return View(); // Görünümü döndürüyoruz
        }

        // Randevu oluşturma işlemi için POST metod
        [HttpPost]
        public IActionResult Create(RandevuKayit model, string RandevuTarihi, string RandevuSaati)
        {
            // Hata oluşursa da dropdown (bölüm listesi) dolu olmalı
            ViewBag.Bolumler = _database.Bolumler.ToList();

            // Model doğrulama
            if (ModelState.IsValid)
            {
                // Tarih ve saat bilgisi boş bırakılmamalı
                if (string.IsNullOrWhiteSpace(RandevuTarihi) || string.IsNullOrWhiteSpace(RandevuSaati))
                {
                    ModelState.AddModelError("", "Lütfen randevu tarihi ve saatini giriniz.");
                }
                else
                {
                    try
                    {
                        // Tarih ve saat bilgisini birleştirip DateTime türüne dönüştürme
                        DateTime randevuTarihiSaat = DateTime.Parse($"{RandevuTarihi} {RandevuSaati}");
                        model.RandevuTarihi = randevuTarihiSaat;

                        // Aynı doktora aynı gün randevu olup olmadığını kontrol etme
                        bool mevcutRandevu = _database.RandevuKayitlar.Any(r =>
                            r.TcKimlikNo == model.TcKimlikNo &&
                            r.DoktorId == model.DoktorId &&
                            r.RandevuTarihi.Date == randevuTarihiSaat.Date);

                        // Eğer bu doktora randevu alınmışsa hata mesajı ekle
                        if (mevcutRandevu)
                        {
                            ModelState.AddModelError("", "Bu doktora gün içinde randevunuz bulunmaktadır.");
                        }
                        else
                        {
                            // Randevuyu veritabanına kaydet
                            _database.RandevuKayitlar.Add(model);
                            _database.SaveChanges(); // Değişiklikleri kaydet
                            return RedirectToAction(nameof(Index)); // Randevular sayfasına yönlendir
                        }
                    }
                    catch (Exception ex)
                    {
                        // Hata durumunda kullanıcıya bilgi verme
                        ModelState.AddModelError("", "Geçersiz tarih veya saat formatı: " + ex.Message);
                    }
                }
            }

            // Formu yeniden göster
            return View(model);
        }

        // Randevu sorgulama sayfası için GET metod
        [HttpGet]
        public IActionResult Search(string tcKimlikNo)
        {
            if (string.IsNullOrEmpty(tcKimlikNo)) // T.C. kimlik numarası boş ise
            {
                ViewBag.IsFirstLoad = true; // İlk yükleme olduğuna dair bilgi gönder
                return View(); // Görünümü döndür
            }

            ViewBag.IsFirstLoad = false; // İlk yükleme değil

            // T.C. kimlik numarasına göre randevuları filtreleyerek getiriyoruz
            var randevular = _database.RandevuKayitlar
                .Where(r => r.TcKimlikNo == tcKimlikNo) // T.C. kimlik numarasına göre filtrele
                .Include(r => r.Hasta) // Hasta bilgisi dahil
                .Include(r => r.Bolum) // Bölüm bilgisi dahil
                .Include(r => r.Doktor) // Doktor bilgisi dahil
                .ToList(); // Listeyi al

            // Randevuları view'e gönder
            return View(randevular);
        }

        // Randevu silme sayfası için GET metod
        [HttpGet]
        public IActionResult Delete(string tcKimlikNo)
        {
            if (string.IsNullOrEmpty(tcKimlikNo)) // T.C. kimlik numarası boş ise
            {
                ViewBag.IsFirstLoad = true; // İlk yükleme olduğuna dair bilgi gönder
                return View(); // Görünümü döndür
            }

            ViewBag.IsFirstLoad = false; // İlk yükleme değil

            // T.C. kimlik numarasına göre randevuları filtreleyerek getiriyoruz
            var randevular = _database.RandevuKayitlar
                .Where(r => r.TcKimlikNo == tcKimlikNo) // T.C. kimlik numarasına göre filtrele
                .Include(r => r.Hasta) // Hasta bilgisi dahil
                .Include(r => r.Bolum) // Bölüm bilgisi dahil
                .Include(r => r.Doktor) // Doktor bilgisi dahil
                .ToList(); // Listeyi al

            // Randevuları view'e gönder
            return View(randevular);
        }

        // Silme işlemi için POST metod
        [HttpPost, ActionName("Delete")]
        public IActionResult Delete(int id)
        {
            // Randevuyu ID ile buluyoruz
            var randevuKayit = _database.RandevuKayitlar.Find(id);
            if (randevuKayit == null)
            {
                return NotFound(); // Randevu bulunamazsa 404 döndür
            }

            // Randevuyu veritabanından sil
            _database.RandevuKayitlar.Remove(randevuKayit);
            _database.SaveChanges(); // Değişiklikleri kaydet

            // Silme işlemi sonrası listeleme sayfasına yönlendir
            return RedirectToAction(nameof(Index));
        }

        // Bölüm seçildiğinde o bölüme ait doktorları JSON olarak döndüren metod
        [HttpGet]
        public JsonResult GetDoktorlarByBolum(int bolumId)
        {
            // Bölüm ID'ye göre doktorları filtreleyerek getiriyoruz
            var doktorlar = _database.Doktorlar
                .Where(d => d.BolumId == bolumId)
                .Select(d => new { d.Id, d.AdSoyad }) // Doktorun ID ve AdSoyad bilgilerini al
                .ToList();

            return Json(doktorlar); // JSON formatında geri döndür
        }

        // Doktor seçildiğinde o doktor için uygun randevu saatlerini döndüren metod
        [HttpGet]
        public JsonResult GetRandevuSaatleri(int doktorId, string tarih)
        {
            try
            {
                // Tarih formatını kontrol et
                if (!DateTime.TryParseExact(tarih, "yyyy-MM-dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime secilenTarih))
                {
                    return Json(new { Error = "Geçersiz tarih formatı. Lütfen 'yyyy-MM-dd' formatında bir tarih gönderin." });
                }

                // Seçilen tarihteki randevuları getir
                var randevuSaatleri = _database.RandevuKayitlar
                    .Where(r => r.DoktorId == doktorId && r.RandevuTarihi.Date == secilenTarih.Date)
                    .Select(r => r.RandevuTarihi.ToString("HH:mm")) // Saat ve dakika formatında döndür
                    .ToList();

                return Json(randevuSaatleri); // JSON formatında geri döndür
            }
            catch (Exception ex)
            {
                // Hata durumunda kullanıcıya bilgi verme
                return Json(new { Error = "Bir hata oluştu. Lütfen daha sonra tekrar deneyin." });
            }
        }

        [HttpPost]
        public IActionResult TwoCreate(RandevuKayit model, string RandevuTarihi, string RandevuSaati)
        {
            ViewBag.Bolumler = _database.Bolumler.ToList();

            if (ModelState.IsValid)
            {
                DateTime randevuTarihiSaat = DateTime.Parse($"{RandevuTarihi} {RandevuSaati}");
                model.RandevuTarihi = randevuTarihiSaat;

                _database.RandevuKayitlar.Add(model);
                _database.SaveChanges();
                return RedirectToAction(nameof(Islemler));
            }

            return View(model);
        }
    }
}
