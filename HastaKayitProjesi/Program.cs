using Microsoft.EntityFrameworkCore;
using HastaKayitProjesi.Models;

var builder = WebApplication.CreateBuilder(args);

// 1. Veritabanı bağlantısını yapılandırma
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));


// 2. MVC (Model-View-Controller) desteği ekleme
builder.Services.AddControllersWithViews();

// 3. Uygulama oluşturuluyor
var app = builder.Build();

// 4. Hata işleme (Error Handling) yapılandırması: 
//   - Geliştirme ortamı dışında uygulama hata sayfasına yönlendirilir.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/RandevuKayit/Index");
    app.UseHsts(); // HSTS (HTTP Strict Transport Security) aktif edilir.
}

// 5. HTTPS yönlendirmesi (HTTP'den HTTPS'ye)
app.UseHttpsRedirection();

// 6. Rotaların (Routing) yapılandırılması
app.UseRouting();

// 7. Kimlik doğrulama (Authorization) işlemleri için middleware ekleniyor
app.UseAuthorization();

// 8. Statik dosya desteği (CSS, JS, resimler vs.)
app.UseStaticFiles(); // Statik dosyaları sunmak için doğru kullanım

// 9. Varsayılan controller route yapılandırması
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=RandevuKayit}/{action=Index}/{id?}"
);

// 10. Uygulamayı çalıştır
app.Run();
