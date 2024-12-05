using Microsoft.EntityFrameworkCore;

namespace HastaKayitProjesi.Models
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<Hasta> Hastalar { get; set; }
        public DbSet<Doktor> Doktorlar { get; set; }
        public DbSet<Bolum> Bolumler { get; set; }
        public DbSet<RandevuKayit> RandevuKayitlar { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Hasta ile ilişki
            modelBuilder.Entity<RandevuKayit>()
                .HasOne(r => r.Hasta)
                .WithMany(h => h.Randevular)
                .HasForeignKey(r => r.TcKimlikNo)
                .OnDelete(DeleteBehavior.Restrict);

            // Doktor ile ilişki
            modelBuilder.Entity<RandevuKayit>()
                .HasOne(r => r.Doktor)
                .WithMany(d => d.Randevular)
                .HasForeignKey(r => r.DoktorId)
                .OnDelete(DeleteBehavior.Restrict);

            // Bolum ile ilişki
            modelBuilder.Entity<RandevuKayit>()
                .HasOne(r => r.Bolum)
                .WithMany()
                .HasForeignKey(r => r.BolumId)
                .OnDelete(DeleteBehavior.Restrict);

            base.OnModelCreating(modelBuilder);
        }
    }
}